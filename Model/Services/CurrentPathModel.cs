namespace rgz1_timp.Services
{
    /// <summary>
    /// Модель текущего пути навигации, управляющая историей переходов.
    /// </summary>
    public class CurrentPathModel
    {
        // Текущий отображаемый путь.
        private string? currentPath;
        // Список всех посещённых путей.
        private List<string> history = new List<string>();
        // Индекс текущего пути в списке истории.
        private int historyIndex = -1;             

        /// <summary>
        /// Событие, возникающее при изменении текущего пути.
        /// </summary>
        public event Action<string?> PathChanged;

        /// <summary>
        /// Событие, возникающее при изменении состояния навигации (например, доступности кнопок "Назад"/"Вперёд").
        /// </summary>
        public event Action? NavigationStateChanged;

        /// <summary>
        /// Текущий путь. При установке автоматически управляет историей.
        /// </summary>
        public string? Path
        {
            get => currentPath;
            set
            {
                // Защита от повторной установки того же пути.
                if (currentPath == value) return;
                // Пустая строка или null не обрабатываются.
                if (string.IsNullOrEmpty(value)) return;

                // Если мы находимся не в конце истории (был переход "Назад"),
                // то удаляем все элементы после текущего индекса.
                if (historyIndex < history.Count - 1)
                    history.RemoveRange(historyIndex + 1, history.Count - historyIndex - 1);

                // Добавляем новый путь, если он не повторяет последний элемент истории.
                if (history.Count == 0 || history[historyIndex] != value)
                {
                    history.Add(value);
                    historyIndex = history.Count - 1;
                }

                currentPath = value;
                PathChanged?.Invoke(currentPath);
                NavigationStateChanged?.Invoke();
            }
        }

        /// <summary>
        /// Возвращает true, если возможно выполнить переход назад.
        /// </summary>
        public bool CanGoBack => historyIndex > 0;

        /// <summary>
        /// Возвращает true, если возможно выполнить переход вперёд.
        /// </summary>
        public bool CanGoForward => historyIndex < history.Count - 1;

        /// <summary>
        /// Выполняет переход на один шаг назад в истории.
        /// </summary>
        public void GoBack()
        {
            if (!CanGoBack) return;
            historyIndex--;
            currentPath = history[historyIndex];
            PathChanged?.Invoke(currentPath);
            NavigationStateChanged?.Invoke();
        }

        /// <summary>
        /// Выполняет переход на один шаг вперёд в истории.
        /// </summary>
        public void GoForward()
        {
            if (!CanGoForward) return;
            historyIndex++;
            currentPath = history[historyIndex];
            PathChanged?.Invoke(currentPath);
            NavigationStateChanged?.Invoke();
        }

        /// <summary>
        /// Принудительно вызывает событие изменения пути, заставляя интерфейс обновиться.
        /// </summary>
        public void Refresh()
        {
            PathChanged?.Invoke(currentPath);
        }

        /// <summary>
        /// Возвращает текущий путь из истории без изменения состояния.
        /// </summary>
        /// <returns>Текущий путь или null.</returns>
        public string? GetCurrentPathFromHistory() => historyIndex >= 0 ? history[historyIndex] : null;
    }
}