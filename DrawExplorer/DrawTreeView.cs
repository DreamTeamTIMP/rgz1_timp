using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawTreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public static void DrawSystemTreeView(TreeView treeView)
        {
            SetWindowTheme(treeView.Handle, "explorer", null);
            LoadDrives(treeView);
        }

        private static void LoadDrives(TreeView treeView)
        {
            treeView.Nodes.Clear();
            TreeNode quickAccess = new TreeNode("Быстрый доступ");
            quickAccess.Tag = "QUICK_ACCESS"; // Метка для отрисовки
            quickAccess.Nodes.Add(new TreeNode("Рабочий стол") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) });
            quickAccess.Nodes.Add(new TreeNode("Загрузки") { Tag = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") });
            quickAccess.Nodes.Add(new TreeNode("Документы") { Tag = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) });

            // 2. Этот компьютер
            TreeNode thisPC = new TreeNode("Этот компьютер");
            thisPC.Tag = "THIS_PC";
            
            // Добавляем диски в "Этот компьютер"
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                driveNode.Nodes.Add(""); // Пустышка для возможности раскрытия
                thisPC.Nodes.Add(driveNode);
            }
            
            treeView.Nodes.Add(quickAccess);
            treeView.Nodes.Add(thisPC);

            quickAccess.Expand();
            thisPC.Expand();
        }

        internal static void AddNodes(TreeViewCancelEventArgs e)
        {
            if (e.Node.Text == "") return;
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "")
            {
                e.Node.Nodes.Clear();
                string path = e.Node.Tag.ToString();
                try
                {
                    foreach (string dir in Directory.GetDirectories(path))
                    {

                        DirectoryInfo di = new(dir);

                        // Пропускаем скрытые папки для чистоты вида
                        if ((di.Attributes & FileAttributes.Hidden) != 0) continue;

                        //Icon.ExtractAssociatedIcon(dir);
                        TreeNode subNode = new(di.Name)
                        {
                            Tag = di.FullName
                        };
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
