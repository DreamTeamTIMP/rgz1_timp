namespace rgz1_timp.Command
{
    public class CreateFileCommand : ICommand
    {
        private readonly string parentPath;
        private string fileName;
        private string fullPath;
        public CreateFileCommand(string parentPath, string fileName)
        {
            this.parentPath = parentPath;
            this.fileName = fileName;
            fullPath = Path.Combine(parentPath, fileName);
        }
        public void Execute()
        {
            if (!File.Exists(fullPath))
                File.WriteAllText(fullPath, "");
        }
        public void Undo()
        {
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}