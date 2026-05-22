namespace rgz1_timp.DrawExplorer
{
    public class DrawDropDownList
    {
        private readonly ComboBox comboBox;
        private readonly TreeView treeView;

        public DrawDropDownList(ComboBox comboBox, TreeView treeView)
        {
            this.comboBox = comboBox;
            this.treeView = treeView;
            DrawSystemDropDownList();
        }

        private void DrawSystemDropDownList()
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
                allNodes.Add(node);
                allNodes.AddRange(GetAllNodes(node.Nodes));
            }
            return allNodes;
        }
    }
}