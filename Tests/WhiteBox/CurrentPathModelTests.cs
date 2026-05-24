using rgz1_timp.Services;
namespace Tests
{
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
            /// Тест 1: Установка пути в пустую историю -> добавляется, CanGoBack=false
            /// Покрывает условие history.Count==0 (C1=true) в D4.
            /// </summary>
            [TestMethod]
            public void SetPath_WhenHistoryEmpty_AddsPath()
            {
                // Arrange
                string path = "C:\\Test";

                // Act
                model.Path = path;

                // Assert
                Assert.AreEqual(path, model.Path);
                Assert.IsFalse(model.CanGoBack, "История должна содержать только один элемент -> назад нельзя");
                Assert.IsFalse(model.CanGoForward);
            }

            /// <summary>
            /// Тест 2: Установка другого пути, когда история не пуста и последний путь отличается.
            /// Покрывает C1=false, C2=true в D4.
            /// </summary>
            [TestMethod]
            public void SetPath_WhenHistoryNotEmptyAndNewPathDifferent_AddsPath()
            {
                // Arrange
                model.Path = "C:\\First";
                // Act
                model.Path = "C:\\Second";

                // Assert
                Assert.AreEqual("C:\\Second", model.Path);
                Assert.IsTrue(model.CanGoBack, "Должна быть возможность вернуться на первый путь");
                Assert.IsFalse(model.CanGoForward);
            }

            /// <summary>
            /// Тест 3: Установка того же пути, что и текущий (дубликат).
            /// Покрывает C1=false, C2=false в D4 -> не добавляет.
            /// </summary>
            [TestMethod]
            public void SetPath_WhenSameAsCurrent_DoesNotDuplicateInHistory()
            {
                // Arrange
                model.Path = "C:\\Same";
                bool eventRaised = false;
                model.PathChanged += (p) => eventRaised = true;

                // Act
                model.Path = "C:\\Same";

                // Assert
                Assert.AreEqual("C:\\Same", model.Path);
                Assert.IsFalse(model.CanGoBack, "История не должна увеличиваться");
                Assert.IsFalse(eventRaised, "Событие PathChanged не должно вызываться при одинаковом пути");
            }

            /// <summary>
            /// Тест 4: Установка null или пустой строки -> не меняет путь.
            /// Покрывает ветку D2 (string.IsNullOrEmpty).
            /// </summary>
            [TestMethod]
            public void SetPath_WhenNullOrEmpty_DoesNotChange()
            {
                // Arrange
                model.Path = "C:\\Existing";
                string expected = model.Path;

                // Act
                model.Path = null;
                Assert.AreEqual(expected, model.Path, "Путь не должен измениться");

                model.Path = "";
                Assert.AreEqual(expected, model.Path);
            }

            /// <summary>
            /// Тест 5: Установка пути после навигации "назад/вперёд" -> удаляет хвост истории.
            /// Покрывает ветку D3 (истина) – удаление хвоста.
            /// </summary>
            [TestMethod]
            public void SetPath_WhenNotAtEndOfHistory_RemovesRedoStack()
            {
                // Arrange: создаём историю A -> B -> C
                model.Path = "C:\\A";
                model.Path = "C:\\B";
                model.Path = "C:\\C";
                // Идём назад два раза: должны быть на A
                model.GoBack();
                model.GoBack(); // текущий путь = A
                                 // Теперь в истории: [A, B, C], индекс = 0
                                 // Удаляем хвост: должны остаться [A]

                // Act
                model.Path = "C:\\D";

                // Assert
                Assert.AreEqual("C:\\D", model.Path);
                // После такого, назад можно только на A? Нет, мы удалили B,C, так что новая история: [A, D]
                // Тогда CanGoBack = true (на A)
                Assert.IsTrue(model.CanGoBack, "Должна быть возможность вернуться на A");
                Assert.IsFalse(model.CanGoForward, "Нет форварда");
                // Проверим, что GoBack вернёт на A
                model.GoBack();
                Assert.AreEqual("C:\\A", model.Path);
            }
        }
}
