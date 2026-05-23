using rgz1_timp.Command;
using Microsoft.VisualBasic.FileIO;
using System.IO;

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
            if (path != null) copiedPath = path;
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
            if (string.IsNullOrEmpty(copiedPath))
            {
                ShowError("Буфер обмена пуст.");
                return false;
            }

            string destDir = pathModel.Path!;
            if (string.IsNullOrEmpty(destDir) || !Directory.Exists(destDir))
            {
                ShowError("Целевая папка не существует.");
                return false;
            }

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
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir))
            {
                ShowError("Текущая папка недоступна.");
                return false;
            }

            string newFolderName = GetUniqueFolderName(currentDir);
            if (!FileNameValidator.IsValidFileName(newFolderName))
            {
                ShowError(FileNameValidator.GetErrorMessage(newFolderName) ?? "Недопустимое имя папки.");
                return false;
            }

            try
            {
                var command = new NewFolderCommand(currentDir, newFolderName);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Не удалось создать папку: {ex.Message}");
                return false;
            }
        }

        //  Создание файла 
        public bool CreateNewFile()
        {
            string currentDir = pathModel.Path!;
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir))
            {
                ShowError("Текущая папка недоступна.");
                return false;
            }

            string baseName = "Новый текстовый документ.txt";
            string fileName = baseName;
            int counter = 1;
            while (File.Exists(Path.Combine(currentDir, fileName)))
            {
                fileName = $"Новый текстовый документ ({counter}).txt";
                counter++;
            }

            if (!FileNameValidator.IsValidFileName(fileName))
            {
                ShowError(FileNameValidator.GetErrorMessage(fileName) ?? "Недопустимое имя файла.");
                return false;
            }

            try
            {
                var command = new CreateFileCommand(currentDir, fileName);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Не удалось создать файл: {ex.Message}");
                return false;
            }
        }

        //  Удаление 
        public bool DeleteItem(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                ShowError("Ничего не выбрано.");
                return false;
            }

            try
            {
                var command = new DeleteCommand(path);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (OperationCanceledException)
            {
                // Пользователь отменил удаление в диалоге – не ошибка
                return false;
            }
            catch (Exception ex)
            {
                ShowError($"Не удалось удалить: {ex.Message}");
                return false;
            }
        }

        //  Переименование 
        public bool RenameItem(string? oldPath, string newName)
        {
            if (string.IsNullOrEmpty(oldPath))
            {
                ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(newName) || newName == Path.GetFileName(oldPath))
                return false;

            if (!FileNameValidator.IsValidFileName(newName))
            {
                ShowError(FileNameValidator.GetErrorMessage(newName) ?? "Недопустимое имя.");
                return false;
            }

            try
            {
                var command = new RenameCommand(oldPath, newName);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Не удалось переименовать: {ex.Message}");
                return false;
            }
        }

        //  Переместить в диалог 
        public bool MoveToFolder(string? sourcePath, string destinationDir)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(destinationDir) || !Directory.Exists(destinationDir))
            {
                ShowError("Папка назначения не существует.");
                return false;
            }
            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                ShowError("Нельзя переместить папку в саму себя.");
                return false;
            }

            string destPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
            try
            {
                var command = new MoveCommand(sourcePath, destPath);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка перемещения: {ex.Message}");
                return false;
            }
        }

        //  Копировать в диалог 
        public bool CopyToFolder(string? sourcePath, string destinationDir)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(destinationDir) || !Directory.Exists(destinationDir))
            {
                ShowError("Папка назначения не существует.");
                return false;
            }
            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                ShowError("Нельзя скопировать папку в саму себя.");
                return false;
            }

            string destPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
            try
            {
                var command = new CopyCommand(sourcePath, destPath);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка копирования: {ex.Message}");
                return false;
            }
        }

        //  Копировать путь в буфер 
        public void CopyPathToClipboard(string? path)
        {
            if (!string.IsNullOrEmpty(path))
                Clipboard.SetText(path);
        }

        //  Приватные вспомогательные методы 
        private string GetUniqueFolderName(string parentPath)
        {
            string baseName = "Новая папка";
            string folderName = baseName;
            int counter = 1;
            while (Directory.Exists(Path.Combine(parentPath, folderName)))
            {
                counter++;
                folderName = $"{baseName} ({counter})";
                if (counter > 1000) break; // защита от бесконечного цикла
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

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}