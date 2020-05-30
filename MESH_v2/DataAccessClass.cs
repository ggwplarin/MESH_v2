using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.Reflection.Metadata.Ecma335;
using SQLitePCL;
using Microsoft.Toolkit.Uwp.UI.Animations.Behaviors;
using MESH_v2;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace DataAccessLib
{
    public static class DataAccessClass
    {
        static string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "MESH.sqlite");
        public static void InitializeDatabase()
        {


            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Users (id_users INTEGER PRIMARY KEY, login VARCHAR UNIQUE NOT NULL, password VARCHAR NOT NULL, userRole TEXT, userGroup TEXT);" +
                    "CREATE TABLE IF NOT EXISTS Disciplines (id_disciplines INTEGER PRIMARY KEY, disciplineTitle TEXT UNIQUE NOT NULL, teacherId INT NOT NULL, inactive BOOLEAN NOT NULL);" +
                    "CREATE TABLE IF NOT EXISTS StudentsGroups (id_stgroups INTEGER PRIMARY KEY, groupTitle TEXT UNIQUE NOT NULL,hidden BOOLEAN NOT NULL, groupDisciplines TEXT NOT NULL);" +
                    "CREATE TABLE IF NOT EXISTS StudentsMarks (studentId INT NOT NULL, date BLOB NOT NULL,disciplineId INT NOT NULL, mark TEXT NOT NULL, description TEXT, FOREIGN KEY (studentId) REFERENCES Users(id))";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        //=============================================================
        //users
        //=============================================================
        public static int ValidateUser(string login, string password)
        {

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM Users WHERE login = '{login}' AND password = '{password}'";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetString(3) == "Admin") return 0;
                    if (reader.GetString(3) == "Teacher") return 1;
                    if (reader.GetString(3) == "Student") return 2;
                }

                return -1;
            }

        }

        public static int AddUser(string login, string password, string role, string group)
        {

            if (login != "" && password != "")
            {
                using (SqliteConnection db =
                   new SqliteConnection($"Data Source ={dbpath}"))
                {
                    db.Open();

                    String tableCommand = $"INSERT INTO Users(login, password, userRole, userGroup) VALUES('{login}', '{password}', '{role}','{group}')";

                    try
                    {
                        SqliteCommand command = new SqliteCommand(tableCommand, db);

                        command.ExecuteReader();
                    }
                    catch (SqliteException)
                    {
                        return 1;
                    }
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public static int ChangeUserData(int id, string login, string password, string role, string group)
        {
            using (SqliteConnection db =
                              new SqliteConnection($"Data Source ={dbpath}"))
            {
                db.Open();

                String tableCommand = $"UPDATE users SET login = '{login}', password = '{password}', userRole = '{role}', userGroup = '{group}' WHERE id_users = {id}";

                //try
                //{
                SqliteCommand command = new SqliteCommand(tableCommand, db);

                command.ExecuteReader();
                //}
                //catch (SqliteException)
                //{
                //    return -1;
                //}
            }
            return 0;
        }

        public static int DeleteUser(int id)
        {

            using (SqliteConnection db =
                               new SqliteConnection($"Data Source ={dbpath}"))
            {
                db.Open();

                String tableCommand = $"DELETE FROM users WHERE id = {id}";

                try
                {
                    SqliteCommand command = new SqliteCommand(tableCommand, db);

                    command.ExecuteReader();
                }
                catch (SqliteException)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static ObservableCollection<MESH_v2.User> GetUsers()
        {
            ObservableCollection<MESH_v2.User> users = new ObservableCollection<MESH_v2.User>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM Users";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new MESH_v2.User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
                return users;
            }

        }

        public static ObservableCollection<MESH_v2.User> GetUsersFromGroup(string group)
        {
            ObservableCollection<MESH_v2.User> users = new ObservableCollection<MESH_v2.User>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM Users WHERE GROUP = '{group}'";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new MESH_v2.User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
                return users;
            }

        }
        //=============================================================


        //=============================================================
        //groups
        //=============================================================
        public static int AddGroup(string title, string disciplines)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"INSERT INTO StudentsGroups (groupTitle, groupDisciplines, hidden) VALUES('{title}', '{disciplines}', 'false')";



                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
            return 0;
        }

        public static ObservableCollection<string> GetGroupsTitles()
        {
            ObservableCollection<string> users = new ObservableCollection<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM StudentsGroups";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(reader.GetString(1));
                }
                return users;
            }

        }

        public static ObservableCollection<string> GetGroupsTitles(bool getHidden)
        {
            ObservableCollection<string> users = new ObservableCollection<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM StudentsGroups WHERE hidden = {getHidden}";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(reader.GetString(1));
                }
                return users;
            }

        }
        //=============================================================




        //=============================================================
        //disciplines
        //=============================================================
        public static int AddDiscipline(string title, int teacherId, bool inactive)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"INSERT INTO Disciplines (disciplineTitle, teacherId, inactive) VALUES('{title}', '{teacherId}', 'false')";


                try
                {
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                }
                catch (SqliteException)
                {
                    return 1;
                }
            }
            return 0;
        }


        public static ObservableCollection<Discipline> GetDisciplines()
        {
            ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM Disciplines";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    disciplines.Add(new Discipline(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetBoolean(3)));
                }
                return disciplines;
            }
        }
        public static ObservableCollection<Discipline> GetDisciplines(bool getInactive)
        {
            ObservableCollection<Discipline> users = new ObservableCollection<Discipline>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM Disciplines WHERE inactive = {getInactive}";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Discipline(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetBoolean(3)));
                }
                return users;
            }
        }
        //=============================================================



        //=============================================================
        //marks
        //=============================================================
        public static int AddMark(int id, DateTimeOffset date ,string discipline, string mark)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"INSERT INTO StudentsMarks (studentId, date, disciplineId, mark) VALUES('{id}', '{date}', '{discipline}', '{mark}')";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
            return 0;
        }
    }
}
