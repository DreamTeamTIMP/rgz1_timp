namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда переименования файла или папки.
    /// Поддерживает отмену (возврат старого имени).
    /// </summary>
    public class RenameCommand : FileSystemCommand
    {
        // Новое имя (без пути).
        private readonly string newName;
        // Старое имя.
        private string oldName;
        // Родительская папка.
        private string parentDir;
        // Полный путь после переименования.
        private string newPath;              

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="oldFullPath">Полный путь к существующему объекту.</param>
        /// <param name="newName">Новое имя (только имя, без пути).</param>
        public RenameCommand(string oldFullPath, string newName) : base(oldFullPath)
        {
            this.newName = newName;
            oldName = Path.GetFileName(oldFullPath);
            parentDir = Path.GetDirectoryName(oldFullPath)!;
            newPath = Path.Combine(parentDir, newName);
        }

        /// <summary>
        /// Выполняет переименование (фактически – перемещение в той же папке).
        /// </summary>
        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                Directory.Move(sourcePath, newPath);
            else if (File.Exists(sourcePath))
                File.Move(sourcePath, newPath);
        }

        /// <summary>
        /// Отменяет переименование – возвращает исходное имя.
        /// </summary>
        public override void Undo()
        {
            if (Directory.Exists(newPath))
                Directory.Move(newPath, sourcePath);
            else if (File.Exists(newPath))
                File.Move(newPath, sourcePath);
        }
    }
}