using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    public class DrawListView
    {
        private readonly ListView listView;
        private readonly DrawIcons icons;
        private bool _drawDetails = true;

        public DrawListView(ListView listView, DrawIcons icons)
        {
            this.listView = listView;
            this.icons = icons;
            DrawSystemListView();
        }

        private void DrawSystemListView()
        {
            _ = Dll.SetWindowTheme(listView.Handle, "explorer", null);
            listView.SmallImageList = icons.SmallIcons;
            listView.LargeImageList = icons.LargeIcons;
        }

        public void LoadDirectory(string path, bool drawDetails)
        {
            _drawDetails = drawDetails;
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;

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

            listView.View = _drawDetails ? View.Details : View.LargeIcon;
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
    }
}