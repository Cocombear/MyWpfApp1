using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieWpf1
{
    internal class User
    {
        public User(int id, string Name, string Surname,string Address, string Login, string Password) 
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Login = Login;
            this.Password = Password;
            this.Address = Address;
            this.id = id;
            IdNumber++;
            Position = "Нету";
            Characteristic = "Нету";
            Current_tasks = "Отсуствует";
            Salary = 0;
        }

        static public int IdNumber = 0;
        public string Login { get; set; }
        public string Password { get; set; }
        int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string Current_tasks { get; set; }
        public string Characteristic { get; set; }
    }
}
