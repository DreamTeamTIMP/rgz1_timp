
namespace rgz1_timp.DrawExplorer
{
    internal class DrawStatusStrip
    {
        internal static void UpdateStatusStrip(StatusStrip statusStrip, ListView listView1)
        {
            statusStrip.Items.Clear();

            if (listView1 == null) return;

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Элементов: " + listView1.Items.Count.ToString() + " |") { ForeColor = Color.White} ;
            statusStrip.Items.Add(statusLabel);
            if (listView1.SelectedItems.Count > 0)
            {
                ToolStripStatusLabel selectedItems = listView1.SelectedItems.Count == 1 ? new("Выбран 1 элемент |") : new($"Выбрано {listView1.SelectedItems.Count} элем. |");
                statusStrip.Items.Add(selectedItems);
            }
            
        }
    }
}