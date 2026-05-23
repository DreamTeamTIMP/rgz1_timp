namespace rgz1_timp
{
    public class CurrentPathModel
    {
        private string? currentPath;
        private List<string> history = new List<string>();
        private int historyIndex = -1;

        // Событие, когда путь изменился (история может не меняться)
        public event Action<string?> PathChanged;

        // Событие для обновления состояния кнопок (можно отдельно)
        public event Action? NavigationStateChanged;

        public string? Path
        {
            get => currentPath;
            set
            {
                if (currentPath == value) return;
                if (string.IsNullOrEmpty(value)) return;

                // --- Логика добавления в историю ---
                // Если мы не в конце истории (был переход Назад, а потом новое место), удаляем хвост
                if (historyIndex < history.Count - 1)
                    history.RemoveRange(historyIndex + 1, history.Count - historyIndex - 1);

                // Не добавляем дубликат подряд
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

        // Свойства для проверки возможности навигации
        public bool CanGoBack => historyIndex > 0;
        public bool CanGoForward => historyIndex < history.Count - 1;

        // Методы для навигации (они не вызывают PathChanged повторно,
        // а напрямую меняют _currentPath и индекс, потом вручную вызывают событие)
        public void GoBack()
        {
            if (!CanGoBack) return;
            historyIndex--;
            currentPath = history[historyIndex];
            PathChanged?.Invoke(currentPath);
            NavigationStateChanged?.Invoke();
        }

        public void GoForward()
        {
            if (!CanGoForward) return;
            historyIndex++;
            currentPath = history[historyIndex];
            PathChanged?.Invoke(currentPath);
            NavigationStateChanged?.Invoke();
        }

        // CurrentPathModel.cs
        public void Refresh()
        {
            // Вызываем событие с текущим путём, чтобы UI перерисовался
            PathChanged?.Invoke(currentPath);
        }

        // Метод для получения текущего пути из истории (без изменения)
        public string? GetCurrentPathFromHistory() => historyIndex >= 0 ? history[historyIndex] : null;
    }
}