using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TPTtimetable
{
    static class DatabaseService
    {
        static SQLiteConnection db;

        public static void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TPTTunniplaan.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<SchoolWeekClass>();
        }

        public static void CreateTableWithData()
        {
            db.CreateTable<SchoolWeekClass>();
            if (db.Table<SchoolWeekClass>().Count() == 0)
            {
                var firstWeek = new SchoolWeekClass();
                
                db.Insert(firstWeek);
            }
        }

        public static void AddNote(string title, string content)
        {
            var newNote = new SchoolWeekClass();
            db.Insert(newNote);
        }
        public static void EditNote(string newTitle, string newContent, long id)
        {
            var editedNote = new SchoolWeekClass();
            db.Update(editedNote);
        }

        public static void DeleteNote(long id)
        {
            db.Delete<SchoolWeekClass>(id);
        }

        public static TableQuery<SchoolWeekClass> GetAllNotes()
        {
            var table = db.Table<SchoolWeekClass>();
            return table;
        }
    }
}