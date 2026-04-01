using rgz1_timp.DrawExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace rgz1_timp
{
    public partial class FormMain : Form
    {
        private bool dragging = false;
        private Point startPoint = Point.Empty;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public FormMain()
        {
            InitializeComponent();
            DrawTreeView.DrawSystemTreeView(treeView1);
            DrawListView.DrawSystemListView(listView1);
            DrawRibbon.SetupRibbon(tabControl1);
        }

        private void buttonClose_Click(object sender, EventArgs e) => Close();

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void buttonMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void splitContainer3_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void splitContainer3_Panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void splitContainer3_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                string selectedPath = e.Node.Tag.ToString();
                // Вызываем наш метод для ListView
                DrawListView.LoadDirectory(listView1, selectedPath);
                DrawAdressBar.UpdateAddressBar(comboBox1, selectedPath);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            DrawTreeView.AddNodes(e);
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Color backColor = Color.Transparent;
            Color textColor = Color.White;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                // Цвет выделения (светло-синий как в Explorer)
                backColor = Color.FromArgb(98, 98, 98);
            }
            else if ((e.State & TreeNodeStates.Hot) != 0)
            {
                // Цвет при наведении
                backColor = Color.FromArgb(119, 119, 119);
            }

            if (backColor != Color.Transparent)
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    // Рисуем фон на всю ширину строки
                    e.Graphics.FillRectangle(brush, new Rectangle(0, e.Bounds.Y, this.Width, e.Bounds.Height));
                }
            }

            // 2. Рисуем иконку (если привязан ImageList)
            if (treeView1.ImageList != null && e.Node.ImageIndex >= 0)
            {
                Image img = treeView1.ImageList.Images[e.Node.ImageIndex];
                // Смещение иконки: e.Bounds.X уже учитывает вложенность
                e.Graphics.DrawImage(img, new Rectangle(e.Bounds.X - 20, e.Bounds.Y + 4, 16, 16));
            }

            // 3. Рисуем текст узла
            TextRenderer.DrawText(e.Graphics, e.Node.Text, this.Font,
                new Point(e.Bounds.X, e.Bounds.Y + 3), textColor);
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {


        }
        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // "Отпускаем" мышь от контрола
                ReleaseCapture();
                // Посылаем сообщение форме, что нажата "шапка" (заголовок) окна
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x01;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                Point pos = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                int resizeArea = 10; // Размер области захвата краев в пикселях

                // Проверка углов и границ
                if (pos.X <= resizeArea && pos.Y <= resizeArea) m.Result = (IntPtr)HTTOPLEFT;
                else if (pos.X >= this.ClientSize.Width - resizeArea && pos.Y <= resizeArea) m.Result = (IntPtr)HTTOPRIGHT;
                else if (pos.X <= resizeArea && pos.Y >= this.ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (pos.X >= this.ClientSize.Width - resizeArea && pos.Y >= this.ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (pos.X <= resizeArea) m.Result = (IntPtr)HTLEFT;
                else if (pos.X >= this.ClientSize.Width - resizeArea) m.Result = (IntPtr)HTRIGHT;
                else if (pos.Y <= resizeArea) m.Result = (IntPtr)HTTOP;
                else if (pos.Y >= this.ClientSize.Height - resizeArea) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Получаем элемент, на который кликнули
            ListViewItem selectedItem = listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0] : null;

            if (selectedItem != null && selectedItem.Tag != null)
            {
                string path = selectedItem.Tag.ToString();

                // Проверяем, является ли путь папкой
                if (Directory.Exists(path))
                {
                    // 1. Загружаем содержимое этой папки в ListView
                    DrawListView.LoadDirectory(listView1, path);
                    DrawAdressBar.UpdateAddressBar(comboBox1, path);

                    // 2. (Опционально) Обновляем адресную строку, если она у вас есть
                    // txtAddress.Text = path;

                    // 3. (Сложно, но нужно) Синхронизируем TreeView, если хотите, 
                    // чтобы ветка слева тоже раскрылась.
                }
                else if (File.Exists(path))
                {
                    // Если это файл, открываем его стандартной программой Windows
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = path,
                            UseShellExecute = true // Важно для .NET Core / .NET 5+
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось открыть файл: " + ex.Message);
                    }
                }
            }
        }

        private void buttonMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonShare_Click(object sender, EventArgs e)
        {

        }

        private void buttonVid_Click(object sender, EventArgs e)
        {

        }

        private void buttonFile_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string targetPath = comboBox1.Text;

                if (Directory.Exists(targetPath))
                {
                    DirectoryInfo target = new DirectoryInfo(targetPath);

                    DrawListView.LoadDirectory(listView1, targetPath);
                    // Опционально: можно найти этот узел в дереве и выделить его
                }
                else
                {
                    MessageBox.Show("Путь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.SuppressKeyPress = true; // Убираем системный "дзинь" при нажатии Enter
            }
        }
    }
}
