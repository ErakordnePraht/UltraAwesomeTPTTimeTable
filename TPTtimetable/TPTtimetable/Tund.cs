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

namespace TPTtimetable
{
    public class Tund
    {
        public string lessonname { get; set; }
        public string classname { get; set; }
        public string teachername { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}