using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    public class DrawAddressBar
    {
        private readonly ComboBox addressBar;
        
        public DrawAddressBar(ComboBox addressBar)
        {
            this.addressBar = addressBar;
        }

        public void UpdateAddressBar(string path)
        {
            _ = Dll.SetWindowTheme(addressBar.Handle, "explorer", null);
            addressBar.Text = path;


            addressBar.SelectionStart = addressBar.Text.Length;
            addressBar.SelectionLength = 0;

            if (!addressBar.Items.Contains(path))
                addressBar.Items.Insert(0, path);
        }
        public void ResetAddressBarSelection()
        {
            System.Windows.Forms.Timer timer = new()
            {
                Interval = 10
            };
            timer.Tick += (s, e) =>
            {
                // Устанавливаем курсор в конец строки и снимаем выделение
                addressBar.SelectionStart = addressBar.Text.Length;
                addressBar.SelectionLength = 0;

                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }
}