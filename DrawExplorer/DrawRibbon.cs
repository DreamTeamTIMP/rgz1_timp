using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawRibbon
    {

        public static void SetupRibbon(TabControl tabControl)
        {
            CreateHomePage(tabControl.TabPages[0]);
        }

        private static void CreateHomePage(TabPage page)
        {
            FlowLayoutPanel ribbonGroupBuff = CreateRibbonGroup(page, "Буфер обмена");
            AddRibbonButton(ribbonGroupBuff, "Закрепить на панели быстрого дотступа", "\uE10F;", true); // Большая кнопка
            AddRibbonButton(ribbonGroupBuff, "Копировать", "paste_icon", true);
            AddRibbonButton(ribbonGroupBuff, "Вставить", "paste_icon", true);
            AddRibbonButton(ribbonGroupBuff, "Вырезать", "cut_icon", false); // Маленькая кнопка
            AddRibbonButton(ribbonGroupBuff, "Скопировать как путь", "cut_icon", false);
            AddRibbonButton(ribbonGroupBuff, "Вставить ярлык", "cut_icon", false);

            FlowLayoutPanel ribbonGroupSort = CreateRibbonGroup(page, "Упорядочить");
            AddRibbonButton(ribbonGroupSort, "Переместить в", "paste_icon", true); // Большая кнопка
            AddRibbonButton(ribbonGroupSort, "Копировать в", "paste_icon", true);
            AddRibbonButton(ribbonGroupSort, "Удалить", "paste_icon", true);
            AddRibbonButton(ribbonGroupSort, "Переименовать", "paste_icon", true);


            FlowLayoutPanel ribbonGroupCreate = CreateRibbonGroup(page, "Создать");
            AddRibbonButton(ribbonGroupCreate, "Переместить в", "paste_icon", true); // Большая кнопка
            AddRibbonButton(ribbonGroupCreate, "Копировать в", "paste_icon", true);
            AddRibbonButton(ribbonGroupCreate, "Удалить", "paste_icon", true);
            AddRibbonButton(ribbonGroupCreate, "Переименовать", "paste_icon", true);
        }

        public static void AddRibbonButton(Control parent, string text, string glyph, bool isLarge)
        {
            Button btn = new Button
            {
                Text = isLarge ? glyph + "\n" + text : glyph + "  " + text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Margin = new Padding(2),
                TextAlign = isLarge ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.Overlay // Мы используем только текст (шрифт)
            };

            // Настройка шрифта для иконки (Segoe MDL2 Assets) и текста (Segoe UI)
            // В WinForms сложно задать два шрифта одной кнопке, поэтому мы делаем "финт":
            // Устанавливаем MDL2 как основной, а текст будет выглядеть сносно, 
            // Либо используем Label поверх кнопки (для идеального сходства).

            btn.Font = new Font("Segoe MDL2 Assets", isLarge ? 20 : 12);
            btn.Size = isLarge ? new Size(75, 90) : new Size(120, 28);

            // Убираем рамки и настраиваем цвета наведения
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 243, 255); // Светло-голубой
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(204, 232, 255);

            // Если кнопка большая, нам нужно, чтобы текст под иконкой был обычным шрифтом.
            // Для этого лучше использовать Paint или вложенный Label:
            if (isLarge)
            {
                btn.Paint += (s, e) => {
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    using (Font textFont = new Font("Segoe UI", 8f))
                    {
                        SizeF textSize = e.Graphics.MeasureString(text, textFont);
                        e.Graphics.DrawString(text, textFont, Brushes.Black,
                            (btn.Width - textSize.Width) / 2, btn.Height - 25);
                    }
                };
                btn.Text = glyph; // В самом Text оставляем только иконку
            }

            parent.Controls.Add(btn);
        }
        public static FlowLayoutPanel CreateRibbonGroup(TabPage parent, string title)
        {
            Panel groupWrapper = new Panel
            {
                Dock = DockStyle.Left,
                Width = 120,
                Padding = new Padding(0, 0, 1, 0), // Место для разделителя
                BackColor = Color.FromArgb(32,32,32)
            };

            // Рисуем вертикальную линию-разделитель справа
            groupWrapper.Paint += (s, e) => {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(220, 220, 220)),
                    groupWrapper.Width - 1, 10, groupWrapper.Width - 1, groupWrapper.Height - 25);
            };

            FlowLayoutPanel container = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            Label lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 20,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 7.5f)
            };

            groupWrapper.Controls.Add(container);
            groupWrapper.Controls.Add(lblTitle);
            parent.Controls.Add(groupWrapper);

            return container;
        }
    }
}
