using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    public class DrawTreeView
    {
        private readonly TreeView _treeView;
        private readonly DrawIcons _icons;

        public DrawTreeView(TreeView treeView, DrawIcons icons)
        {
            _treeView = treeView;
            _icons = icons;
            DrawSystemTreeView();
        }

        private void DrawSystemTreeView()
        {
            _ = Dll.SetWindowTheme(_treeView.Handle, "explorer", null);
            _treeView.ImageList = _icons.SmallIcons;
            LoadDrives();
        }
        public void RefreshNodeByPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            TreeNode? node = FindNodeByPath(_treeView.Nodes, path);
            if (node != null)
                RefreshNode(node, path);
            else
            {
                // Попробуем обновить родительский узел (на случай, если папка переименована/перемещена)
                string? parent = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(parent))
                {
                    TreeNode? parentNode = FindNodeByPath(_treeView.Nodes, parent);
                    if (parentNode != null)
                        RefreshNode(parentNode, parent);
                }
            }
        }

        private TreeNode? FindNodeByPath(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is string tag && string.Equals(tag, path, StringComparison.OrdinalIgnoreCase))
                    return node;
                TreeNode? found = FindNodeByPath(node.Nodes, path);
                if (found != null) return found;
            }
            return null;
        }

        private void RefreshNode(TreeNode node, string path)
        {
            bool wasExpanded = node.IsExpanded;
            node.Nodes.Clear();
            try
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    if ((new DirectoryInfo(dir).Attributes & FileAttributes.Hidden) != 0) continue;
                    TreeNode subNode = new(Path.GetFileName(dir)) { Tag = dir };
                    subNode.ImageKey = subNode.SelectedImageKey = "folder";
                    subNode.Nodes.Add("");
                    node.Nodes.Add(subNode);
                }
            }
            catch (UnauthorizedAccessException) { }
            if (wasExpanded) node.Expand();
        }

        private void LoadDrives()
        {
            _treeView.Nodes.Clear();
            TreeNode quickAccess = new TreeNode("Быстрый доступ");
            quickAccess.Tag = "Быстрый доступ";
            quickAccess.Nodes.Add(new TreeNode("Рабочий стол") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) });
            quickAccess.Nodes.Add(new TreeNode("Загрузки") { Tag = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") });
            quickAccess.Nodes.Add(new TreeNode("Документы") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) });

            TreeNode thisPC = new TreeNode("Этот компьютер");
            thisPC.Tag = "Этот компьютер";
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                driveNode.ImageKey = driveNode.SelectedImageKey = "drive";
                driveNode.Nodes.Add("");
                thisPC.Nodes.Add(driveNode);
            }

            _treeView.Nodes.Add(quickAccess);
            _treeView.Nodes.Add(thisPC);
            quickAccess.ImageKey = quickAccess.SelectedImageKey = "quick";
            thisPC.ImageKey = thisPC.SelectedImageKey = "pc";
        }

        public void AddNodes(TreeViewCancelEventArgs e)
        {
            if (e.Node?.Tag is not string path) return;
            if (e.Node.Text == "") return;
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "")
            {
                e.Node.Nodes.Clear();
                try
                {
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        DirectoryInfo di = new(dir);
                        if ((di.Attributes & FileAttributes.Hidden) != 0) continue;

                        TreeNode subNode = new(di.Name) { Tag = di.FullName };
                        subNode.ImageKey = subNode.SelectedImageKey = "folder";
                        subNode.Nodes.Add("");
                        e.Node.Nodes.Add(subNode);
                    }
                }
                catch (UnauthorizedAccessException) { }
            }
        }

        public void RefreshDrives()
        {
            TreeNode? thisPC = null;
            foreach (TreeNode node in _treeView.Nodes)
                if (node.Text == "Этот компьютер") thisPC = node;
            if (thisPC == null) return;

            bool wasExpanded = thisPC.IsExpanded;
            thisPC.Nodes.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                driveNode.ImageKey = driveNode.SelectedImageKey = "drive";
                driveNode.Nodes.Add("");
                thisPC.Nodes.Add(driveNode);
            }

            if (wasExpanded) thisPC.Expand();
        }
    }
}