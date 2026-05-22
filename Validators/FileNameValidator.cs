using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgz1_timp.Validators
{
    public static class FileNameValidator
    {
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();
        private static readonly string[] ReservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

        public static bool IsValidFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (name.IndexOfAny(InvalidChars) >= 0) return false;
            // Проверка зарезервированных имен (без расширения)
            string nameWithoutExt = Path.GetFileNameWithoutExtension(name);
            if (ReservedNames.Contains(nameWithoutExt, StringComparer.OrdinalIgnoreCase)) return false;
            // Запрещаем имена, состоящие только из точек
            if (name.Trim().All(c => c == '.')) return false;
            return true;
        }

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
            return null;
        }
    }
}
