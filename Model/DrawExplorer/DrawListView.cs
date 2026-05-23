using rgz1_timp.ImportedDll;
using System.Drawing;

namespace rgz1_timp.DrawExplorer
{
    public class DrawListView
    {
        private readonly ListView listView;
        private readonly DrawIcons icons;
        private View viewList;
        
        public DrawListView(ListView listView, DrawIcons icons, View viewList)
        {
            this.listView = listView;
            this.icons = icons;
            this.viewList = viewList;
            DrawSystemListView();
        }

        private void DrawSystemListView()
        {
            _ = Dll.SetWindowTheme(listView.Handle, "explorer", null);
            listView.SmallImageList = icons.SmallIcons;
            listView.LargeImageList = icons.LargeIcons;
        }

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

                foreach (var dir in di.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) != 0) continue;
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.ImageKey = icons.GetIconKey(dir.FullName, true);
                    item.SubItems.Add(dir.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add("Папка с файлами");
                    item.SubItems.Add("");
                    item.Tag = dir.FullName;
                    listView.Items.Add(item);
                }

                foreach (var file in di.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.Hidden) != 0) continue;
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

        public void LoadDriveList()
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                ListViewItem item = new ListViewItem(drive.Name);
                item.ImageKey = icons.GetIconKey(drive.RootDirectory.FullName, false);
                item.SubItems.Add(""); // дата изменения
                item.SubItems.Add("Локальный диск");
                item.SubItems.Add(FormatFileSize(drive.TotalFreeSpace)); // свободное место
                item.Tag = drive.RootDirectory.FullName;
                listView.Items.Add(item);
            }

            listView.EndUpdate();
        }

        public void SetView(View view)
        {
            listView.View = view;
        }
        public void SortByColumn(int columnIndex = 0, bool ascending = true)
        {
            listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, ascending);
        }
        public void AutoResizeColumns()
        {
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

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
        public void LoadDirectoryWithFilter(string path, string filter)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;
            
            listView.BeginUpdate();
            listView.Items.Clear();

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                // Папки
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

                // Файлы
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