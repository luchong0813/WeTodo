using Microsoft.Xaml.Behaviors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeTodoForWindows.Extensions
{
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var pwdBox = (PasswordBox)sender;
            var password = PasswordExtenions.GetPassWord(pwdBox);
            if (pwdBox != null && pwdBox.Password != password)
            {
                PasswordExtenions.SetPassWord(pwdBox, pwdBox.Password);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }
}
