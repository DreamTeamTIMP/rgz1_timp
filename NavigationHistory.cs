namespace rgz1_timp
{
    public class NavigationHistory
    {
        private List<string> _history = new List<string>();
        private int _currentIndex = -1;

        // Событие для уведомления об изменении состояния (Enabled для кнопок)
        public event System.Action? HistoryChanged;

        public bool CanGoBack => _currentIndex > 0;
        public bool CanGoForward => _currentIndex < _history.Count - 1;

        /// <summary>
        /// Добавляет новый путь в историю (если он отличается от текущего)
        /// </summary>
        public void AddPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            // Если мы не в конце истории (пользователь нажал "Назад" и потом зашёл в новую папку),
            // то обрезаем историю после текущего индекса.
            if (_currentIndex < _history.Count - 1)
                _history.RemoveRange(_currentIndex + 1, _history.Count - _currentIndex - 1);

            // Если последний добавленный путь совпадает с текущим – не дублируем
            if (_history.Count > 0 && _history[_currentIndex] == path)
                return;

            _history.Add(path);
            _currentIndex = _history.Count - 1;
            OnHistoryChanged();
        }

        public string? GoBack()
        {
            if (!CanGoBack) return null;
            _currentIndex--;
            OnHistoryChanged();
            return _history[_currentIndex];
        }

        public string? GoForward()
        {
            if (!CanGoForward) return null;
            _currentIndex++;
            OnHistoryChanged();
            return _history[_currentIndex];
        }

        public string? CurrentPath => _currentIndex >= 0 ? _history[_currentIndex] : null;

        public void Clear()
        {
            _history.Clear();
            _currentIndex = -1;
            OnHistoryChanged();
        }

        private void OnHistoryChanged() => HistoryChanged?.Invoke();
    }
}