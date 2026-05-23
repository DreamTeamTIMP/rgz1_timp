namespace rgz1_timp.Command
{
    public class MoveCommand : FileSystemCommand
    {
        private readonly string destination;

        public MoveCommand(string source, string destination) : base(source)
        {
            this.destination = destination;
        }

        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                Directory.Move(sourcePath, destination);
            else if (File.Exists(sourcePath))
                File.Move(sourcePath, destination);
        }

        public override void Undo()
        {
            // Перемещаем обратно
            if (Directory.Exists(destination))
                Directory.Move(destination, sourcePath);
            else if (File.Exists(destination))
                File.Move(destination, sourcePath);
        }
    }
}