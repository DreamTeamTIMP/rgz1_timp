namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда создания нового пустого текстового файла.
    /// Поддерживает отмену (удаление созданного файла).
    /// </summary>
    public class CreateFileCommand : ICommand
    {
        // Путь к родительской папке.
        private readonly string parentPath;
        // Имя файла.
        private string fileName;
        // Полный путь к файлу.
        private string fullPath;              

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="parentPath">Родительская папка.</param>
        /// <param name="fileName">Имя создаваемого файла.</param>
        public CreateFileCommand(string parentPath, string fileName)
        {
            this.parentPath = parentPath;
            this.fileName = fileName;
            fullPath = Path.Combine(parentPath, fileName);
        }

        /// <summary>
        /// Создаёт пустой файл, если он не существует.
        /// </summary>
        public void Execute()
        {
            if (!File.Exists(fullPath))
                // Создаём файл с пустым содержимым.
                File.WriteAllText(fullPath, "");   
        }

        /// <summary>
        /// Удаляет созданный файл (отмена операции).
        /// </summary>
        public void Undo()
        {
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}