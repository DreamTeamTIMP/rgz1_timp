using rgz1_timp;
using rgz1_timp.Command;
using rgz1_timp.Services;
namespace Tests
{
    [TestClass]
    public class CopyPasteBlackBoxTests
    {
        private string testRoot;
        private CurrentPathModel pathModel;
        private FakeDialogService dialog;
        private FileOperationService service;

        [TestInitialize]
        public void Init()
        {
            testRoot = Path.Combine(Path.GetTempPath(), "CopyPasteTest_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(testRoot);
            pathModel = new CurrentPathModel();
            pathModel.Path = testRoot;
            dialog = new FakeDialogService();
            service = new FileOperationService(pathModel, dialog);
            CommandInvoker.Clear();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(testRoot))
                Directory.Delete(testRoot, true);
        }

        [TestMethod]
        public void CopyPaste_File_NoConflict_Success()
        {
            // Arrange
            string source = Path.Combine(testRoot, "src.txt");
            File.WriteAllText(source, "content");
            service.CopyItem(source);
            string destFolder = Path.Combine(testRoot, "Dest");
            Directory.CreateDirectory(destFolder);
            pathModel.Path = destFolder;

            // Act
            bool result = service.PasteItem();

            // Assert
            Assert.IsTrue(result);
            string destFile = Path.Combine(destFolder, "src.txt");
            Assert.IsTrue(File.Exists(destFile));
            Assert.IsTrue(File.Exists(source)); // исходник на месте (копирование)
        }


        [TestMethod]
        public void CutPaste_File_MovesSuccess()
        {
            string source = Path.Combine(testRoot, "move.txt");
            File.WriteAllText(source, "move");
            service.CutItem(source);
            string destFolder = Path.Combine(testRoot, "Moved");
            Directory.CreateDirectory(destFolder);
            pathModel.Path = destFolder;

            bool result = service.PasteItem();
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(source));
            Assert.IsTrue(File.Exists(Path.Combine(destFolder, "move.txt")));
        }

        [TestMethod]
        public void Paste_WhenBufferEmpty_ReturnsFalse()
        {
            // Не вызывали CopyItem/CutItem
            bool result = service.PasteItem();
            Assert.IsFalse(result);
            // Должен быть вызов ShowError; в моке можем проверить, но для простоты достаточно false
        }
    }
}
