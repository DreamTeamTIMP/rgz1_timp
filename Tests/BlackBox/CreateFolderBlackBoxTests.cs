using rgz1_timp;
using rgz1_timp.Command;
using rgz1_timp.Services;
namespace Tests
{
    [TestClass]
    public class CreateFolderBlackBoxTests
    {
        private string testRoot;
        private CurrentPathModel pathModel;
        private FakeDialogService dialog;
        private FileOperationService service;

        [TestInitialize]
        public void Init()
        {
            testRoot = Path.Combine(Path.GetTempPath(), "CreateFolderTest_" + Guid.NewGuid().ToString());
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
        public void CreateFolder_InWritableDirectory_Success()
        {
            bool result = service.CreateNewFolder();
            Assert.IsTrue(result);
            string newFolder = Path.Combine(testRoot, "Новая папка");
            Assert.IsTrue(Directory.Exists(newFolder));
        }

        [TestMethod]
        public void CreateFolder_WhenNameConflict_AddsNumber()
        {
            Directory.CreateDirectory(Path.Combine(testRoot, "Новая папка"));
            bool result = service.CreateNewFolder();
            Assert.IsTrue(result);
            string secondFolder = Path.Combine(testRoot, "Новая папка (2)");
            Assert.IsTrue(Directory.Exists(secondFolder));
        }

        [TestMethod]
        public void CreateFolder_WhenCurrentPathIsSpecialObject_Fails()
        {
            pathModel.Path = "Этот компьютер";
            bool result = service.CreateNewFolder();
            Assert.IsFalse(result);
        }
    }
}
