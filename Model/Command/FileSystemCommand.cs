namespace rgz1_timp.Command
{
    /// <summary>
    /// Абстрактный базовый класс для команд, работающих с файловой системой.
    /// Содержит общий путь к источнику и поле для резервной копии (не используется во всех наследниках).
    /// </summary>
    public abstract class FileSystemCommand : ICommand
    {
        /// <summary>
        /// Полный путь к исходному файлу или папке.
        /// </summary>
        protected string sourcePath;

        /// <summary>
        /// Путь для временного хранения резервной копии (например, при перемещении/удалении).
        /// </summary>
        protected string? backupPath;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sourcePath">Путь к исходному объекту.</param>
        protected FileSystemCommand(string sourcePath)
        {
            this.sourcePath = sourcePath;
        }

        /// <summary>
        /// Отмена операции (должна быть реализована в наследниках).
        /// </summary>
        public abstract void Undo();

        /// <summary>
        /// Выполнение операции (должно быть реализовано в наследниках).
        /// </summary>
        public abstract void Execute();
    }
}