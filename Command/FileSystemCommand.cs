using Microsoft.VisualBasic.FileIO;

namespace rgz1_timp.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public abstract class FileSystemCommand : ICommand
    {
        protected string _sourcePath;
        protected string? _backupPath; // для временного хранения при удалении/перемещении

        protected FileSystemCommand(string sourcePath)
        {
            _sourcePath = sourcePath;
        }

        public abstract void Undo();
        public abstract void Execute();
    }
    public class NewFolderCommand : FileSystemCommand
    {
        private readonly string _parentPath;
        private string _folderName;
        private string _fullPath;

        public NewFolderCommand(string parentPath, string folderName) : base(parentPath)
        {
            _parentPath = parentPath;
            _folderName = folderName;
            _fullPath = Path.Combine(parentPath, folderName);
        }

        public override void Execute()
        {
            if (!Directory.Exists(_fullPath))
                Directory.CreateDirectory(_fullPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(_fullPath))
                Directory.Delete(_fullPath);
        }

    }
    public class RenameCommand : FileSystemCommand
    {
        private readonly string _newName;
        private string _oldName;
        private string _parentDir;
        private string _newPath;

        public RenameCommand(string oldFullPath, string newName) : base(oldFullPath)
        {
            _newName = newName;
            _oldName = Path.GetFileName(oldFullPath);
            _parentDir = Path.GetDirectoryName(oldFullPath)!;
            _newPath = Path.Combine(_parentDir, newName);
        }

        public override void Execute()
        {
            if (Directory.Exists(_sourcePath))
                Directory.Move(_sourcePath, _newPath);
            else if (File.Exists(_sourcePath))
                File.Move(_sourcePath, _newPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(_newPath))
                Directory.Move(_newPath, _sourcePath);
            else if (File.Exists(_newPath))
                File.Move(_newPath, _sourcePath);
        }
    }
        public class DeleteCommand : FileSystemCommand
    {
        private string _backupPath;
        private bool _isDirectory;

        public DeleteCommand(string source) : base(source)
        {
            string temp = Path.GetTempPath();
            _backupPath = Path.Combine(temp, Guid.NewGuid().ToString());
        }

        public override void Execute()
        {
            if (Directory.Exists(_sourcePath))
            {
                FileSystem.DeleteDirectory(_sourcePath, UIOption.AllDialogs,RecycleOption.SendToRecycleBin);
            }
            else if (File.Exists(_sourcePath))
            {
                FileSystem.DeleteFile(_sourcePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
        }

        public override void Undo()
        {

            //if (_isDirectory && Directory.Exists(_backupPath))
            //    Directory.Move(_backupPath, _sourcePath);
            //else if (File.Exists(_backupPath))
            //    File.Move(_backupPath, _sourcePath);
        }
    }
        public class MoveCommand : FileSystemCommand
    {
        private readonly string _destination;
        private string? _tempBackup; // если нужен бэкап перед перемещением (не обязательно)

        public MoveCommand(string source, string destination) : base(source)
        {
            _destination = destination;
        }

        public override void Execute()
        {
            if (Directory.Exists(_sourcePath))
                Directory.Move(_sourcePath, _destination);
            else if (File.Exists(_sourcePath))
                File.Move(_sourcePath, _destination);
        }

        public override void Undo()
        {
            // Перемещаем обратно
            if (Directory.Exists(_destination))
                Directory.Move(_destination, _sourcePath);
            else if (File.Exists(_destination))
                File.Move(_destination, _sourcePath);
        }
    }

    public class CreateFileCommand : ICommand
    {
        private readonly string _parentPath;
        private string _fileName;
        private string _fullPath;
        public CreateFileCommand(string parentPath, string fileName)
        {
            _parentPath = parentPath;
            _fileName = fileName;
            _fullPath = Path.Combine(parentPath, fileName);
        }
        public void Execute()
        {
            if (!File.Exists(_fullPath))
                File.WriteAllText(_fullPath, "");
        }
        public void Undo()
        {
            if (File.Exists(_fullPath))
                File.Delete(_fullPath);
        }
        public string Description => $"Создание файла '{_fileName}'";
    }


    internal class CopyCommand : FileSystemCommand
    {
        private readonly string _destination;
        private bool _isDirectory;

        public CopyCommand(string source, string destination) : base(source)
        {
            _destination = destination;
        }

        public override void Execute()
        {
            if (Directory.Exists(_sourcePath))
            {
                _isDirectory = true;
                CopyDirectory(_sourcePath, _destination);
            }
            else if (File.Exists(_sourcePath))
            {
                _isDirectory = false;
                File.Copy(_sourcePath, _destination, overwrite: true);
            }
        }

        public override void Undo()
        {
            // Удаляем скопированное
            if (Directory.Exists(_destination))
                Directory.Delete(_destination, recursive: true);
            else if (File.Exists(_destination))
                File.Delete(_destination);
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
        private static Stack<ICommand> _undoStack = new Stack<ICommand>();
        private static Stack<ICommand> _redoStack = new Stack<ICommand>();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear(); // новая команда стирает Redo
        }

        public static void Undo()
        {
            if (_undoStack.Count > 0)
            {
                ICommand cmd = _undoStack.Pop();
                cmd.Undo();
                _redoStack.Push(cmd);
            }
        }

        public static void Redo()
        {
            if (_redoStack.Count > 0)
            {
                ICommand cmd = _redoStack.Pop();
                cmd.Execute();
                _undoStack.Push(cmd);
            }
        }

        public static bool CanUndo => _undoStack.Count > 0;
        public static bool CanRedo => _redoStack.Count > 0;
    }
}