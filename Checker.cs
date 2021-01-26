using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UC_UML_Error_Finder
{
    class Checker
    {
        private RichTextBox output;
        private Dictionary<string, Element> elements;
        public Checker(RichTextBox output, Dictionary<string, Element> elements)
        {
            this.output = output;
            this.elements = elements;
        }
        public void Check()
        {
            CheckTokenizing();
        }
        void CheckTokenizing()
        {
            CheckActors();
            CheckComments();
            СheckPackages();
            CheckPrecedents();
        }
        void CheckActors()
        {
            var actors = elements.Where(element => element.Value.Type == Types.Actor);

            foreach (var actorName in actors.GroupBy(a => a.Value.Name))
            {
                if (actorName.Count() > 1)
                    output.Text += $"Ошибка: Имя актора повторяется: {actorName.Key}\n";
                if (string.IsNullOrEmpty(actorName.Key.Trim()) || !char.IsUpper(actorName.Key[0]))
                    output.Text += $"Ошибка: Имя актора должно быть представлено в виде существительного с заглавной буквы: {actorName.Key}\n";
            }
        }
        void CheckComments()
        {
            var comments = elements.Where(element => element.Value.Type == Types.Comment);
            foreach (var comment in comments)
                if (string.IsNullOrEmpty(comment.Value.Name.Trim()))
                    output.Text += $"Ошибка: Отсутствует текст в условии расширения\n";
        }
        void СheckPackages()
        {
            var packages = elements.Where(element => element.Value.Type == Types.Package);
            foreach (var package in packages)
                if (string.IsNullOrEmpty(package.Value.Name.Trim()))
                    output.Text += $"Ошибка: Отсутствует назние системы\n";
        }
        void CheckPrecedents()
        {
            var extensionPoints = elements.Where(element => element.Value.Type == Types.ExtensionPoint);
            foreach (var point in extensionPoints)
                if (string.IsNullOrEmpty(point.Value.Name.Trim()))
                    output.Text += $"Ошибка: Отсутствует текст в точке расширения прецедента\n";

            var precedent = elements.Where(element => element.Value.Type == Types.Precedent);
        }
    }
}
