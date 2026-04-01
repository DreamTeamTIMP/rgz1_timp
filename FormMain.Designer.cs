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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            splitContainer1 = new SplitContainer();
            panelRibbon = new Panel();
            panelRibbonButtons = new Panel();
            buttonVid = new Button();
            buttonShare = new Button();
            buttonMain = new Button();
            buttonFile = new Button();
            tabControl1 = new TabControl();
            tabPageHome = new TabPage();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tabPageShare = new TabPage();
            tabPageVid = new TabPage();
            panelHeader = new Panel();
            buttonMinimize = new Button();
            buttonMaximize = new Button();
            buttonClose = new Button();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonCreateFolder = new ToolStripButton();
            toolStripButtonDelete = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            toolStripButton7 = new ToolStripButton();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            настройкаПанелиБыстрогоДоступаToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            panel4 = new Panel();
            splitContainer2 = new SplitContainer();
            treeView1 = new TreeView();
            listView1 = new ListView();
            columnHeaderName = new ColumnHeader();
            columnHeaderEditDate = new ColumnHeader();
            columnHeaderType = new ColumnHeader();
            columnHeaderSize = new ColumnHeader();
            panel2 = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel1 = new Panel();
            button3 = new Button();
            comboBox2 = new ComboBox();
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelRibbon.SuspendLayout();
            panelRibbonButtons.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageHome.SuspendLayout();
            panelHeader.SuspendLayout();
            toolStrip1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            panel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BackColor = Color.FromArgb(32, 32, 32);
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(1, 1);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.Desktop;
            splitContainer1.Panel1.Controls.Add(panelRibbon);
            splitContainer1.Panel1.Controls.Add(panelHeader);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel4);
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(797, 305);
            splitContainer1.SplitterDistance = 151;
            splitContainer1.TabIndex = 0;
            // 
            // panelRibbon
            // 
            panelRibbon.Controls.Add(panelRibbonButtons);
            panelRibbon.Controls.Add(tabControl1);
            panelRibbon.Dock = DockStyle.Fill;
            panelRibbon.Location = new Point(0, 21);
            panelRibbon.Name = "panelRibbon";
            panelRibbon.Size = new Size(797, 130);
            panelRibbon.TabIndex = 11;
            // 
            // panelRibbonButtons
            // 
            panelRibbonButtons.Controls.Add(buttonVid);
            panelRibbonButtons.Controls.Add(buttonShare);
            panelRibbonButtons.Controls.Add(buttonMain);
            panelRibbonButtons.Controls.Add(buttonFile);
            panelRibbonButtons.Dock = DockStyle.Top;
            panelRibbonButtons.Location = new Point(0, 0);
            panelRibbonButtons.Name = "panelRibbonButtons";
            panelRibbonButtons.Size = new Size(797, 22);
            panelRibbonButtons.TabIndex = 0;
            // 
            // buttonVid
            // 
            buttonVid.BackColor = Color.Black;
            buttonVid.Dock = DockStyle.Left;
            buttonVid.FlatAppearance.BorderSize = 0;
            buttonVid.FlatStyle = FlatStyle.Flat;
            buttonVid.Font = new Font("Segoe UI", 8.25F);
            buttonVid.ForeColor = Color.White;
            buttonVid.Location = new Point(212, 0);
            buttonVid.Name = "buttonVid";
            buttonVid.Size = new Size(53, 22);
            buttonVid.TabIndex = 3;
            buttonVid.Text = "Вид";
            buttonVid.UseVisualStyleBackColor = false;
            buttonVid.Click += buttonVid_Click;
            // 
            // buttonShare
            // 
            buttonShare.BackColor = Color.Black;
            buttonShare.Dock = DockStyle.Left;
            buttonShare.FlatAppearance.BorderSize = 0;
            buttonShare.FlatStyle = FlatStyle.Flat;
            buttonShare.Font = new Font("Segoe UI", 8.25F);
            buttonShare.ForeColor = Color.White;
            buttonShare.Location = new Point(127, 0);
            buttonShare.Name = "buttonShare";
            buttonShare.Size = new Size(85, 22);
            buttonShare.TabIndex = 2;
            buttonShare.Text = "Поделиться";
            buttonShare.UseVisualStyleBackColor = false;
            buttonShare.Click += buttonShare_Click;
            // 
            // buttonMain
            // 
            buttonMain.BackColor = Color.Black;
            buttonMain.Dock = DockStyle.Left;
            buttonMain.FlatAppearance.BorderSize = 0;
            buttonMain.FlatStyle = FlatStyle.Flat;
            buttonMain.Font = new Font("Segoe UI", 8.25F);
            buttonMain.ForeColor = Color.White;
            buttonMain.Location = new Point(52, 0);
            buttonMain.Name = "buttonMain";
            buttonMain.Size = new Size(75, 22);
            buttonMain.TabIndex = 1;
            buttonMain.Text = "Главная";
            buttonMain.UseVisualStyleBackColor = false;
            buttonMain.Click += buttonMain_Click;
            // 
            // buttonFile
            // 
            buttonFile.BackColor = Color.FromArgb(160, 40, 1);
            buttonFile.Dock = DockStyle.Left;
            buttonFile.FlatAppearance.BorderSize = 0;
            buttonFile.FlatStyle = FlatStyle.Flat;
            buttonFile.Font = new Font("Segoe UI", 8.25F);
            buttonFile.Location = new Point(0, 0);
            buttonFile.Name = "buttonFile";
            buttonFile.Size = new Size(52, 22);
            buttonFile.TabIndex = 0;
            buttonFile.Text = "Файл";
            buttonFile.UseVisualStyleBackColor = false;
            buttonFile.Click += buttonFile_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPageHome);
            tabControl1.Controls.Add(tabPageShare);
            tabControl1.Controls.Add(tabPageVid);
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(-7, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(811, 136);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 1;
            // 
            // tabPageHome
            // 
            tabPageHome.Controls.Add(flowLayoutPanel1);
            tabPageHome.Location = new Point(4, 5);
            tabPageHome.Name = "tabPageHome";
            tabPageHome.Padding = new Padding(3);
            tabPageHome.Size = new Size(803, 127);
            tabPageHome.TabIndex = 0;
            tabPageHome.Text = "tabPage1";
            tabPageHome.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.BackColor = Color.FromArgb(32, 32, 32);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(797, 121);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // tabPageShare
            // 
            tabPageShare.Location = new Point(4, 5);
            tabPageShare.Name = "tabPageShare";
            tabPageShare.Padding = new Padding(3);
            tabPageShare.Size = new Size(803, 127);
            tabPageShare.TabIndex = 1;
            tabPageShare.Text = "tabPage2";
            tabPageShare.UseVisualStyleBackColor = true;
            // 
            // tabPageVid
            // 
            tabPageVid.Location = new Point(4, 5);
            tabPageVid.Name = "tabPageVid";
            tabPageVid.Size = new Size(803, 127);
            tabPageVid.TabIndex = 2;
            tabPageVid.Text = "tabPage1";
            tabPageVid.UseVisualStyleBackColor = true;
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(buttonMinimize);
            panelHeader.Controls.Add(buttonMaximize);
            panelHeader.Controls.Add(buttonClose);
            panelHeader.Controls.Add(toolStrip1);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(797, 21);
            panelHeader.TabIndex = 10;
            panelHeader.MouseDown += panelHeader_MouseDown;
            panelHeader.MouseMove += panelHeader_MouseMove;
            panelHeader.MouseUp += panelHeader_MouseUp;
            // 
            // buttonMinimize
            // 
            buttonMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMinimize.FlatAppearance.BorderSize = 0;
            buttonMinimize.FlatStyle = FlatStyle.Popup;
            buttonMinimize.ForeColor = SystemColors.ControlLightLight;
            buttonMinimize.Location = new Point(689, -1);
            buttonMinimize.Name = "buttonMinimize";
            buttonMinimize.Size = new Size(36, 24);
            buttonMinimize.TabIndex = 12;
            buttonMinimize.Text = "--";
            buttonMinimize.UseVisualStyleBackColor = true;
            buttonMinimize.Click += buttonMinimize_Click;
            // 
            // buttonMaximize
            // 
            buttonMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMaximize.FlatAppearance.BorderSize = 0;
            buttonMaximize.FlatStyle = FlatStyle.Popup;
            buttonMaximize.Font = new Font("Segoe MDL2 Assets", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonMaximize.ForeColor = SystemColors.ControlLightLight;
            buttonMaximize.Location = new Point(725, -1);
            buttonMaximize.Name = "buttonMaximize";
            buttonMaximize.Size = new Size(36, 24);
            buttonMaximize.TabIndex = 11;
            buttonMaximize.Text = "";
            buttonMaximize.UseVisualStyleBackColor = true;
            buttonMaximize.Click += buttonMaximize_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.BackColor = SystemColors.Desktop;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Segoe MDL2 Assets", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonClose.ForeColor = SystemColors.ButtonHighlight;
            buttonClose.Location = new Point(761, -1);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(36, 24);
            buttonClose.TabIndex = 10;
            buttonClose.Text = "";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.Black;
            toolStrip1.GripMargin = new Padding(0);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripSeparator1, toolStripButtonCreateFolder, toolStripButtonDelete, toolStripButton4, toolStripButton5, toolStripButton6, toolStripButton7, toolStripDropDownButton1, toolStripSeparator2, toolStripLabel1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0);
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Size = new Size(797, 25);
            toolStrip1.TabIndex = 9;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.MouseDown += toolStrip1_MouseDown;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.ForeColor = Color.FromArgb(60, 60, 60, 60);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButtonCreateFolder
            // 
            toolStripButtonCreateFolder.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonCreateFolder.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButtonCreateFolder.ForeColor = Color.Khaki;
            toolStripButtonCreateFolder.Image = (Image)resources.GetObject("toolStripButtonCreateFolder.Image");
            toolStripButtonCreateFolder.ImageTransparentColor = Color.Magenta;
            toolStripButtonCreateFolder.Name = "toolStripButtonCreateFolder";
            toolStripButtonCreateFolder.Size = new Size(24, 22);
            toolStripButtonCreateFolder.Text = "";
            toolStripButtonCreateFolder.ToolTipText = "Создать папку\r\nСоздание папки";
            // 
            // toolStripButtonDelete
            // 
            toolStripButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonDelete.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButtonDelete.ForeColor = Color.FromArgb(64, 64, 64);
            toolStripButtonDelete.Image = (Image)resources.GetObject("toolStripButtonDelete.Image");
            toolStripButtonDelete.ImageTransparentColor = Color.Magenta;
            toolStripButtonDelete.Name = "toolStripButtonDelete";
            toolStripButtonDelete.Size = new Size(24, 22);
            toolStripButtonDelete.Text = "";
            toolStripButtonDelete.ToolTipText = "Удалить";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold);
            toolStripButton4.ForeColor = Color.Gray;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "";
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton5.Font = new Font("Segoe MDL2 Assets", 10F);
            toolStripButton5.ForeColor = Color.Gray;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(25, 22);
            toolStripButton5.Text = "";
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton6.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton6.ForeColor = Color.Gray;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(23, 22);
            toolStripButton6.Text = "";
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton7.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageAlign = ContentAlignment.BottomRight;
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(23, 22);
            toolStripButton7.Text = "";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.BackColor = Color.Black;
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { настройкаПанелиБыстрогоДоступаToolStripMenuItem });
            toolStripDropDownButton1.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripDropDownButton1.ForeColor = Color.White;
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.ShowDropDownArrow = false;
            toolStripDropDownButton1.Size = new Size(21, 22);
            toolStripDropDownButton1.Text = "";
            // 
            // настройкаПанелиБыстрогоДоступаToolStripMenuItem
            // 
            настройкаПанелиБыстрогоДоступаToolStripMenuItem.Name = "настройкаПанелиБыстрогоДоступаToolStripMenuItem";
            настройкаПанелиБыстрогоДоступаToolStripMenuItem.Size = new Size(229, 22);
            настройкаПанелиБыстрогоДоступаToolStripMenuItem.Text = "Настройка панели быстрого доступа";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.BackColor = Color.Black;
            toolStripSeparator2.ForeColor = Color.FromArgb(60, 60, 60, 60);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.ForeColor = Color.White;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(69, 22);
            toolStripLabel1.Text = "Проводник";
            // 
            // panel4
            // 
            panel4.Controls.Add(splitContainer2);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(797, 100);
            panel4.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
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
            splitContainer2.Size = new Size(797, 100);
            splitContainer2.SplitterDistance = 187;
            splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.FromArgb(25, 25, 25);
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.Dock = DockStyle.Fill;
            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeView1.ForeColor = Color.White;
            treeView1.FullRowSelect = true;
            treeView1.HotTracking = true;
            treeView1.ItemHeight = 18;
            treeView1.LineColor = Color.White;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.ShowLines = false;
            treeView1.Size = new Size(187, 100);
            treeView1.TabIndex = 0;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            treeView1.DrawNode += treeView1_DrawNode;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(32, 32, 32);
            listView1.BorderStyle = BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderEditDate, columnHeaderType, columnHeaderSize });
            listView1.Dock = DockStyle.Fill;
            listView1.ForeColor = Color.White;
            listView1.FullRowSelect = true;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(606, 100);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.MouseDoubleClick += listView1_MouseDoubleClick;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Имя";
            columnHeaderName.Width = 250;
            // 
            // columnHeaderEditDate
            // 
            columnHeaderEditDate.Text = "Дата изменения";
            columnHeaderEditDate.Width = 150;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "Тип";
            columnHeaderType.Width = 130;
            // 
            // columnHeaderSize
            // 
            columnHeaderSize.Text = "Размер";
            columnHeaderSize.Width = 100;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(51, 51, 51);
            panel2.Controls.Add(statusStrip1);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 127);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3, 0, 3, 3);
            panel2.Size = new Size(797, 23);
            panel2.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(51, 51, 51);
            statusStrip1.Dock = DockStyle.Left;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(3, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(120, 20);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 15);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(25, 25, 25);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(797, 27);
            panel1.TabIndex = 1;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(25, 25, 25);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(105, 2);
            button3.Name = "button3";
            button3.Size = new Size(33, 23);
            button3.TabIndex = 5;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = false;
            // 
            // comboBox2
            // 
            comboBox2.BackColor = Color.FromArgb(25, 25, 25);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(77, 2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(22, 23);
            comboBox2.TabIndex = 4;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(25, 25, 25);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(39, 1);
            button2.Name = "button2";
            button2.Size = new Size(32, 23);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(25, 25, 25);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(3, 1);
            button1.Name = "button1";
            button1.Size = new Size(30, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.BackColor = Color.FromArgb(25, 25, 25);
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.ForeColor = Color.White;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(166, -1);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(402, 24);
            comboBox1.TabIndex = 1;
            comboBox1.KeyDown += comboBox1_KeyDown;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.BackColor = Color.FromArgb(25, 25, 25);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(608, 1);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(185, 23);
            textBox1.TabIndex = 0;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(800, 307);
            Controls.Add(splitContainer1);
            ForeColor = Color.Beige;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormMain";
            Text = "FormMain";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelRibbon.ResumeLayout(false);
            panelRibbonButtons.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageHome.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel4.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TreeView treeView1;
        private ListView listView1;
        private Panel panel2;
        private StatusStrip statusStrip1;
        private Panel panel1;
        private Panel panel4;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panelHeader;
        private Button buttonMinimize;
        private Button buttonMaximize;
        private Button buttonClose;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonCreateFolder;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem настройкаПанелиБыстрогоДоступаToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripButtonDelete;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderEditDate;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderSize;
        private Panel panelRibbon;
        private Panel panelRibbonButtons;
        private Button buttonMain;
        private Button buttonFile;
        private Button buttonShare;
        private Button buttonVid;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Button button3;
        private ComboBox comboBox2;
        private Button button2;
        private Button button1;
        private TabControl tabControl1;
        private TabPage tabPageHome;
        private TabPage tabPageShare;
        private FlowLayoutPanel flowLayoutPanel1;
        private TabPage tabPageVid;
    }
}