using rgz1_timp.Services.rgz1_timp.Services;

namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда копирования файла или папки в указанное место назначения.
    /// Поддерживает отмену (удаление скопированного объекта).
    /// </summary>
    internal class CopyCommand : FileSystemCommand
    {
        // Путь назначения.
        private readonly string destination;
        // Флаг: true – копируется папка, false – файл.
        private bool isDirectory;
        // Сервис для диалогов (подтверждение перезаписи).
        private IDialogService dialog;         

        /// <summary>
        /// Конструктор команды копирования.
        /// </summary>
        /// <param name="source">Исходный путь.</param>
        /// <param name="destination">Путь назначения.</param>
        /// <param name="dialog">Сервис диалогов.</param>
        public CopyCommand(string source, string destination, IDialogService dialog) : base(source)
        {
            this.destination = destination;
            this.dialog = dialog;
        }

        /// <summary>
        /// Выполняет копирование. Если целевой объект существует, запрашивает подтверждение перезаписи.
        /// </summary>
        public override void Execute()
        {
            // Копирование папки.
            if (Directory.Exists(sourcePath))
            {
                // Если папка назначения уже существует, спрашиваем разрешение на перезапись.
                if (Directory.Exists(destination))
                {
                    bool overwrite = dialog.ConfirmOverwrite(sourcePath, destination);
                    if (!overwrite) return;
                    Directory.Delete(destination, recursive: true);
                }
                isDirectory = true;
                CopyDirectory(sourcePath, destination);
            }
            // Копирование файла.
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

        /// <summary>
        /// Отмена копирования – удаляет скопированный объект.
        /// </summary>
        public override void Undo()
        {
            if (Directory.Exists(destination))
                Directory.Delete(destination, recursive: true);
            else if (File.Exists(destination))
                File.Delete(destination);
        }

        /// <summary>
        /// Рекурсивное копирование содержимого папки.
        /// </summary>
        /// <param name="sourceDir">Исходная папка.</param>
        /// <param name="destDir">Целевая папка.</param>
        private static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            // Копирование файлов текущего уровня.
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }
            // Рекурсивный обход подпапок.
            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}