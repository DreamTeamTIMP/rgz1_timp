using rgz1_timp;
using rgz1_timp.Services;

namespace Tests
{
    /// <summary>
    /// Тестирование навигации (установка пути, кнопки "Назад"/"Вперёд").
    /// </summary>
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
            model.Path = testRoot;
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(testRoot))
                Directory.Delete(testRoot, true);
        }

        // Вспомогательный метод для установки пути через модель (имитация адресной строки).
        private void SetPath(string path) => model.Path = path;

        // Допустимые классы эквивалентности.

        [TestMethod]
        public void Navigate_ToRootDirectory_Success()
        {
            string root = Path.GetPathRoot(testRoot);
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

        // Недопустимые классы эквивалентности.

        [TestMethod]
        public void Navigate_ToNonExistentPath_ShowsErrorAndPathNotChanged()
        {
            string invalidPath = Path.Combine(testRoot, "NonExistentFolder");
            string originalPath = model.Path;
            SetPath(invalidPath);
            Assert.AreEqual(originalPath, model.Path);
        }

        // Граничные значения (длина пути).

        [TestMethod]
        public void Navigate_ToVeryLongPath_IfSupportedOrError()
        {
            // Создаём глубокую структуру, чтобы путь был длинным (около 250 символов).
            string longPath = testRoot;
            while (longPath.Length < 250)
            {
                string next = Path.Combine(longPath, "Folder");
                Directory.CreateDirectory(next);
                longPath = next;
            }
            SetPath(longPath);
            // Если система поддерживает длинные пути, переход успешен, иначе путь не меняется.
            bool changed = model.Path == longPath;
            Assert.IsTrue(changed || model.Path == testRoot);
        }
    }
}