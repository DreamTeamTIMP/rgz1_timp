using System.IO;
using System.Linq;

namespace rgz1_timp.Services
{
    /// <summary>
    /// Статический класс для проверки корректности имён файлов и папок в Windows.
    /// </summary>
    public static class FileNameValidator
    {
        // Массив недопустимых символов для имён файлов.
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();

        // Список зарезервированных системных имён.
        private static readonly string[] ReservedNames =
        {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        /// <summary>
        /// Проверяет, является ли имя файла или папки допустимым.
        /// </summary>
        /// <param name="name">Проверяемое имя.</param>
        /// <returns>True, если имя корректно.</returns>
        public static bool IsValidFileName(string name)
        {
            // Имя не должно быть пустым или состоять из пробелов.
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Имя не должно содержать недопустимые символы.
            if (name.IndexOfAny(InvalidChars) >= 0) return false;
            // Имя без расширения не должно совпадать с зарезервированными.
            string nameWithoutExt = Path.GetFileNameWithoutExtension(name);
            if (ReservedNames.Contains(nameWithoutExt, StringComparer.OrdinalIgnoreCase)) return false;
            // Имя не может состоять только из точек.
            if (name.Trim().All(c => c == '.')) return false;
            // Ограничение длины имени (255 символов).
            if (name.Length > 255) return false;
            return true;
        }

        /// <summary>
        /// Возвращает сообщение об ошибке для некорректного имени.
        /// </summary>
        /// <param name="name">Проверяемое имя.</param>
        /// <returns>Текст ошибки или null, если имя корректно.</returns>
        public static string? GetErrorMessage(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "Имя не может быть пустым.";
            if (name.IndexOfAny(InvalidChars) >= 0)
                return $"Имя содержит недопустимые символы: {string.Join(" ", InvalidChars)}";
            string nameWithoutExt = Path.GetFileNameWithoutExtension(name);
            if (ReservedNames.Contains(nameWithoutExt, StringComparer.OrdinalIgnoreCase))
                return $"Имя '{nameWithoutExt}' зарезервировано системой.";
            if (name.Trim().All(c => c == '.'))
                return "Имя не может состоять только из точек.";
            if (name.Length > 255)
                return "Имя слишком длинное (максимум 255 символов).";
            return null;
        }
    }
}