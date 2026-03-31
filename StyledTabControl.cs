namespace rgz1_timp
{
    public class StyledTabControl : TabControl
    {
        public StyledTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }

        // Убираем рамку
        protected override void WndProc(ref Message m)
        {
            const int WM_NCPAINT = 0x85;   // сообщение о перерисовке неклиентской области
            const int WM_SIZE = 0x05;

            if (m.Msg == WM_NCPAINT || m.Msg == WM_SIZE)
            {
                // Позволяем стандартную обработку, но рамка всё равно будет нарисована.
                // Чтобы её убрать, нужно после базовой обработки переопределить область отрисовки.
                base.WndProc(ref m);
                // Перерисовываем неклиентскую область, скрывая рамку
                int style = GetWindowLong(this.Handle, GWL_STYLE);
                style &= ~WS_BORDER;
                SetWindowLong(this.Handle, GWL_STYLE, style);
                // Заставляем перерисовать рамку заново
                SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0,
                    SWP_NOZORDER | SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_FRAMECHANGED);
                return;
            }
            base.WndProc(ref m);
        }
        // Событие DrawItem (можно переопределить OnDrawItem)
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Ваша логика рисования
            base.OnDrawItem(e);
        }

        // Импорты необходимых функций
        private const int GWL_STYLE = -16;
        private const int WS_BORDER = 0x00800000;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020; // эта уже была

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);
    }
}
