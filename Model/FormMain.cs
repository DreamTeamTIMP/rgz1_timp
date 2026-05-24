using Microsoft.VisualBasic;
using rgz1_timp.Command;
using rgz1_timp.DrawExplorer;
using rgz1_timp.ImportedDll;
using rgz1_timp.Services;
using rgz1_timp.Services.rgz1_timp.Services;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace rgz1_timp
{
    /// <summary>
    /// Главная форма файлового менеджера.
    /// </summary>
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

        // Текущий режим отображения ListView.
        private View viewList = View.Details;
        // Текущий поисковый запрос.
        private string currentSearchQuery = string.Empty; 

        /// <summary>
        /// Конструктор главной формы. Инициализирует компоненты, сервисы и обработчики событий.
        /// </summary>
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

            // Подписка на события модели навигации.
            currentPathModel.PathChanged += OnPathChanged;
            currentPathModel.NavigationStateChanged += UpdateNavigationButtons;

            // Дополнительная настройка событий.
            listViewFiles.KeyDown += ListViewFiles_KeyDown;
            this.Shown += (s, e) => drawAddressBar.ResetAddressBarSelection();
            currentPathModel.Path = "Этот компьютер";
        }

        /// <summary>
        /// Обработчик изменения текущего пути. Обновляет интерфейс, сбрасывает поиск.
        /// </summary>
        /// <param name="path">Новый путь.</param>
        private void OnPathChanged(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;

            // Сбрасываем строку поиска и фильтр.
            textBoxFind.Text = string.Empty;
            currentSearchQuery = string.Empty;
            drawAddressBar.UpdateAddressBar(path);
            drawStatusStrip.UpdateStatusStrip();
            drawTreeView.RefreshNodeByPath(path);
            drawListView.LoadDirectory(path);
            drawTreeView.SelectNodeByPath(path);
        }

        /// <summary>
        /// Обновляет доступность кнопок навигации "Назад" и "Вперёд".
        /// </summary>
        private void UpdateNavigationButtons()
        {
            buttonBack.Enabled = currentPathModel.CanGoBack;
            buttonForward.Enabled = currentPathModel.CanGoForward;
        }

        #region Вспомогательные методы UI

        /// <summary>
        /// Возвращает полный путь выделенного в списке объекта (файла или папки).
        /// </summary>
        /// <returns>Путь или null, если ничего не выделено.</returns>
        private string? GetSelectedItemPath()
        {
            return listViewFiles.SelectedItems.Count > 0
                ? listViewFiles.SelectedItems[0].Tag?.ToString()
                : null;
        }

        /// <summary>
        /// Открывает выделенный объект: папку – переход, файл – запуск ассоциированной программой.
        /// </summary>
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

        /// <summary>
        /// Обновляет содержимое текущего каталога (принудительная перезагрузка).
        /// </summary>
        private void RefreshUi()
        {
            currentPathModel.Refresh();
        }

        #endregion

        #region Публичные методы для ленты (Ribbon)
        
        /// <summary>
        /// Создаёт новую папку в текущем каталоге.
        /// </summary>
        public void CreateNewFolder()
        {
            if (fileService.CreateNewFolder())
                RefreshUi();
        }

        /// <summary>
        /// Создаёт новый текстовый файл в текущем каталоге.
        /// </summary>
        public void CreateNewFile()
        {
            if (fileService.CreateNewFile())
                RefreshUi();
        }

        /// <summary>
        /// Копирует выделенный объект во внутренний буфер (без перемещения).
        /// </summary>
        public void CopySelectedItem()
        {
            fileService.CopyItem(GetSelectedItemPath());
        }

        /// <summary>
        /// Вырезает выделенный объект (помещает в буфер с флагом перемещения).
        /// </summary>
        public void CutSelectedItem()
        {
            fileService.CutItem(GetSelectedItemPath());
        }

        /// <summary>
        /// Вставляет объект из буфера в текущую папку.
        /// </summary>
        public void PasteItem()
        {
            if (fileService.PasteItem())
                RefreshUi();
        }

        /// <summary>
        /// Удаляет выделенный объект (отправляет в Корзину).
        /// </summary>
        public void DeleteSelectedItem()
        {
            if (fileService.DeleteItem(GetSelectedItemPath()))
                RefreshUi();
        }

        /// <summary>
        /// Переименовывает выделенный объект. Запрашивает новое имя через диалог.
        /// </summary>
        public void RenameSelectedItem()
        {
            string? oldPath = GetSelectedItemPath();
            if (oldPath == null) return;

            string newName = Interaction.InputBox("Новое имя:", "Переименование", Path.GetFileName(oldPath));
            if (fileService.RenameItem(oldPath, newName))
                RefreshUi();
        }

        /// <summary>
        /// Открывает диалог выбора папки и перемещает выделенный объект в неё.
        /// </summary>
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

        /// <summary>
        /// Открывает диалог выбора папки и копирует выделенный объект в неё.
        /// </summary>
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

        /// <summary>
        /// Копирует полный путь выделенного объекта в системный буфер обмена.
        /// </summary>
        public void CopyPath()
        {
            fileService.CopyPathToClipboard(GetSelectedItemPath());
        }

        /// <summary>
        /// Сортирует файлы в текущем каталоге по имени (по возрастанию).
        /// </summary>
        public void SortFiles()
        {
            drawListView.SortByColumn(0, true);
        }

        /// <summary>
        /// Автоматически подбирает ширину колонок в режиме Details.
        /// </summary>
        public void AutoResizeColumns()
        {
            drawListView.AutoResizeColumns();
        }

        #endregion

        #region Обработчики кнопок формы

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

        /// <summary>
        /// Переключает на вкладку "Главная" ленты инструментов.
        /// </summary>
        private void ButtonMain_Click(object sender, EventArgs e)
        {
            panelHome.Visible = true;
            panelView.Visible = false;
            buttonVid.BackColor = Color.Black;
            buttonMain.BackColor = Color.FromArgb(32, 32, 32);
        }

        /// <summary>
        /// Переключает на вкладку "Вид" ленты инструментов.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            panelHome.Visible = false;
            panelView.Visible = true;
            buttonMain.BackColor = Color.Black;
            buttonVid.BackColor = Color.FromArgb(32, 32, 32);
        }

        #endregion

        #region Переключение режимов отображения

        private void ButtonSmallElements_Click(object sender, EventArgs e) => SetDetails();
        private void ButtonBigElements_Click(object sender, EventArgs e) => SetLargeIcons();

        /// <summary>Устанавливает режим крупных значков (LargeIcon).</summary>
        public void SetLargeIcons() { drawListView.SetView(View.LargeIcon); RefreshUi(); }
        /// <summary>Устанавливает режим мелких значков (SmallIcon).</summary>
        public void SetSmallIcons() { drawListView.SetView(View.SmallIcon); RefreshUi(); }
        /// <summary>Устанавливает режим списка (List).</summary>
        public void SetList() { drawListView.SetView(View.List); RefreshUi(); }
        /// <summary>Устанавливает табличный режим с колонками (Details).</summary>
        public void SetDetails() { drawListView.SetView(View.Details); RefreshUi(); }
        /// <summary>Устанавливает режим плитки (Tile).</summary>
        public void SetTiles() { drawListView.SetView(View.Tile); RefreshUi(); }

        #endregion

        #region Контекстное меню

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e) => OpenFile();
        private void CreateFolderToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFolder();

        #endregion

        #region Обработчики событий элементов управления

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) => drawTreeView.AddNodes(e);

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e) => OpenFile();

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            drawStatusStrip.UpdateStatusStrip();
        }

        /// <summary>
        /// Обрабатывает нажатие правой кнопки мыши в списке файлов.
        /// Показывает контекстное меню: для элемента – меню действий, для пустого места – меню создания.
        /// </summary>
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

        /// <summary>
        /// Обрабатывает нажатие клавиш в списке файлов (например, Enter для открытия).
        /// </summary>
        private void ListViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenFile();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Обрабатывает ввод пути в адресной строке и переход по Enter.
        /// </summary>
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
        /// <summary>
        /// Обработчик клика по метке поиска (очистка поиска при нажатии на "X").
        /// </summary>
        private void labelFind_Click(object sender, EventArgs e)
        {
            if (labelFind.Text == "X")
            {
                textBoxFind.Text = "";
                labelFind.Text = "";
            }
            PerformSearch();
        }

        private void labelUpdateDrivers_Click(object sender, EventArgs e) => currentPathModel.Refresh();

        /// <summary>
        /// Обработчик клика по узлу дерева папок: переходит в выбранный каталог.
        /// </summary>
        private void treeViewFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag is not string tag) return;

            if (Directory.Exists(tag) || tag == "Этот компьютер" || tag == "Быстрый доступ")
            {
                if (currentPathModel.Path == tag)
                    currentPathModel.Refresh();
                else
                    currentPathModel.Path = tag;
            }
            listViewFiles.Focus();
        }

        private void TxtToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFile();

        /// <summary>
        /// Обработчик выбора элемента в выпадающем списке быстрых переходов (comboBoxLastWas).
        /// </summary>
        private void comboBoxLastWas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLastWas.SelectedItem is TreeNode pathNode)
            {
                currentPathModel.Path = (string)pathNode.Tag;
            }
        }

        private void ButtonAddressBar_Click(object sender, EventArgs e) => comboBoxAddressBar.DroppedDown = true;
        private void ButtonDropDown_Click(object sender, EventArgs e) => comboBoxLastWas.DroppedDown = true;
        private void ButtonCopy_Click(object sender, EventArgs e) => CopySelectedItem();
        private void ButtonRename_Click(object sender, EventArgs e) => RenameSelectedItem();
        private void ButtonDelete_Click(object sender, EventArgs e) => DeleteSelectedItem();
        private void ButtonPaste_Click(object sender, EventArgs e) => PasteItem();

        #endregion

        #region Поиск и фильтрация

        /// <summary>
        /// Обрабатывает нажатие Enter в поле поиска и запускает фильтрацию.
        /// </summary>
        private void TextBoxFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Выполняет поиск (фильтрацию) файлов и папок в текущем каталоге по введённой подстроке.
        /// </summary>
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

        /// <summary>
        /// Обработчик кнопки "Файл" – открывает новый экземпляр приложения.
        /// </summary>
        private void ButtonFile_Click(object sender, EventArgs e)
        {
            string exePath = Application.ExecutablePath;

            try
            {
                Process.Start(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть новое окно: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Перетаскивание окна с помощью мыши.
        /// </summary>
        private void PanelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Освобождаем захват мыши и посылаем сообщение о перемещении заголовка
                Dll.ReleaseCapture();                     
                Dll.SendMessage(this.Handle, 0xA1, 2, 0);
            }
        }

        #endregion

        #region Переопределённые методы (WndProc, ProcessCmdKey)

        /// <summary>
        /// Переопределяет оконную процедуру для поддержки изменения размера без рамки и отслеживания подключения устройств.
        /// </summary>
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

            // Обновляем список дисков при подключении/отключении устройств.
            if (m.Msg == WM_DEVICECHANGE)
            {
                drawTreeView.RefreshDrives();
                if (comboBoxAddressBar.Text == "Этот компьютер")
                    currentPathModel.Path = comboBoxAddressBar.Text;
            }

            // Обработка изменения размера окна.
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

                // Перемещение окна за заголовок, если клик не на кнопках управления.
                if (pos.Y <= panelHeader.Height && pos.Y > 0)
                {
                    if (!(pos.X > buttonMinimize.Left && pos.X < buttonClose.Right))
                    {
                        m.Result = (IntPtr)HTCAPTION;
                        return;
                    }
                }
            }
            drawAddressBar.ResetAddressBarSelection();
        }

        /// <summary>
        /// Обрабатывает горячие клавиши на уровне формы (Ctrl+Z, Ctrl+Y, F1, F2, Delete, F5, Ctrl+C, Ctrl+X, Ctrl+V).
        /// </summary>
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

            if (keyData == Keys.F1)
            {
                using (var help = new HelpForm()) help.ShowDialog(this);
                return true;
            }

            if (keyData == Keys.Delete)
            {
                DeleteSelectedItem();
                return true;
            }

            if (keyData == Keys.F2)
            {
                RenameSelectedItem();
                return true;
            }

            if (keyData == Keys.F5)
            {
                RefreshUi();
                return true;
            }

            if (keyData == (Keys.Control | Keys.C))
            {
                CopySelectedItem();
                return true;
            }

            if (keyData == (Keys.Control | Keys.X))
            {
                CutSelectedItem();
                return true;
            }

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

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            CommandInvoker.Redo();
            RefreshUi();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            CommandInvoker.Undo();
            RefreshUi();
        }

        #endregion
    }
}