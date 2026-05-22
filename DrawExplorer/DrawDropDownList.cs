namespace rgz1_timp.DrawExplorer
{
    public class DrawDropDownList
    {
        private readonly ComboBox _comboBox;
        private readonly TreeView _treeView;

        public DrawDropDownList(ComboBox comboBox, TreeView treeView)
        {
            _comboBox = comboBox;
            _treeView = treeView;
            DrawSystemDropDownList();
        }

        private void DrawSystemDropDownList()
        {
            var nodes = GetAllNodes(_treeView.Nodes);
            foreach (var node in nodes)
            {
                if (node.Text == "") continue;
                _comboBox.Items.Add(node.Text);
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