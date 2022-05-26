using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager
{
    public class Task
    {
        public string Title { get; set; }
        // Время в минутах
        public int Time { get; set; }
        public Task()
        {
            Title = "";
        }
    }
}
