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
        //static AppServiceConnection connection = null;
        //static AutoResetEvent appServiceExit;

        private Discipline selectedDiscipline;
        private StudentGroup selectedGroup;
        private int selectedStudentId;
        private string selectedMark;

        private ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();
        private ObservableCollection<User> students = new ObservableCollection<User>();
        private ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();

        //private ObservableCollection<StudentMark> marks = new ObservableCollection<StudentMark>();
        private ObservableCollection<ObservableCollection<string>> studentMarks = new ObservableCollection<ObservableCollection<string>>();

        private ObservableCollection<string> markTypes = new ObservableCollection<string> { "5", "4", "3", "2", "НБ" };

        //static string tableName;
        //static int i1, i2;
        //static string[][] arrToExport;
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

        // ebal rot uwp i microsoft
        //private void FillMarksGrid()
        //{
        //    //studentMarks.Add(new ObservableCollection<string>((new List<string>() {  }).Concat)
        //    //studentMarks
        //}

        //static async void InitializeAppServiceConnection()
        //{
        //    connection = new AppServiceConnection();
        //    connection.AppServiceName = "ExcelInteropService";
        //    connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
        //    connection.RequestReceived += Connection_RequestReceived;
        //    connection.ServiceClosed += Connection_ServiceClosed;

        //    AppServiceConnectionStatus status = await connection.OpenAsync();
        //    if (status != AppServiceConnectionStatus.Success)
        //    {
        //        // TODO: error handling
        //    }
        //}
        //private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        //{
        //    // signal the event so the process can shut down
        //    appServiceExit.Set();
        //}

        //private async static void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        //{
        //    string value = args.Request.Message["REQUEST"] as string;
        //    string result = "";
        //    switch (value)
        //    {
        //        case "CreateSpreadsheet":
        //            //try
        //            //{
        //                // call Office Interop APIs to create the Excel spreadsheet
        //                //Excel.Application excel = new Excel.Application();
        //                //excel.Visible = true;
        //                //Excel.Workbook wb = excel.Workbooks.Add();
        //                //Excel.Worksheet sh = wb.Sheets.Add();
        //                //sh.Name = "DataGrid";
        //                //sh.Cells[1, "A"].Value2 = "Id";
        //                //sh.Cells[1, "B"].Value2 = "Description";
        //                //sh.Cells[1, "C"].Value2 = "Quantity";
        //                //sh.Cells[1, "D"].Value2 = "UnitPrice";

        //                //for (int i = 0; i < args.Request.Message.Values.Count / 4; i++)
        //                //{
        //                //    sh.Cells[i + 2, "A"].Value2 = args.Request.Message["Id" + i.ToString()] as string;
        //                //    sh.Cells[i + 2, "B"].Value2 = args.Request.Message["Description" + i.ToString()] as string;
        //                //    sh.Cells[i + 2, "C"].Value2 = args.Request.Message["Quantity" + i.ToString()].ToString();
        //                //    sh.Cells[i + 2, "D"].Value2 = args.Request.Message["UnitPrice" + i.ToString()].ToString();
        //                //}

        //                Excel.Application ex = new Excel.Application();
        //                ex.Visible = true;

        //                var wsh = new Excel.Worksheet();
        //                Excel.Range c1 = (Excel.Range)wsh.Cells[1, 1];
        //                Excel.Range c2 = (Excel.Range)wsh.Cells[i1, i2];
        //                Excel.Range range = wsh.get_Range(c1, c2);
        //                //string[][] arrToExport = (studentMarks.Select(t => t.ToArray()).ToArray());
        //                range.Value = arrToExport;
        //                range.EntireColumn.AutoFit();
        //                ex.Application.ActiveWorkbook.SaveAs(tableName);
        //                ex.Application.ActiveWorkbook.Close();
        //                ex.Application.Quit();

        //                result = "SUCCESS";
        //            //}
        //            //catch (Exception exc)
        //            //{
        //            //    result = exc.Message;
        //            //}
        //            break;
        //        default:
        //            result = "unknown request";
        //            break;
        //    }

        //    ValueSet response = new ValueSet();
        //    response.Add("RESPONSE", result);
        //    await args.Request.SendResponseAsync(response);
        //}

        //public void ExportMarks()
        //{
        //    if (GroupSelectionBox.SelectedIndex != -1 && DisciplineSelectionBox.SelectedIndex != -1)
        //    {
        //        appServiceExit = new AutoResetEvent(false);

        //        marks = DataAccessClass.GetStudentsMarks();
        //        ObservableCollection<StudentMark> filtredMarks = new ObservableCollection<StudentMark>(marks.Where(m =>
        //        students.Select(s => s.Id).Contains(m.stId)));
        //        List<DateTimeOffset> dates = filtredMarks.Select(m => m.Date).Distinct().OrderBy(d => d).ToList();

        //        studentMarks = new ObservableCollection<ObservableCollection<string>>(dates.Select(d =>
        //        new ObservableCollection<string>(new List<string>() { "" }.Concat(filtredMarks.Select(m => m.Date == d ? m.Mark : "")))));
        //        studentMarks.Insert(0, new ObservableCollection<string>((new List<string>() { "STUDENTS" }).Concat(students.Select(s => s.Login).ToList())));

        //        //Excel.Application ex = new Excel.Application();
        //        //ex.Visible = true;
        //        tableName = $"{(GroupSelectionBox.SelectedItem as StudentGroup).Title}_{DateTime.Now.Day}.xlsx";
        //        //var wsh = new Excel.Worksheet();
        //        //Excel.Range c1 = (Excel.Range)wsh.Cells[1, 1];
        //        //Excel.Range c2 = (Excel.Range)wsh.Cells[studentMarks.Count, studentMarks.First().Count];
        //        //Excel.Range range = wsh.get_Range(c1, c2);
        //        i1 = studentMarks.Count;
        //        i2 = studentMarks.First().Count;
        //        arrToExport = (studentMarks.Select(t => t.ToArray()).ToArray());
        //        //range.Value = arrToExport;
        //        //range.EntireColumn.AutoFit();
        //        //ex.Application.ActiveWorkbook.SaveAs(tableName);
        //        //ex.Application.ActiveWorkbook.Close();
        //        //ex.Application.Quit();

        //        // ща буит говнокод
        //        //int column,row = 1;

        //        //foreach(string[] c in listToExport)
        //        //{
        //        //    foreach
        //        //}
        //        // не, не буит
        //        InitializeAppServiceConnection();

        //    }

        //ExcelMapper excel = new ExcelMapper();

        //excel.Save($@"{ApplicationData.Current.LocalFolder.Path}\{tableName}",listToExport,0,true);

        //}

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
            //ExportMarks();
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
                            Children =
                                        {
                                            new AdaptiveText()
                {
                    Text = "Success!"
                },
                new AdaptiveText()
                {
                    Text = "Документ успешно экспортирован"
                }
            }
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
                            Children =
                                        {
                                            new AdaptiveText()
                {
                    Text = "Export error!"
                },
                new AdaptiveText()
                {
                    Text = "Группа и дисциплина обязательно должны быть выбраны."
                }
            }
                        }
                    }
                };

                var toastNotif = new ToastNotification(toastContent.GetXml());

                ToastNotificationManager.CreateToastNotifier().Show(toastNotif);
            }
        }
    }
}