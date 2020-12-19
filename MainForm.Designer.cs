namespace UC_UML_Error_Finder
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.btnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunch = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.output = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFile,
            this.btnLaunch});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(815, 24);
            this.mainMenu.TabIndex = 0;
            // 
            // btnFile
            // 
            this.btnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFileOpen,
            this.btnSave,
            this.btnExit});
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(48, 20);
            this.btnFile.Text = "Файл";
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(189, 22);
            this.btnFileOpen.Text = "Открыть";
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(189, 22);
            this.btnSave.Text = "Сохранить результат";
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(189, 22);
            this.btnExit.Text = "Выйти";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Enabled = false;
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(129, 20);
            this.btnLaunch.Text = "Запустить проверку";
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Диаграммы (*.xmi)|*.xmi|Все файлы (*.*)|*.*";
            // 
            // output
            // 
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.output.Location = new System.Drawing.Point(3, 16);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(809, 417);
            this.output.TabIndex = 1;
            this.output.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.output);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 436);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 460);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "UC UML Error Finder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem btnFile;
        private System.Windows.Forms.ToolStripMenuItem btnFileOpen;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.ToolStripMenuItem btnLaunch;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

