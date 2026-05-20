using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawAdressBar
    {
        
        public static void UpdateAddressBar(ComboBox addressBar, string path)
        {
            _ = Dll.SetWindowTheme(addressBar.Handle, "explorer", null);
            addressBar.Text = path;

            // Добавляем путь в историю выпадающего списка, если его там еще нет
            if (!addressBar.Items.Contains(path))
            {
                addressBar.Items.Insert(0, path);
            }
        }
    }
}
