using rgz1_timp.DrawExplorer;
using System.Runtime.InteropServices;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawListView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public static void DrawSystemListView(ListView listView)
        {
            SetWindowTheme(listView.Handle, "explorer", null);
        }

        public static void LoadDirectory(ListView listView, string path)
        {
            // Включаем визуальный стиль Windows 10 (синее выделение и т.д.)
            SetWindowTheme(listView.Handle, "explorer", null);

            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;

            listView.Items.Clear();
            listView.BeginUpdate(); // Отключаем перерисовку для скорости

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                // 1. Сначала загружаем папки
                foreach (var dir in di.GetDirectories())
                {
                    // Пропускаем скрытые папки для чистоты вида
                    if ((dir.Attributes & FileAttributes.Hidden) != 0) continue;

                    ListViewItem item = new ListViewItem(dir.Name);
                   // item.ImageKey = "folder"; // Имя иконки в вашем ImageList
                    item.SubItems.Add(dir.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add("Папка с файлами");
                    item.SubItems.Add("");
                    item.Tag = dir.FullName; // Сохраняем полный путь

                    listView.Items.Add(item);
                }

                // 2. Затем загружаем файлы
                foreach (var file in di.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.Hidden) != 0) continue;

                    ListViewItem item = new ListViewItem(file.Name);

                    // Установка иконки по расширению (если вы добавили их в ImageList)
                    //item.ImageKey = "file";

                    item.SubItems.Add(file.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(file.Extension.ToUpper() + " File");
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.Tag = file.FullName;

                    listView.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ошибка доступа — можно добавить уведомление
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

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
