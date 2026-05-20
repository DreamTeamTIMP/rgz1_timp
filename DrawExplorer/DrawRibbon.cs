namespace rgz1_timp.DrawExplorer
{
    internal static class DrawRibbon
    {
        
        public static void SetupRibbon(TabControl tabControl, FormMain form)
        {
            CreateHomePage(tabControl.TabPages[0], form);
            CreateSharePage(tabControl.TabPages[1]);
            CreateViewPage(tabControl.TabPages[2]);
        }

        private static void CreateViewPage(TabPage page)
        {
            //  Группа "Области" (Panes) 
            Panel ribbonGroupPanes = CreateRibbonGroup(page, "Области");
            FlowLayoutPanel smallButtonsPanes = CreatePanelForButtons(ribbonGroupPanes, false);

            FlowLayoutPanel bigButtonsPanes = CreatePanelForButtons(ribbonGroupPanes, true);

            AddRibbonButton(bigButtonsPanes, "Область навигации", "\uE8D5", true);   // Navigation Pane
            AddRibbonButton(smallButtonsPanes, "Область просмотра", "\uE8A4", false); // Preview Pane
            AddRibbonButton(smallButtonsPanes, "Область сведений", "\uE946", false);  // Details Pane
            ResizeRibbonGroup(ribbonGroupPanes);

            //  Группа "Структура" (Layout) 
            Panel ribbonGroupLayout = CreateRibbonGroup(page, "Структура");
            FlowLayoutPanel smallButtonsLayoutGrid = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutTypes = CreatePanelForButtons(ribbonGroupLayout, false);
            FlowLayoutPanel smallButtonsLayoutSome = CreatePanelForButtons(ribbonGroupLayout, false);

            AddRibbonButton(smallButtonsLayoutGrid, "Огромные значки", "\uE80A", false); // Grid View (Large)
            AddRibbonButton(smallButtonsLayoutGrid, "Крупные значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutGrid, "Обычные значки", "\uE80A", false);

            AddRibbonButton(smallButtonsLayoutTypes, "Мелкие значки", "\uE80A", false);
            AddRibbonButton(smallButtonsLayoutTypes, "Список", "\uE8B0", false);         // List
            AddRibbonButton(smallButtonsLayoutTypes, "Таблица", "\uE8EF", false);        // Details/Table
            
            AddRibbonButton(smallButtonsLayoutSome, "Плитка", "\uE895", false);         // Tiles
            AddRibbonButton(smallButtonsLayoutSome, "Содержимое", "\uE8D2", false);     // Content
            ResizeRibbonGroup(ribbonGroupLayout);

            //  Группа "Текущее представление" (Current View) 
            Panel ribbonGroupCurrent = CreateRibbonGroup(page, "Текущее представление");

            FlowLayoutPanel smallButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, false);
            FlowLayoutPanel bigButtonsCurrent = CreatePanelForButtons(ribbonGroupCurrent, true);
            
            AddRibbonButton(bigButtonsCurrent, "Сортировать", "\uE8CB", true);      // Sort
            AddRibbonButton(smallButtonsCurrent, "Группировать", "\uF012", false);   // Group
            AddRibbonButton(smallButtonsCurrent, "Добавить столбцы", "\uE8A6", false); // Add columns
            AddRibbonButton(smallButtonsCurrent, "Размер всех столбцов", "\uE8B3", false); // Size all columns
            ResizeRibbonGroup(ribbonGroupCurrent);

            //  Группа "Показать или скрыть" (Show/Hide) 
            Panel ribbonGroupShowHide = CreateRibbonGroup(page, "Показать или скрыть");
            FlowLayoutPanel smallButtonsShow = CreatePanelForButtons(ribbonGroupShowHide, false);
            FlowLayoutPanel bigButtonsShow = CreatePanelForButtons(ribbonGroupShowHide, true);

            AddRibbonButton(smallButtonsShow, "Флажки элементов", "\uE739", false);      // Checkbox
            AddRibbonButton(smallButtonsShow, "Расширения имен", "\uE8A5", false);      // File extension info
            AddRibbonButton(smallButtonsShow, "Скрытые элементы", "\uE7B3", false);     // Hidden items (Ghost/Eye)
            AddRibbonButton(bigButtonsShow, "Скрыть выбранные", "\uED1A", true);        // Hide selected
            ResizeRibbonGroup(ribbonGroupShowHide);

            //  Группа "Параметры" 
            Panel ribbonGroupOptions = CreateRibbonGroup(page, "Параметры");
            FlowLayoutPanel bigButtonsOptions = CreatePanelForButtons(ribbonGroupOptions, true);

            AddRibbonButton(bigButtonsOptions, "Параметры", "\uE713", true);            // Settings/Options
            ResizeRibbonGroup(ribbonGroupOptions);
        }

        private static void CreateSharePage(TabPage page)
        {
            //  Группа "Поделиться" 
            Panel ribbonGroupShare = CreateRibbonGroup(page, "Поделиться");
            FlowLayoutPanel bigButtonsShare = CreatePanelForButtons(ribbonGroupShare, true);

            // Для иконки "Сделать недоступными" (Замок)
            AddRibbonButton(bigButtonsShare, "Сделать недоступными", "\uE72E", true); // Lock
            ResizeRibbonGroup(ribbonGroupShare);

            //  Группа "Безопасность" (Дополнительно) 
            Panel ribbonGroupSecurity = CreateRibbonGroup(page, "Безопасность");
            FlowLayoutPanel bigButtonsSecurity = CreatePanelForButtons(ribbonGroupSecurity, true);

            AddRibbonButton(bigButtonsSecurity, "Доп. параметры безопасности", "\uE8D7", true); // Permissions / Shield
            ResizeRibbonGroup(ribbonGroupSecurity);
            //  Группа "Отправить" 
            
            Panel ribbonGroupSend = CreateRibbonGroup(page, "Отправить");
            FlowLayoutPanel smallButtonsSend = CreatePanelForButtons(ribbonGroupSend, false);

            FlowLayoutPanel bigButtonsSend = CreatePanelForButtons(ribbonGroupSend, true);
            
            
            AddRibbonButton(smallButtonsSend, "Запись на компакт-диск", "\uE958", false); // CD Rom
            AddRibbonButton(smallButtonsSend, "Печать", "\uE749", false);                 // Print
            AddRibbonButton(smallButtonsSend, "Факс", "\uE7C8", false);                  // Fax

            AddRibbonButton(bigButtonsSend, "Отправить", "\uE72D", true);         // Share / Отправить
            AddRibbonButton(bigButtonsSend, "Электронная почта", "\uE715", true); // Mail / Почта
            AddRibbonButton(bigButtonsSend, "Сжать", "\uF012", true);             // Zip / Сжать (ZipFolder)

            ResizeRibbonGroup(ribbonGroupSend);

        }
        private static void CreateHomePage(TabPage page, FormMain form)
        {
            // Группа "Создать"
            Panel ribbonGroupCreate = CreateRibbonGroup(page, "Создать");
            FlowLayoutPanel smallButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, false);
            AddRibbonButton(smallButtonsPanelCreate, "Создать элемент", "\uE72D", false);
            AddRibbonButton(smallButtonsPanelCreate, "Простой доступ", "\uE7C3", false, (s, e) => form.SimpleAccess()); // пока заглушка
            FlowLayoutPanel bigButtonsPanelCreate = CreatePanelForButtons(ribbonGroupCreate, true);
            AddRibbonButton(bigButtonsPanelCreate, "Новая папка", "", true, (s, e) => form.CreateNewFolder());
            ResizeRibbonGroup(ribbonGroupCreate);

            // Группа "Упорядочить"
            Panel ribbonGroupSort = CreateRibbonGroup(page, "Упорядочить");
            FlowLayoutPanel bigButtonsPanelSort = CreatePanelForButtons(ribbonGroupSort, true);
            AddRibbonButton(bigButtonsPanelSort, "Переместить в", "\uE8DE", true, (s, e) => form.MoveToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Копировать в", "\uE8C8", true, (s, e) => form.CopyToDialog());
            AddRibbonButton(bigButtonsPanelSort, "Удалить", "\uE74D", true, (s, e) => form.DeleteSelectedItem());
            AddRibbonButton(bigButtonsPanelSort, "Переименовать", "\uE8AC", true, (s, e) => form.RenameSelectedItem());
            ResizeRibbonGroup(ribbonGroupSort);

            // Группа "Буфер обмена"
            Panel ribbonGroupBuff = CreateRibbonGroup(page, "Буфер обмена");
            FlowLayoutPanel smallButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, false);
            FlowLayoutPanel bigButtonsPanel = CreatePanelForButtons(ribbonGroupBuff, true);

            AddRibbonButton(bigButtonsPanel, "Закрепить на панели быстрого доступа", "\uE840", true, (s, e) => form.PinToQuickAccess());
            AddRibbonButton(bigButtonsPanel, "Копировать", "\uE8C8", true, (s, e) => form.CopySelectedItem());
            AddRibbonButton(bigButtonsPanel, "Вставить", "\uE77F", true, (s, e) => form.PasteItem());
            AddRibbonButton(smallButtonsPanel, "Вырезать", "\uE8C6", false, (s, e) => form.CutSelectedItem());
            AddRibbonButton(smallButtonsPanel, "Скопировать путь", "\uE8C1", false, (s, e) => form.CopyPath());
            AddRibbonButton(smallButtonsPanel, "Вставить ярлык", "\uEED1", false, (s, e) => form.PasteShortcut());
            ResizeRibbonGroup(ribbonGroupBuff);
        }
        private static void ResizeRibbonGroup(Panel ribbonGroup)
        {
            // После добавления всех кнопок в ribbonGroupBuff
            int totalWidth = 0;
            foreach (Control ctl in ribbonGroup.Controls)
                if (ctl is FlowLayoutPanel flp)
                    totalWidth += flp.Width;
            ribbonGroup.Width = totalWidth + 10;

        }
        private static FlowLayoutPanel CreatePanelForButtons(Control parent, bool toSmallText)
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

        public static void AddRibbonButton(Control parent, string text, string glyph, bool isLarge, EventHandler? clickHandler = null)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(32, 32, 32),
                Margin = new Padding(2),
                Size = isLarge ? new Size(70, 80) : new Size(130, 22),
                UseVisualStyleBackColor = false
            };

            if (text.Length > 20)
                btn.Size = isLarge ? new Size(100, 80) : new Size(170,22) ;

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 243, 255);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(204, 232, 255);

            if (isLarge)
            {
                btn.Text = ""; // Не используем стандартный текст
                btn.Paint += (s, e) =>
                {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    // Рисуем иконку (глиф) шрифтом Segoe MDL2 Assets
                    using (Font iconFont = new Font("Segoe MDL2 Assets", 20))
                    {
                        SizeF iconSize = e.Graphics.MeasureString(glyph, iconFont);
                        float iconX = (btn.Width - iconSize.Width) / 2;
                        float iconY = 8; // отступ сверху
                        e.Graphics.DrawString(glyph, iconFont, Brushes.White, iconX, iconY);
                    }

                    // Рисуем текст под иконкой с переносом
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
            parent.Controls.Add(btn);
        }

        public static Panel CreateRibbonGroup(TabPage parent, string title)
        {
            Panel groupWrapper = new Panel
            {
                Dock = DockStyle.Left,
                AutoSize = false,          // отключаем авторазмер
                Width = 100,               // начальная ширина, будет увеличена содержимым
                Height = 130,              // фиксированная высота, чтобы Label был виден
                Padding = new Padding(0, 0, 2, 0),
                BackColor = Color.FromArgb(32, 32, 32)
            };

            // Рисуем разделитель справа
            groupWrapper.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220)),
                    groupWrapper.Width - 1, 10, groupWrapper.Width - 1, groupWrapper.Height - 25);
            };

            Label lblTitle = new Label
            {
                Text = title,
                TextAlign = ContentAlignment.MiddleCenter, // Центровка текста внутри Label
                Height = 13,
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 7.5f),
                AutoSize = false, // Обязательно false, чтобы Dock работал на всю ширину
                Location = new Point(0, groupWrapper.Height - 13)
            };

            groupWrapper.Controls.Add(lblTitle);
            parent.Controls.Add(groupWrapper);
           lblTitle.BringToFront();
            return groupWrapper;
        }
    }
}
