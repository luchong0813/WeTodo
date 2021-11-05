using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WeTodoForWindows.Extensions
{
    public class PasswordExtenions
    {

        public static string GetPassWord(DependencyObject obj)
        {
            return (string)obj.GetValue(PassWordProperty);
        }

        public static void SetPassWord(DependencyObject obj, string value)
        {
            obj.SetValue(PassWordProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord",
                typeof(string),
                typeof(PasswordExtenions),
                new PropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        private static void OnPassWordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pwdBox = d as PasswordBox;
            string password = (string)e.NewValue;

            if (!string.IsNullOrEmpty(password) && pwdBox.Password != password)
            {
                pwdBox.Password = password;
            }
        }
    }
}
