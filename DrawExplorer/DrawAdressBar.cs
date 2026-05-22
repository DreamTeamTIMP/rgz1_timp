using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    public class DrawAdressBar
    {
        private readonly ComboBox _addressBar;

        public DrawAdressBar(ComboBox addressBar)
        {
            _addressBar = addressBar;
        }

        public void UpdateAddressBar(string path)
        {
            _ = Dll.SetWindowTheme(_addressBar.Handle, "explorer", null);
            _addressBar.Text = path;

            if (!_addressBar.Items.Contains(path))
                _addressBar.Items.Insert(0, path);
        }
    }
}