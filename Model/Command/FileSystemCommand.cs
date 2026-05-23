using Microsoft.VisualBasic.FileIO;
using rgz1_timp.Services.rgz1_timp.Services;

namespace rgz1_timp.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public abstract class FileSystemCommand : ICommand
    {
        protected string sourcePath;
        protected string? backupPath; // для временного хранения при удалении/перемещении

        protected FileSystemCommand(string sourcePath)
        {
            this.sourcePath = sourcePath;
        }

        public abstract void Undo();
        public abstract void Execute();
    }
    public class NewFolderCommand : FileSystemCommand
    {
        private readonly string parentPath;
        private string folderName;
        private string fullPath;

        public NewFolderCommand(string parentPath, string folderName) : base(parentPath)
        {
            this.parentPath = parentPath;
            this.folderName = folderName;
            fullPath = Path.Combine(parentPath, folderName);
        }

        public override void Execute()
        {
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath);
        }

    }
    public class RenameCommand : FileSystemCommand
    {
        private readonly string newName;
        private string oldName;
        private string parentDir;
        private string newPath;

        public RenameCommand(string oldFullPath, string newName) : base(oldFullPath)
        {
            this.newName = newName;
            oldName = Path.GetFileName(oldFullPath);
            parentDir = Path.GetDirectoryName(oldFullPath)!;
            newPath = Path.Combine(parentDir, newName);
        }

        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                Directory.Move(sourcePath, newPath);
            else if (File.Exists(sourcePath))
                File.Move(sourcePath, newPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(newPath))
                Directory.Move(newPath, sourcePath);
            else if (File.Exists(newPath))
                File.Move(newPath, sourcePath);
        }
    }
    public class DeleteCommand : ICommand
    {
        private string sourcePath;
        private bool isDirectory;
        private IDialogService dialog;

        public DeleteCommand(string source, IDialogService dialog)
        {
            sourcePath = source;
            isDirectory = Directory.Exists(source);
            this.dialog = dialog;
        }

        public void Execute()
        {
            if (isDirectory)
            {
                // Показывает диалог подтверждения и отправляет в корзину
                FileSystem.DeleteDirectory(sourcePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                FileSystem.DeleteFile(sourcePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
        }

        public void Undo()
        {
            dialog.ShowInfo("Отмена удаления из корзины недоступна.");
        }
    }
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
    public static class CommandInvoker
    {
        private static Stack<ICommand> undoStack = new Stack<ICommand>();
        private static Stack<ICommand> redoStack = new Stack<ICommand>();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear(); // новая команда стирает Redo
        }

        public static void Undo()
        {
            if (undoStack.Count > 0)
            {
                ICommand cmd = undoStack.Pop();
                cmd.Undo();
                redoStack.Push(cmd);
            }
        }

        public static void Redo()
        {
            if (redoStack.Count > 0)
            {
                ICommand cmd = redoStack.Pop();
                cmd.Execute();
                undoStack.Push(cmd);
            }
        }

        public static void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
        public static bool CanUndo => undoStack.Count > 0;
        public static bool CanRedo => redoStack.Count > 0;
    }
}