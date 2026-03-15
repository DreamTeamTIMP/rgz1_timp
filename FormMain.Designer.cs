namespace rgz1_timp
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            buttonMinimize = new Button();
            buttonMaximize = new Button();
            buttonClose = new Button();
            panel1 = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            splitContainer2 = new SplitContainer();
            treeView1 = new TreeView();
            listView1 = new ListView();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.WindowFrame;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(686, 307);
            splitContainer1.SplitterDistance = 154;
            splitContainer1.TabIndex = 0;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.BackColor = SystemColors.Desktop;
            splitContainer3.Panel1.Controls.Add(buttonMinimize);
            splitContainer3.Panel1.Controls.Add(buttonMaximize);
            splitContainer3.Panel1.Controls.Add(buttonClose);
            splitContainer3.Panel1.MouseDown += splitContainer3_Panel1_MouseDown;
            splitContainer3.Panel1.MouseMove += splitContainer3_Panel1_MouseMove;
            splitContainer3.Panel1.MouseUp += splitContainer3_Panel1_MouseUp;
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(panel1);
            splitContainer3.Panel2.Controls.Add(tabControl1);
            splitContainer3.Size = new Size(686, 154);
            splitContainer3.SplitterDistance = 28;
            splitContainer3.TabIndex = 0;
            // 
            // buttonMinimize
            // 
            buttonMinimize.Dock = DockStyle.Right;
            buttonMinimize.FlatAppearance.BorderSize = 0;
            buttonMinimize.FlatStyle = FlatStyle.Popup;
            buttonMinimize.ForeColor = SystemColors.ControlLightLight;
            buttonMinimize.Location = new Point(578, 0);
            buttonMinimize.Name = "buttonMinimize";
            buttonMinimize.Size = new Size(36, 28);
            buttonMinimize.TabIndex = 2;
            buttonMinimize.Text = "--";
            buttonMinimize.UseVisualStyleBackColor = true;
            buttonMinimize.Click += buttonMinimize_Click;
            // 
            // buttonMaximize
            // 
            buttonMaximize.Dock = DockStyle.Right;
            buttonMaximize.FlatAppearance.BorderSize = 0;
            buttonMaximize.FlatStyle = FlatStyle.Popup;
            buttonMaximize.ForeColor = SystemColors.ControlLightLight;
            buttonMaximize.Location = new Point(614, 0);
            buttonMaximize.Name = "buttonMaximize";
            buttonMaximize.Size = new Size(36, 28);
            buttonMaximize.TabIndex = 1;
            buttonMaximize.Text = "O";
            buttonMaximize.UseVisualStyleBackColor = true;
            buttonMaximize.Click += buttonMaximize_Click;
            // 
            // buttonClose
            // 
            buttonClose.Dock = DockStyle.Right;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Popup;
            buttonClose.ForeColor = SystemColors.Control;
            buttonClose.Location = new Point(650, 0);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(36, 28);
            buttonClose.TabIndex = 0;
            buttonClose.Text = "X";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GrayText;
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 100);
            panel1.Name = "panel1";
            panel1.Size = new Size(686, 22);
            panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Top;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(686, 100);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.ActiveBorder;
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(678, 69);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Файл";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(678, 69);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Главная";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(678, 69);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Поделиться";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 27);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(678, 69);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Вид";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(listView1);
            splitContainer2.Size = new Size(686, 149);
            splitContainer2.SplitterDistance = 228;
            splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(228, 149);
            treeView1.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(454, 149);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(308, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(14, -1);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(100, 2);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(181, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 3;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 307);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer2;
        private TreeView treeView1;
        private ListView listView1;
        private Panel panel1;
        private Button buttonClose;
        private Button buttonMaximize;
        private Button buttonMinimize;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private ComboBox comboBox1;
    }
}