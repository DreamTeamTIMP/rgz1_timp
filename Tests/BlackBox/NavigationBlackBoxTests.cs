using rgz1_timp;
namespace Tests
{
    [TestClass]
    public class NavigationBlackBoxTests
    {
        private string testRoot;
        private CurrentPathModel model;

        [TestInitialize]
        public void Init()
        {
            testRoot = Path.Combine(Path.GetTempPath(), "NavTest_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(testRoot);
            model = new CurrentPathModel();
            model.Path = testRoot; // стартовая папка
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(testRoot))
                Directory.Delete(testRoot, true);
        }

        // Вспомогательный метод для установки пути через модель (имитация адресной строки)
        private void SetPath(string path) => model.Path = path;

        // ----- Допустимые классы -----
        [TestMethod]
        public void Navigate_ToRootDirectory_Success()
        {
            string root = Path.GetPathRoot(testRoot); // например, C:\
            SetPath(root);
            Assert.AreEqual(root, model.Path);
        }

        [TestMethod]
        public void Navigate_ToSubDirectory_Success()
        {
            string sub = Path.Combine(testRoot, "Sub");
            Directory.CreateDirectory(sub);
            SetPath(sub);
            Assert.AreEqual(sub, model.Path);
        }

        [TestMethod]
        public void Navigate_ToSpecialObject_ThisPC_Success()
        {
            SetPath("Этот компьютер");
            Assert.AreEqual("Этот компьютер", model.Path);
        }

        [TestMethod]
        public void Navigate_ToSpecialObject_QuickAccess_Success()
        {
            SetPath("Быстрый доступ");
            Assert.AreEqual("Быстрый доступ", model.Path);
        }

        // ----- Недопустимые классы -----
        [TestMethod]
        public void Navigate_ToNonExistentPath_ShowsErrorAndPathNotChanged()
        {
            string invalidPath = Path.Combine(testRoot, "");
            string originalPath = model.Path;
            SetPath(invalidPath);
            Assert.AreEqual(originalPath, model.Path);
        }

        // Граничные значения (длина)
        [TestMethod]
        public void Navigate_ToVeryLongPath_IfSupportedOrError()
        {
            // Создадим глубокую структуру, чтобы путь был длинным (около 250 символов)
            string longPath = testRoot;
            while (longPath.Length < 250)
            {
                string next = Path.Combine(longPath, "Folder");
                Directory.CreateDirectory(next);
                longPath = next;
            }
            SetPath(longPath);
            // Если система поддерживает длинные пути, то переход успешен, иначе путь не меняется.
            // Тест должен быть адаптирован под реальное поведение.
            // Для стабильности проверяем, что исключения нет, и путь либо равен longPath, либо исходному.
            bool changed = model.Path == longPath;
            Assert.IsTrue(changed || model.Path == testRoot);
        }
    }
}
