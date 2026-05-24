using Microsoft.VisualBasic.FileIO;
using rgz1_timp.Command;
using rgz1_timp.Services.rgz1_timp.Services;
using System.IO;

namespace rgz1_timp.Services
{
    /// <summary>
    /// Сервис, предоставляющий методы для выполнения операций с файловой системой (копирование, вставка, создание, удаление и т.д.).
    /// Инкапсулирует логику работы с буфером обмена и командами.
    /// </summary>
    public class FileOperationService
    {
        // Модель текущего пути.
        private readonly CurrentPathModel pathModel;
        // Сервис диалогов.
        private readonly IDialogService dialog;
        // Путь скопированного/вырезанного объекта.
        private string? copiedPath;
        // True – операция вырезания, false – копирования.
        private bool isCut;                           

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pathModel">Модель текущего пути.</param>
        /// <param name="dialog">Сервис диалогов.</param>
        public FileOperationService(CurrentPathModel pathModel, IDialogService dialog)
        {
            this.pathModel = pathModel;
            this.dialog = dialog;
        }

        /// <summary>
        /// Копирует указанный путь во внутренний буфер.
        /// </summary>
        /// <param name="path">Путь к объекту.</param>
        public void CopyItem(string? path)
        {
            if (path != null) copiedPath = path;
        }

        /// <summary>
        /// Вырезает указанный объект (помещает в буфер с флагом перемещения).
        /// </summary>
        /// <param name="path">Путь к объекту.</param>
        public void CutItem(string? path)
        {
            if (path != null)
            {
                copiedPath = path;
                isCut = true;
            }
        }

        /// <summary>
        /// Вставляет объект из буфера в текущую папку.
        /// </summary>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool PasteItem()
        {
            // Проверка, что буфер не пуст.
            if (string.IsNullOrEmpty(copiedPath))
            {
                dialog.ShowError("Буфер обмена пуст.");
                return false;
            }

            string destDir = pathModel.Path!;
            // Проверка существования целевой папки.
            if (string.IsNullOrEmpty(destDir) || !Directory.Exists(destDir))
            {
                dialog.ShowError("Целевая папка не существует.");
                return false;
            }

            // Защита от циклического перемещения/копирования папки в саму себя.
            if (IsCyclicOperation(copiedPath, destDir))
            {
                dialog.ShowError("Нельзя вставить папку в саму себя или в её подпапку.");
                return false;
            }

            string destPath = Path.Combine(destDir, Path.GetFileName(copiedPath));
            ICommand command = isCut
                ? new MoveCommand(copiedPath, destPath)
                : new CopyCommand(copiedPath, destPath, dialog);

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
                dialog.ShowError($"Ошибка при вставке: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Создаёт новую папку в текущем каталоге с автоматическим именем (Новая папка, Новая папка (2) и т.д.).
        /// </summary>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool CreateNewFolder()
        {
            string currentDir = pathModel.Path!;
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir))
            {
                dialog.ShowError("Текущая папка недоступна.");
                return false;
            }

            string newFolderName = GetUniqueFolderName(currentDir);
            if (!FileNameValidator.IsValidFileName(newFolderName))
            {
                dialog.ShowError(FileNameValidator.GetErrorMessage(newFolderName) ?? "Недопустимое имя папки.");
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
                dialog.ShowError($"Не удалось создать папку: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Создаёт новый текстовый файл в текущем каталоге с автоматическим именем.
        /// </summary>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool CreateNewFile()
        {
            string currentDir = pathModel.Path!;
            if (string.IsNullOrEmpty(currentDir) || !Directory.Exists(currentDir))
            {
                dialog.ShowError("Текущая папка недоступна.");
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
                dialog.ShowError(FileNameValidator.GetErrorMessage(fileName) ?? "Недопустимое имя файла.");
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
                dialog.ShowError($"Не удалось создать файл: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Удаляет указанный объект (с отправкой в Корзину).
        /// </summary>
        /// <param name="path">Путь к объекту.</param>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool DeleteItem(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                dialog.ShowError("Ничего не выбрано.");
                return false;
            }

            try
            {
                var command = new DeleteCommand(path, dialog);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (OperationCanceledException)
            {
                // Пользователь отменил удаление в диалоге – не ошибка.
                return false;
            }
            catch (Exception ex)
            {
                dialog.ShowError($"Не удалось удалить: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Переименовывает файл или папку.
        /// </summary>
        /// <param name="oldPath">Текущий полный путь объекта.</param>
        /// <param name="newName">Новое имя (без пути).</param>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool RenameItem(string? oldPath, string newName)
        {
            if (string.IsNullOrEmpty(oldPath))
            {
                dialog.ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(newName) || newName == Path.GetFileName(oldPath))
                return false;

            if (!FileNameValidator.IsValidFileName(newName))
            {
                dialog.ShowError(FileNameValidator.GetErrorMessage(newName) ?? "Недопустимое имя.");
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
                dialog.ShowError($"Не удалось переименовать: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Перемещает объект в указанную папку.
        /// </summary>
        /// <param name="sourcePath">Исходный путь объекта.</param>
        /// <param name="destinationDir">Целевая папка.</param>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool MoveToFolder(string? sourcePath, string destinationDir)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                dialog.ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(destinationDir) || !Directory.Exists(destinationDir))
            {
                dialog.ShowError("Папка назначения не существует.");
                return false;
            }
            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                dialog.ShowError("Нельзя переместить папку в саму себя.");
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
                dialog.ShowError($"Ошибка перемещения: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Копирует объект в указанную папку.
        /// </summary>
        /// <param name="sourcePath">Исходный путь объекта.</param>
        /// <param name="destinationDir">Целевая папка.</param>
        /// <returns>True, если операция выполнена успешно.</returns>
        public bool CopyToFolder(string? sourcePath, string destinationDir)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                dialog.ShowError("Ничего не выбрано.");
                return false;
            }
            if (string.IsNullOrEmpty(destinationDir) || !Directory.Exists(destinationDir))
            {
                dialog.ShowError("Папка назначения не существует.");
                return false;
            }
            if (Directory.Exists(sourcePath) && IsCyclicOperation(sourcePath, destinationDir))
            {
                dialog.ShowError("Нельзя скопировать папку в саму себя.");
                return false;
            }

            string destPath = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
            try
            {
                var command = new CopyCommand(sourcePath, destPath, dialog);
                CommandInvoker.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                dialog.ShowError($"Ошибка копирования: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Копирует полный путь указанного объекта в Clipboard.
        /// </summary>
        /// <param name="path">Путь к объекту.</param>
        public void CopyPathToClipboard(string? path)
        {
            if (!string.IsNullOrEmpty(path))
                Clipboard.SetText(path);
        }

        /// <summary>
        /// Генерирует уникальное имя папки, добавляя числовой суффикс при конфликте.
        /// </summary>
        /// <param name="parentPath">Родительская папка.</param>
        /// <returns>Уникальное имя папки.</returns>
        private string GetUniqueFolderName(string parentPath)
        {
            string baseName = "Новая папка";
            string folderName = baseName;
            int counter = 1;
            while (Directory.Exists(Path.Combine(parentPath, folderName)))
            {
                counter++;
                folderName = $"{baseName} ({counter})";
                if (counter > 1000) break; // Защита от бесконечного цикла.
            }
            return folderName;
        }

        /// <summary>
        /// Проверяет, не является ли операция копирования/перемещения циклической (папка в саму себя или подпапку).
        /// </summary>
        /// <param name="sourcePath">Исходный путь.</param>
        /// <param name="destinationDir">Целевая папка.</param>
        /// <returns>True, если операция циклическая.</returns>
        private bool IsCyclicOperation(string sourcePath, string destinationDir)
        {
            if (!Directory.Exists(sourcePath)) return false;
            string sourceFull = Path.GetFullPath(sourcePath).TrimEnd(Path.DirectorySeparatorChar);
            string destFull = Path.GetFullPath(destinationDir).TrimEnd(Path.DirectorySeparatorChar);
            return destFull == sourceFull || destFull.StartsWith(sourceFull + Path.DirectorySeparatorChar);
        }
    }
}