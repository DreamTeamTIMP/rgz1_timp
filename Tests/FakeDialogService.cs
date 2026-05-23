using rgz1_timp.Services.rgz1_timp.Services;

namespace Tests
{
    class FakeDialogService : IDialogService
    {
        public bool ConfirmResult { get; set; } = true;
        public bool ConfirmOverwriteResult { get; set; } = true;

        public bool Confirm(string message, string title) => ConfirmResult;
        public void ShowError(string message) { }
        public void ShowInfo(string message) { }
        public bool ConfirmOverwrite(string source, string destination) => ConfirmOverwriteResult;
    }
}
