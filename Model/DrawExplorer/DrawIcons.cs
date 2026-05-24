﻿using rgz1_timp.ImportedDll;

namespace rgz1_timp.DrawExplorer
{
    /// <summary>
    /// Загружает и кэширует системные значки (маленькие и большие) для файлов и папок.
    /// </summary>
    public class DrawIcons
    {
        public ImageList SmallIcons { get; }
        public ImageList LargeIcons { get; }

        // Флаги для вызова SHGetFileInfo.
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_SMALLICON = 0x1;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        /// <summary>
        /// Конструктор. Инициализирует списки изображений и загружает стандартные иконки.
        /// </summary>
        public DrawIcons()
        {
            SmallIcons = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            LargeIcons = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };

            InitializeStandardIcons();
        }

        /// <summary>
        /// Добавляет иконки для стандартных типов (папка, диск, быстрый доступ, компьютер, файл).
        /// </summary>
        private void InitializeStandardIcons()
        {
            AddSystemIcon("folder", "shell32.dll", 3);
            AddSystemIcon("drive", "shell32.dll", 7);
            AddSystemIcon("quick", "shell32.dll", 43);
            AddSystemIcon("pc", "imageres.dll", 140);
            AddSystemIcon("file", "shell32.dll", 0);
        }

        /// <summary>
        /// Извлекает иконку из системной библиотеки и добавляет её в оба списка.
        /// </summary>
        /// <param name="key">Ключ иконки.</param>
        /// <param name="dll">Имя DLL-библиотеки.</param>
        /// <param name="index">Индекс иконки в библиотеке.</param>
        private void AddSystemIcon(string key, string dll, int index)
        {
            IntPtr hIcon = Dll.ExtractIcon(IntPtr.Zero, dll, index);
            if (hIcon != IntPtr.Zero)
            {
                using (Icon icon = Icon.FromHandle(hIcon))
                {
                    SmallIcons.Images.Add(key, (Icon)icon.Clone());
                    LargeIcons.Images.Add(key, (Icon)icon.Clone());
                }
                Dll.DestroyIcon(hIcon);
            }
        }

        /// <summary>
        /// Возвращает ключ иконки для заданного пути (файла или папки).
        /// При первом обращении к расширению загружает иконку через Win32 API.
        /// </summary>
        /// <param name="path">Путь к объекту.</param>
        /// <param name="isDirectory">Является ли объект папкой.</param>
        /// <returns>Ключ иконки в ImageList.</returns>
        public string GetIconKey(string path, bool isDirectory)
        {
            // Определяем ключ: "folder" для папок, расширение файла (с точкой) для файлов.
            string ext = isDirectory ? "folder" : Path.GetExtension(path).ToLower();
            if (string.IsNullOrEmpty(ext))
                ext = ".none";

            // Если иконка уже загружена, возвращаем её ключ.
            if (SmallIcons.Images.ContainsKey(ext))
                return ext;

            Dll.SHFILEINFO shinfo = new();
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;

            // Загружаем маленькую иконку.
            Dll.SHGetFileInfo(path, (uint)(isDirectory ? 0x10 : 0x80), ref shinfo,
                (uint)System.Runtime.InteropServices.Marshal.SizeOf(shinfo), flags | SHGFI_SMALLICON);
            if (shinfo.hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(shinfo.hIcon);
                SmallIcons.Images.Add(ext, icon.Clone() as Icon);
                Dll.DestroyIcon(shinfo.hIcon);
            }

            // Загружаем большую иконку.
            Dll.SHGetFileInfo(path, (uint)(isDirectory ? 0x10 : 0x80), ref shinfo,
                (uint)System.Runtime.InteropServices.Marshal.SizeOf(shinfo), flags | SHGFI_LARGEICON);
            if (shinfo.hIcon != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(shinfo.hIcon);
                LargeIcons.Images.Add(ext, icon.Clone() as Icon);
                Dll.DestroyIcon(shinfo.hIcon);
            }

            return ext;
        }
    }
}