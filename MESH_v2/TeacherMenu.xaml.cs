using DataAccessLib;

//using Excel = Microsoft.Office.Interop.Excel;
//using Windows.ApplicationModel.AppService;
//using System.Threading;
using Ganss.Excel;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MESH_v2
{
    public sealed partial class TeacherMenu : Page
    {
        private Discipline selectedDiscipline;
        private StudentGroup selectedGroup;
        private int selectedStudentId;
        private string selectedMark;

        private ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();
        private ObservableCollection<User> students = new ObservableCollection<User>();
        private ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();

        private ObservableCollection<ObservableCollection<string>> studentMarks = new ObservableCollection<ObservableCollection<string>>();
        private ObservableCollection<string> markTypes = new ObservableCollection<string> { "5", "4", "3", "2", "НБ" };
        
        public TeacherMenu()
        {
            groups = DataAccessClass.GetGroups();

            this.InitializeComponent();
            GroupSelectionBox.ItemsSource = groups;
        }

        public class Mark
        {
            public string date { get; set; }
            public string student { get; set; }
            public string mark { get; set; }
        }

        

        private void GroupSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedGroup = (sender as ComboBox).SelectedItem as StudentGroup;
            students = DataAccessClass.GetUsersFromGroup(selectedGroup.Title);
            disciplines = new ObservableCollection<Discipline>(DataAccessClass.GetDisciplines().Where(d =>
           (GroupSelectionBox.SelectedItem as StudentGroup).DisciplinesIds.Split('|').Select(t => Convert.ToInt32(t)).Contains(d.Id)));

            StudentSelectionBox.ItemsSource = students;
            DisciplineSelectionBox.ItemsSource = disciplines;

            MarkSelectionBox.SelectedIndex = -1;
            DisciplineSelectionBox.SelectedIndex = -1;
            StudentSelectionBox.SelectedIndex = -1;
        }

        private void MarkSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                selectedMark = (sender as ComboBox).SelectedItem as string;
            }
        }

        private void DisciplineSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                selectedDiscipline = (sender as ComboBox).SelectedItem as Discipline;
            }
        }

        private void StudentSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                selectedStudentId = ((sender as ComboBox).SelectedItem as User).Id;
            }
        }

        private void AddMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudentSelectionBox.SelectedItem != null && selectedDiscipline != null && selectedGroup != null && selectedMark != null)
            {
                DateTimeOffset date = MarkDatePicker.Date ?? DateTimeOffset.Now;

                DataAccessClass.AddMark(selectedStudentId, date, selectedDiscipline.Id, selectedMark, DescriptionBox.Text);
            }
        }

        private void ExportToExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            //pohui//rabotaet
            if (GroupSelectionBox.SelectedIndex != -1 && DisciplineSelectionBox.SelectedIndex != -1)
            {
                var marks = DataAccessClass.GetStudentsMarks().OrderBy(m => m.Date);
                List<Mark> marksToExport = new List<Mark>();
                foreach (StudentMark m in marks)
                {
                    marksToExport.Add(new Mark { date = m.Date.Date.ToString(), mark = m.Mark, student = students.Where(s => s.Id == m.stId).FirstOrDefault().Login });
                }

                string tableName = $@"{ApplicationData.Current.LocalFolder.Path}\{(GroupSelectionBox.SelectedItem as StudentGroup).Title}_{DateTime.Now.Day}.xlsx";
                ExcelMapper mapper = new ExcelMapper();
                mapper.Save(tableName, marksToExport, "retards", true);
                var toastContent = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children ={new AdaptiveText(){Text = "Success!"}, new AdaptiveText(){Text = "Документ успешно экспортирован"}}
                        }
                    }
                };

                var toastNotif = new ToastNotification(toastContent.GetXml());

                ToastNotificationManager.CreateToastNotifier().Show(toastNotif);


            }
            else
            {
                var toastContent = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children ={new AdaptiveText() {Text = "Export error!"},new AdaptiveText(){Text = "Группа и дисциплина обязательно должны быть выбраны."}}
                        }
                    }
                };

                var toastNotif = new ToastNotification(toastContent.GetXml());

                ToastNotificationManager.CreateToastNotifier().Show(toastNotif);
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}