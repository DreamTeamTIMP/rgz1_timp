using System.Runtime.InteropServices;

namespace rgz1_timp.ImportedDll
{
    /// <summary>
    /// Статический класс, содержащий объявления P/Invoke для вызова функций Win32 API.
    /// </summary>
    internal static class Dll
    {
        /// <summary>
        /// Структура, возвращаемая функцией SHGetFileInfo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            // Дескриптор иконки.
            public IntPtr hIcon;         
            // Индекс иконки в системном списке.
            public int iIcon;            
            // Атрибуты файла.
            public uint dwAttributes;    
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            // Отображаемое имя.
            public string szDisplayName; 
            // Тип объекта.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;    
        }

        /// <summary>
        /// Устанавливает тему оформления для элемента управления.
        /// </summary>
        /// <param name="hWnd">Дескриптор окна.</param>
        /// <param name="pszSubAppName">Имя приложения темы.</param>
        /// <param name="pszSubIdList">Идентификатор темы (может быть null).</param>
        /// <returns>Код возврата.</returns>
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);

        /// <summary>
        /// Извлекает иконку из исполняемого файла или библиотеки.
        /// </summary>
        /// <param name="hInst">Дескриптор модуля (можно IntPtr.Zero).</param>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="nIconIndex">Индекс иконки.</param>
        /// <returns>Дескриптор иконки.</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string file, int nIconIndex);

        /// <summary>
        /// Уничтожает иконку, освобождая ресурсы.
        /// </summary>
        /// <param name="hIcon">Дескриптор иконки.</param>
        /// <returns>True в случае успеха.</returns>
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Получает информацию о файле, включая иконку.
        /// </summary>
        /// <param name="pszPath">Путь к файлу.</param>
        /// <param name="dwFileAttributes">Атрибуты файла.</param>
        /// <param name="psfi">Ссылка на структуру SHFILEINFO.</param>
        /// <param name="cbFileInfo">Размер структуры.</param>
        /// <param name="uFlags">Флаги запроса.</param>
        /// <returns>Дескриптор иконки.</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        /// <summary>
        /// Освобождает захват мыши для окна.
        /// </summary>
        /// <returns>True в случае успеха.</returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// Посылает сообщение окну.
        /// </summary>
        /// <param name="hWnd">Дескриптор окна.</param>
        /// <param name="Msg">Код сообщения.</param>
        /// <param name="wParam">Первый параметр.</param>
        /// <param name="lParam">Второй параметр.</param>
        /// <returns>Результат обработки сообщения.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}