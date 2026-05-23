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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            splitContainerMain = new SplitContainer();
            panelRibbon = new Panel();
            panelRibbonMain = new Panel();
            panelHome = new Panel();
            panelView = new Panel();
            panelRibbonButtons = new Panel();
            buttonVid = new Button();
            buttonMain = new Button();
            buttonFile = new Button();
            panelHeader = new Panel();
            buttonMinimize = new Button();
            buttonMaximize = new Button();
            buttonClose = new Button();
            toolStripMain = new ToolStrip();
            toolStripButtonIcon = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonCreateFolder = new ToolStripButton();
            toolStripButtonDelete = new ToolStripButton();
            toolStripButtonForward = new ToolStripButton();
            toolStripButtonCopy = new ToolStripButton();
            toolStripButtonUndo = new ToolStripButton();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            ToolStripMenuss = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            panel4 = new Panel();
            splitContainerForFiles = new SplitContainer();
            treeViewFiles = new TreeView();
            listViewFiles = new ListView();
            columnHeaderName = new ColumnHeader();
            columnHeaderEditDate = new ColumnHeader();
            columnHeaderType = new ColumnHeader();
            columnHeaderSize = new ColumnHeader();
            panel1 = new Panel();
            panel3 = new Panel();
            buttonAdressBar = new Button();
            labelUpdateDrivers = new Label();
            comboBoxAddressBar = new ComboBox();
            comboBoxLastWas = new ComboBox();
            labelFind = new Label();
            ButtonDesktop = new Button();
            buttonForward = new Button();
            buttonBack = new Button();
            textBoxFind = new TextBox();
            buttonDropDown = new Button();
            panel2 = new Panel();
            buttonSmallElements = new Button();
            buttonBigElements = new Button();
            panel5 = new Panel();
            statusStripMain = new StatusStrip();
            contextMenuStripListView = new ContextMenuStrip(components);
            createToolStripMenuItem = new ToolStripMenuItem();
            createFolderToolStripMenuItem = new ToolStripMenuItem();
            TxtToolStripMenuItem = new ToolStripMenuItem();
            InsertToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripMain = new ContextMenuStrip(components);
            ToolStripMenuItemOpen = new ToolStripMenuItem();
            ToolStripMenuItemCopy = new ToolStripMenuItem();
            ToolStripMenuItemRename = new ToolStripMenuItem();
            ToolStripMenuItemDelete = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelRibbon.SuspendLayout();
            panelRibbonMain.SuspendLayout();
            panelRibbonButtons.SuspendLayout();
            panelHeader.SuspendLayout();
            toolStripMain.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerForFiles).BeginInit();
            splitContainerForFiles.Panel1.SuspendLayout();
            splitContainerForFiles.Panel2.SuspendLayout();
            splitContainerForFiles.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel5.SuspendLayout();
            contextMenuStripListView.SuspendLayout();
            contextMenuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerMain.BackColor = Color.FromArgb(32, 32, 32);
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.IsSplitterFixed = true;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Margin = new Padding(3, 2, 3, 2);
            splitContainerMain.Name = "splitContainerMain";
            splitContainerMain.Orientation = Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = SystemColors.Desktop;
            splitContainerMain.Panel1.Controls.Add(panelRibbon);
            splitContainerMain.Panel1.Controls.Add(panelHeader);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(panel4);
            splitContainerMain.Panel2.Controls.Add(panel1);
            splitContainerMain.Panel2.Controls.Add(panel2);
            splitContainerMain.Size = new Size(799, 304);
            splitContainerMain.SplitterDistance = 151;
            splitContainerMain.TabIndex = 0;
            // 
            // panelRibbon
            // 
            panelRibbon.Controls.Add(panelRibbonMain);
            panelRibbon.Controls.Add(panelRibbonButtons);
            panelRibbon.Dock = DockStyle.Fill;
            panelRibbon.Location = new Point(0, 22);
            panelRibbon.Name = "panelRibbon";
            panelRibbon.Size = new Size(799, 129);
            panelRibbon.TabIndex = 11;
            // 
            // panelRibbonMain
            // 
            panelRibbonMain.BackColor = Color.FromArgb(52, 52, 52);
            panelRibbonMain.Controls.Add(panelHome);
            panelRibbonMain.Controls.Add(panelView);
            panelRibbonMain.Dock = DockStyle.Fill;
            panelRibbonMain.Location = new Point(0, 20);
            panelRibbonMain.Margin = new Padding(3, 2, 3, 2);
            panelRibbonMain.Name = "panelRibbonMain";
            panelRibbonMain.Size = new Size(799, 109);
            panelRibbonMain.TabIndex = 1;
            // 
            // panelHome
            // 
            panelHome.AutoScroll = true;
            panelHome.AutoSize = true;
            panelHome.BackColor = Color.FromArgb(32, 32, 32);
            panelHome.Dock = DockStyle.Fill;
            panelHome.Location = new Point(0, 0);
            panelHome.Margin = new Padding(3, 2, 3, 2);
            panelHome.Name = "panelHome";
            panelHome.Size = new Size(799, 109);
            panelHome.TabIndex = 1;
            // 
            // panelView
            // 
            panelView.AutoScroll = true;
            panelView.AutoSize = true;
            panelView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelView.BackColor = Color.FromArgb(32, 32, 32);
            panelView.Dock = DockStyle.Fill;
            panelView.Location = new Point(0, 0);
            panelView.Margin = new Padding(3, 2, 3, 2);
            panelView.Name = "panelView";
            panelView.Size = new Size(799, 109);
            panelView.TabIndex = 0;
            // 
            // panelRibbonButtons
            // 
            panelRibbonButtons.Controls.Add(buttonVid);
            panelRibbonButtons.Controls.Add(buttonMain);
            panelRibbonButtons.Controls.Add(buttonFile);
            panelRibbonButtons.Dock = DockStyle.Top;
            panelRibbonButtons.Location = new Point(0, 0);
            panelRibbonButtons.Name = "panelRibbonButtons";
            panelRibbonButtons.Size = new Size(799, 20);
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
            buttonVid.Location = new Point(127, 0);
            buttonVid.Name = "buttonVid";
            buttonVid.Size = new Size(53, 20);
            buttonVid.TabIndex = 3;
            buttonVid.Text = "Вид";
            buttonVid.UseVisualStyleBackColor = false;
            buttonVid.Click += ButtonView_Click;
            // 
            // buttonMain
            // 
            buttonMain.BackColor = Color.FromArgb(32, 32, 32);
            buttonMain.Dock = DockStyle.Left;
            buttonMain.FlatAppearance.BorderSize = 0;
            buttonMain.FlatStyle = FlatStyle.Flat;
            buttonMain.Font = new Font("Segoe UI", 8.25F);
            buttonMain.ForeColor = Color.White;
            buttonMain.Location = new Point(52, 0);
            buttonMain.Name = "buttonMain";
            buttonMain.Size = new Size(75, 20);
            buttonMain.TabIndex = 1;
            buttonMain.Text = "Главная";
            buttonMain.UseVisualStyleBackColor = false;
            buttonMain.Click += ButtonMain_Click;
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
            buttonFile.Size = new Size(52, 20);
            buttonFile.TabIndex = 0;
            buttonFile.Text = "Файл";
            buttonFile.UseVisualStyleBackColor = false;
            buttonFile.Click += ButtonFile_Click;
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(buttonMinimize);
            panelHeader.Controls.Add(buttonMaximize);
            panelHeader.Controls.Add(buttonClose);
            panelHeader.Controls.Add(toolStripMain);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(799, 22);
            panelHeader.TabIndex = 10;
            // 
            // buttonMinimize
            // 
            buttonMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMinimize.BackColor = Color.Black;
            buttonMinimize.FlatAppearance.BorderSize = 0;
            buttonMinimize.FlatStyle = FlatStyle.Popup;
            buttonMinimize.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonMinimize.ForeColor = Color.White;
            buttonMinimize.Location = new Point(690, -1);
            buttonMinimize.Name = "buttonMinimize";
            buttonMinimize.Size = new Size(36, 19);
            buttonMinimize.TabIndex = 12;
            buttonMinimize.Text = "—";
            buttonMinimize.UseVisualStyleBackColor = false;
            buttonMinimize.Click += ButtonMinimize_Click;
            // 
            // buttonMaximize
            // 
            buttonMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMaximize.BackColor = Color.Black;
            buttonMaximize.FlatAppearance.BorderSize = 0;
            buttonMaximize.FlatStyle = FlatStyle.Popup;
            buttonMaximize.Font = new Font("Segoe MDL2 Assets", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonMaximize.ForeColor = Color.White;
            buttonMaximize.Location = new Point(727, -1);
            buttonMaximize.Name = "buttonMaximize";
            buttonMaximize.Size = new Size(36, 19);
            buttonMaximize.TabIndex = 11;
            buttonMaximize.Text = "";
            buttonMaximize.UseVisualStyleBackColor = false;
            buttonMaximize.Click += ButtonMaximize_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.BackColor = Color.Black;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Popup;
            buttonClose.Font = new Font("Segoe MDL2 Assets", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonClose.ForeColor = Color.White;
            buttonClose.Location = new Point(763, -1);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(36, 19);
            buttonClose.TabIndex = 10;
            buttonClose.Text = "";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += ButtonClose_Click;
            // 
            // toolStripMain
            // 
            toolStripMain.BackColor = Color.Black;
            toolStripMain.Dock = DockStyle.Fill;
            toolStripMain.GripMargin = new Padding(0);
            toolStripMain.GripStyle = ToolStripGripStyle.Hidden;
            toolStripMain.ImageScalingSize = new Size(20, 20);
            toolStripMain.Items.AddRange(new ToolStripItem[] { toolStripButtonIcon, toolStripSeparator1, toolStripButtonCreateFolder, toolStripButtonDelete, toolStripButtonForward, toolStripButtonCopy, toolStripButtonUndo, toolStripDropDownButton1, toolStripSeparator2, toolStripLabel1 });
            toolStripMain.LayoutStyle = ToolStripLayoutStyle.Flow;
            toolStripMain.Location = new Point(0, 0);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.Padding = new Padding(0);
            toolStripMain.RenderMode = ToolStripRenderMode.System;
            toolStripMain.Size = new Size(799, 22);
            toolStripMain.TabIndex = 9;
            toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonIcon
            // 
            toolStripButtonIcon.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonIcon.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButtonIcon.ForeColor = Color.Khaki;
            toolStripButtonIcon.Image = (Image)resources.GetObject("toolStripButtonIcon.Image");
            toolStripButtonIcon.ImageTransparentColor = Color.Magenta;
            toolStripButtonIcon.Name = "toolStripButtonIcon";
            toolStripButtonIcon.Size = new Size(24, 17);
            toolStripButtonIcon.Text = "";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.ForeColor = Color.FromArgb(60, 60, 60, 60);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 23);
            // 
            // toolStripButtonCreateFolder
            // 
            toolStripButtonCreateFolder.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonCreateFolder.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButtonCreateFolder.ForeColor = Color.Khaki;
            toolStripButtonCreateFolder.Image = (Image)resources.GetObject("toolStripButtonCreateFolder.Image");
            toolStripButtonCreateFolder.ImageTransparentColor = Color.Magenta;
            toolStripButtonCreateFolder.Name = "toolStripButtonCreateFolder";
            toolStripButtonCreateFolder.Size = new Size(24, 17);
            toolStripButtonCreateFolder.Text = "";
            toolStripButtonCreateFolder.ToolTipText = "Создать папку\r\nСоздание папки";
            toolStripButtonCreateFolder.Click += CreateFolderToolStripMenuItem_Click;
            // 
            // toolStripButtonDelete
            // 
            toolStripButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonDelete.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripButtonDelete.ForeColor = Color.FromArgb(64, 64, 64);
            toolStripButtonDelete.Image = (Image)resources.GetObject("toolStripButtonDelete.Image");
            toolStripButtonDelete.ImageTransparentColor = Color.Magenta;
            toolStripButtonDelete.Name = "toolStripButtonDelete";
            toolStripButtonDelete.Size = new Size(24, 17);
            toolStripButtonDelete.Text = "";
            toolStripButtonDelete.ToolTipText = "Удалить";
            toolStripButtonDelete.Click += ButtonDelete_Click;
            // 
            // toolStripButtonForward
            // 
            toolStripButtonForward.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonForward.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold);
            toolStripButtonForward.ForeColor = Color.Gray;
            toolStripButtonForward.Image = (Image)resources.GetObject("toolStripButtonForward.Image");
            toolStripButtonForward.ImageTransparentColor = Color.Magenta;
            toolStripButtonForward.Name = "toolStripButtonForward";
            toolStripButtonForward.Size = new Size(23, 16);
            toolStripButtonForward.Text = "";
            toolStripButtonForward.Click += toolStripButtonForward_Click;
            // 
            // toolStripButtonCopy
            // 
            toolStripButtonCopy.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonCopy.Font = new Font("Segoe MDL2 Assets", 10F);
            toolStripButtonCopy.ForeColor = Color.Gray;
            toolStripButtonCopy.Image = (Image)resources.GetObject("toolStripButtonCopy.Image");
            toolStripButtonCopy.ImageTransparentColor = Color.Magenta;
            toolStripButtonCopy.Name = "toolStripButtonCopy";
            toolStripButtonCopy.Size = new Size(25, 18);
            toolStripButtonCopy.Text = "";
            toolStripButtonCopy.Click += ButtonCopy_Click;
            // 
            // toolStripButtonUndo
            // 
            toolStripButtonUndo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonUndo.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButtonUndo.ForeColor = Color.Gray;
            toolStripButtonUndo.Image = (Image)resources.GetObject("toolStripButtonUndo.Image");
            toolStripButtonUndo.ImageTransparentColor = Color.Magenta;
            toolStripButtonUndo.Name = "toolStripButtonUndo";
            toolStripButtonUndo.Size = new Size(23, 16);
            toolStripButtonUndo.Text = "";
            toolStripButtonUndo.Click += toolStripButtonUndo_Click;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.BackColor = Color.Black;
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuss });
            toolStripDropDownButton1.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolStripDropDownButton1.ForeColor = Color.White;
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.ShowDropDownArrow = false;
            toolStripDropDownButton1.Size = new Size(21, 16);
            toolStripDropDownButton1.Text = "";
            // 
            // ToolStripMenuss
            // 
            ToolStripMenuss.Name = "ToolStripMenuss";
            ToolStripMenuss.Size = new Size(107, 22);
            ToolStripMenuss.Text = "Справка";
            ToolStripMenuss.Click += ShowHelp_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.BackColor = Color.Black;
            toolStripSeparator2.ForeColor = Color.FromArgb(60, 60, 60, 60);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 23);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.ForeColor = Color.White;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(69, 15);
            toolStripLabel1.Text = "Проводник";
            // 
            // panel4
            // 
            panel4.Controls.Add(splitContainerForFiles);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(799, 99);
            panel4.TabIndex = 1;
            // 
            // splitContainerForFiles
            // 
            splitContainerForFiles.Dock = DockStyle.Fill;
            splitContainerForFiles.FixedPanel = FixedPanel.Panel1;
            splitContainerForFiles.Location = new Point(0, 0);
            splitContainerForFiles.Name = "splitContainerForFiles";
            // 
            // splitContainerForFiles.Panel1
            // 
            splitContainerForFiles.Panel1.Controls.Add(treeViewFiles);
            // 
            // splitContainerForFiles.Panel2
            // 
            splitContainerForFiles.Panel2.Controls.Add(listViewFiles);
            splitContainerForFiles.Size = new Size(799, 99);
            splitContainerForFiles.SplitterDistance = 187;
            splitContainerForFiles.TabIndex = 0;
            // 
            // treeViewFiles
            // 
            treeViewFiles.BackColor = Color.FromArgb(25, 25, 25);
            treeViewFiles.BorderStyle = BorderStyle.None;
            treeViewFiles.Dock = DockStyle.Fill;
            treeViewFiles.ForeColor = Color.White;
            treeViewFiles.FullRowSelect = true;
            treeViewFiles.HotTracking = true;
            treeViewFiles.ItemHeight = 25;
            treeViewFiles.LineColor = Color.White;
            treeViewFiles.Location = new Point(0, 0);
            treeViewFiles.Name = "treeViewFiles";
            treeViewFiles.ShowLines = false;
            treeViewFiles.Size = new Size(187, 99);
            treeViewFiles.TabIndex = 0;
            treeViewFiles.BeforeExpand += TreeView1_BeforeExpand;
            treeViewFiles.NodeMouseClick += treeViewFiles_NodeMouseClick;
            // 
            // listViewFiles
            // 
            listViewFiles.BackColor = Color.FromArgb(32, 32, 32);
            listViewFiles.BorderStyle = BorderStyle.None;
            listViewFiles.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderEditDate, columnHeaderType, columnHeaderSize });
            listViewFiles.Dock = DockStyle.Fill;
            listViewFiles.ForeColor = Color.White;
            listViewFiles.FullRowSelect = true;
            listViewFiles.Location = new Point(0, 0);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(608, 99);
            listViewFiles.TabIndex = 0;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            listViewFiles.ItemSelectionChanged += ListView1_ItemSelectionChanged;
            listViewFiles.MouseDoubleClick += ListView1_MouseDoubleClick;
            listViewFiles.MouseUp += ListView1_MouseUp;
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
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(25, 25, 25);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(labelFind);
            panel1.Controls.Add(ButtonDesktop);
            panel1.Controls.Add(buttonForward);
            panel1.Controls.Add(buttonBack);
            panel1.Controls.Add(textBoxFind);
            panel1.Controls.Add(buttonDropDown);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(799, 27);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(buttonAdressBar);
            panel3.Controls.Add(labelUpdateDrivers);
            panel3.Controls.Add(comboBoxAddressBar);
            panel3.Controls.Add(comboBoxLastWas);
            panel3.Location = new Point(153, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(423, 17);
            panel3.TabIndex = 1;
            // 
            // buttonAdressBar
            // 
            buttonAdressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAdressBar.FlatAppearance.BorderSize = 0;
            buttonAdressBar.FlatStyle = FlatStyle.Flat;
            buttonAdressBar.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAdressBar.ForeColor = Color.White;
            buttonAdressBar.Location = new Point(373, -2);
            buttonAdressBar.Name = "buttonAdressBar";
            buttonAdressBar.Size = new Size(26, 23);
            buttonAdressBar.TabIndex = 9;
            buttonAdressBar.Text = "";
            buttonAdressBar.UseVisualStyleBackColor = true;
            buttonAdressBar.Click += ButtonAddressBar_Click;
            // 
            // labelUpdateDrivers
            // 
            labelUpdateDrivers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelUpdateDrivers.AutoSize = true;
            labelUpdateDrivers.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUpdateDrivers.Location = new Point(399, 3);
            labelUpdateDrivers.Name = "labelUpdateDrivers";
            labelUpdateDrivers.Size = new Size(18, 12);
            labelUpdateDrivers.TabIndex = 7;
            labelUpdateDrivers.Text = "";
            labelUpdateDrivers.Click += labelUpdateDrivers_Click;
            // 
            // comboBoxAddressBar
            // 
            comboBoxAddressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxAddressBar.BackColor = Color.FromArgb(25, 25, 25);
            comboBoxAddressBar.ForeColor = Color.White;
            comboBoxAddressBar.FormattingEnabled = true;
            comboBoxAddressBar.ItemHeight = 15;
            comboBoxAddressBar.Location = new Point(-3, -3);
            comboBoxAddressBar.Name = "comboBoxAddressBar";
            comboBoxAddressBar.Size = new Size(396, 23);
            comboBoxAddressBar.TabIndex = 1;
            comboBoxAddressBar.KeyDown += ComboBoxAddressBar_KeyDown;
            // 
            // comboBoxLastWas
            // 
            comboBoxLastWas.BackColor = Color.FromArgb(25, 25, 25);
            comboBoxLastWas.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLastWas.ForeColor = Color.White;
            comboBoxLastWas.FormattingEnabled = true;
            comboBoxLastWas.Location = new Point(-143, -3);
            comboBoxLastWas.Name = "comboBoxLastWas";
            comboBoxLastWas.Size = new Size(136, 23);
            comboBoxLastWas.TabIndex = 4;
            comboBoxLastWas.SelectedIndexChanged += comboBoxLastWas_SelectedIndexChanged;
            // 
            // labelFind
            // 
            labelFind.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelFind.AutoSize = true;
            labelFind.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFind.ForeColor = SystemColors.ActiveBorder;
            labelFind.Location = new Point(771, 7);
            labelFind.Name = "labelFind";
            labelFind.Size = new Size(18, 12);
            labelFind.TabIndex = 6;
            labelFind.Text = "";
            labelFind.Click += labelFind_Click;
            // 
            // ButtonDesktop
            // 
            ButtonDesktop.BackColor = Color.FromArgb(25, 25, 25);
            ButtonDesktop.FlatAppearance.BorderSize = 0;
            ButtonDesktop.FlatStyle = FlatStyle.Flat;
            ButtonDesktop.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonDesktop.Location = new Point(105, 2);
            ButtonDesktop.Name = "ButtonDesktop";
            ButtonDesktop.Size = new Size(33, 23);
            ButtonDesktop.TabIndex = 5;
            ButtonDesktop.Text = "";
            ButtonDesktop.UseVisualStyleBackColor = false;
            ButtonDesktop.Click += ButtonDesktop_Click;
            // 
            // buttonForward
            // 
            buttonForward.BackColor = Color.FromArgb(25, 25, 25);
            buttonForward.FlatAppearance.BorderSize = 0;
            buttonForward.FlatStyle = FlatStyle.Flat;
            buttonForward.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonForward.Location = new Point(39, 1);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(32, 23);
            buttonForward.TabIndex = 3;
            buttonForward.Text = "";
            buttonForward.UseVisualStyleBackColor = false;
            buttonForward.Click += ButtonForward_Click;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.FromArgb(25, 25, 25);
            buttonBack.FlatAppearance.BorderSize = 0;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonBack.Location = new Point(3, 1);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(30, 23);
            buttonBack.TabIndex = 2;
            buttonBack.Text = "";
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += ButtonBack_Click;
            // 
            // textBoxFind
            // 
            textBoxFind.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxFind.BackColor = Color.FromArgb(25, 25, 25);
            textBoxFind.BorderStyle = BorderStyle.FixedSingle;
            textBoxFind.ForeColor = Color.White;
            textBoxFind.Location = new Point(609, 2);
            textBoxFind.Name = "textBoxFind";
            textBoxFind.Size = new Size(185, 23);
            textBoxFind.TabIndex = 0;
            textBoxFind.KeyDown += TextBoxFind_KeyDown;
            // 
            // buttonDropDown
            // 
            buttonDropDown.FlatAppearance.BorderSize = 0;
            buttonDropDown.FlatStyle = FlatStyle.Flat;
            buttonDropDown.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonDropDown.ForeColor = Color.White;
            buttonDropDown.Location = new Point(77, 2);
            buttonDropDown.Name = "buttonDropDown";
            buttonDropDown.Size = new Size(26, 23);
            buttonDropDown.TabIndex = 10;
            buttonDropDown.Text = "";
            buttonDropDown.UseVisualStyleBackColor = true;
            buttonDropDown.Click += ButtonDropDown_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(51, 51, 51);
            panel2.Controls.Add(buttonSmallElements);
            panel2.Controls.Add(buttonBigElements);
            panel2.Controls.Add(panel5);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 126);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3, 0, 3, 3);
            panel2.Size = new Size(799, 23);
            panel2.TabIndex = 2;
            // 
            // buttonSmallElements
            // 
            buttonSmallElements.BackColor = Color.FromArgb(51, 51, 51);
            buttonSmallElements.Dock = DockStyle.Right;
            buttonSmallElements.FlatAppearance.BorderSize = 0;
            buttonSmallElements.FlatStyle = FlatStyle.Flat;
            buttonSmallElements.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSmallElements.ForeColor = Color.White;
            buttonSmallElements.Location = new Point(756, 0);
            buttonSmallElements.Name = "buttonSmallElements";
            buttonSmallElements.Size = new Size(20, 20);
            buttonSmallElements.TabIndex = 2;
            buttonSmallElements.Text = "";
            buttonSmallElements.UseVisualStyleBackColor = false;
            buttonSmallElements.Click += ButtonSmallElements_Click;
            // 
            // buttonBigElements
            // 
            buttonBigElements.Dock = DockStyle.Right;
            buttonBigElements.FlatAppearance.BorderSize = 0;
            buttonBigElements.FlatStyle = FlatStyle.Flat;
            buttonBigElements.Font = new Font("Segoe MDL2 Assets", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonBigElements.ForeColor = Color.White;
            buttonBigElements.Location = new Point(776, 0);
            buttonBigElements.Name = "buttonBigElements";
            buttonBigElements.Size = new Size(20, 20);
            buttonBigElements.TabIndex = 1;
            buttonBigElements.Text = "";
            buttonBigElements.UseVisualStyleBackColor = true;
            buttonBigElements.Click += ButtonBigElements_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(statusStripMain);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(3, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(722, 20);
            panel5.TabIndex = 0;
            // 
            // statusStripMain
            // 
            statusStripMain.BackColor = Color.FromArgb(51, 51, 51, 51);
            statusStripMain.ImageScalingSize = new Size(20, 20);
            statusStripMain.Location = new Point(0, -2);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(722, 22);
            statusStripMain.SizingGrip = false;
            statusStripMain.TabIndex = 0;
            statusStripMain.Text = "statusStrip1";
            // 
            // contextMenuStripListView
            // 
            contextMenuStripListView.BackColor = Color.FromArgb(32, 32, 32);
            contextMenuStripListView.ImageScalingSize = new Size(20, 20);
            contextMenuStripListView.Items.AddRange(new ToolStripItem[] { createToolStripMenuItem, InsertToolStripMenuItem });
            contextMenuStripListView.Name = "contextMenuStrip1";
            contextMenuStripListView.RenderMode = ToolStripRenderMode.System;
            contextMenuStripListView.Size = new Size(123, 48);
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createFolderToolStripMenuItem, TxtToolStripMenuItem });
            createToolStripMenuItem.ForeColor = Color.White;
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(122, 22);
            createToolStripMenuItem.Text = "Создать";
            // 
            // createFolderToolStripMenuItem
            // 
            createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            createFolderToolStripMenuItem.Size = new Size(187, 22);
            createFolderToolStripMenuItem.Text = "Папку";
            createFolderToolStripMenuItem.Click += CreateFolderToolStripMenuItem_Click;
            // 
            // TxtToolStripMenuItem
            // 
            TxtToolStripMenuItem.Name = "TxtToolStripMenuItem";
            TxtToolStripMenuItem.Size = new Size(187, 22);
            TxtToolStripMenuItem.Text = "Текстовый документ";
            TxtToolStripMenuItem.Click += TxtToolStripMenuItem_Click;
            // 
            // InsertToolStripMenuItem
            // 
            InsertToolStripMenuItem.ForeColor = Color.White;
            InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
            InsertToolStripMenuItem.Size = new Size(122, 22);
            InsertToolStripMenuItem.Text = "Вставить";
            InsertToolStripMenuItem.Click += ButtonPaste_Click;
            // 
            // contextMenuStripMain
            // 
            contextMenuStripMain.BackColor = Color.FromArgb(32, 32, 32);
            contextMenuStripMain.ImageScalingSize = new Size(20, 20);
            contextMenuStripMain.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemOpen, ToolStripMenuItemCopy, ToolStripMenuItemRename, ToolStripMenuItemDelete });
            contextMenuStripMain.Name = "contextMenuStrip1";
            contextMenuStripMain.RenderMode = ToolStripRenderMode.System;
            contextMenuStripMain.Size = new Size(162, 92);
            // 
            // ToolStripMenuItemOpen
            // 
            ToolStripMenuItemOpen.ForeColor = Color.White;
            ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            ToolStripMenuItemOpen.Size = new Size(161, 22);
            ToolStripMenuItemOpen.Text = "Открыть";
            ToolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            // 
            // ToolStripMenuItemCopy
            // 
            ToolStripMenuItemCopy.ForeColor = Color.White;
            ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            ToolStripMenuItemCopy.Size = new Size(161, 22);
            ToolStripMenuItemCopy.Text = "Копировать";
            ToolStripMenuItemCopy.Click += ButtonCopy_Click;
            // 
            // ToolStripMenuItemRename
            // 
            ToolStripMenuItemRename.ForeColor = Color.White;
            ToolStripMenuItemRename.Name = "ToolStripMenuItemRename";
            ToolStripMenuItemRename.Size = new Size(161, 22);
            ToolStripMenuItemRename.Text = "Переименовать";
            ToolStripMenuItemRename.Click += ButtonRename_Click;
            // 
            // ToolStripMenuItemDelete
            // 
            ToolStripMenuItemDelete.ForeColor = Color.White;
            ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            ToolStripMenuItemDelete.Size = new Size(161, 22);
            ToolStripMenuItemDelete.Text = "Удалить";
            ToolStripMenuItemDelete.Click += ButtonDelete_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(800, 307);
            Controls.Add(splitContainerMain);
            ForeColor = Color.Beige;
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(400, 300);
            Name = "FormMain";
            Text = "FormMain";
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelRibbon.ResumeLayout(false);
            panelRibbonMain.ResumeLayout(false);
            panelRibbonMain.PerformLayout();
            panelRibbonButtons.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            panel4.ResumeLayout(false);
            splitContainerForFiles.Panel1.ResumeLayout(false);
            splitContainerForFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerForFiles).EndInit();
            splitContainerForFiles.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            contextMenuStripListView.ResumeLayout(false);
            contextMenuStripMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerMain;
        private SplitContainer splitContainerForFiles;
        private TreeView treeViewFiles;
        private ListView listViewFiles;
        private Panel panel2;
        private Panel panel1;
        private Panel panel4;
        private Panel panelHeader;
        private Button buttonMinimize;
        private Button buttonMaximize;
        private Button buttonClose;
        private ToolStrip toolStripMain;
        private ToolStripButton toolStripButtonIcon;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonCreateFolder;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem ToolStripMenuss;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripButtonDelete;
        private ToolStripButton toolStripButtonForward;
        private ToolStripButton toolStripButtonCopy;
        private ToolStripButton toolStripButtonUndo;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderEditDate;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderSize;
        private Panel panelRibbon;
        private Panel panelRibbonButtons;
        private Button buttonMain;
        private Button buttonFile;
        private TextBox textBoxFind;
        private ComboBox comboBoxAddressBar;
        private Button ButtonDesktop;
        private ComboBox comboBoxLastWas;
        private Button buttonForward;
        private Button buttonBack;
        private Label labelFind;
        private Label labelUpdateDrivers;
        private Panel panel3;
        private Button buttonAdressBar;
        private Button buttonDropDown;
        private ContextMenuStrip contextMenuStripListView;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem createFolderToolStripMenuItem;
        private ContextMenuStrip contextMenuStripMain;
        private ToolStripMenuItem ToolStripMenuItemOpen;
        private ToolStripMenuItem ToolStripMenuItemCopy;
        private ToolStripMenuItem ToolStripMenuItemRename;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ToolStripMenuItem InsertToolStripMenuItem;
        private Button buttonSmallElements;
        private Button buttonBigElements;
        private Panel panel5;
        private StatusStrip statusStripMain;
        private Button buttonVid;
        private ToolStripMenuItem TxtToolStripMenuItem;
        private Panel panelRibbonMain;
        private Panel panelView;
        private Panel panelHome;
    }
}