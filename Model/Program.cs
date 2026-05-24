namespace rgz1_timp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Инициализирует конфигурацию приложения
            ApplicationConfiguration.Initialize();
            // Запускает главную форму файлового менеджера.
            Application.Run(new FormMain());
        }
    }
}