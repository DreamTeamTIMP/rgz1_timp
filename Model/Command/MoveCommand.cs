namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда перемещения (вырезания) файла или папки.
    /// Поддерживает отмену – перемещение обратно.
    /// </summary>
    public class MoveCommand : FileSystemCommand
    {
        // Новый путь объекта.
        private readonly string destination;   

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="source">Исходный путь.</param>
        /// <param name="destination">Целевой путь.</param>
        public MoveCommand(string source, string destination) : base(source)
        {
            this.destination = destination;
        }

        /// <summary>
        /// Выполняет перемещение (переименование) средствами .NET.
        /// </summary>
        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                Directory.Move(sourcePath, destination);
            else if (File.Exists(sourcePath))
                File.Move(sourcePath, destination);
        }

        /// <summary>
        /// Отменяет перемещение – возвращает объект на исходный путь.
        /// </summary>
        public override void Undo()
        {
            if (Directory.Exists(destination))
                Directory.Move(destination, sourcePath);
            else if (File.Exists(destination))
                File.Move(destination, sourcePath);
        }
    }
}