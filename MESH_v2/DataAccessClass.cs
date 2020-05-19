﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.Reflection.Metadata.Ecma335;
using SQLitePCL;
using Microsoft.Toolkit.Uwp.UI.Animations.Behaviors;
using MESH_v2;

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

                String tableCommand = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY, login VARCHAR UNIQUE NOT NULL, password VARCHAR NOT NULL, access TEXT)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static bool ValidateUser(string login, string password)
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
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static int AddUser(string login, string password, string access)
        {
            if (login != "" && password != "")
            {
                using (SqliteConnection db =
                   new SqliteConnection($"Data Source ={dbpath}"))
                {
                    db.Open();

                    String tableCommand = $"INSERT INTO users(login, password, access) VALUES('{login}', '{password}', '{access}')";

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

        public static int ChangeUserData(User user)
        {
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
                    users.Add(new MESH_v2.User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                return users;
            }
            
        }
    }
}
