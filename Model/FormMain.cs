using Microsoft.VisualBasic;
using rgz1_timp.Command;
using rgz1_timp.DrawExplorer;
using rgz1_timp.Services;
using rgz1_timp.Services.rgz1_timp.Services;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace rgz1_timp
{
    public partial class FormMain : Form
    {
        private readonly CurrentPathModel currentPathModel = new();
        private readonly FileOperationService fileService;
        private readonly DrawIcons icons;
        private readonly DrawTreeView drawTreeView;
        private readonly DrawListView drawListView;
        private readonly DrawAddressBar drawAddressBar;
        private readonly DrawDropDownList drawDropDownList;
        private readonly DrawStatusStrip drawStatusStrip;
        private readonly DrawRibbon drawRibbon;

        private View viewList = View.Details;
        private string currentSearchQuery = string.Empty;

        public FormMain()
        {
            InitializeComponent();

            IDialogService dialog = new DialogService();
            fileService = new FileOperationService(currentPathModel, dialog);
            icons = new DrawIcons();

            drawTreeView = new DrawTreeView(treeViewFiles, icons);
            drawListView = new DrawListView(listViewFiles, icons, viewList);
            drawAddressBar = new DrawAddressBar(comboBoxAddressBar);
            drawDropDownList = new DrawDropDownList(comboBoxLastWas, treeViewFiles);
            drawStatusStrip = new DrawStatusStrip(statusStripMain, listViewFiles);
            drawRibbon = new DrawRibbon(panelRibbonMain, this); 
            // Подписка на события модели
            currentPathModel.PathChanged += OnPathChanged;
            currentPathModel.NavigationStateChanged += UpdateNavigationButtons;

            // Дополнительная настройка событий
            listViewFiles.KeyDown += ListViewFiles_KeyDown;
            this.Shown += (s, e) => drawAddressBar.ResetAddressBarSelection();
            currentPathModel.Path = "Этот компьютер";

        }

        private void OnPathChanged(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;

            // Сбрасываем строку поиска и фильтр
            textBoxFind.Text = string.Empty;
            currentSearchQuery = string.Empty;
            drawAddressBar.UpdateAddressBar(path);
            drawStatusStrip.UpdateStatusStrip();
            drawTreeView.RefreshNodeByPath(path);
            drawListView.LoadDirectory(path);
            drawTreeView.SelectNodeByPath(path);
        }

        private void UpdateNavigationButtons()
        {
            buttonBack.Enabled = currentPathModel.CanGoBack;
            buttonForward.Enabled = currentPathModel.CanGoForward;
        }

        //  ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ UI 
        private string? GetSelectedItemPath()
        {
            return listViewFiles.SelectedItems.Count > 0
                ? listViewFiles.SelectedItems[0].Tag?.ToString()
                : null;
        }

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

        private void RefreshUi()
        {
            currentPathModel.Refresh();
        }

        //  ПУБЛИЧНЫЕ МЕТОДЫ, ВЫЗЫВАЕМЫЕ ИЗ ЛЕНТЫ (RIBBON) 
        public void CreateNewFolder()
        {
            if (fileService.CreateNewFolder())
                RefreshUi();
        }

        public void CreateNewFile()
        {
            if (fileService.CreateNewFile())
                RefreshUi();
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
                RefreshUi();
        }

        public void DeleteSelectedItem()
        {
            if (fileService.DeleteItem(GetSelectedItemPath()))
                RefreshUi();
        }

        public void RenameSelectedItem()
        {
            string? oldPath = GetSelectedItemPath();
            if (oldPath == null) return;

            string newName = Interaction.InputBox("Новое имя:", "Переименование", Path.GetFileName(oldPath));
            if (fileService.RenameItem(oldPath, newName))
                RefreshUi();
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
                        RefreshUi();
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
                        RefreshUi();
                }
            }
        }

        public void CopyPath()
        {
            fileService.CopyPathToClipboard(GetSelectedItemPath());
        }

        public void SortFiles()
        {
            drawListView.SortByColumn(0, true);
        }

        // Автоматический размер колонок
        public void AutoResizeColumns()
        {
            drawListView.AutoResizeColumns();
        }


        //  ОБРАБОТЧИКИ КНОПОК ФОРМЫ 
        private void ButtonClose_Click(object sender, EventArgs e) => Close();
        private void ButtonMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal
                ? FormWindowState.Maximized
                : FormWindowState.Normal;
            drawAddressBar.ResetAddressBarSelection();
        }
        private void ButtonMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void ButtonBack_Click(object sender, EventArgs e) => currentPathModel.GoBack();
        private void ButtonForward_Click(object sender, EventArgs e) => currentPathModel.GoForward();
        private void ButtonDesktop_Click(object sender, EventArgs e) => currentPathModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Переключение вкладок ленты
        private void ButtonMain_Click(object sender, EventArgs e)
        {
            panelHome.Visible = true;
            panelView.Visible = false;
            buttonVid.BackColor = Color.Black;
            buttonMain.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void ButtonView_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            panelView.Visible = true;
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.FromArgb(32, 32, 32);
        }

        // Переключение режимов отображения
        private void ButtonSmallElements_Click(object sender, EventArgs e)
        {
            SetDetails();
        }

        private void ButtonBigElements_Click(object sender, EventArgs e)
        {
            SetLargeIcons();
        }

        public void SetLargeIcons() { drawListView.SetView(View.LargeIcon); RefreshUi(); }
        public void SetSmallIcons() { drawListView.SetView(View.SmallIcon); RefreshUi(); }
        public void SetList() { drawListView.SetView(View.List); RefreshUi(); }
        public void SetDetails() { drawListView.SetView(View.Details); RefreshUi(); }
        public void SetTiles() { drawListView.SetView(View.Tile); RefreshUi(); }

        //  ОБРАБОТЧИКИ КОНТЕКСТНОГО МЕНЮ 
        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e) => OpenFile();
        private void CreateFolderToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFolder();

        //  ОБРАБОТЧИКИ ДРУГИХ ЭЛЕМЕНТОВ УПРАВЛЕНИЯ 

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) => drawTreeView.AddNodes(e);

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e) => OpenFile();

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            drawStatusStrip.UpdateStatusStrip();
        }

        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hitTest = listViewFiles.HitTest(e.Location);
            if (hitTest.Item != null)
            {
                hitTest.Item.Selected = true;
                contextMenuStripMain.Show(listViewFiles, e.Location);
            }
            else
            {
                contextMenuStripListView.Show(listViewFiles, e.Location);
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
                string targetPath = comboBoxAddressBar.Text;
                if (Directory.Exists(targetPath) || targetPath == "Этот компьютер" || targetPath == "Быстрый доступ")
                    currentPathModel.Path = targetPath;
                else
                    MessageBox.Show("Путь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.SuppressKeyPress = true;
            }
        }

        private void ButtonAddressBar_Click(object sender, EventArgs e) => comboBoxAddressBar.DroppedDown = true;
        private void ButtonDropDown_Click(object sender, EventArgs e) => comboBoxLastWas.DroppedDown = true;
        private void ButtonCopy_Click(object sender, EventArgs e) => CopySelectedItem();
        private void ButtonRename_Click(object sender, EventArgs e) => RenameSelectedItem();
        private void ButtonDelete_Click(object sender, EventArgs e) => DeleteSelectedItem();
        private void ButtonPaste_Click(object sender, EventArgs e) => PasteItem();
        private void TextBoxFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.SuppressKeyPress = true;
            }
        }

        private void PerformSearch()
        {
            string query = textBoxFind.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                currentSearchQuery = string.Empty;
                if (!string.IsNullOrEmpty(currentPathModel.Path))
                    drawListView.LoadDirectory(currentPathModel.Path);
                labelFind.Text = "";

            }
            else
            {
                currentSearchQuery = query;
                drawListView.LoadDirectoryWithFilter(currentPathModel.Path, query);
                labelFind.Text = "X";
            }
            drawStatusStrip.UpdateStatusStrip();
        }

        private void ButtonFile_Click(object sender, EventArgs e)
        {
            // Можно реализовать меню "Файл" или оставить пустым
        }


        //  ПЕРЕОПРЕДЕЛЁННЫЕ МЕТОДЫ ДЛЯ РЕСАЙЗА И ГОРЯЧИХ КЛАВИШ 
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCAPTION = 2;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            const int WM_DEVICECHANGE = 0x0219;

            base.WndProc(ref m);


            if (m.Msg == WM_DEVICECHANGE)
            {
                drawTreeView.RefreshDrives();
                if (comboBoxAddressBar.Text == "Этот компьютер")
                    currentPathModel.Path = comboBoxAddressBar.Text;
            }

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
                if (pos.Y <= panelHeader.Height && pos.Y > 0)
                {
                    // Проверяем, что мышь не на кнопках управления (примерно их области)
                    if (!(pos.X > buttonMinimize.Left && pos.X < buttonClose.Right))
                    {
                        m.Result = (IntPtr)HTCAPTION;
                        return;
                    }
                }
            }
            drawAddressBar.ResetAddressBarSelection();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                CommandInvoker.Undo();
                RefreshUi();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Y))
            {
                CommandInvoker.Redo();
                RefreshUi();
                return true;
            }

            // Справка
            if (keyData == Keys.F1)
            {
                using (var help = new HelpForm()) help.ShowDialog(this);
                return true;
            }

            // Удаление (Delete)
            if (keyData == Keys.Delete)
            {
                DeleteSelectedItem();
                return true;
            }

            // Переименование (F2)
            if (keyData == Keys.F2)
            {
                RenameSelectedItem();
                return true;
            }

            // Обновить (F5)
            if (keyData == Keys.F5)
            {
                RefreshUi();
                return true;
            }

            // Копировать (Ctrl+C)
            if (keyData == (Keys.Control | Keys.C))
            {
                CopySelectedItem();
                return true;
            }

            // Вырезать (Ctrl+X)
            if (keyData == (Keys.Control | Keys.X))
            {
                CutSelectedItem();
                return true;
            }

            // Вставить (Ctrl+V)
            if (keyData == (Keys.Control | Keys.V))
            {
                PasteItem();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowHelp_Click(object sender, EventArgs e)
        {
            using var help = new HelpForm();
            help.ShowDialog(this);
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            CommandInvoker.Redo();
            RefreshUi();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            CommandInvoker.Undo();
            RefreshUi();
        }

        private void labelFind_Click(object sender, EventArgs e)
        {
            if (labelFind.Text == "X")
            {
                textBoxFind.Text = "";
                labelFind.Text = "";
            }
            PerformSearch();
        }

        private void labelUpdateDrivers_Click(object sender, EventArgs e)
        {
            currentPathModel.Refresh();
        }

        private void treeViewFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag is not string tag) return;

            // Обработка специальных узлов (можно расширить)
            if (Directory.Exists(tag) || tag == "Этот компьютер" || tag == "Быстрый доступ")
                if (currentPathModel.Path == tag)
                {
                    currentPathModel.Refresh();
                }
                else
                {
                    currentPathModel.Path = tag;
                }
            listViewFiles.Focus();
        }

        private void TxtToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFile();
    }
}