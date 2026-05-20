namespace rgz1_timp.DrawExplorer
{
    internal static class DrawDropDownList
    {
        internal static void DrawSystemDropDownList(ComboBox comboBox, TreeView treeView)
        {
            var nodes = GetAllNodes(treeView.Nodes);

            foreach (var node in nodes)
            {
                if (node.Text == "") continue;
                comboBox.Items.Add(node.Text);
            }
        }

        private static List<TreeNode> GetAllNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> allNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                allNodes.Add(node); // Добавляем текущий узел
                allNodes.AddRange(GetAllNodes(node.Nodes)); // Рекурсивно добавляем детей
            }
            return allNodes;
        }

    }
}
