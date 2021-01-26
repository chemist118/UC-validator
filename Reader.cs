using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UC_UML_Error_Finder
{
    class Reader
    {
        private RichTextBox output;
        private Dictionary<string, Element> elements;

        public Reader(RichTextBox output, Dictionary<string, Element> elements)
        {
            this.output = output;
            this.elements = elements;
        }

        public static XmlNode InitFile(string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement xRoot;
            XmlNode UMLpackage;

            try
            {
                xDoc.Load(filename);
                // получим корневой элемент
                xRoot = xDoc.DocumentElement;
                UMLpackage = xRoot.FirstChild;

                if (UMLpackage.Name != "uml:Model")
                    throw new Exception();
            }
            catch (Exception)
            {
                // файл отстутствует, либо содержимое не соответствует формату
                return null;
            }
            return UMLpackage;
        }

        public void ReadData(XmlNode UMLpackage)
        {
            foreach (XmlNode childnode in UMLpackage.ChildNodes)
            {
                if (childnode.Name == "packageImport" || childnode.Name == "xmi:Extension")
                    continue;

                string type = getType(childnode),
                parent = getParent(childnode),
                name = getName(childnode),
                id = getId(childnode);


                if (Types.List.Contains(type))
                {
                    switch (type)
                    {
                        case Types.Association:
                            {
                                // обработка ассоциации
                                string from = childnode.ChildNodes[1].Attributes.GetNamedItem("type")?.Value,
                                    to = childnode.ChildNodes[2].Attributes.GetNamedItem("type")?.Value;
                                elements.Add(id, new Arrow(id, type, name, parent, from, to));
                                break;
                            }
                        case Types.Actor:
                            {

                                ReadActor(childnode);
                                elements.Add(id, new Element(id, type, name, parent));
                                break;
                            }
                        case Types.Package:
                            {
                                ReadPackage(childnode);
                                elements.Add(id, new Element(id, type, name, parent));
                                break;
                            }
                        case Types.Precedent:
                            {
                                elements.Add(id, new Element(id, type, name, parent));
                                ReadPrecedent(childnode);
                                break;
                            }
                        default:
                            {
                                if (childnode.Name == Types.Comment)
                                {
                                    type = Types.Comment;
                                    string to = childnode.Attributes.GetNamedItem("annotatedElement")?.Value;
                                    name = childnode.Attributes.GetNamedItem("body")?.Value;
                                    elements.Add(id, new Arrow(id, type, name, parent, null, to));
                                }
                                else
                                    output.Text += $"Элемент находится за пределами системы: {type} - {name}\n";
                                break;
                            }
                    }
                }
                else
                    output.Text += $"Недопустимый элемент элемент: {type} - {name}\n";
            }
        }

        private void ReadPackage(XmlNode package)
        {
            foreach (XmlNode childnode in package.ChildNodes)
            {
                if (childnode.Name == "xmi:Extension")
                    continue;

                string type = getType(childnode),
                    parent = getParent(childnode),
                    name = getName(childnode),
                    id = getId(childnode);

                switch (type)
                {
                    case Types.Association:
                        {
                            // обработка ассоциации
                            string from = childnode.ChildNodes[1].Attributes.GetNamedItem("type")?.Value,
                                to = childnode.ChildNodes[2].Attributes.GetNamedItem("type")?.Value;
                            elements.Add(id, new Arrow(id, type, name, parent, from, to));
                            break;
                        }
                    case Types.Precedent:
                        {
                            elements.Add(id, new Element(id, type, name, parent));
                            ReadPrecedent(childnode);
                            break;
                        }
                    default:
                        {
                            if (childnode.Name == Types.Comment)
                            {
                                type = Types.Comment;
                                string to = childnode.Attributes.GetNamedItem("annotatedElement")?.Value;
                                name = childnode.Attributes.GetNamedItem("body")?.Value;
                                elements.Add(id, new Arrow(id, type, name, parent, null, to));
                            }
                            else
                                output.Text += $"Недопустимый элемент элемент внутри системы {getName(package)}: {id} - {name}\n";
                            break;
                        }
                }
            }
        }

        private void ReadPrecedent(XmlNode precedent)
        {
            foreach (XmlNode childnode in precedent.ChildNodes)
            {
                if (childnode.Name == "xmi:Extension")
                    continue;

                string type = childnode.Name,
                    parent = getParent(childnode),
                    name = getName(childnode),
                    id = getId(childnode);

                switch (type)
                {
                    case Types.Include:
                        {
                            string from = childnode.Attributes.GetNamedItem("includingCase")?.Value,
                                to = childnode.Attributes.GetNamedItem("addition")?.Value;
                            elements.Add(id, new Arrow(id, type, name, parent, from, to));
                            break;
                        }
                    case Types.Extend:
                        {
                            string from = childnode.Attributes.GetNamedItem("extension")?.Value,
                                to = childnode.Attributes.GetNamedItem("extendedCase")?.Value;
                            elements.Add(id, new Arrow(id, type, name, parent, from, to));
                            break;
                        }
                    case Types.ExtensionPoint:
                        {
                            elements.Add(id, new Element(id, type, name, parent));
                            break;
                        }
                    default:
                        {
                            output.Text += $"Недопустимый элемент элемент внутри системы {getName(precedent.ParentNode)}: {type} - {name}\n";
                            break;
                        }
                }
            }
        }

        private void ReadActor(XmlNode actor)
        {
            foreach (XmlNode childnode in actor.ChildNodes)
            {
                if (childnode.Name == "xmi:Extension")
                    continue;

                string type = getType(childnode),
                parent = getParent(childnode),
                name = getName(childnode),
                id = getId(childnode);

                if (childnode.Name == Types.Generalization)
                {
                    type = Types.Generalization;
                    string from = childnode.Attributes.GetNamedItem("specific")?.Value,
                        to = childnode.Attributes.GetNamedItem("general")?.Value;

                    elements.Add(id, new Arrow(id, type, name, parent, from, to));
                }
                else
                    output.Text += $"Недопустимый элемент элемент: {type} - {name}\n";
            }
        }

        private string getId(XmlNode item)
        {
            return item.Attributes.GetNamedItem("xmi:id")?.Value;
        }

        private string getType(XmlNode item)
        {
            return item.Attributes.GetNamedItem("xsi:type")?.Value;
        }
        private string getName(XmlNode item)
        {
            return item.Attributes.GetNamedItem("name")?.Value;
        }

        private string getParent(XmlNode item)
        {
            return item.ParentNode.Attributes.GetNamedItem("xmi:id")?.Value;
        }
    }
}
