using rgz1_timp.Services.rgz1_timp.Services;

namespace rgz1_timp.Command
{
    internal class CopyCommand : FileSystemCommand
    {
        private readonly string destination;
        private bool isDirectory;
        private IDialogService dialog;

        public CopyCommand(string source, string destination, IDialogService dialog) : base(source)
        {
            this.destination = destination;
            this.dialog = dialog;
        }

        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
            {
                if (Directory.Exists(destination))
                {
                    bool overwrite = dialog.ConfirmOverwrite(sourcePath, destination);
                    if (!overwrite) return;
                    Directory.Delete(destination, recursive: true);
                }
                isDirectory = true;
                CopyDirectory(sourcePath, destination);
            }
            else if (File.Exists(sourcePath))
            {
                isDirectory = false;
                if (File.Exists(destination))
                {
                    bool overwrite = dialog.ConfirmOverwrite(sourcePath, destination);
                    if (!overwrite) return;
                }
                File.Copy(sourcePath, destination, overwrite: true);
            }
        }
        public override void Undo()
        {
            // Удаляем скопированное
            if (Directory.Exists(destination))
                Directory.Delete(destination, recursive: true);
            else if (File.Exists(destination))
                File.Delete(destination);
        }

        private static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }
            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}