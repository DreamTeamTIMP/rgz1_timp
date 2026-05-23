namespace rgz1_timp.DrawExplorer
{
    public class DrawStatusStrip
    {
        private readonly StatusStrip statusStrip;
        private readonly ListView listView;

        public DrawStatusStrip(StatusStrip statusStrip, ListView listView)
        {
            this.statusStrip = statusStrip;
            this.listView = listView;
        }

        public void UpdateStatusStrip()
        {
            statusStrip.Items.Clear();

            var countLabel = new ToolStripStatusLabel($"Элементов: {listView.Items.Count}  |")
            {
                ForeColor = Color.White
            };
            statusStrip.Items.Add(countLabel);

            if (listView.SelectedItems.Count == 1)
            {
                ListViewItem selected = listView.SelectedItems[0];
                string name = selected.Text;
                string type = selected.SubItems.Count > 2 ? selected.SubItems[2].Text : "";
                string sizeInfo = "";
                if (selected.SubItems.Count > 3 && !string.IsNullOrEmpty(selected.SubItems[3]?.Text))
                    sizeInfo = $" | Размер: {selected.SubItems[3].Text}";

                var selectedLabel = new ToolStripStatusLabel($"Выбран: {name} | Тип: {type}{sizeInfo}")
                {
                    ForeColor = Color.White,
                    Spring = true  
                };
                statusStrip.Items.Add(selectedLabel);
            }
            else if (listView.SelectedItems.Count > 1)
            {
                var selectedLabel = new ToolStripStatusLabel($"Выбрано: {listView.SelectedItems.Count} элементов")
                {
                    ForeColor = Color.White
                };
                statusStrip.Items.Add(selectedLabel);
            }
        }
    }
}