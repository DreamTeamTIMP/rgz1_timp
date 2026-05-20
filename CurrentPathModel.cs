namespace rgz1_timp
{
    public class CurrentPathModel
    {
        private string? _currentPath;
        private List<string> _history = new List<string>();
        private int _historyIndex = -1;

        // Событие, когда путь изменился (история может не меняться)
        public event Action<string?> PathChanged;

        // Событие для обновления состояния кнопок (можно отдельно)
        public event Action? NavigationStateChanged;

        public string? Path
        {
            get => _currentPath;
            set
            {
                if (_currentPath == value) return;
                if (string.IsNullOrEmpty(value)) return;

                // --- Логика добавления в историю ---
                // Если мы не в конце истории (был переход Назад, а потом новое место), удаляем хвост
                if (_historyIndex < _history.Count - 1)
                    _history.RemoveRange(_historyIndex + 1, _history.Count - _historyIndex - 1);

                // Не добавляем дубликат подряд
                if (_history.Count == 0 || _history[_historyIndex] != value)
                {
                    _history.Add(value);
                    _historyIndex = _history.Count - 1;
                }

                _currentPath = value;
                PathChanged?.Invoke(_currentPath);
                NavigationStateChanged?.Invoke();
            }
        }

        // Свойства для проверки возможности навигации
        public bool CanGoBack => _historyIndex > 0;
        public bool CanGoForward => _historyIndex < _history.Count - 1;

        // Методы для навигации (они не вызывают PathChanged повторно,
        // а напрямую меняют _currentPath и индекс, потом вручную вызывают событие)
        public void GoBack()
        {
            if (!CanGoBack) return;
            _historyIndex--;
            _currentPath = _history[_historyIndex];
            PathChanged?.Invoke(_currentPath);
            NavigationStateChanged?.Invoke();
        }

        public void GoForward()
        {
            if (!CanGoForward) return;
            _historyIndex++;
            _currentPath = _history[_historyIndex];
            PathChanged?.Invoke(_currentPath);
            NavigationStateChanged?.Invoke();
        }

        // CurrentPathModel.cs
        public void Refresh()
        {
            // Вызываем событие с текущим путём, чтобы UI перерисовался
            PathChanged?.Invoke(_currentPath);
        }

        // Метод для получения текущего пути из истории (без изменения)
        public string? GetCurrentPathFromHistory() => _historyIndex >= 0 ? _history[_historyIndex] : null;
    }
}