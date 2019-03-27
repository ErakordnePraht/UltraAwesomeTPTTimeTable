using System;
using System.Collections.Generic;
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
    class SchoolWeekClass
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public long Id { get; set; }
        public List<Tund> Monday { get; set; }
        public List<Tund> Tuesday { get; set; }
        public List<Tund> Wednesday { get; set; }
        public List<Tund> Thursday { get; set; }
        public List<Tund> Friday { get; set; }
    }
}