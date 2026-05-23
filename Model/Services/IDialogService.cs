namespace rgz1_timp.Services
{
    namespace rgz1_timp.Services
    {
        public interface IDialogService
        {
            bool Confirm(string message, string title);
            void ShowError(string message);
            void ShowInfo(string message);
            bool ConfirmOverwrite(string source, string destination);
        }
    }
}