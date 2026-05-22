using rgz1_timp.Command;
using rgz1_timp.Validators;

namespace rgz1_timp.Services
{
    public class FileOperationService
    {
        private readonly CurrentPathModel pathModel;
        private string? copiedPath;
        private bool isCut;

        public FileOperationService(CurrentPathModel pathModel)
        {
            this.pathModel = pathModel;
        }

        //  Буфер обмена 
        public void CopyItem(string? path)
        {
            if (path != null)
            {
                copiedPath = path;
                isCut = false;
            }
        }

        public void CutItem(string? path)
        {
            if (path != null)
            {
                copiedPath = path;
                isCut = true;
            }
        }

        public bool PasteItem()
        {
            if (string.IsNullOrEmpty(copiedPath)) return false;
            string destDir = pathModel.Path!;
            if (!Directory.Exists(destDir)) return false;

            if (IsCyclicOperation(copiedPath, destDir))
            {
                ShowError("Нельзя вставить папку в саму себя или в её подпапку.");
                return false;
            }

            string destPath = Path.Combine(destDir, Path.GetFileName(copiedPath));
            ICommand command = isCut
                ? new MoveCommand(copiedPath, destPath)
                : new CopyCommand(copiedPath, destPath);

            try
            {
                CommandInvoker.ExecuteCommand(command);
                if (isCut)
                {
                    copiedPath = null;
                    isCut = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при вставке: {ex.Message}");
                return false;
            }
        }

        //  Создание папки 
        public bool CreateNewFolder()
        {
            string currentDir = pathModel.Path!;
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir)) return false;

            string newFolderName = GetUniqueFolderName(currentDir);
            if (!IsValidFileName(newFolderName))
            {
                ShowError(FileNameValidator.GetErrorMessage(newFolderName) ?? "Недопустимое имя папки.");
                return false;
            }

            var command = new NewFolderCommand(currentDir, newFolderName);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Создание файла 
        public bool CreateNewFile()
        {
            string currentDir = pathModel.Path!;
            if (!Directory.Exists(currentDir)) return false;

            string baseName = "Новый текстовый документ.txt";
            string fileName = baseName;
            int counter = 1;

            while (File.Exists(Path.Combine(currentDir, fileName)))
            {
                fileName = $"Новый текстовый документ ({counter}).txt";
                counter++;
            }

            if (!IsValidFileName(fileName))
            {
                ShowError(FileNameValidator.GetErrorMessage(fileName) ?? "Недопустимое имя файла.");
                return false;
            }

            var command = new CreateFileCommand(currentDir, fileName);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Удаление 
        public bool DeleteItem(string? path)
        {
            if (path == null) return false;
            var command = new DeleteCommand(path);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Переименование 
        public bool RenameItem(string? oldPath, string newName)
        {
            if (oldPath == null) return false;
            if (string.IsNullOrEmpty(newName) || newName == Path.GetFileName(oldPath)) return false;

            if (!IsValidFileName(newName))
            {
                ShowError(FileNameValidator.GetErrorMessage(newName) ?? "Недопустимое имя.");
                return false;
            }

            var command = new RenameCommand(oldPath, newName);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Переместить в (диалог) 
        public bool MoveToFolder(string? sourcePath, string destinationDir)
        {
            if (sourcePath == null) return false;
            if (!Directory.Exists(destinationDir)) return false;

            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                ShowError("Нельзя переместить папку в саму себя.");
                return false;
            }

            string destPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
            var command = new MoveCommand(sourcePath, destPath);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Копировать в (диалог) 
        public bool CopyToFolder(string? sourcePath, string destinationDir)
        {
            if (sourcePath == null) return false;
            if (!Directory.Exists(destinationDir)) return false;

            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                ShowError("Нельзя скопировать папку в саму себя.");
                return false;
            }

            string destPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
            var command = new CopyCommand(sourcePath, destPath);
            CommandInvoker.ExecuteCommand(command);
            return true;
        }

        //  Копировать путь в буфер 
        public void CopyPathToClipboard(string? path)
        {
            if (path != null)
                Clipboard.SetText(path);
        }

        //  Вспомогательные методы 
        private string GetUniqueFolderName(string parentPath)
        {
            string baseName = "Новая папка";
            string folderName = baseName;
            int counter = 1;

            while (Directory.Exists(Path.Combine(parentPath, folderName)))
            {
                counter++;
                folderName = $"{baseName} ({counter})";
            }
            return folderName;
        }

        private bool IsCyclicOperation(string sourcePath, string destinationDir)
        {
            if (!Directory.Exists(sourcePath)) return false;

            string sourceFull = Path.GetFullPath(sourcePath).TrimEnd(Path.DirectorySeparatorChar);
            string destFull = Path.GetFullPath(destinationDir).TrimEnd(Path.DirectorySeparatorChar);

            return destFull == sourceFull || destFull.StartsWith(sourceFull + Path.DirectorySeparatorChar);
        }

        private bool IsValidFileName(string name) => FileNameValidator.IsValidFileName(name);

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}