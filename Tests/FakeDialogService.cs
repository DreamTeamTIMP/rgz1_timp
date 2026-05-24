using rgz1_timp.Services.rgz1_timp.Services;

namespace Tests
{
    /// <summary>
    /// Поддельная реализация IDialogService для модульного тестирования.
    /// Позволяет управлять результатами диалогов без реального отображения окон.
    /// </summary>
    class FakeDialogService : IDialogService
    {
        /// <summary>
        /// Результат, возвращаемый методом Confirm.
        /// </summary>
        public bool ConfirmResult { get; set; } = true;

        /// <summary>
        /// Результат, возвращаемый методом ConfirmOverwrite.
        /// </summary>
        public bool ConfirmOverwriteResult { get; set; } = true;

        /// <summary>
        /// Имитирует диалог подтверждения.
        /// </summary>
        public bool Confirm(string message, string title) => ConfirmResult;

        /// <summary>
        /// Пустая реализация показа ошибки (не требуется в тестах).
        /// </summary>
        public void ShowError(string message) { }

        /// <summary>
        /// Пустая реализация показа информации (не требуется в тестах).
        /// </summary>
        public void ShowInfo(string message) { }

        /// <summary>
        /// Имитирует диалог подтверждения перезаписи.
        /// </summary>
        public bool ConfirmOverwrite(string source, string destination) => ConfirmOverwriteResult;
    }
}