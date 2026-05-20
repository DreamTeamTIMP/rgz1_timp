using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{

    internal static class DrawListView
    {
        public static bool drawType = false; // false - маленькие, true - большие
        private static ListView list;
        public static void DrawSystemListView(ListView listView)
        {
            list = listView;
            _ = Dll.SetWindowTheme(listView.Handle, "explorer", null);
            // Привязываем оба списка (для разных режимов отображения)
            listView.SmallImageList = DrawIcons.SmallIcons;
            listView.LargeImageList = DrawIcons.LargeIcons;
        }

        public static void LoadDirectory(ListView listView, string path, bool drawDetails)
        {
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
                    // Получаем ключ иконки (для папки)
                    item.ImageKey = DrawIcons.GetIconKey(dir.FullName, true);

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
                    // Получаем ключ иконки (по расширению файла)
                    item.ImageKey = DrawIcons.GetIconKey(file.FullName, false);

                    item.SubItems.Add(file.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(file.Extension.ToUpper() + " File");
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.Tag = file.FullName;
                    listView.Items.Add(item);

                }
            }
            catch { /* Обработка ошибок */ }

            if (drawDetails)
                listView.View = View.Details;
            else
                listView.View = View.LargeIcon;
            listView.EndUpdate();
        }
        

        /// <summary>
        /// Красивое форматирование размера файла как в Windows
        /// </summary>
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
