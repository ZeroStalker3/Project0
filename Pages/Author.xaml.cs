using Project0.ClassHelper;
using Project0.DataBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project0.Pages
{
    /// <summary>
    /// Логика взаимодействия для Author.xaml
    /// </summary>
    public partial class Author : Page
    {
        public Author()
        {
            InitializeComponent();
        }

        private void LoginTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void PasswordTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTxt.Password == "" | LoginTxt.Text == "")
            {
                MessageBox.Show("Не веден логин или пароль",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    var userObj = OdbConnecHelper.entObj.User.FirstOrDefault(x => x.Password == PasswordTxt.Password && x.Login == LoginTxt.Text);

                    if (userObj == null)
                    {
                        MessageBox.Show("Что-то пошло не так!",
                            "Ошибка",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error);
                        resetpassword_Click(sender, e);
                    }
                    else
                    {
                        if (userObj.Id_Post == 5 || userObj.Id_Post == 6)
                        {
                            MessageBox.Show("Добро пожаловать в систему",
                                "Приветствую",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
                            FrameApp.frmObj.Navigate(new PageAdmin());
                            PasswordTxt.Password = null;
                            LoginTxt.Text = null;
                        }
                        else
                        {
                            MessageBox.Show("Добро пожаловать в систему",
                                "Приветствую",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
                            FrameApp.frmObj.Navigate(new PageMain());
                            PasswordTxt.Password = null;
                            LoginTxt.Text = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Критический сбор в работе приложения", "Уведомление",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }

        private void resetpassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Свяжитесь с администратором для сброса пароля");
        }
    }
}
