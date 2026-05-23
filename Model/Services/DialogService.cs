namespace rgz1_timp.Services
{
    namespace rgz1_timp.Services
    {
        public class DialogService : IDialogService
        {
            public bool Confirm(string message, string title)
            {
                return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            public void ShowError(string message)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            public void ShowInfo(string message)
            {
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            public bool ConfirmOverwrite(string source, string destination)
            {
                string msg = $"Файл или папка уже существует:\n{destination}\n\nЗаменить его?";
                return MessageBox.Show(msg, "Подтверждение перезаписи", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;
            }
        }
    }
}