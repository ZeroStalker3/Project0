using Project0.ClassHelper;
using Project0.DataBase;
using Project0.Pages;
using System.Windows;
using System.Windows.Input;

namespace Project0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameApp.frmObj = MainFrm;
            //NextPageForMain.frmObj = FrmPage;
            OdbConnecHelper.entObj = new DataBaseForProjectFEntities();
            FrameApp.frmObj.Navigate(new Author());
        }

        // Обработчик нажатия клавиши
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            // Если нажата клавиша Esc
            if (e.Key == Key.Back)
            {
                // Проверяем, находимся ли мы на начальной странице
                if (FrameApp.frmObj.Content is Author)
                {
                    // Показываем диалог для подтверждения выхода
                    MessageBoxResult result = MessageBox.Show(
                        "Вы действительно хотите закрыть программу?",
                        "Выход",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    // Если пользователь выбрал "Да", закрываем приложение
                    if (result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                }
                else if (FrameApp.frmObj.CanGoBack) // Возврат на предыдущую страницу
                {
                    FrameApp.frmObj.GoBack();
                }
            }
        }
    }
}
