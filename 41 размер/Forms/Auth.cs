using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace _41_размер.Forms
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        static bool Captcha = false;
        static String StrCaptcha = "";
        public static string Role = "";
        public static string User = "";


        private void Auth_Load(object sender, EventArgs e)
        {
            try
            {
                Captcha = false;
                groupBox1.Visible=Captcha;
                textBox1.Text="";
                textBox2.Text="";
                textBox3.Text="";
            }
            catch (Exception) { MessageBox.Show("Ошибка формы авторизации."); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewProduct ViewProduct = new ViewProduct();
            this.Visible = false;
            ViewProduct.ShowDialog();
            this.Visible = true;
        }

        private void RandomCaptcha()
        {
            try
            {
                string rez = "";
                string str = "qwertyuiopasdfghjklzxcvbnm1234567890";
                Random rdm = new Random();
                for (int i = 0; i < 8; i++)
                {
                    rez += str[rdm.Next(str.Length - 1)];
                }
                label4.Text = rez[0].ToString() + rez[1].ToString();
                label5.Text = rez[2].ToString() + rez[3].ToString();
                label6.Text = rez[4].ToString() + rez[5].ToString();
                label7.Text = rez[6].ToString() + rez[7].ToString();
                StrCaptcha = rez;
            }
            catch (Exception) { MessageBox.Show("Ошибка в создании капчи"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text=="" || textBox2.Text==""){
                    MessageBox.Show("Поля Логин и Пароль обязательны для ввода");
                    return;
                }
                DataTable UserTable = Classes.RequestsDB.SelectTable("User", "UserLogin='" + textBox1.Text + "' and  UserPassword='" + textBox2.Text + "'");
                if(UserTable.Rows.Count>0)
                {
                    if (Captcha)
                    {
                        if (StrCaptcha != textBox3.Text) 
                        {
                           
                            MessageBox.Show("Каптча введена неверно. Приложение заблокировано на 10 с");
                            Thread.Sleep(10000);
                            RandomCaptcha();
                            textBox3.Text = "";
                            MessageBox.Show("Приложение разблокировано");
                        }

                    }
                    Captcha = true;
                    groupBox1.Visible = Captcha;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    User = UserTable.Rows[0]["UserSurname"].ToString() + " " + UserTable.Rows[0]["UserName"].ToString() + " " + UserTable.Rows[0]["UserPatronymic"].ToString();
                    Role = UserTable.Rows[0]["UserRole"].ToString();
                    if (Role == "1")
                    {
                        ViewProduct ViewProduct = new ViewProduct();
                        this.Visible = false;
                        ViewProduct.ShowDialog();
                        User = "";
                        Role = "";
                        this.Visible = true;
                    }
                    else
                    {
                        Menu Menu = new Menu();
                        this.Visible = false;
                        Menu.ShowDialog();
                        User = "";
                        Role = "";
                        this.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Пароль или Логин введен неверно");
                    if (Captcha)
                    {
                        MessageBox.Show("Приложение заблокировано на 10 с");
                        Thread.Sleep(10000);
                        RandomCaptcha();
                        textBox3.Text = "";
                        MessageBox.Show("Приложение разблокировано");
                    }
                    else
                    {
                        Captcha = true;
                        groupBox1.Visible = Captcha;
                        RandomCaptcha();
                        textBox3.Text = "";
                    }
                }
            
            }
            catch (Exception) { MessageBox.Show("Ошибка Авторизации"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandomCaptcha();
            textBox3.Text = "";
        }
    }
}
