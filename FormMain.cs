using Microsoft.VisualBasic;
using rgz1_timp.Command;
using rgz1_timp.DrawExplorer;
using rgz1_timp.Services;
using System.Diagnostics;

namespace rgz1_timp
{
    public partial class FormMain : Form
    {
        private readonly CurrentPathModel currentPathModel = new();
        private readonly FileOperationService fileService;
        private readonly DrawIcons icons;
        private readonly DrawTreeView drawTreeView;
        private readonly DrawListView drawListView;
        private readonly DrawAdressBar drawAddressBar;
        private readonly DrawDropDownList drawDropDownList;
        private readonly DrawStatusStrip drawStatusStrip;
        private readonly DrawRibbon drawRibbon;

        private bool drawDetails = true;

        public FormMain()
        {
            InitializeComponent();

            // Инициализация сервисов
            fileService = new FileOperationService(currentPathModel);
            icons = new DrawIcons();

            drawTreeView = new DrawTreeView(treeViewFiles, icons);
            drawListView = new DrawListView(listViewFIles, icons);
            drawAddressBar = new DrawAdressBar(comboBoxAdressBar);
            drawDropDownList = new DrawDropDownList(comboBoxLastWas, treeViewFiles);
            drawStatusStrip = new DrawStatusStrip(statusStripMain, listViewFIles);
            drawRibbon = new DrawRibbon(tabControlShare, this);

            // Подписка на события модели
            currentPathModel.PathChanged += OnPathChanged;
            currentPathModel.NavigationStateChanged += UpdateNavigationButtons;

            // Дополнительная настройка событий
            listViewFIles.KeyDown += ListViewFiles_KeyDown;
        }

        private void OnPathChanged(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;
            drawListView.LoadDirectory(path, drawDetails);
            drawAddressBar.UpdateAddressBar(path);
            drawStatusStrip.UpdateStatusStrip();
        }

        private void UpdateNavigationButtons()
        {
            buttonBack.Enabled = currentPathModel.CanGoBack;
            buttonForward.Enabled = currentPathModel.CanGoForward;
        }

        //  ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ UI 
        private string? GetSelectedItemPath()
        {
            return listViewFIles.SelectedItems.Count > 0
                ? listViewFIles.SelectedItems[0].Tag?.ToString()
                : null;
        }

        private string GetCurrentDirectory() => comboBoxAdressBar.Text;

        private void OpenFile()
        {
            string? path = GetSelectedItemPath();
            if (string.IsNullOrEmpty(path)) return;

            if (Directory.Exists(path))
            {
                currentPathModel.Path = path;
            }
            else if (File.Exists(path))
            {
                try
                {
                    Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshUiAfterCommand()
        {
            currentPathModel.Refresh();
        }

        //  ПУБЛИЧНЫЕ МЕТОДЫ, ВЫЗЫВАЕМЫЕ ИЗ ЛЕНТЫ (RIBBON) 
        public void CreateNewFolder()
        {
            if (fileService.CreateNewFolder())
                RefreshUiAfterCommand();
        }

        public void CreateNewFile()
        {
            if (fileService.CreateNewFile())
                RefreshUiAfterCommand();
        }

        public void CopySelectedItem()
        {
            fileService.CopyItem(GetSelectedItemPath());
        }

        public void CutSelectedItem()
        {
            fileService.CutItem(GetSelectedItemPath());
        }

        public void PasteItem()
        {
            if (fileService.PasteItem())
                RefreshUiAfterCommand();
        }

        public void DeleteSelectedItem()
        {
            if (fileService.DeleteItem(GetSelectedItemPath()))
                RefreshUiAfterCommand();
        }

        public void RenameSelectedItem()
        {
            string? oldPath = GetSelectedItemPath();
            if (oldPath == null) return;

            string newName = Interaction.InputBox("Новое имя:", "Переименование", Path.GetFileName(oldPath));
            if (fileService.RenameItem(oldPath, newName))
                RefreshUiAfterCommand();
        }

        public void MoveToDialog()
        {
            string? path = GetSelectedItemPath();
            if (path == null) return;

            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (fileService.MoveToFolder(path, fbd.SelectedPath))
                        RefreshUiAfterCommand();
                }
            }
        }

        public void CopyToDialog()
        {
            string? path = GetSelectedItemPath();
            if (path == null) return;

            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (fileService.CopyToFolder(path, fbd.SelectedPath))
                        RefreshUiAfterCommand();
                }
            }
        }

        public void CopyPath()
        {
            fileService.CopyPathToClipboard(GetSelectedItemPath());
        }

        // Заглушки для нереализованных функций 
        public void SimpleAccess() { /* TODO */ }
        public void PinToQuickAccess() { /* TODO */ }
        public void PasteShortcut() { /* TODO */ }

        //  ОБРАБОТЧИКИ КНОПОК ФОРМЫ 
        private void ButtonClose_Click(object sender, EventArgs e) => Close();
        private void ButtonMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal
                ? FormWindowState.Maximized
                : FormWindowState.Normal;
        }
        private void ButtonMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void ButtonBack_Click(object sender, EventArgs e) => currentPathModel.GoBack();
        private void ButtonForward_Click(object sender, EventArgs e) => currentPathModel.GoForward();
        private void ButtonDesktop_Click(object sender, EventArgs e) => currentPathModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Переключение вкладок ленты
        private void ButtonMain_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 0;
            buttonVid.BackColor = Color.Black;
            buttonShare.BackColor = Color.Black;
            buttonMain.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void ButtonShare_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 1;
            buttonShare.BackColor = Color.FromArgb(32, 32, 32);
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.Black;
        }

        private void ButtonVid_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 2;
            buttonShare.BackColor = Color.Black;
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.FromArgb(32, 32, 32);
        }

        // Переключение режимов отображения
        private void ButtonSmallElements_Click(object sender, EventArgs e)
        {
            drawDetails = true;
            if (comboBoxAdressBar.Items.Count > 0)
                currentPathModel.Path = comboBoxAdressBar.Text;
        }

        private void ButtonBigElements_Click(object sender, EventArgs e)
        {
            drawDetails = false;
            if (comboBoxAdressBar.Items.Count > 0)
                currentPathModel.Path = comboBoxAdressBar.Text;
        }

        //  ОБРАБОТЧИКИ КОНТЕКСТНОГО МЕНЮ 
        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e) => OpenFile();
        private void CreateFolderToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFolder();

        //  ОБРАБОТЧИКИ ДРУГИХ ЭЛЕМЕНТОВ УПРАВЛЕНИЯ 
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not string tag) return;

            // Обработка специальных узлов (можно расширить)
            if (tag == "Этот компьютер" || tag == "Быстрый доступ")
                return;

            if (Directory.Exists(tag))
                currentPathModel.Path = tag;
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) => drawTreeView.AddNodes(e);

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e) => OpenFile();

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            drawStatusStrip.UpdateStatusStrip();
        }

        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hitTest = listViewFIles.HitTest(e.Location);
            if (hitTest.Item != null)
            {
                hitTest.Item.Selected = true;
                contextMenuStripMain.Show(listViewFIles, e.Location);
            }
            else
            {
                contextMenuStripListView.Show(listViewFIles, e.Location);
            }
        }

        private void ListViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenFile();
                e.SuppressKeyPress = true;
            }
        }

        private void ComboBoxAddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string targetPath = comboBoxAdressBar.Text;
                if (Directory.Exists(targetPath))
                    currentPathModel.Path = targetPath;
                else
                    MessageBox.Show("Путь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.SuppressKeyPress = true;
            }
        }

        private void ButtonAddressBar_Click(object sender, EventArgs e) => comboBoxAdressBar.DroppedDown = true;
        private void ButtonDropDown_Click(object sender, EventArgs e) => comboBoxLastWas.DroppedDown = true;
        private void ButtonCopy_Click(object sender, EventArgs e) => CopySelectedItem();
        private void ButtonRename_Click(object sender, EventArgs e) => RenameSelectedItem();
        private void ButtonDelete_Click(object sender, EventArgs e) => DeleteSelectedItem();
        private void ButtonPaste_Click(object sender, EventArgs e) => PasteItem();
        private void TextBoxFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("В разработке");
                textBoxFind.Text = "";
            }
        }

        private void ButtonFile_Click(object sender, EventArgs e)
        {
            // Можно реализовать меню "Файл" или оставить пустым
        }


        //  ПЕРЕОПРЕДЕЛЁННЫЕ МЕТОДЫ ДЛЯ РЕСАЙЗА И ГОРЯЧИХ КЛАВИШ 
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                Point pos = PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                int resizeArea = 10;

                if (pos.X <= resizeArea && pos.Y <= resizeArea) m.Result = (IntPtr)HTTOPLEFT;
                else if (pos.X >= ClientSize.Width - resizeArea && pos.Y <= resizeArea) m.Result = (IntPtr)HTTOPRIGHT;
                else if (pos.X <= resizeArea && pos.Y >= ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (pos.X >= ClientSize.Width - resizeArea && pos.Y >= ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (pos.X <= resizeArea) m.Result = (IntPtr)HTLEFT;
                else if (pos.X >= ClientSize.Width - resizeArea) m.Result = (IntPtr)HTRIGHT;
                else if (pos.Y <= resizeArea) m.Result = (IntPtr)HTTOP;
                else if (pos.Y >= ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                CommandInvoker.Undo();
                RefreshUiAfterCommand();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Y))
            {
                CommandInvoker.Redo();
                RefreshUiAfterCommand();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}