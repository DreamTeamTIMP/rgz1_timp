using rgz1_timp.ImportedDll;
using System.Drawing;

namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Управляет списком файлов (ListView): загрузка содержимого папки, режимы отображения, сортировка.
    /// </summary>
    public class DrawListView
    {
        private readonly ListView listView;
        private readonly DrawIcons icons;
        private View viewList;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="listView">Элемент ListView.</param>
        /// <param name="icons">Объект для получения иконок.</param>
        /// <param name="viewList">Начальный режим отображения.</param>
        public DrawListView(ListView listView, DrawIcons icons, View viewList)
        {
            this.listView = listView;
            this.icons = icons;
            this.viewList = viewList;
            DrawSystemListView();
        }

        /// <summary>
        /// Применяет к ListView тему «explorer» и назначает списки иконок.
        /// </summary>
        private void DrawSystemListView()
        {
            _ = Dll.SetWindowTheme(listView.Handle, "explorer", null);
            listView.SmallImageList = icons.SmallIcons;
            listView.LargeImageList = icons.LargeIcons;
        }

        /// <summary>
        /// Загружает содержимое каталога (папки и файлы) в ListView.
        /// Обрабатывает специальные объекты: «Этот компьютер» и «Быстрый доступ».
        /// </summary>
        /// <param name="path">Путь к папке или имя специального объекта.</param>
        public void LoadDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            if (path == "Этот компьютер")
            {
                LoadDriveList();
                return;
            }
            if (path == "Быстрый доступ")
            {
                LoadQuickAccessFolders();
                return;
            }
            if (!Directory.Exists(path)) return;

            listView.Items.Clear();
            listView.BeginUpdate();

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                // Добавляем подпапки.
                foreach (var dir in di.GetDirectories())
                {
                    // Пропускаем скрытые папки.
                    if ((dir.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    ListViewItem item = new ListViewItem(dir.Name);
                    item.ImageKey = icons.GetIconKey(dir.FullName, true);
                    item.SubItems.Add(dir.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add("Папка с файлами");
                    item.SubItems.Add("");
                    item.Tag = dir.FullName;
                    listView.Items.Add(item);
                }

                // Добавляем файлы.
                foreach (var file in di.GetFiles())
                {
                    // Пропускаем скрытые файлы.
                    if ((file.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    ListViewItem item = new ListViewItem(file.Name);
                    item.ImageKey = icons.GetIconKey(file.FullName, false);
                    item.SubItems.Add(file.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(file.Extension.ToUpper() + " File");
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.Tag = file.FullName;
                    listView.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Нет доступа к папке: {path}\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке папки {path}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            listView.EndUpdate();
        }

        /// <summary>
        /// Загружает список доступных логических дисков.
        /// </summary>
        public void LoadDriveList()
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                ListViewItem item = new ListViewItem(drive.Name);
                item.ImageKey = icons.GetIconKey(drive.RootDirectory.FullName, false);
                item.SubItems.Add("");                                   
                item.SubItems.Add("Локальный диск");
                item.SubItems.Add(FormatFileSize(drive.TotalFreeSpace)); 
                item.Tag = drive.RootDirectory.FullName;
                listView.Items.Add(item);
            }

            listView.EndUpdate();
        }

        /// <summary>
        /// Устанавливает режим отображения (плитка, список, таблица и т.д.).
        /// </summary>
        /// <param name="view">Режим отображения.</param>
        public void SetView(View view)
        {
            listView.View = view;
        }

        /// <summary>
        /// Сортирует элементы ListView по указанной колонке.
        /// </summary>
        /// <param name="columnIndex">Индекс колонки.</param>
        /// <param name="ascending">Направление сортировки.</param>
        public void SortByColumn(int columnIndex = 0, bool ascending = true)
        {
            listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, ascending);
        }

        /// <summary>
        /// Автоматически подбирает ширину колонок по содержимому.
        /// </summary>
        public void AutoResizeColumns()
        {
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Загружает стандартные папки быстрого доступа (Рабочий стол, Загрузки и т.д.).
        /// </summary>
        public void LoadQuickAccessFolders()
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            string[] quickFolders = {
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            };

            foreach (string folder in quickFolders)
            {
                if (Directory.Exists(folder))
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(folder));
                    item.ImageKey = icons.GetIconKey(folder, true);
                    item.SubItems.Add(new DirectoryInfo(folder).LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add("Папка с файлами");
                    item.SubItems.Add("");
                    item.Tag = folder;
                    listView.Items.Add(item);
                }
            }
            listView.EndUpdate();
        }

        /// <summary>
        /// Загружает содержимое папки с фильтрацией по имени (поиск).
        /// </summary>
        /// <param name="path">Путь к папке.</param>
        /// <param name="filter">Подстрока для поиска (без учёта регистра).</param>
        public void LoadDirectoryWithFilter(string path, string filter)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;

            listView.BeginUpdate();
            listView.Items.Clear();

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                // Отбираем папки, содержащие фильтр в имени.
                var dirs = di.GetDirectories()
                    .Where(d => (d.Attributes & FileAttributes.Hidden) == 0 &&
                                d.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
                foreach (var dir in dirs)
                {
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.ImageKey = icons.GetIconKey(dir.FullName, true);
                    item.SubItems.Add(dir.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add("Папка с файлами");
                    item.SubItems.Add("");
                    item.Tag = dir.FullName;
                    listView.Items.Add(item);
                }

                // Отбираем файлы, содержащие фильтр в имени.
                var files = di.GetFiles()
                    .Where(f => (f.Attributes & FileAttributes.Hidden) == 0 &&
                                f.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
                foreach (var file in files)
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.ImageKey = icons.GetIconKey(file.FullName, false);
                    item.SubItems.Add(file.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(file.Extension.ToUpper() + " File");
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.Tag = file.FullName;
                    listView.Items.Add(item);
                }

                if (listView.Items.Count == 0)
                {
                    MessageBox.Show($"Ничего не найдено по запросу \"{filter}\"", "Поиск",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SetView(viewList);
            listView.EndUpdate();
        }

        /// <summary>
        /// Форматирует размер файла в удобочитаемый вид (Б, КБ, МБ, ГБ).
        /// </summary>
        /// <param name="bytes">Размер в байтах.</param>
        /// <returns>Строка с размером и единицей измерения.</returns>
        private static string FormatFileSize(long bytes)
        {
            if (bytes >= 1024 * 1024 * 1024)
                return (bytes / 1024 / 1024 / 1024D).ToString("0.##") + " ГБ";
            if (bytes >= 1024 * 1024)
                return (bytes / 1024 / 1024D).ToString("0.##") + " МБ";
            if (bytes >= 1024)
                return (bytes / 1024D).ToString("0") + " КБ";
            return bytes.ToString() + " Б";
        }

        /// <summary>
        /// Вспомогательный компаратор для сортировки элементов ListView по тексту подэлемента.
        /// </summary>
        private class ListViewItemComparer : System.Collections.IComparer
        {
            private int column;
            private bool ascending;

            public ListViewItemComparer(int column, bool ascending)
            {
                this.column = column;
                this.ascending = ascending;
            }

            public int Compare(object x, object y)
            {
                string xText = ((ListViewItem)x).SubItems[column].Text;
                string yText = ((ListViewItem)y).SubItems[column].Text;
                int result = string.Compare(xText, yText);
                return ascending ? result : -result;
            }
        }
    }
}