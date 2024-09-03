using Project0.ClassHelper;
using Project0.DataBase;
using Project0.PagesSecondaires;
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

namespace Project0.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            InitializeComponent();

            LoadData();
            rolecmb.SelectedValue = 2;
        }

        private void LoadData()
        {
            Employees.DisplayMemberPath = "Nam";
            Employees.SelectedValuePath = "Id";
            Employees.ItemsSource = OdbConnecHelper.entObj.Post.ToList();

            rolecmb.DisplayMemberPath = "Name";
            rolecmb.SelectedValuePath = "Id";
            rolecmb.ItemsSource = OdbConnecHelper.entObj.Role.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void AddIncidentButton_Click(object sender, RoutedEventArgs e)
        {
            string employees;
            string role;
            string nameEmp = NameEmp.Text;
            string PasswordEmpl = PasswordEmployee.Text;
            string LogEmpl = LoginEmployee.Text;
            employees = Convert.ToString(Employees.SelectedValue);
            role = Convert.ToString(rolecmb.SelectedValue);
            //MessageBox.Show(employees); //Для проверки какой ID пользователя
            if (string.IsNullOrWhiteSpace(nameEmp) || string.IsNullOrWhiteSpace(employees) || string.IsNullOrWhiteSpace(PasswordEmpl) || string.IsNullOrWhiteSpace(LogEmpl) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Employee employee = new Employee() 
            {
                Name = nameEmp,
                Id_Post = Convert.ToInt32(employees)
            };

            User user = new User()
            {
                IdRole = Convert.ToInt32(role),
                Id_Post = Convert.ToInt32(employees),
                Name = nameEmp,
                Password = PasswordEmpl,
                Login = LoginEmployee.Text
            };

            // Вставьте код для сохранения записи, например, в базу данных или файл
            OdbConnecHelper.entObj.Employee.Add(employee);
            OdbConnecHelper.entObj.User.Add(user);
            OdbConnecHelper.entObj.SaveChangesAsync();
            MessageBox.Show("Запись добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очистка полей ввода
            NameEmp.Clear();
            Employees.SelectedIndex = 0;
            LoginEmployee.Clear();
            PasswordEmployee.Clear();
            rolecmb.SelectedIndex = 2;
        }
    }
}
