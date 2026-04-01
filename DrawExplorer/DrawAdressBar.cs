using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgz1_timp.DrawExplorer
{
    internal static class DrawAdressBar
    {
        public static void UpdateAddressBar(ComboBox addressBar, string path)
        {
            addressBar.Text = path;

            // Добавляем путь в историю выпадающего списка, если его там еще нет
            if (!addressBar.Items.Contains(path))
            {
                addressBar.Items.Insert(0, path);
            }
        }
    }
}
