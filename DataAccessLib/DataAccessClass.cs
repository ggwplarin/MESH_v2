﻿using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Windows.Storage;
namespace DataAccessLib
{
    public class DataAccessClass
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
}
