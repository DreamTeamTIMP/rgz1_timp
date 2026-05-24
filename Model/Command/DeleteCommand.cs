using Microsoft.VisualBasic.FileIO;
using rgz1_timp.Services.rgz1_timp.Services;

namespace rgz1_timp.Command
{
    /// <summary>
    /// Команда удаления файла или папки с отправкой в Корзину.
    /// Отмена операции недоступна (выводится информационное сообщение).
    /// </summary>
    public class DeleteCommand : ICommand
    {
        // Путь к удаляемому объекту.
        private string sourcePath;
        // Флаг: папка или файл.
        private bool isDirectory;
        // Сервис для диалогов.
        private IDialogService dialog;   

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="source">Путь к удаляемому объекту.</param>
        /// <param name="dialog">Сервис диалогов.</param>
        public DeleteCommand(string source, IDialogService dialog)
        {
            sourcePath = source;
            isDirectory = Directory.Exists(source);
            this.dialog = dialog;
        }

        /// <summary>
        /// Выполняет удаление через FileSystem с отображением стандартного диалога подтверждения.
        /// Объект отправляется в Корзину, а не удаляется безвозвратно.
        /// </summary>
        public void Execute()
        {
            if (isDirectory)
            {
                // Вызов системного диалога с возможностью отмены и отправкой в корзину.
                FileSystem.DeleteDirectory(sourcePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                FileSystem.DeleteFile(sourcePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
        }

        /// <summary>
        /// Отмена удаления. Реализована только информационным сообщением,
        /// так как восстановление из Корзины программно не поддерживается.
        /// </summary>
        public void Undo()
        {
            dialog.ShowInfo("Отмена удаления из корзины недоступна.");
        }
    }
}