using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLib;
using Microsoft.Toolkit.Extensions;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MESH_v2
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>

    
    public sealed partial class AdminMenu : Page
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        public AdminMenu()
        {
            
            DataAccessClass.InitializeDatabase();
            
            users = DataAccessClass.GetUsers();
           
            this.InitializeComponent();
        }

        private void DeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            DataAccessClass.DeleteUser(users[UsersGrid.SelectedIndex].Id);
            users = DataAccessClass.GetUsers();
            UsersGrid.ItemsSource = users;
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void AddUserConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            string role = "";
            string group = "";
            switch (AddNewUserRoleCBox.SelectedIndex){
                case 0:
                    role = "Admin";
                    break;
                case 1:
                    role = "Teacher";
                    break;
                case 2:
                    role = "Student";
                    group = $"Group №{AddNewUserGroupCBox.SelectedIndex}";
                    break;
            }
            DataAccessClass.AddUser(AddUserLoginBox.Text,AddUSerPasswordBox.Password,role,group);
            users = DataAccessClass.GetUsers();
            UsersGrid.ItemsSource = users;
        }

        

        private void AddNewUserRoleCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AddNewUserRoleCBox.SelectedIndex == 2)
            {
                AddNewUserGroupCBox.IsEnabled = true;
            }
            else
            {
                AddNewUserGroupCBox.IsEnabled = false;
            }
        }
    }
}
