namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Заполняет выпадающий список (ComboBox) всеми узлами дерева каталогов.
    /// </summary>
    public class DrawDropDownList
    {
        private readonly ComboBox comboBox;
        private readonly TreeView treeView;

        /// <summary>
        /// Конструктор. Сразу заполняет список узлами дерева.
        /// </summary>
        /// <param name="comboBox">Выпадающий список.</param>
        /// <param name="treeView">Дерево каталогов.</param>
        public DrawDropDownList(ComboBox comboBox, TreeView treeView)
        {
            this.comboBox = comboBox;
            this.treeView = treeView;
            DrawSystemDropDownList();
        }

        /// <summary>
        /// Рекурсивно обходит все узлы дерева и добавляет их в выпадающий список.
        /// </summary>
        private void DrawSystemDropDownList()
        {
            var nodes = GetAllNodes(treeView.Nodes);
            foreach (var node in nodes)
            {
                // Пропускаем пустые узлы-заглушки.
                if (node.Text == "")
                    continue;
                comboBox.Items.Add(node);
            }
            comboBox.DisplayMember = "Text";
        }

        /// <summary>
        /// Рекурсивно собирает все узлы из заданной коллекции.
        /// </summary>
        /// <param name="nodes">Коллекция узлов.</param>
        /// <returns>Список всех узлов.</returns>
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