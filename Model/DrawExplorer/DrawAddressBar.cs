using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Управляет адресной строкой (ComboBox) и её обновлением.
    /// </summary>
    public class DrawAddressBar
    {
        private readonly ComboBox addressBar;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="addressBar">Элемент адресной строки.</param>
        public DrawAddressBar(ComboBox addressBar)
        {
            this.addressBar = addressBar;
        }

        /// <summary>
        /// Обновляет текст адресной строки и добавляет путь в историю выпадающего списка.
        /// </summary>
        /// <param name="path">Новый путь для отображения.</param>
        public void UpdateAddressBar(string path)
        {
            // Применяем тему оформления explorer к адресной строке.
            _ = Dll.SetWindowTheme(addressBar.Handle, "explorer", null);
            addressBar.Text = path;

            // Перемещаем курсор в конец строки.
            addressBar.SelectionStart = addressBar.Text.Length;
            addressBar.SelectionLength = 0;

            // Добавляем путь в список истории, если его там ещё нет.
            if (!addressBar.Items.Contains(path))
                addressBar.Items.Insert(0, path);
        }

        /// <summary>
        /// Сбрасывает выделение в адресной строке и устанавливает курсор в конец.
        /// Используется таймер для отложенного выполнения после обработки событий.
        /// </summary>
        public void ResetAddressBarSelection()
        {
            System.Windows.Forms.Timer timer = new()
            {
                Interval = 10
            };
            timer.Tick += (s, e) =>
            {
                // Устанавливаем курсор в конец строки и снимаем выделение.
                addressBar.SelectionStart = addressBar.Text.Length;
                addressBar.SelectionLength = 0;

                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }
}