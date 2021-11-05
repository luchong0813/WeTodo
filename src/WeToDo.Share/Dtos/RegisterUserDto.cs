using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Dtos
{
    public class RegisterUserDto : BaseDto
    {
        private string account;
        private string userName;
        private string passWord;
        private string newPassWord;


        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; OnPropertyChanged(); }
        }
        public string NewPassWord
        {
            get { return newPassWord; }
            set { newPassWord = value; OnPropertyChanged(); }
        }
    }
}
