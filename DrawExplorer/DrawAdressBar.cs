using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    public class DrawAdressBar
    {
        private readonly ComboBox addressBar;

        public DrawAdressBar(ComboBox addressBar)
        {
            this.addressBar = addressBar;
        }

        public void UpdateAddressBar(string path)
        {
            _ = Dll.SetWindowTheme(addressBar.Handle, "explorer", null);
            addressBar.Text = path;

            if (!addressBar.Items.Contains(path))
                addressBar.Items.Insert(0, path);
        }
    }
}