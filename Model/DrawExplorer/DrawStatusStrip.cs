namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Управляет строкой состояния (StatusStrip): отображает количество элементов,
    /// информацию о выделенном объекте (имя, тип, размер).
    /// </summary>
    public class DrawStatusStrip
    {
        private readonly StatusStrip statusStrip;
        private readonly ListView listView;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="statusStrip">Строка состояния.</param>
        /// <param name="listView">Список файлов (для получения данных).</param>
        public DrawStatusStrip(StatusStrip statusStrip, ListView listView)
        {
            this.statusStrip = statusStrip;
            this.listView = listView;
        }

        /// <summary>
        /// Обновляет содержимое строки состояния в соответствии с текущим списком и выделением.
        /// </summary>
        public void UpdateStatusStrip()
        {
            statusStrip.Items.Clear();

            // Отображаем общее количество элементов в папке.
            var countLabel = new ToolStripStatusLabel($"Элементов: {listView.Items.Count}  |")
            {
                ForeColor = Color.White
            };
            statusStrip.Items.Add(countLabel);

            // Если выделен ровно один объект, показываем его имя, тип и размер.
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
                    // Занимает оставшееся пространство.
                    Spring = true   
                };
                statusStrip.Items.Add(selectedLabel);
            }
            // Если выделено несколько объектов, показываем их количество.
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