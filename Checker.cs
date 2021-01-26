﻿using System;
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
            CheckActors();
            CheckComments();
            СheckPackages();
            CheckPrecedents();
        }

        #region Checks
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

            var precedents = elements.Where(element => element.Value.Type == Types.Precedent);
            foreach (var precedentName in precedents.GroupBy(p => p.Value.Name))
            {
                if (precedentName.Count() > 1)
                    output.Text += $"Ошибка: Имя прецедента повторяется: {precedentName.Key}\n";
                if (string.IsNullOrEmpty(precedentName.Key.Trim()) || !char.IsUpper(precedentName.Key[0]))
                    output.Text += $"Ошибка: Имя прецедента должно быть представлено в виде действия, начинаясь с заглавной буквы: {precedentName.Key}\n";
            }

            foreach (var precedent in precedents)
            {
                bool haveAssociation = HaveConnection(precedent.Value.Id, Types.Association),
                    haveGeneralization = HaveConnection(precedent.Value.Id, Types.Generalization),
                    haveExtendsion = HaveConnection(precedent.Value.Id, Types.Extend),
                    haveIncluding = HaveConnection(precedent.Value.Id, Types.Include);

                if (!haveAssociation && !haveGeneralization && !haveExtendsion && !haveIncluding)
                    output.Text += $"Ошибка: Прецедент должен иметь связь с актором в виде ассоциации," +
                        $" либо иметь отношения расширения," +
                        $" дополнения или включения с другими прецедентами: {precedent.Value.Name}\n";

                if (haveExtendsion)
                {
                    bool havePoint = elements.Where(element =>
                    {
                        if (element.Value.Type != Types.ExtensionPoint) return false;
                        if (((Arrow)element.Value).To.Equals(precedent.Key))
                            return true;
                        return false;
                    }).Count() > 0;
                    bool extended = elements.Where(element =>
                    {
                        if (element.Value.Type != Types.Extend) return false;
                        if (((Arrow)element.Value).To.Equals(precedent.Key))
                            return true;
                        return false;
                    }).Count() > 0;

                    if(extended && !havePoint)
                        output.Text += $"Ошибка: Отсутствие точки расширения у прецедента с связью extend: {precedent.Value.Name}\n";
                }
            }
        }
        #endregion

        #region Support Functions
        bool HaveConnection(string id, string type)
        {
            var assosiations = elements.Where(element => element.Value.Type == type);
            return assosiations.Where(a =>
            {
                if (((Arrow)a.Value).To.Equals(id) ||
                ((Arrow)a.Value).From.Equals(id))
                    return true;
                return false;
            }).Count() > 0;
        }
        #endregion
    }
}
