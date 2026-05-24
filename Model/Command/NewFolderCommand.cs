namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда создания новой папки в указанной родительской директории.
    /// Поддерживает отмену (удаление созданной папки).
    /// </summary>
    public class NewFolderCommand : FileSystemCommand
    {
        // Путь, в котором создаётся папка.
        private readonly string parentPath;
        // Имя создаваемой папки.
        private string folderName;
        // Полный путь к новой папке.
        private string fullPath;              

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parentPath">Путь родительской папки.</param>
        /// <param name="folderName">Имя новой папки.</param>
        public NewFolderCommand(string parentPath, string folderName) : base(parentPath)
        {
            this.parentPath = parentPath;
            this.folderName = folderName;
            fullPath = Path.Combine(parentPath, folderName);
        }

        /// <summary>
        /// Создаёт папку, если она ещё не существует.
        /// </summary>
        public override void Execute()
        {
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
        }

        /// <summary>
        /// Удаляет созданную папку (отмена операции).
        /// </summary>
        public override void Undo()
        {
            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath);
        }
    }
}