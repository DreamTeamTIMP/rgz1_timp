namespace rgz1_timp.DrawExplorer
{
    public class DrawRibbon
    {
        private readonly TabControl _tabControl;
        private readonly FormMain _form;

        public DrawRibbon(TabControl tabControl, FormMain form)
        {
            _tabControl = tabControl;
            _form = form;
            SetupRibbon();
        }

        private void SetupRibbon()
        {
            CreateHomePage(_tabControl.TabPages[0]);
            CreateSharePage(_tabControl.TabPages[1]);
            CreateViewPage(_tabControl.TabPages[2]);
        }

        private void CreateViewPage(TabPage page)
        {
            // Группа "Области"
            Panel ribbonGroupPanes = CreateRibbonGroup(page, "Области");
            FlowLayoutPanel smallButtonsPanes = CreatePanelForButtons(ribbonGroupPanes, false);
            FlowLayoutPanel bigButtonsPanes = CreatePanelForButtons(ribbonGroupPanes, true);
            AddRibbonButton(bigButtonsPanes, "Область навигации", "\uE8D5", true);
            AddRibbonButton(smallButtonsPanes, "Область просмотра", "\uE8A4", false);
            AddRibbonButton(smallButtonsPanes, "Область сведений", "\uE946", false);
            ResizeRibbonGroup(ribbonGroupPanes);

            // Группа "Структура"
            Panel ribbonGroupLayout = CreateRibbonGroup(page, "Структура");
            FlowLayoutPanel smallButtonsLayoutGrid = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutTypes = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutSome = CreatePanelForButtons(ribbonGroupLayout, false);
            AddRibbonButton(smallButtonsLayoutGrid, "Огромные значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutGrid, "Крупные значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutGrid, "Обычные значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutTypes, "Мелкие значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutTypes, "Список", "\uE8B0", false);
            AddRibbonButton(smallButtonsLayoutTypes, "Таблица", "\uE8EF", false);
            AddRibbonButton(smallButtonsLayoutSome, "Плитка", "\uE895", false);
            AddRibbonButton(smallButtonsLayoutSome, "Содержимое", "\uE8D2", false);
            ResizeRibbonGroup(ribbonGroupLayout);

            // Группа "Текущее представление"
            Panel ribbonGroupCurrent = CreateRibbonGroup(page, "Текущее представление");
            FlowLayoutPanel smallButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, false);
            FlowLayoutPanel bigButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, true);
            AddRibbonButton(bigButtonsCurrent, "Сортировать", "\uE8CB", true);
            AddRibbonButton(smallButtonsCurrent, "Группировать", "\uF012", false);
            AddRibbonButton(smallButtonsCurrent, "Добавить столбцы", "\uE8A6", false);
            AddRibbonButton(smallButtonsCurrent, "Размер всех столбцов", "\uE8B3", false);
            ResizeRibbonGroup(ribbonGroupCurrent);

            // Группа "Показать или скрыть"
            Panel ribbonGroupShowHide = CreateRibbonGroup(page, "Показать или скрыть");
            FlowLayoutPanel smallButtonsShow = CreatePanelForButtons(ribbonGroupShowHide, false);
            FlowLayoutPanel bigButtonsShow = CreatePanelForButtons(ribbonGroupShowHide, true);
            AddRibbonButton(smallButtonsShow, "Флажки элементов", "\uE739", false);
            AddRibbonButton(smallButtonsShow, "Расширения имен", "\uE8A5", false);
            AddRibbonButton(smallButtonsShow, "Скрытые элементы", "\uE7B3", false);
            AddRibbonButton(bigButtonsShow, "Скрыть выбранные", "\uED1A", true);
            ResizeRibbonGroup(ribbonGroupShowHide);

            // Группа "Параметры"
            Panel ribbonGroupOptions = CreateRibbonGroup(page, "Параметры");
            FlowLayoutPanel bigButtonsOptions = CreatePanelForButtons(ribbonGroupOptions, true);
            AddRibbonButton(bigButtonsOptions, "Параметры", "\uE713", true);
            ResizeRibbonGroup(ribbonGroupOptions);
        }

        private void CreateSharePage(TabPage page)
        {
            Panel ribbonGroupShare = CreateRibbonGroup(page, "Поделиться");
            FlowLayoutPanel bigButtonsShare = CreatePanelForButtons(ribbonGroupShare, true);
            AddRibbonButton(bigButtonsShare, "Сделать недоступными", "\uE72E", true);
            ResizeRibbonGroup(ribbonGroupShare);

            Panel ribbonGroupSecurity = CreateRibbonGroup(page, "Безопасность");
            FlowLayoutPanel bigButtonsSecurity = CreatePanelForButtons(ribbonGroupSecurity, true);
            AddRibbonButton(bigButtonsSecurity, "Доп. параметры безопасности", "\uE8D7", true);
            ResizeRibbonGroup(ribbonGroupSecurity);

            Panel ribbonGroupSend = CreateRibbonGroup(page, "Отправить");
            FlowLayoutPanel smallButtonsSend = CreatePanelForButtons(ribbonGroupSend, false);
            FlowLayoutPanel bigButtonsSend = CreatePanelForButtons(ribbonGroupSend, true);
            AddRibbonButton(smallButtonsSend, "Запись на компакт-диск", "\uE958", false);
            AddRibbonButton(smallButtonsSend, "Печать", "\uE749", false);
            AddRibbonButton(smallButtonsSend, "Факс", "\uE7C8", false);
            AddRibbonButton(bigButtonsSend, "Отправить", "\uE72D", true);
            AddRibbonButton(bigButtonsSend, "Электронная почта", "\uE715", true);
            AddRibbonButton(bigButtonsSend, "Сжать", "\uF012", true);
            ResizeRibbonGroup(ribbonGroupSend);
        }

        private void CreateHomePage(TabPage page)
        {
            Panel ribbonGroupCreate = CreateRibbonGroup(page, "Создать");
            FlowLayoutPanel smallButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, false);
            AddRibbonButton(smallButtonsPanelCreate, "Создать элемент", "\uE72D", false);
            AddRibbonButton(smallButtonsPanelCreate, "Простой доступ", "\uE7C3", false, (s, e) => _form.SimpleAccess());
            FlowLayoutPanel bigButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, true);
            AddRibbonButton(bigButtonsPanelCreate, "Новая папка", "", true, (s, e) => _form.CreateNewFolder());
            ResizeRibbonGroup(ribbonGroupCreate);

            Panel ribbonGroupSort = CreateRibbonGroup(page, "Упорядочить");
            FlowLayoutPanel bigButtonsPanelSort = CreatePanelForButtons(ribbonGroupSort, true);
            AddRibbonButton(bigButtonsPanelSort, "Переместить в", "\uE8DE", true, (s, e) => _form.MoveToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Копировать в", "\uE8C8", true, (s, e) => _form.CopyToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Удалить", "\uE74D", true, (s, e) => _form.DeleteSelectedItem());
            AddRibbonButton(bigButtonsPanelSort, "Переименовать", "\uE8AC", true, (s, e) => _form.RenameSelectedItem());
            ResizeRibbonGroup(ribbonGroupSort);

            Panel ribbonGroupBuff = CreateRibbonGroup(page, "Буфер обмена");
            FlowLayoutPanel smallButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, false);
            FlowLayoutPanel bigButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, true);
            AddRibbonButton(bigButtonsPanel, "Закрепить на панели быстрого доступа", "\uE840", true, (s, e) => _form.PinToQuickAccess());
            AddRibbonButton(bigButtonsPanel, "Копировать", "\uE8C8", true, (s, e) => _form.CopySelectedItem());
            AddRibbonButton(bigButtonsPanel, "Вставить", "\uE77F", true, (s, e) => _form.PasteItem());
            AddRibbonButton(smallButtonsPanel, "Вырезать", "\uE8C6", false, (s, e) => _form.CutSelectedItem());
            AddRibbonButton(smallButtonsPanel, "Скопировать путь", "\uE8C1", false, (s, e) => _form.CopyPath());
            AddRibbonButton(smallButtonsPanel, "Вставить ярлык", "\uEED1", false, (s, e) => _form.PasteShortcut());
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