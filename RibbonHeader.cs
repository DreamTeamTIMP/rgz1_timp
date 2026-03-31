namespace rgz1_timp
{
    using System.Drawing;
    using System.Windows.Forms;

    public class RibbonHeader : Panel
    {
        private Panel contentArea; // Панель, где лежат инструменты (буфер обмена и т.д.)
        private ContextMenuStrip fileMenu;
        private Button activeTab = null;

        public RibbonHeader()
        {
            this.Dock = DockStyle.Top;
            this.Height = 145; // Высота шапки + высота ленты
            this.BackColor = Color.FromArgb(30, 30, 30); // Темная тема как на скрине

            InitializeRibbon();
        }

        private void InitializeRibbon()
        {
            // 1. Создаем кнопку "Файл"
            Button btnFile = CreateTabButton("Файл", 0);
            btnFile.BackColor = Color.FromArgb(190, 71, 38); // Оранжевый цвет
            btnFile.ForeColor = Color.White;
            btnFile.Click += (s, e) => ShowFileMenu(btnFile);

            // 2. Создаем обычные вкладки
            Button tabHome = CreateTabButton("Главная", 60);
            Button tabShare = CreateTabButton("Поделиться", 140);
            Button tabView = CreateTabButton("Вид", 235);

            // 3. Панель для содержимого вкладок (сама лента)
            contentArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 115,
                BackColor = Color.FromArgb(45, 45, 45), // Цвет ленты под кнопками
                Location = new Point(0, 30)
            };

            // Настраиваем меню для кнопки Файл
            fileMenu = new ContextMenuStrip();
            fileMenu.Items.Add("Открыть новое окно");
            fileMenu.Items.Add("Параметры");
            fileMenu.Items.Add(new ToolStripSeparator());
            fileMenu.Items.Add("Выход");

            this.Controls.Add(contentArea);
            this.Controls.Add(tabView);
            this.Controls.Add(tabShare);
            this.Controls.Add(tabHome);
            this.Controls.Add(btnFile);
        }

        private Button CreateTabButton(string text, int x)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(x, 0),
                Size = new Size(text.Length * 12 + 20, 30),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f),
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            btn.ForeColor = Color.White;

            // Эффект активации вкладки (кроме Файла)
            if (text != "Файл")
            {
                btn.Click += (s, e) => SetActiveTab(btn);
            }

            return btn;
        }

        private void SetActiveTab(Button btn)
        {
            if (activeTab != null) activeTab.BackColor = Color.Transparent;
            activeTab = btn;
            activeTab.BackColor = Color.FromArgb(45, 45, 45); // Совпадает с цветом ленты
        }

        private void ShowFileMenu(Button target)
        {
            fileMenu.Show(target, new Point(0, target.Height));
        }
    }
}
