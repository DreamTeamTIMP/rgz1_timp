using Microsoft.VisualBasic.FileIO;
using rgz1_timp.Services.rgz1_timp.Services;

namespace rgz1_timp.Command
{
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
}