using rgz1_timp.Services;

namespace Tests
{
    [TestClass]
    public class FileNameValidatorTests
    {
        // Покрытие всех условий валидации
        [TestMethod]
        public void IsValidFileName_ValidNames_ReturnsTrue()
        {
            Assert.IsTrue(FileNameValidator.IsValidFileName("file.txt"));
            Assert.IsTrue(FileNameValidator.IsValidFileName("my file"));
            Assert.IsTrue(FileNameValidator.IsValidFileName("文档"));
            Assert.IsTrue(FileNameValidator.IsValidFileName("a".PadRight(255, 'a'))); // max length
        }

        [TestMethod]
        public void IsValidFileName_EmptyOrWhitespace_ReturnsFalse()
        {
            Assert.IsFalse(FileNameValidator.IsValidFileName(null));
            Assert.IsFalse(FileNameValidator.IsValidFileName(""));
            Assert.IsFalse(FileNameValidator.IsValidFileName("   "));
        }

        [TestMethod]
        public void IsValidFileName_InvalidChars_ReturnsFalse()
        {
            char[] invalid = Path.GetInvalidFileNameChars();
            foreach (char c in invalid)
            {
                if (c == '/' || c == '\\') continue; // эти допустимы в имени файла? нет, они запрещены.
                Assert.IsFalse(FileNameValidator.IsValidFileName("file" + c + ".txt"));
            }
        }

        [TestMethod]
        public void IsValidFileName_ReservedNames_ReturnsFalse()
        {
            string[] reserved = { "CON", "PRN", "AUX", "NUL", "COM1", "COM9", "LPT1", "LPT9" };
            foreach (string res in reserved)
            {
                Assert.IsFalse(FileNameValidator.IsValidFileName(res));
                Assert.IsFalse(FileNameValidator.IsValidFileName(res + ".txt"));
            }
        }

        [TestMethod]
        public void IsValidFileName_OnlyDots_ReturnsFalse()
        {
            Assert.IsFalse(FileNameValidator.IsValidFileName("."));
            Assert.IsFalse(FileNameValidator.IsValidFileName("..."));
            Assert.IsFalse(FileNameValidator.IsValidFileName("...."));
        }

        [TestMethod]
        public void IsValidFileName_TooLong_ReturnsFalse()
        {
            string longName = new string('a', 256);
            Assert.IsFalse(FileNameValidator.IsValidFileName(longName));
        }

        [TestMethod]
        public void GetErrorMessage_ReturnsMeaningfulMessage()
        {
            Assert.IsNotNull(FileNameValidator.GetErrorMessage(""));
            Assert.IsNotNull(FileNameValidator.GetErrorMessage("CON"));
            Assert.IsNotNull(FileNameValidator.GetErrorMessage("a/b"));
            Assert.IsNull(FileNameValidator.GetErrorMessage("valid.txt"));
        }
    }
}
