using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using rgz1_timp.Command;
using rgz1_timp.DrawExplorer;
using rgz1_timp.ImportedDll;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace rgz1_timp
{
    public partial class FormMain : Form
    {
        //  ПОЛЯ 
        private static bool drawDetails = true;

        private readonly CurrentPathModel currentPathModel = new();
        private string? _copiedPath;
        private bool _isCut; // true - вырезать, false - копировать

        //  КОНСТРУКТОР 
        public FormMain()
        {
            InitializeComponent();

            currentPathModel.PathChanged += OnPathChanged;
            currentPathModel.NavigationStateChanged += UpdateNavigationButtons;

            DrawTreeView.DrawSystemTreeView(treeViewFiles);
            DrawListView.DrawSystemListView(listViewFIles);
            DrawRibbon.SetupRibbon(tabControlShare, this);
            DrawDropDownList.DrawSystemDropDownList(comboBoxLastWas, treeViewFiles);
        }

        //  ОБРАБОТЧИКИ СОБЫТИЙ МОДЕЛИ 
        private void OnPathChanged(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;
            DrawListView.LoadDirectory(listViewFIles, path, drawDetails);
            DrawAdressBar.UpdateAddressBar(comboBoxAdressBar, path);
            DrawStatusStrip.UpdateStatusStrip(statusStripMain, listViewFIles);
        }

        private void UpdateNavigationButtons()
        {
            buttonBack.Enabled = currentPathModel.CanGoBack;
            buttonForward.Enabled = currentPathModel.CanGoForward;
        }

        //  ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ 
        private string? GetSelectedItemPath()
        {
            return listViewFIles.SelectedItems.Count > 0
                ? listViewFIles.SelectedItems[0].Tag?.ToString()
                : null;
        }

        private string GetCurrentDirectory()
        {
            return comboBoxAdressBar.Text;
        }

        private string GetUniqueFolderName(string parentPath)
        {
            string baseName = "Новая папка";
            string folderName = baseName;
            int counter = 1;

            while (Directory.Exists(Path.Combine(parentPath, folderName)))
            {
                counter++;
                folderName = $"{baseName} ({counter})";
            }
            return folderName;
        }

        private bool IsCyclicOperation(string sourcePath, string destinationDir)
        {
            if (!Directory.Exists(sourcePath)) return false;

            string sourceFull = Path.GetFullPath(sourcePath).TrimEnd(Path.DirectorySeparatorChar);
            string destFull = Path.GetFullPath(destinationDir).TrimEnd(Path.DirectorySeparatorChar);

            return destFull == sourceFull || destFull.StartsWith(sourceFull + Path.DirectorySeparatorChar);
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
                    MessageBox.Show($"Не удалось открыть файл: {ex.Message}");
                }
            }
        }

        private void RefreshUiAfterCommand()
        {
            currentPathModel.Refresh();
        }

        //  ОПЕРАЦИИ С ФАЙЛАМИ/ПАПКАМИ 
        public void CreateNewFolder()
        {
            string currentDir = currentPathModel.Path!;
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir)) return;

            string newFolderName = GetUniqueFolderName(currentDir);
            var command = new NewFolderCommand(currentDir, newFolderName);
            CommandInvoker.ExecuteCommand(command);
            RefreshUiAfterCommand();
        }

        public void CreateNewFile()
        {
            string currentDir = GetCurrentDirectory();
            if (!Directory.Exists(currentDir)) return;

            string baseName = "Новый текстовый документ.txt";
            string fileName = baseName;
            int counter = 1;

            while (File.Exists(Path.Combine(currentDir, fileName)))
            {
                fileName = $"Новый текстовый документ ({counter}).txt";
                counter++;
            }

            var command = new CreateFileCommand(currentDir, fileName);
            CommandInvoker.ExecuteCommand(command);
            RefreshUiAfterCommand();
        }

        //  БУФЕР ОБМЕНА 
        public void CopySelectedItem()
        {
            string? path = GetSelectedItemPath();
            if (path != null)
            {
                _copiedPath = path;
                _isCut = false;
            }
        }

        public void CutSelectedItem()
        {
            string? path = GetSelectedItemPath();
            if (path != null)
            {
                _copiedPath = path;
                _isCut = true;
            }
        }

        public void PasteItem()
        {
            if (string.IsNullOrEmpty(_copiedPath)) return;
            string destDir = GetCurrentDirectory();
            if (!Directory.Exists(destDir)) return;

            if (IsCyclicOperation(_copiedPath, destDir))
            {
                MessageBox.Show("Нельзя вставить папку в саму себя или в её подпапку.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string destPath = Path.Combine(destDir, Path.GetFileName(_copiedPath));
            ICommand command = _isCut
                ? new MoveCommand(_copiedPath, destPath)
                : new CopyCommand(_copiedPath, destPath);

            try
            {
                CommandInvoker.ExecuteCommand(command);
                if (_isCut)
                {
                    _copiedPath = null;
                    _isCut = false;
                }
                RefreshUiAfterCommand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вставке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void SimpleAccess()
        {
            // todo
        }

        public void MoveToDialog()
        {
            // Можно реализовать выбор папки через FolderBrowserDialog
            string? path = GetSelectedItemPath();
            if (path == null) return;
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string destDir = fbd.SelectedPath;
                    if (Directory.Exists(path) && IsCyclicOperation(path, destDir))
                    {
                        MessageBox.Show("Нельзя переместить папку в саму себя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string destPath = Path.Combine(destDir, Path.GetFileName(path));
                    var cmd = new MoveCommand(path, destPath);
                    CommandInvoker.ExecuteCommand(cmd);
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
                    string destDir = fbd.SelectedPath;
                    if (Directory.Exists(path) && IsCyclicOperation(path, destDir))
                    {
                        MessageBox.Show("Нельзя скопировать папку в саму себя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string destPath = Path.Combine(destDir, Path.GetFileName(path));
                    var cmd = new CopyCommand(path, destPath);
                    CommandInvoker.ExecuteCommand(cmd);
                    RefreshUiAfterCommand();
                }
            }
        }

        public void DeleteSelectedItem()
        {
            string? path = GetSelectedItemPath();
            if (path == null) return;
            var cmd = new DeleteCommand(path);
            CommandInvoker.ExecuteCommand(cmd);
            RefreshUiAfterCommand();
        }

        public void RenameSelectedItem()
        {
            string? oldPath = GetSelectedItemPath();
            if (oldPath == null) return;
            string newName = Microsoft.VisualBasic.Interaction.InputBox("Новое имя:", "Переименование", Path.GetFileName(oldPath));
            if (string.IsNullOrEmpty(newName) || newName == Path.GetFileName(oldPath)) return;
            var cmd = new RenameCommand(oldPath, newName);
            CommandInvoker.ExecuteCommand(cmd);
            RefreshUiAfterCommand();
        }

        internal void PinToQuickAccess()
        {
            // todo
        }

        public void CopyPath()
        {
            string? path = GetSelectedItemPath();
            if (path != null) Clipboard.SetText(path);
        }

        public void PasteShortcut()
        {
            // todo
        }


        //  ОБРАБОТЧИКИ КНОПОК ЛЕНТЫ И ФОРМЫ 
        private void ButtonClose_Click(object sender, EventArgs e) => Close();
        private void ButtonMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Normal
                ? FormWindowState.Maximized
                : FormWindowState.Normal;
        }
        private void ButtonMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void buttonBack_Click(object sender, EventArgs e) => currentPathModel.GoBack();
        private void buttonForward_Click(object sender, EventArgs e) => currentPathModel.GoForward();
        private void button3_Click(object sender, EventArgs e) => currentPathModel.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private void buttonMain_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 0;
            buttonVid.BackColor = Color.Black;
            buttonShare.BackColor = Color.Black;
            buttonMain.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void buttonShare_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 1;
            buttonShare.BackColor = Color.FromArgb(32, 32, 32);
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.Black;
        }

        private void buttonVid_Click(object sender, EventArgs e)
        {
            tabControlShare.SelectedIndex = 2;
            buttonShare.BackColor = Color.Black;
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void buttonSmallElements_Click(object sender, EventArgs e)
        {
            drawDetails = true;
            if (comboBoxAdressBar.Items.Count > 0)
                currentPathModel.Path = comboBoxAdressBar.Text;
        }

        private void buttonBigElements_Click(object sender, EventArgs e)
        {
            drawDetails = false;
            if (comboBoxAdressBar.Items.Count > 0)
                currentPathModel.Path = comboBoxAdressBar.Text;
        }

        private void ButtonCopy_Click(object sender, EventArgs e) => CopySelectedItem();
        private void ButtonCut_Click(object sender, EventArgs e) => CutSelectedItem();
        private void ButtonPaste_Click(object sender, EventArgs e) => PasteItem();

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            string? path = GetSelectedItemPath();
            if (path != null)
            {
                CommandInvoker.ExecuteCommand(new DeleteCommand(path));
                RefreshUiAfterCommand();
            }
        }

        private void ButtonRename_Click(object sender, EventArgs e)
        {
            string? oldPath = GetSelectedItemPath();
            if (oldPath == null) return;

            string newName = Interaction.InputBox("Новое имя:", "Переименование", Path.GetFileName(oldPath));
            if (string.IsNullOrEmpty(newName) || newName == Path.GetFileName(oldPath)) return;

            CommandInvoker.ExecuteCommand(new RenameCommand(oldPath, newName));
            RefreshUiAfterCommand();
        }

        //  ОБРАБОТЧИКИ КОНТЕКСТНОГО МЕНЮ 
        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e) => OpenFile();

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFolder();

        //  ПРОЧИЕ ОБРАБОТЧИКИ 
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not string tag) return;

            if (tag == "Этот компьютер")
            {

            }
            else if (tag == "Быстрый доступ")
            {

            }
            else if (Directory.Exists(tag))
            {
                currentPathModel.Path = tag;
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) => DrawTreeView.AddNodes(e);

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e) => OpenFile();

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            DrawStatusStrip.UpdateStatusStrip(statusStripMain, listViewFIles);
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
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

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonAdressBar_Click(object sender, EventArgs e) => comboBoxAdressBar.DroppedDown = true;
        private void buttonDropDown_Click(object sender, EventArgs e) => comboBoxLastWas.DroppedDown = true;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("В разработке");
                textBoxFind.Text = "";
            }
        }

        private void buttonFile_Click(object sender, EventArgs e) { } // пока пусто

        //  ПЕРЕОПРЕДЕЛЁННЫЕ МЕТОДЫ 
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