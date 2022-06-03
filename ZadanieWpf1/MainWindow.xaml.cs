using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZadanieWpf1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TextBox> text = new List<TextBox>();
        List<User> UserList = new List<User>();
        List<object> UserListBD = new List<object>();
        List<UIElement> obj = new List<UIElement>();
        public MainWindow()
        {
            GetConnection();
            DataBaseUpdate();
            InitializeComponent();
        }
        private void DataBaseUpdate() 
        {
                using(NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string qutry = $"SELECT COUNT(user_id) FROM users;";
                    NpgsqlCommand cmd = new NpgsqlCommand(qutry, con);
                    int count_ = Convert.ToInt32(cmd.ExecuteScalar());
                    for (int i = 0; i < count_; i++)
                    {
                            string q2 = $"SELECT user_name FROM users WHERE user_id = {i}";
                        NpgsqlCommand cmd2 = new NpgsqlCommand(q2, con);
                        string Name = cmd2.ExecuteScalar().ToString();
                        UserListBD.Add(Name);
                            string q3 = $"SELECT user_surname FROM users WHERE user_id = {i}";
                        NpgsqlCommand cmd3 = new NpgsqlCommand(q3, con);
                        string Surname = cmd3.ExecuteScalar().ToString();
                        UserListBD.Add(Surname);
                            string q4 = $"SELECT user_addres FROM users WHERE user_id = {i}";
                        NpgsqlCommand cmd4 = new NpgsqlCommand(q4, con);
                        string Addres = cmd4.ExecuteScalar().ToString();
                        UserListBD.Add(Addres);
                            string q5 = $"SELECT user_login FROM users WHERE user_id = {i}";
                        NpgsqlCommand cmd5 = new NpgsqlCommand(q5, con);
                        string Login = cmd5.ExecuteScalar().ToString();
                        UserListBD.Add(Login);
                            string q6 = $"SELECT user_password FROM users WHERE user_id = {i}";
                        NpgsqlCommand cmd6 = new NpgsqlCommand(q6, con);
                        string Password = cmd6.ExecuteScalar().ToString();
                        UserListBD.Add(Password);
                        User user = new User(i,Name,Surname,Addres,Login,Password);
                        UserList.Add(user);
                    }
                    

                }
                
        }
        static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Host=localhost;Port=1901;Database=BaseDataWPF;Username=postgres;Password=1901");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Remove(Start);
            canvas.Children.Remove(Start2);
            Password();
            
        }
        //Вход
        private void Regis_Click(object sender, RoutedEventArgs e) 
        {
            Destroy();
            Login();
        }
        //Если нажал вохд но нету учётки
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Children.Remove(Start);
            canvas.Children.Remove(Start2);
            Login();
        }
        
        private void Password()
        { 
            TextBlock LoginText = new TextBlock();
                LoginText.Height = 20; LoginText.Text = "Login"; LoginText.Width = 60; obj.Add(LoginText);
                Canvas.SetLeft(LoginText, 330);
                Canvas.SetTop(LoginText, 100);
                canvas.Children.Add(LoginText); 
            TextBox LoginBox = new TextBox();
                LoginBox.Height = 20; LoginBox.Width = 100;
                Canvas.SetLeft(LoginBox, 330);
                Canvas.SetTop(LoginBox, 120);
                canvas.Children.Add(LoginBox); obj.Add(LoginBox);
            TextBlock PasswordText = new TextBlock();
                PasswordText.Height = 20; PasswordText.Width = 60; PasswordText.Text = "Password";
                Canvas.SetLeft(PasswordText, 330);
                Canvas.SetTop(PasswordText, 140);
                canvas.Children.Add(PasswordText); obj.Add(PasswordText);
            TextBox PasswordBox = new TextBox();
                PasswordBox.Height = 20; PasswordBox.Width = 100;
                Canvas.SetLeft(PasswordBox, 330);
                Canvas.SetTop(PasswordBox, 160);
                canvas.Children.Add(PasswordBox); obj.Add(PasswordBox);
            Button sing = new Button(); sing.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); sing.Foreground = Brushes.White;
                sing.Height = 20; sing.Width = 45; sing.Content = "Sing in";
                Canvas.SetLeft(sing, 330);
                Canvas.SetTop(sing, 190);
                canvas.Children.Add(sing); obj.Add(sing);
                sing.Click += SingIn_Click;
            Button regis = new Button(); regis.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); regis.Foreground = Brushes.White;
                regis.Height = 20; regis.Width = 60; regis.Content = "Register";
                Canvas.SetLeft(regis, 380);
                Canvas.SetTop(regis, 190);
                canvas.Children.Add(regis); obj.Add(regis);
                regis.Click += Regis_Click;

            void SingIn_Click(object sender, RoutedEventArgs e)
            {
                LoginPassword();
                void LoginPassword()
                {
                    string user_id; string PasswordSql; string user_position;
                    using (NpgsqlConnection con = GetConnection())
                    {
                        con.Open();
                        string qutry = $"SELECT user_password FROM users WHERE user_login = '{LoginBox.Text}';";
                        NpgsqlCommand cmd = new NpgsqlCommand(qutry, con);
                        PasswordSql = cmd.ExecuteScalar().ToString();
                        UserListBD.Add(PasswordSql);
                        string q2 = $"SELECT user_id FROM users WHERE user_login = '{LoginBox.Text}';";
                        NpgsqlCommand cmd2 = new NpgsqlCommand(q2, con);
                        user_id = cmd2.ExecuteScalar().ToString();
                        UserListBD.Add(user_id);
                        string q3 = $"SELECT user_position FROM users WHERE user_login = '{LoginBox.Text}';";
                        NpgsqlCommand cmd3 = new NpgsqlCommand(q3, con);
                        user_position = cmd3.ExecuteScalar().ToString();
                        UserListBD.Add(user_position);


                    }
                    if (UserList[Convert.ToInt32(user_id)].Password == PasswordSql)
                    {
                        if (user_position == "Администратор")
                        {
                            Destroy(); Admininterface(Convert.ToInt32(user_id));
                        }
                        else
                        {
                            Destroy();
                            Userinterface(Convert.ToInt32(user_id));
                        }
                    }
                       
                }
            }//Проверка пароля и вход
        }//Поля для входа
        private void Login() //регистация
        {
            //Имя
            TextBlock NameBlock = new TextBlock(); NameBlock.Text = "Имя";
            Canvas.SetLeft(NameBlock, 100); Canvas.SetTop(NameBlock, 40); canvas.Children.Add(NameBlock); obj.Add(NameBlock);
            TextBox NameBox = new TextBox(); NameBox.Width = 100;
            Canvas.SetLeft(NameBox, 100); Canvas.SetTop(NameBox, 60); canvas.Children.Add(NameBox); obj.Add(NameBox);
            //Фамилия
            TextBlock SurNameBlock = new TextBlock(); SurNameBlock.Text = "Фамилия";
            Canvas.SetLeft(SurNameBlock, 100); Canvas.SetTop(SurNameBlock, 80); canvas.Children.Add(SurNameBlock); obj.Add(SurNameBlock);
            TextBox SurNameBox = new TextBox(); SurNameBox.Width = 100;
            Canvas.SetLeft(SurNameBox, 100); Canvas.SetTop(SurNameBox, 100); canvas.Children.Add(SurNameBox); obj.Add(SurNameBox);
            //Адрес
            TextBlock AddressBlock = new TextBlock(); AddressBlock.Text = "Адрес";
            Canvas.SetLeft(AddressBlock, 100); Canvas.SetTop(AddressBlock, 120); canvas.Children.Add(AddressBlock); obj.Add(AddressBlock);
            TextBox AddressBox = new TextBox(); AddressBox.Width = 300;
            Canvas.SetLeft(AddressBox, 100); Canvas.SetTop(AddressBox, 140); canvas.Children.Add(AddressBox); obj.Add(AddressBox);
            //Логин
            TextBlock LoginText = new TextBlock();
            LoginText.Height = 20; LoginText.Text = "Login"; LoginText.Width = 60;
            Canvas.SetLeft(LoginText, 100);
            Canvas.SetTop(LoginText, 160);
            canvas.Children.Add(LoginText); obj.Add(LoginText);
            TextBox LoginBox = new TextBox();
            LoginBox.Height = 20; LoginBox.Width = 100;
            Canvas.SetLeft(LoginBox, 100);
            Canvas.SetTop(LoginBox, 180);
            canvas.Children.Add(LoginBox); obj.Add(LoginBox);
            //Пароль
            TextBlock PasswordText = new TextBlock();
            PasswordText.Height = 20; PasswordText.Width = 60; PasswordText.Text = "Password";
            Canvas.SetLeft(PasswordText, 100);
            Canvas.SetTop(PasswordText, 200);
            canvas.Children.Add(PasswordText); obj.Add(PasswordText);
            TextBox PasswordBox = new TextBox();
            PasswordBox.Height = 20; PasswordBox.Width = 100;
            Canvas.SetLeft(PasswordBox, 100);
            Canvas.SetTop(PasswordBox, 220);
            canvas.Children.Add(PasswordBox); obj.Add(PasswordBox);
            //Кнопка ригестрации
            Button reg = new Button(); reg.Background = new SolidColorBrush(Color.FromRgb(96,171,255)); reg.Foreground = Brushes.White;
            reg.Height = 20; reg.Width = 80; reg.Content = "Регистарция"; 
            Canvas.SetLeft(reg, 100);
            Canvas.SetTop(reg, 245);
            canvas.Children.Add(reg); obj.Add(reg);
            reg.Click += register_Click;
            
            void register_Click(object sender, RoutedEventArgs e) //Создание пользователя и внесение в базу 
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    var cmd3 = new NpgsqlCommand("INSERT INTO users(user_id, user_name, user_surname, user_login, user_password, user_addres) VALUES(@User.IdNumber,@NameBox.Text,@SurNameBox.Text,@LoginBox.Text,@PasswordBox.Text,@AddressBox.Text)", con);
                    cmd3.Parameters.AddWithValue("@User.IdNumber", User.IdNumber);
                    cmd3.Parameters.AddWithValue("@NameBox.Text", NameBox.Text);
                    cmd3.Parameters.AddWithValue("@SurNameBox.Text", SurNameBox.Text);
                    cmd3.Parameters.AddWithValue("@LoginBox.Text", LoginBox.Text);
                    cmd3.Parameters.AddWithValue("@PasswordBox.Text", PasswordBox.Text);
                    cmd3.Parameters.AddWithValue("@AddressBox.Text", AddressBox.Text);
                    cmd3.ExecuteNonQuery();
                }
                rege();
                void rege()
                {
                    User ne = new User(User.IdNumber, NameBox.Text, SurNameBox.Text, AddressBox.Text, LoginBox.Text, PasswordBox.Text);
                    UserList.Add(ne);
                    User.IdNumber++;
                    Destroy();
                    Password();
                }
            }

        }//Поля для регистрации
        private void Userinterface(int id) 
        {
            //Фотография
            Rectangle Photo = new Rectangle(); Photo.Width = 60; Photo.Height = 80; Photo.Stroke = Brushes.Black;
            Canvas.SetLeft(Photo,30); Canvas.SetTop(Photo,60); canvas.Children.Add(Photo); obj.Add(Photo);
            //Размещение полей Фио, должность и тд.
            TextBlock Name = new TextBlock(); Canvas.SetLeft(Name, 110); Canvas.SetTop(Name,60);
                Name.Text = UserList[id].Name; canvas.Children.Add(Name); obj.Add(Name);
            TextBlock SurName = new TextBlock(); Canvas.SetLeft(SurName, 110); Canvas.SetTop(SurName, 80);
                SurName.Text = UserList[id].Surname; canvas.Children.Add(SurName); obj.Add(SurName);
            TextBlock Adderss = new TextBlock(); Canvas.SetLeft(Adderss, 110); Canvas.SetTop(Adderss, 100);
                Adderss.Text = UserList[id].Address; canvas.Children.Add(Adderss); obj.Add(Adderss);
            TextBlock Position = new TextBlock(); Canvas.SetLeft(Position, 110); Canvas.SetTop(Position, 120);
                Position.Text = UserList[id].Position; canvas.Children.Add(Position); obj.Add(Position);
            TextBlock Salary = new TextBlock(); Canvas.SetLeft(Salary, 110); Canvas.SetTop(Salary, 140);
                Salary.Text = Convert.ToString(UserList[id].Salary); canvas.Children.Add(Salary); obj.Add(Salary);
            TextBlock Current_tasks = new TextBlock(); Canvas.SetLeft(Current_tasks,300); Canvas.SetTop(Current_tasks,45);
                Current_tasks.Text = Convert.ToString(UserList[id].Current_tasks); Current_tasks.Height = 100; Current_tasks.Width = 300;
                canvas.Children.Add(Current_tasks); obj.Add(Current_tasks);
            TextBlock Characteristic = new TextBlock(); Canvas.SetLeft(Characteristic,300); Canvas.SetTop(Characteristic,150);
                Characteristic.Text = Convert.ToString(UserList[id].Characteristic); 
                Characteristic.Width = 300; Characteristic.Height = 100;
                canvas.Children.Add(Characteristic); obj.Add(Characteristic);
            Button exit = new Button(); exit.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); exit.Foreground = Brushes.White;
                Canvas.SetLeft(exit, 5); Canvas.SetTop(exit,35);
                exit.Content = "Exit"; exit.Click += Exit_Click; 
                canvas.Children.Add(exit); obj.Add(exit);
                void Exit_Click(object sender, RoutedEventArgs e) 
                {
                    Destroy(); Password(); 
                }


        }//Интерфейс для поль.
        private void Destroy() 
        {
            for (int i = 0; i < obj.Count; i++)
            {
                canvas.Children.Remove(obj[i]);
            }
        }//Удаление объектов с Canvas
        private void Admininterface(int id) 
        {
            //Фотография
            Rectangle Photo = new Rectangle(); Photo.Width = 60; Photo.Height = 80; Photo.Stroke = Brushes.Black;
            Canvas.SetLeft(Photo, 30); Canvas.SetTop(Photo, 60); canvas.Children.Add(Photo); obj.Add(Photo);
            //Размещение полей Фио, должность и тд.
            TextBox Name = new TextBox(); Canvas.SetLeft(Name, 110); Canvas.SetTop(Name, 60);
                Name.Text = UserList[id].Name; canvas.Children.Add(Name); obj.Add(Name);
            TextBox SurName = new TextBox(); Canvas.SetLeft(SurName, 110); Canvas.SetTop(SurName, 80);
                SurName.Text = UserList[id].Surname; canvas.Children.Add(SurName); obj.Add(SurName);
            TextBox Adderss = new TextBox(); Canvas.SetLeft(Adderss, 110); Canvas.SetTop(Adderss, 100);
                Adderss.Text = UserList[id].Address; canvas.Children.Add(Adderss); obj.Add(Adderss);
            TextBox Position = new TextBox(); Canvas.SetLeft(Position, 110); Canvas.SetTop(Position, 120);
                Position.Text = UserList[id].Position; canvas.Children.Add(Position); obj.Add(Position);
            TextBox Salary = new TextBox(); Canvas.SetLeft(Salary, 110); Canvas.SetTop(Salary, 140);
                Salary.Text = Convert.ToString(UserList[id].Salary); canvas.Children.Add(Salary); obj.Add(Salary);
            TextBox Current_tasks = new TextBox(); Canvas.SetLeft(Current_tasks, 300); Canvas.SetTop(Current_tasks, 45);
                Current_tasks.Text = Convert.ToString(UserList[id].Current_tasks); Current_tasks.Height = 100; Current_tasks.Width = 300;
                canvas.Children.Add(Current_tasks); obj.Add(Current_tasks);
            TextBox Characteristic = new TextBox(); Canvas.SetLeft(Characteristic, 300); Canvas.SetTop(Characteristic, 150);
                Characteristic.Text = Convert.ToString(UserList[id].Characteristic);
                Characteristic.Width = 300; Characteristic.Height = 100;
                canvas.Children.Add(Characteristic); obj.Add(Characteristic);
            Button exit = new Button(); exit.Content = "Exit";
                Canvas.SetLeft(exit, 5); Canvas.SetTop(exit, 35); exit.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); 
                exit.Foreground = Brushes.White;
                exit.Click += Exit_Click; canvas.Children.Add(exit); obj.Add(exit);
                void Exit_Click(object sender, RoutedEventArgs e)
                {
                    Destroy(); Password();
                }

                TextBox count = new TextBox(); count.Text = $"{id}/{User.IdNumber - 1}";
            count.Foreground = Brushes.Black;
            canvas.Children.Add(count); obj.Add(count);
            Canvas.SetLeft(count, 130); Canvas.SetTop(count, 280);
            

            Button next = new Button(); next.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); 
                next.Foreground = Brushes.White;
                next.Click += Next_Click; canvas.Children.Add(next); obj.Add(next);
                Canvas.SetLeft(next,60); Canvas.SetTop(next,280); next.Content = "Next";
                void Next_Click(object sender, RoutedEventArgs e) 
                {
                   Destroy(); Admininterface(id+1);   
                } 
            
            Button Early = new Button(); Early.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255)); 
                Early.Foreground = Brushes.White;
                Early.Click += Early_Click; canvas.Children.Add(Early); obj.Add(Early); 
                Canvas.SetLeft(Early, 10); Canvas.SetTop(Early, 280); Early.Content = "Eraly";
                void Early_Click(object sender, RoutedEventArgs e)
                {
                    Destroy(); Admininterface(id - 1);
                }
            Button saveChanges = new Button(); saveChanges.Background = new SolidColorBrush(Color.FromRgb(96, 171, 255));
                saveChanges.Foreground = Brushes.White; saveChanges.Content = "Save";
                saveChanges.Click += SaveChanges; canvas.Children.Add(saveChanges); obj.Add(saveChanges);
                Canvas.SetLeft(saveChanges,300); Canvas.SetTop(saveChanges,280);
                void SaveChanges(object sender, RoutedEventArgs e)
                {
                    UserList[id].Name = Name.Text;
                    UserList[id].Surname = SurName.Text;
                    UserList[id].Address = Adderss.Text;
                    UserList[id].Position = Position.Text;
                    UserList[id].Salary = Convert.ToInt32(Salary.Text);
                    UserList[id].Current_tasks = Current_tasks.Text;
                    UserList[id].Characteristic = Characteristic.Text;
                int salaryint = Convert.ToInt32(Salary.Text);
                    using (NpgsqlConnection con = GetConnection())
                    {
                        con.Open();
                        var cmd3 = new NpgsqlCommand("UPDATE users SET user_name = @NameBox.Text, user_surname = @SurNameBox.Text, user_position = @Position.Text , user_salary = @salaryint , user_addres = @AddressBox.Text , user_current_tasks = @Current_tasks, user_characteristic = @Characteristic WHERE user_id = @id", con);
                        cmd3.Parameters.AddWithValue("@id", id);//ID=ID of first table
                        cmd3.Parameters.AddWithValue("@NameBox.Text", Name.Text);
                        cmd3.Parameters.AddWithValue("@SurNameBox.Text", SurName.Text);
                        cmd3.Parameters.AddWithValue("@Position.Text", Position.Text);
                        cmd3.Parameters.AddWithValue("@salaryint", Convert.ToInt32(Salary.Text));//ID=ID of first table
                        cmd3.Parameters.AddWithValue("@AddressBox.Text", Adderss.Text);
                        cmd3.Parameters.AddWithValue("@Current_tasks", Current_tasks.Text);
                        cmd3.Parameters.AddWithValue("@Characteristic", Characteristic.Text);
                        cmd3.ExecuteNonQuery();
                    }
                }
        }
    }
}