namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Программно создаёт ленточную панель (Ribbon) с двумя вкладками: «Главная» и «Вид».
    /// </summary>
    public class DrawRibbon
    {
        private readonly Panel ribbonContainer;
        private readonly FormMain form;
        private readonly Panel panelHome;
        private readonly Panel panelView;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="container">Контейнер, содержащий панели вкладок.</param>
        /// <param name="form">Главная форма (для доступа к методам-обработчикам).</param>
        public DrawRibbon(Panel container, FormMain form)
        {
            this.ribbonContainer = container;
            this.form = form;

            SetupRibbon();
        }

        /// <summary>
        /// Настраивает содержимое вкладок «Главная» и «Вид».
        /// </summary>
        private void SetupRibbon()
        {
            CreateHomePage((Panel)ribbonContainer.Controls[0]);
            CreateViewPage((Panel)ribbonContainer.Controls[1]);
        }

        /// <summary>
        /// Создаёт вкладку «Вид».
        /// </summary>
        /// <param name="page">Панель вкладки.</param>
        private void CreateViewPage(Panel page)
        {
            // Группа «Структура».
            Panel ribbonGroupLayout = CreateRibbonGroup(page, "Структура");
            FlowLayoutPanel smallButtonsLayoutGrid = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutTypes = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutSome = CreatePanelForButtons(ribbonGroupLayout, false);

            AddRibbonButton(smallButtonsLayoutGrid, "Огромные значки", "\uE80A", false, (s, e) => form.SetLargeIcons());
            AddRibbonButton(smallButtonsLayoutGrid, "Обычные значки", "\uE80A", false, (s, e) => form.SetSmallIcons());
            AddRibbonButton(smallButtonsLayoutTypes, "Мелкие значки", "\uE80A", false, (s, e) => form.SetDetails());
            AddRibbonButton(smallButtonsLayoutTypes, "Список", "\uE8B0", false, (s, e) => form.SetList());
            AddRibbonButton(smallButtonsLayoutTypes, "Таблица", "\uE8EF", false, (s, e) => form.SetTiles());
            ResizeRibbonGroup(ribbonGroupLayout);

            // Группа «Текущее представление».
            Panel ribbonGroupCurrent = CreateRibbonGroup(page, "Текущее представление");
            FlowLayoutPanel bigButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, true);
            AddRibbonButton(bigButtonsCurrent, "Сортировать", "\uE8CB", true, (s, e) => form.SortFiles());
            AddRibbonButton(bigButtonsCurrent, "Размер всех столбцов", "\uE8B3", true, (s, e) => form.AutoResizeColumns());
            ResizeRibbonGroup(ribbonGroupCurrent);
        }

        /// <summary>
        /// Создаёт вкладку «Главная».
        /// </summary>
        /// <param name="page">Панель вкладки.</param>
        private void CreateHomePage(Panel page)
        {
            // Группа «Создать».
            Panel ribbonGroupCreate = CreateRibbonGroup(page, "Создать");
            FlowLayoutPanel bigButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, true);
            AddRibbonButton(bigButtonsPanelCreate, "Новая папка", "", true, (s, e) => form.CreateNewFolder());
            ResizeRibbonGroup(ribbonGroupCreate);

            // Группа «Упорядочить».
            Panel ribbonGroupSort = CreateRibbonGroup(page, "Упорядочить");
            FlowLayoutPanel bigButtonsPanelSort = CreatePanelForButtons(ribbonGroupSort, true);
            AddRibbonButton(bigButtonsPanelSort, "Переместить в", "\uE8DE", true, (s, e) => form.MoveToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Копировать в", "\uE8C8", true, (s, e) => form.CopyToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Удалить", "\uE74D", true, (s, e) => form.DeleteSelectedItem());
            AddRibbonButton(bigButtonsPanelSort, "Переименовать", "\uE8AC", true, (s, e) => form.RenameSelectedItem());
            ResizeRibbonGroup(ribbonGroupSort);

            // Группа «Буфер обмена».
            Panel ribbonGroupBuff = CreateRibbonGroup(page, "Буфер обмена");
            FlowLayoutPanel smallButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, false);
            FlowLayoutPanel bigButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, true);
            AddRibbonButton(bigButtonsPanel, "Копировать", "\uE8C8", true, (s, e) => form.CopySelectedItem());
            AddRibbonButton(bigButtonsPanel, "Вставить", "\uE77F", true, (s, e) => form.PasteItem());
            AddRibbonButton(smallButtonsPanel, "Вырезать", "\uE8C6", false, (s, e) => form.CutSelectedItem());
            AddRibbonButton(smallButtonsPanel, "Скопировать путь", "\uE8C1", false, (s, e) => form.CopyPath());
            ResizeRibbonGroup(ribbonGroupBuff);
        }

        /// <summary>
        /// Изменяет ширину группы ленты в зависимости от суммарной ширины кнопок.
        /// </summary>
        /// <param name="ribbonGroup">Панель группы.</param>
        private void ResizeRibbonGroup(Panel ribbonGroup)
        {
            int totalWidth = 0;
            foreach (Control ctl in ribbonGroup.Controls)
                if (ctl is FlowLayoutPanel flp)
                    totalWidth += flp.Width;
            ribbonGroup.Width = totalWidth + 10;
        }

        /// <summary>
        /// Создаёт панель (FlowLayoutPanel) для размещения кнопок внутри группы.
        /// </summary>
        /// <param name="parent">Родительский контейнер.</param>
        /// <param name="toSmallText">True – вертикальное расположение (для больших кнопок), False – горизонтальное.</param>
        /// <returns>Созданная панель.</returns>
        private FlowLayoutPanel CreatePanelForButtons(Control parent, bool toSmallText)
        {
            FlowLayoutPanel container = new()
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = toSmallText ? FlowDirection.TopDown : FlowDirection.LeftToRight
            };
            parent.Controls.Add(container);
            return container;
        }

        /// <summary>
        /// Добавляет кнопку на ленту с заданным текстом, глифом и размером.
        /// </summary>
        /// <param name="parent">Контейнер для кнопки.</param>
        /// <param name="text">Текст (подпись).</param>
        /// <param name="glyph">Символ из шрифта Segoe MDL2 Assets.</param>
        /// <param name="isLarge">True – большая кнопка (80px высотой), False – маленькая.</param>
        /// <param name="clickHandler">Обработчик нажатия.</param>
        private void AddRibbonButton(Control parent, string text, string glyph, bool isLarge, EventHandler? clickHandler = null)
        {
            Button btn = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(32, 32, 32),
                Margin = new Padding(2),
                Size = isLarge ? new Size(70, 100) : new Size(130, 25),
                UseVisualStyleBackColor = false
            };

            // Увеличиваем ширину для длинных надписей.
            if (text.Length > 20)
                btn.Size = isLarge ? new Size(100, 80) : new Size(170, 25);

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 243, 255);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(204, 232, 255);

            if (isLarge)
            {
                btn.Text = "";
                // Рисуем иконку и текст вручную.
                btn.Paint += (s, e) =>
                {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    using (Font iconFont = new Font("Segoe MDL2 Assets", 20))
                    {
                        SizeF iconSize = e.Graphics.MeasureString(glyph, iconFont);
                        float iconX = (btn.Width - iconSize.Width) / 2;
                        float iconY = 8;
                        e.Graphics.DrawString(glyph, iconFont, Brushes.White, iconX, iconY);
                    }
                    using (Font textFont = new Font("Segoe UI", 7.5f))
                    {
                        Rectangle textRect = new Rectangle(4, 40, btn.Width - 8, 50);
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Near,
                            Trimming = StringTrimming.Word
                        };
                        e.Graphics.DrawString(text, textFont, Brushes.White, textRect, sf);
                        sf.Dispose();
                    }
                };
            }
            else
            {
                btn.Text = glyph + "  " + text;
                btn.Font = new Font("Segoe MDL2 Assets", 10);
                btn.TextAlign = ContentAlignment.MiddleLeft;
            }

            if (clickHandler != null)
                btn.Click += clickHandler;

            parent.Controls.Add(btn);
        }

        /// <summary>
        /// Создаёт группу на ленте (панель с рамкой и заголовком внизу).
        /// </summary>
        /// <param name="parent">Родительская панель вкладки.</param>
        /// <param name="title">Название группы.</param>
        /// <returns>Панель группы.</returns>
        private Panel CreateRibbonGroup(Panel parent, string title)
        {
            Panel groupWrapper = new Panel
            {
                Dock = DockStyle.Left,
                AutoSize = false,
                Width = 100,
                Height = 130,
                Padding = new Padding(0, 0, 2, 0),
                BackColor = Color.FromArgb(32, 32, 32)
            };

            // Рисуем вертикальную разделительную линию справа.
            groupWrapper.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220)),
                    groupWrapper.Width - 1, 10, groupWrapper.Width - 1, groupWrapper.Height - 30);
            };

            Label lblTitle = new Label
            {
                Text = title,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 18,
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 7.5f),
                AutoSize = false,
                Location = new Point(0, groupWrapper.Height - 20)
            };

            groupWrapper.Controls.Add(lblTitle);
            parent.Controls.Add(groupWrapper);
            lblTitle.BringToFront();
            return groupWrapper;
        }
    }
}