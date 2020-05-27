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
using System.Reflection.Metadata;
using SQLitePCL;

namespace MESH_v2
{



    public sealed partial class AdminMenu : Page
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        ObservableCollection<string> roles = new ObservableCollection<string>{"Admin","Teacher","Student"};
        ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();
        ObservableCollection<Discipline> selectedDisciplines = new ObservableCollection<Discipline>();

        
        public AdminMenu()
        {
            if((users.Where(u => u.Login == "" && u.Password == "").Count())== 1){

            }
            
            
            
            users = DataAccessClass.GetUsers();
           
            this.InitializeComponent();
        }

        private void DeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedIndex != -1)
            {
                DataAccessClass.DeleteUser(users[UsersGrid.SelectedIndex].Id);
                users = DataAccessClass.GetUsers();
                UsersGrid.ItemsSource = users;
            }
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void AddUserConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            string role = "";
            string group = "";
            if ($"{AddNewUserRoleCBox.SelectedItem as string}" == "Student")
            {
                group = $"{AddNewUserGroupCBox.SelectedItem as string}";
            }
            role = $"{AddNewUserRoleCBox.SelectedItem as string}";
            if (AddNewUserRoleCBox.SelectedIndex != -1&& AddUserLoginBox.Text != ""&& AddUSerPasswordBox.Password != "")
            {
                DataAccessClass.AddUser(AddUserLoginBox.Text, AddUSerPasswordBox.Password, role, group);
                users = DataAccessClass.GetUsers();
                UsersGrid.ItemsSource = users;

                AddUserLoginBox.Text = "";
                AddUSerPasswordBox.Password = "";
                AddNewUserRoleCBox.SelectedIndex = -1;
                AddNewUserGroupCBox.SelectedIndex = -1;
                AddNewUserFlyout.Hide();
            }
        }
        
        

        private void AddNewUserRoleCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (AddNewUserRoleCBox.SelectedIndex == 2)
            {
                AddNewUserGroupCBox.IsEnabled = true;
            }
            else
            {
                AddNewUserGroupCBox.IsEnabled = false;
            }
        }

        private void UsersGrid_BeginningEdit(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridBeginningEditEventArgs e)
        {

        }

        private void UsersGrid_CellEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridCellEditEndedEventArgs e)
        {
            if (users[UsersGrid.SelectedIndex].Role != "Student")
            {
                users[UsersGrid.SelectedIndex].Group = "";
                
            }
            DataAccessClass.ChangeUserData(users[UsersGrid.SelectedIndex].Id, users[UsersGrid.SelectedIndex].Login, users[UsersGrid.SelectedIndex].Password, users[UsersGrid.SelectedIndex].Role, users[UsersGrid.SelectedIndex].Group);
            
            

        }


        private void AddNewGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }


        public List<string> searchDisciplines(string query)
        {
            
            return disciplines.Select(t => t.Title).Where(
            d => d.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1).
            OrderByDescending(i => i.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ThenBy(i => i).ToList();
        }

        private void DisciplineSelectionASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //sender.ItemsSource = 
            }
        }

        private void DisciplineSelectionASBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void DisciplineSelectionASBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }
    }
}
