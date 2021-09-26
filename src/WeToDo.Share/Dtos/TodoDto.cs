﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Dtos
{
    public class TodoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }


        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }


        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

    }
}
