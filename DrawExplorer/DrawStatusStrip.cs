namespace rgz1_timp.DrawExplorer
{
    public class DrawStatusStrip
    {
        private readonly StatusStrip _statusStrip;
        private readonly ListView _listView;

        public DrawStatusStrip(StatusStrip statusStrip, ListView listView)
        {
            _statusStrip = statusStrip;
            _listView = listView;
        }

        public void UpdateStatusStrip()
        {
            _statusStrip.Items.Clear();

            var countLabel = new ToolStripStatusLabel($"Элементов: {_listView.Items.Count}  |")
            {
                ForeColor = Color.White
            };
            _statusStrip.Items.Add(countLabel);

            if (_listView.SelectedItems.Count == 1)
            {
                ListViewItem selected = _listView.SelectedItems[0];
                string name = selected.Text;
                string sizeInfo = "";
                if (selected.SubItems.Count > 3 && !string.IsNullOrEmpty(selected.SubItems[3]?.Text))
                    sizeInfo = $" | Размер: {selected.SubItems[3].Text}";

                var selectedLabel = new ToolStripStatusLabel($"Выбран: {name}{sizeInfo}")
                {
                    ForeColor = Color.White
                };
                _statusStrip.Items.Add(selectedLabel);
            }
            else if (_listView.SelectedItems.Count > 1)
            {
                var selectedLabel = new ToolStripStatusLabel($"Выбрано: {_listView.SelectedItems.Count} элементов")
                {
                    ForeColor = Color.White
                };
                _statusStrip.Items.Add(selectedLabel);
            }
        }
    }
}