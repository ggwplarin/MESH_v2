using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<StudentMark> marks = new ObservableCollection<StudentMark>();
        private ObservableCollection<ObservableCollection<string>> studentMarks = new ObservableCollection<ObservableCollection<string>>();
        private ObservableCollection<string> markTypes = new ObservableCollection<string> { "5", "4", "3", "2", "НБ" };

        public TeacherMenu()
        {
            groups = DataAccessClass.GetGroups();

            this.InitializeComponent();
            GroupSelectionBox.ItemsSource = groups;
        }

        private void FillMarksGrid()
        {
            marks = DataAccessClass.GetStudentsMarks();
            ObservableCollection<StudentMark> filtredMarks = new ObservableCollection<StudentMark>(marks.Where(m =>
            students.Select(s => s.Id).Contains(m.stId)));
            List<DateTimeOffset> dates = filtredMarks.Select(m => m.Date).Distinct().OrderBy(d => d).ToList();

            studentMarks.Add(new ObservableCollection<string>((new List<string>() { "STUDENTS" }).Concat(students.Select(s => s.Login).ToList())));

            //studentMarks
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

                DataAccessClass.AddMark(selectedStudentId, date, selectedDiscipline.Id, selectedMark,DescriptionBox.Text);
            }
        }
    }
}