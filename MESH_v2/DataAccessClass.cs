﻿using MESH_v2;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace DataAccessLib
{
    public static class DataAccessClass
    {
        private static string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "MESH.sqlite");

        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Users (id_users INTEGER PRIMARY KEY, login VARCHAR UNIQUE NOT NULL, password VARCHAR NOT NULL, userRole TEXT, userGroup TEXT);" +
                    "CREATE TABLE IF NOT EXISTS Disciplines (id_disciplines INTEGER PRIMARY KEY, disciplineTitle TEXT UNIQUE NOT NULL, teacherId INT NOT NULL);" +
                    "CREATE TABLE IF NOT EXISTS StudentsGroups (id_stgroups INTEGER PRIMARY KEY, groupTitle TEXT UNIQUE NOT NULL, groupDisciplines TEXT NOT NULL);" +
                    "CREATE TABLE IF NOT EXISTS StudentsMarks (studentId INT NOT NULL, date BLOB NOT NULL,disciplineId INT NOT NULL, mark TEXT NOT NULL, description TEXT, FOREIGN KEY (studentId) REFERENCES Users(id))";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void REInitializeDatabase()
        {

            if (File.Exists(dbpath)) File.Delete(dbpath);
            InitializeDatabase();
        }

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

                String tableCommand = $"DELETE FROM users WHERE id_users = {id}";

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

                String tableCommand = $"SELECT * FROM Users WHERE userGroup = '{group}'";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new MESH_v2.User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
                return users;
            }
        }

        public static int AddGroup(string title, string disciplines)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"INSERT INTO StudentsGroups (groupTitle, groupDisciplines) VALUES('{title}', '{disciplines}')";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                command.ExecuteReader();
            }
            return 0;
        }

        public static ObservableCollection<StudentGroup> GetGroups()
        {
            ObservableCollection<StudentGroup> users = new ObservableCollection<StudentGroup>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM StudentsGroups";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new StudentGroup(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                return users;
            }
        }

        public static int AddDiscipline(string title, int teacherId)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"INSERT INTO Disciplines (disciplineTitle, teacherId, inactive) VALUES('{title}', '{teacherId}')";

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

        public static int AddMark(int id, DateTimeOffset date, string discipline, string mark)
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

        public static ObservableCollection<StudentMark> GetStudentsMarks()
        {
            ObservableCollection<StudentMark> users = new ObservableCollection<StudentMark>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = $"SELECT * FROM StudentsMarks";

                SqliteCommand command = new SqliteCommand(tableCommand, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new StudentMark(reader.GetInt32(0),reader.GetDateTimeOffset(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4)));
                }
                return users;
            }
        }
    }
}