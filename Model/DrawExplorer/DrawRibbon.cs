namespace rgz1_timp.DrawExplorer
{
    public class DrawRibbon
    {
        private readonly TabControl tabControl;
        private readonly FormMain form;

        public DrawRibbon(TabControl tabControl, FormMain form)
        {
            this.tabControl = tabControl;
            this.form = form;
            SetupRibbon();
        }

        private void SetupRibbon()
        {
            CreateHomePage(tabControl.TabPages[0]);
            CreateViewPage(tabControl.TabPages[1]);
        }

        private void CreateViewPage(TabPage page)
        {
            
            // Группа "Структура"
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

            // Группа "Текущее представление"
            Panel ribbonGroupCurrent = CreateRibbonGroup(page, "Текущее представление");
            FlowLayoutPanel bigButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, true);
            AddRibbonButton(bigButtonsCurrent, "Сортировать", "\uE8CB", true, (s,e) => form.SortFiles());
            AddRibbonButton(bigButtonsCurrent, "Размер всех столбцов", "\uE8B3", true, (s, e) => form.AutoResizeColumns());
            ResizeRibbonGroup(ribbonGroupCurrent);

        }


        private void CreateHomePage(TabPage page)
        {
            Panel ribbonGroupCreate = CreateRibbonGroup(page, "Создать");
            FlowLayoutPanel bigButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, true);
            AddRibbonButton(bigButtonsPanelCreate, "Новая папка", "", true, (s, e) => form.CreateNewFolder());
            ResizeRibbonGroup(ribbonGroupCreate);

            Panel ribbonGroupSort = CreateRibbonGroup(page, "Упорядочить");
            FlowLayoutPanel bigButtonsPanelSort = CreatePanelForButtons(ribbonGroupSort, true);
            AddRibbonButton(bigButtonsPanelSort, "Переместить в", "\uE8DE", true, (s, e) => form.MoveToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Копировать в", "\uE8C8", true, (s, e) => form.CopyToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Удалить", "\uE74D", true, (s, e) => form.DeleteSelectedItem());
            AddRibbonButton(bigButtonsPanelSort, "Переименовать", "\uE8AC", true, (s, e) => form.RenameSelectedItem());
            ResizeRibbonGroup(ribbonGroupSort);

            Panel ribbonGroupBuff = CreateRibbonGroup(page, "Буфер обмена");
            FlowLayoutPanel smallButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, false);
            FlowLayoutPanel bigButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, true);
            AddRibbonButton(bigButtonsPanel, "Копировать", "\uE8C8", true, (s, e) => form.CopySelectedItem());
            AddRibbonButton(bigButtonsPanel, "Вставить", "\uE77F", true, (s, e) => form.PasteItem());
            AddRibbonButton(smallButtonsPanel, "Вырезать", "\uE8C6", false, (s, e) => form.CutSelectedItem());
            AddRibbonButton(smallButtonsPanel, "Скопировать путь", "\uE8C1", false, (s, e) => form.CopyPath());
            ResizeRibbonGroup(ribbonGroupBuff);
        }

        // Вспомогательные методы остаются без изменений (можно сделать статическими или приватными экземплярными)
        private void ResizeRibbonGroup(Panel ribbonGroup)
        {
            int totalWidth = 0;
            foreach (Control ctl in ribbonGroup.Controls)
                if (ctl is FlowLayoutPanel flp)
                    totalWidth += flp.Width;
            ribbonGroup.Width = totalWidth + 10;
        }

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

        private void AddRibbonButton(Control parent, string text, string glyph, bool isLarge, EventHandler? clickHandler = null)
        {
            Button btn = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(32, 32, 32),
                Margin = new Padding(2),
                Size = isLarge ? new Size(70, 80) : new Size(130, 22),
                UseVisualStyleBackColor = false
            };

            if (text.Length > 20)
                btn.Size = isLarge ? new Size(100, 80) : new Size(170, 22);

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 243, 255);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(204, 232, 255);

            if (isLarge)
            {
                btn.Text = "";
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

        private Panel CreateRibbonGroup(TabPage parent, string title)
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

            groupWrapper.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220)),
                    groupWrapper.Width - 1, 10, groupWrapper.Width - 1, groupWrapper.Height - 25);
            };

            Label lblTitle = new Label
            {
                Text = title,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 13,
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 7.5f),
                AutoSize = false,
                Location = new Point(0, groupWrapper.Height - 13)
            };

            groupWrapper.Controls.Add(lblTitle);
            parent.Controls.Add(groupWrapper);
            lblTitle.BringToFront();
            return groupWrapper;
        }
    }
}