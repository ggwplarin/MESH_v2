using DataAccessLib;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace MESH_v2
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            DataAccessClass.InitializeDatabase();
            this.InitializeComponent();
            //gg.Text = ApplicationData.Current.LocalFolder.Path;
        }

        private void goAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminMenu));
        }

        private void LoginMenuConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginMenuLoginBox.Text != "" && LoginMenuPasswordBox.Password != "")
            {
                User temp = DataAccessClass.ValidateUser(LoginMenuLoginBox.Text, LoginMenuPasswordBox.Password);
                if (temp != null)//я хочу спать и пока хз на что в 7# это заменить
                {
                    switch (temp.Role)
                    {
                        case "Admin":
                            this.Frame.Navigate(typeof(AdminMenu), temp);
                            break;

                        case "Teacher":
                            this.Frame.Navigate(typeof(TeacherMenu), temp);
                            break;

                        case "Student":
                            this.Frame.Navigate(typeof(StudentMenu), temp);
                            break;
                            
                    }
                    LoginMenuLoginBox.Text = string.Empty;
                    LoginMenuPasswordBox.Password = string.Empty;
                }
                else
                {
                    DisplayBadCredentialsDialog();
                }
            }
        }

        private void goTecher_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TeacherMenu));
        }

        private void REInitializeDB_Click(object sender, RoutedEventArgs e)
        {
            DataAccessClass.REInitializeDatabase();
        }

        private async void DisplayBadCredentialsDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "Ошибка!",
                Content = "Проверьте правильность введенных данных.",
                CloseButtonText = "Продолжить"
            };

            await noWifiDialog.ShowAsync();
        }
    }
}