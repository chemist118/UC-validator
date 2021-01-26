using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UC_UML_Error_Finder
{
    public partial class MainForm : Form
    {

        private string filename;
        private static Dictionary<string, Element> elements = new Dictionary<string, Element>();


        public MainForm()
        {
            InitializeComponent();
        }

        public void ShowInfo()
        {
            Checker checker = new Checker(output, elements);
            checker.Check();
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            // получаем выбранный файл
            filename = openFileDialog.FileName;

            elements.Clear();

            Reader reader = new Reader(output, elements);
            XmlNode UMLpackage = Reader.InitFile(filename);

            if (UMLpackage != null)
            {
                Text = $"UC UML Error Finder {openFileDialog.SafeFileName}";
                output.Text = $"Открыт документ: {openFileDialog.SafeFileName}\n";
                btnLaunch.Enabled = true;

                reader.ReadData(UMLpackage);
            }
            else
                FileError();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        public void FileError()
        {
            Text = "UC UML Error Finder";
            output.Text = $"Убедитесь в корректности входного файла\n {filename}";
            filename = "";
            btnLaunch.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
