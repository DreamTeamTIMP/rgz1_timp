using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawTreeView
    {
        
        public static void DrawSystemTreeView(TreeView treeView)
        {
            _ = Dll.SetWindowTheme(treeView.Handle, "explorer", null);
            treeView.ImageList = DrawIcons.SmallIcons;
            LoadDrives(treeView);
        }
        public static void DrawMyComputer(TreeView treeView)
        {

        }
        private static void LoadDrives(TreeView treeView)
        {
            treeView.Nodes.Clear();
            TreeNode quickAccess = new TreeNode("Быстрый доступ");
            quickAccess.Tag = "Быстрый доступ"; // Метка для отрисовки
            quickAccess.Nodes.Add(new TreeNode("Рабочий стол") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) });
            quickAccess.Nodes.Add(new TreeNode("Загрузки") { Tag = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") });
            quickAccess.Nodes.Add(new TreeNode("Документы") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) });
            // 2. Этот компьютер
            TreeNode thisPC = new TreeNode("Этот компьютер") ;
            thisPC.Tag = "Этот компьютер";
            // Добавляем диски в "Этот компьютер"
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                driveNode.ImageKey = driveNode.SelectedImageKey = "drive";
                driveNode.Nodes.Add(""); // Пустышка для возможности раскрытия
                thisPC.Nodes.Add(driveNode);
            }

            treeView.Nodes.Add(quickAccess);
            treeView.Nodes.Add(thisPC);
            quickAccess.ImageKey = quickAccess.SelectedImageKey = "quick";
            thisPC.ImageKey = thisPC.SelectedImageKey = "pc";
            
        }

        internal static void AddNodes(TreeViewCancelEventArgs e)
        {
            if (e.Node is null) return;
            if (e.Node.Text == "") return;         
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "")
            {
                e.Node.Nodes.Clear();
                string path = e.Node.Tag.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(path)) return;
                try
                {
                    foreach (string dir in Directory.GetDirectories(path))
                    {

                        DirectoryInfo di = new(dir);

                        // Пропускаем скрытые папки для чистоты вида
                        if ((di.Attributes & FileAttributes.Hidden) != 0) continue;

                        TreeNode subNode = new(di.Name)
                        {
                            Tag = di.FullName
                        };
                        subNode.ImageKey = subNode.SelectedImageKey = "folder";

                        subNode.Nodes.Add("");
                        e.Node.Nodes.Add(subNode);
                    }
                }
                catch (UnauthorizedAccessException) 
                {
                    // Нет доступа, не трогаем
                }
            }
        }
    }
}
