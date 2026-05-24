namespace rgz1_timp.Services
{
    namespace rgz1_timp.Services
    {
        /// <summary>
        /// Реализация сервиса диалоговых окон, использующая стандартный MessageBox.
        /// </summary>
        public class DialogService : IDialogService
        {
            /// <summary>
            /// Отображает диалог подтверждения с вопросом "Да/Нет".
            /// </summary>
            /// <param name="message">Текст сообщения.</param>
            /// <param name="title">Заголовок окна.</param>
            /// <returns>True, если пользователь нажал "Да".</returns>
            public bool Confirm(string message, string title)
            {
                return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            /// <summary>
            /// Показывает сообщение об ошибке.
            /// </summary>
            /// <param name="message">Текст ошибки.</param>
            public void ShowError(string message)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /// <summary>
            /// Показывает информационное сообщение.
            /// </summary>
            /// <param name="message">Текст сообщения.</param>
            public void ShowInfo(string message)
            {
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            /// <summary>
            /// Запрашивает подтверждение перезаписи существующего файла или папки.
            /// </summary>
            /// <param name="source">Исходный путь.</param>
            /// <param name="destination">Целевой путь.</param>
            /// <returns>True, если пользователь согласен на перезапись.</returns>
            public bool ConfirmOverwrite(string source, string destination)
            {
                string msg = $"Файл или папка уже существует:\n{destination}\n\nЗаменить его?";
                return MessageBox.Show(msg, "Подтверждение перезаписи", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;
            }
        }
    }
}