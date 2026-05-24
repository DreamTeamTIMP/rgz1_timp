using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Управляет деревом каталогов (TreeView): загрузка дисков, узлов, обновление, навигация.
    /// </summary>
    public class DrawTreeView
    {
        private readonly TreeView treeView;
        private readonly DrawIcons icons;

        /// <summary>
        /// Конструктор. Инициализирует дерево и загружает корневые узлы.
        /// </summary>
        /// <param name="treeView">Элемент TreeView.</param>
        /// <param name="icons">Объект для получения иконок.</param>
        public DrawTreeView(TreeView treeView, DrawIcons icons)
        {
            this.treeView = treeView;
            this.icons = icons;
            DrawSystemTreeView();
        }

        /// <summary>
        /// Применяет тему «explorer» к дереву и загружает диски.
        /// </summary>
        private void DrawSystemTreeView()
        {
            _ = Dll.SetWindowTheme(treeView.Handle, "explorer", null);
            treeView.ImageList = icons.SmallIcons;
            LoadDrives();
        }

        /// <summary>
        /// Обновляет конкретный узел дерева (перезагружает его подкаталоги).
        /// Используется после создания/удаления папок.
        /// </summary>
        /// <param name="path">Путь к папке, которую нужно обновить.</param>
        public void RefreshNodeByPath(string path)
        {
            if (string.IsNullOrEmpty(path) || path == "Этот компьютер" || path == "Быстрый доступ")
                return;

            TreeNode? node = FindNodeByPath(treeView.Nodes, path);
            if (node != null)
                RefreshNode(node, path);
            else
            {
                // Если узел не найден (например, папка переименована), обновляем родительский узел.
                string? parent = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(parent))
                {
                    TreeNode? parentNode = FindNodeByPath(treeView.Nodes, parent);
                    if (parentNode != null)
                        RefreshNode(parentNode, parent);
                }
            }
        }

        /// <summary>
        /// Рекурсивно ищет узел по полному пути, сохранённому в Tag.
        /// </summary>
        /// <param name="nodes">Коллекция узлов для поиска.</param>
        /// <param name="path">Искомый путь.</param>
        /// <returns>Найденный узел или null.</returns>
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

        /// <summary>
        /// Выделяет узел, соответствующий заданному пути, и делает его видимым.
        /// </summary>
        /// <param name="path">Путь к папке.</param>
        public void SelectNodeByPath(string path)
        {
            TreeNode? node = FindNodeByPath(treeView.Nodes, path);
            if (node != null && treeView.SelectedNode != node)
            {
                treeView.SelectedNode = node;
                node.EnsureVisible();
            }
        }

        /// <summary>
        /// Обновляет содержимое узла: очищает дочерние узлы и загружает актуальные подпапки.
        /// </summary>
        /// <param name="node">Узел для обновления.</param>
        /// <param name="path">Путь к папке.</param>
        private void RefreshNode(TreeNode node, string path)
        {
            bool wasExpanded = node.IsExpanded;
            node.Nodes.Clear();
            try
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    // Пропускаем скрытые папки.
                    if ((new DirectoryInfo(dir).Attributes & FileAttributes.Hidden) != 0)
                        continue;
                    TreeNode subNode = new(Path.GetFileName(dir)) { Tag = dir };
                    subNode.ImageKey = subNode.SelectedImageKey = "folder";
                    subNode.Nodes.Add("");   // Добавляем фиктивный узел для появления значка «+».
                    node.Nodes.Add(subNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Нет доступа – просто пропускаем.
            }
            if (wasExpanded) node.Expand();
        }

        /// <summary>
        /// Загружает корневые узлы: «Быстрый доступ» и «Этот компьютер» со списком дисков.
        /// </summary>
        private void LoadDrives()
        {
            treeView.Nodes.Clear();

            // Узел «Быстрый доступ» и его стандартные папки.
            TreeNode quickAccess = new TreeNode("Быстрый доступ");
            quickAccess.Tag = "Быстрый доступ";
            quickAccess.Nodes.Add(new TreeNode("Рабочий стол") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) });
            quickAccess.Nodes.Add(new TreeNode("Загрузки") { Tag = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") });
            quickAccess.Nodes.Add(new TreeNode("Документы") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) });
            quickAccess.Nodes.Add(new TreeNode("Фото") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) });
            quickAccess.Nodes.Add(new TreeNode("Музыка") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) });
            quickAccess.Nodes.Add(new TreeNode("Видео") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) });

            // Узел «Этот компьютер» со списком дисков.
            TreeNode thisPC = new TreeNode("Этот компьютер");
            thisPC.Tag = "Этот компьютер";
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                driveNode.ImageKey = driveNode.SelectedImageKey = "drive";
                driveNode.Nodes.Add("");   // Фиктивный узел для возможности раскрытия.
                thisPC.Nodes.Add(driveNode);
            }

            treeView.Nodes.Add(quickAccess);
            treeView.Nodes.Add(thisPC);
            quickAccess.ImageKey = quickAccess.SelectedImageKey = "quick";
            thisPC.ImageKey = thisPC.SelectedImageKey = "pc";
        }

        /// <summary>
        /// Обработчик события BeforeExpand. Загружает реальные подпапки при раскрытии узла.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
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
                catch (UnauthorizedAccessException)
                {
                    // Нет доступа – не добавляем дочерние узлы.
                }
            }
        }

        /// <summary>
        /// Обновляет список дисков в узле «Этот компьютер» (например, после подключения USB-накопителя).
        /// </summary>
        public void RefreshDrives()
        {
            TreeNode? thisPC = null;
            foreach (TreeNode node in treeView.Nodes)
                if (node.Text == "Этот компьютер")
                    thisPC = node;
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