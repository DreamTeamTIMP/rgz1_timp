using rgz1_timp.Command;

namespace Tests
{
    /// <summary>
    /// Модульные тесты для класса CommandInvoker (система Undo/Redo).
    /// </summary>
    [TestClass]
    public class CommandInvokerTests
    {
        // Вспомогательная тестовая команда.
        private class TestCommand : ICommand
        {
            public bool Executed { get; private set; }
            public bool Undone { get; private set; }
            public void Execute() => Executed = true;
            public void Undo() => Undone = true;
        }

        [TestInitialize]
        public void Setup() => CommandInvoker.Clear();

        [TestMethod]
        public void ExecuteCommand_PushesToUndoStack_ClearsRedo()
        {
            var cmd = new TestCommand();
            CommandInvoker.ExecuteCommand(cmd);
            Assert.IsTrue(cmd.Executed);
            Assert.IsTrue(CommandInvoker.CanUndo);
            Assert.IsFalse(CommandInvoker.CanRedo);
        }

        [TestMethod]
        public void Undo_PopsFromUndo_PushesToRedo()
        {
            var cmd = new TestCommand();
            CommandInvoker.ExecuteCommand(cmd);
            CommandInvoker.Undo();
            Assert.IsTrue(cmd.Undone);
            Assert.IsFalse(CommandInvoker.CanUndo);
            Assert.IsTrue(CommandInvoker.CanRedo);
        }

        [TestMethod]
        public void Redo_PopsFromRedo_PushesToUndo()
        {
            var cmd = new TestCommand();
            CommandInvoker.ExecuteCommand(cmd);
            CommandInvoker.Undo();
            CommandInvoker.Redo();
            Assert.IsTrue(cmd.Executed);  
            Assert.IsTrue(CommandInvoker.CanUndo);
            Assert.IsFalse(CommandInvoker.CanRedo);
        }

        [TestMethod]
        public void NewCommandAfterUndo_ClearsRedoStack()
        {
            var cmd1 = new TestCommand();
            var cmd2 = new TestCommand();
            CommandInvoker.ExecuteCommand(cmd1);
            CommandInvoker.Undo();
            CommandInvoker.ExecuteCommand(cmd2);
            Assert.IsTrue(CommandInvoker.CanUndo);
            Assert.IsFalse(CommandInvoker.CanRedo); 
        }

        [TestMethod]
        public void UndoWhenEmpty_DoesNothing()
        {
            CommandInvoker.Undo();
            Assert.IsFalse(CommandInvoker.CanUndo);
            Assert.IsFalse(CommandInvoker.CanRedo);
        }

        [TestMethod]
        public void RedoWhenEmpty_DoesNothing()
        {
            CommandInvoker.Redo();
            Assert.IsFalse(CommandInvoker.CanUndo);
            Assert.IsFalse(CommandInvoker.CanRedo);
        }
    }
}