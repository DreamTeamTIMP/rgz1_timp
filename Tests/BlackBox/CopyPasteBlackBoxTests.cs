using rgz1_timp;
using rgz1_timp.Command;
using rgz1_timp.Services;

namespace Tests
{
    /// <summary>
    /// Тестирование операций копирования/вырезания/вставки.
    /// </summary>
    [TestClass]
    public class CopyPasteBlackBoxTests
    {
        // Временная корневая папка для тестов.
        private string testRoot;
        // Модель навигации.
        private CurrentPathModel pathModel;
        // Поддельный сервис диалогов.
        private FakeDialogService dialog;
        // Тестируемый сервис операций.
        private FileOperationService service;        

        [TestInitialize]
        public void Init()
        {
            // Создаём временную папку с уникальным именем.
            testRoot = Path.Combine(Path.GetTempPath(), "CopyPasteTest_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(testRoot);
            pathModel = new CurrentPathModel();
            pathModel.Path = testRoot;
            dialog = new FakeDialogService();
            service = new FileOperationService(pathModel, dialog);
            // Очищаем историю команд перед каждым тестом.
            CommandInvoker.Clear();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Удаляем временную папку со всем содержимым.
            if (Directory.Exists(testRoot))
                Directory.Delete(testRoot, true);
        }

        [TestMethod]
        public void CopyPaste_File_NoConflict_Success()
        {
            // Создаём исходный файл.
            string source = Path.Combine(testRoot, "src.txt");
            File.WriteAllText(source, "content");
            service.CopyItem(source);               

            // Создаём целевую папку и переходим в неё.
            string destFolder = Path.Combine(testRoot, "Dest");
            Directory.CreateDirectory(destFolder);
            pathModel.Path = destFolder;

            // Выполняем вставку.
            bool result = service.PasteItem();

            // Проверяем: файл скопирован, оригинал остался.
            Assert.IsTrue(result);
            string destFile = Path.Combine(destFolder, "src.txt");
            Assert.IsTrue(File.Exists(destFile));
            Assert.IsTrue(File.Exists(source));
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

            // Проверяем: файл перемещён, исходник удалён.
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(source));
            Assert.IsTrue(File.Exists(Path.Combine(destFolder, "move.txt")));
        }

        [TestMethod]
        public void Paste_WhenBufferEmpty_ReturnsFalse()
        {
            // Не вызывали CopyItem или CutItem – буфер пуст.
            bool result = service.PasteItem();
            Assert.IsFalse(result);
        }
    }
}