namespace rgz1_timp.Services
{
    namespace rgz1_timp.Services
    {
        /// <summary>
        /// Интерфейс сервиса диалоговых окон. Предоставляет методы для взаимодействия с пользователем через MessageBox.
        /// </summary>
        public interface IDialogService
        {
            /// <summary>
            /// Отображает диалог подтверждения с вопросом "Да/Нет".
            /// </summary>
            /// <param name="message">Текст сообщения.</param>
            /// <param name="title">Заголовок окна.</param>
            /// <returns>True, если пользователь выбрал "Да".</returns>
            bool Confirm(string message, string title);

            /// <summary>
            /// Показывает сообщение об ошибке.
            /// </summary>
            /// <param name="message">Текст ошибки.</param>
            void ShowError(string message);

            /// <summary>
            /// Показывает информационное сообщение.
            /// </summary>
            /// <param name="message">Текст сообщения.</param>
            void ShowInfo(string message);

            /// <summary>
            /// Запрашивает подтверждение перезаписи существующего объекта.
            /// </summary>
            /// <param name="source">Исходный путь.</param>
            /// <param name="destination">Целевой путь.</param>
            /// <returns>True, если пользователь согласился перезаписать.</returns>
            bool ConfirmOverwrite(string source, string destination);
        }
    }
}