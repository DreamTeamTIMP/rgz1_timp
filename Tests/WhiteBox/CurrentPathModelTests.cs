using rgz1_timp.Services;

namespace Tests
{
    /// <summary>
    /// Модульные тесты для модели текущего пути (CurrentPathModel) – управление историей навигации.
    /// </summary>
    [TestClass]
    public class CurrentPathModelTests
    {
        private CurrentPathModel model;

        [TestInitialize]
        public void Setup()
        {
            model = new CurrentPathModel();
        }

        /// <summary>
        /// Тест 1: Установка пути в пустую историю -> путь добавляется, CanGoBack = false.
        /// </summary>
        [TestMethod]
        public void SetPath_WhenHistoryEmpty_AddsPath()
        {
            string path = "C:\\Test";
            model.Path = path;
            Assert.AreEqual(path, model.Path);
            Assert.IsFalse(model.CanGoBack, "История должна содержать только один элемент -> назад нельзя.");
            Assert.IsFalse(model.CanGoForward);
        }

        /// <summary>
        /// Тест 2: Установка другого пути, когда история не пуста и последний путь отличается.
        /// </summary>
        [TestMethod]
        public void SetPath_WhenHistoryNotEmptyAndNewPathDifferent_AddsPath()
        {
            model.Path = "C:\\First";
            model.Path = "C:\\Second";
            Assert.AreEqual("C:\\Second", model.Path);
            Assert.IsTrue(model.CanGoBack, "Должна быть возможность вернуться на первый путь.");
            Assert.IsFalse(model.CanGoForward);
        }

        /// <summary>
        /// Тест 3: Установка того же пути, что и текущий – дубликат не добавляется.
        /// </summary>
        [TestMethod]
        public void SetPath_WhenSameAsCurrent_DoesNotDuplicateInHistory()
        {
            model.Path = "C:\\Same";
            bool eventRaised = false;
            model.PathChanged += (p) => eventRaised = true;

            model.Path = "C:\\Same";

            Assert.AreEqual("C:\\Same", model.Path);
            Assert.IsFalse(model.CanGoBack, "История не должна увеличиваться.");
            Assert.IsFalse(eventRaised, "Событие PathChanged не должно вызываться при одинаковом пути.");
        }

        /// <summary>
        /// Тест 4: Установка null или пустой строки – путь не меняется.
        /// </summary>
        [TestMethod]
        public void SetPath_WhenNullOrEmpty_DoesNotChange()
        {
            model.Path = "C:\\Existing";
            string expected = model.Path;

            model.Path = null;
            Assert.AreEqual(expected, model.Path, "Путь не должен измениться.");

            model.Path = "";
            Assert.AreEqual(expected, model.Path);
        }

        /// <summary>
        /// Установка пути после навигации "назад" – удаляет хвост истории (redo-стек).
        /// </summary>
        [TestMethod]
        public void SetPath_WhenNotAtEndOfHistory_RemovesRedoStack()
        {
            // Создаём историю A -> B -> C.
            model.Path = "C:\\A";
            model.Path = "C:\\B";
            model.Path = "C:\\C";
            // Идём назад два раза: оказываемся на A.
            model.GoBack();
            model.GoBack();   // Текущий путь = A.
            // Теперь история: [A, B, C], индекс = 0.

            model.Path = "C:\\D";

            Assert.AreEqual("C:\\D", model.Path);
            // Новая история: [A, D], назад можно на A.
            Assert.IsTrue(model.CanGoBack, "Должна быть возможность вернуться на A.");
            Assert.IsFalse(model.CanGoForward, "Нет форварда.");

            model.GoBack();
            Assert.AreEqual("C:\\A", model.Path);
        }
    }
}