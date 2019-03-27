﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TPTtimetable
{
    class GetTimetable
    {
        public async Task<List<Tund>> Pull(string url)
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            var timetablejson = html.Substring(7480);
            timetablejson = timetablejson.Substring(0, timetablejson.IndexOf(']') + 1);
            var timetableobject = JsonConvert.DeserializeObject<IList<JsonTund>>(timetablejson);
            List<Tund> timetable = new List<Tund>();
            foreach (var item in timetableobject)
            {
                string lessonname = item.title.Substring(item.title.IndexOf('>') + 1);
                string teachername = "Default";
                string classname = "Default";
                if (lessonname.Split(';').Count() >= 3)
                {
                    teachername = lessonname.Substring(lessonname.IndexOf(';') + 2);
                    classname = teachername.Substring(teachername.IndexOf(';') + 2);
                    classname = classname.Substring(classname.IndexOf("-") + 2, 4);
                    lessonname = lessonname.Substring(0, lessonname.IndexOf('<') - 1);
                    teachername = teachername.Substring(0, teachername.LastIndexOf(';'));
                }
                else
                {
                    classname = lessonname.Substring(lessonname.IndexOf(';') + 2);
                    classname = classname.Substring(classname.IndexOf("-") + 2, 4);
                    lessonname = lessonname.Substring(0, lessonname.IndexOf('<') - 1);

                }
                //Some lessons have an extra HTML element: "valikaine", this code removes it from the teachername variable.
                //This also fixes classname.
                if (teachername.Contains("valikaine"))
                {
                    teachername = teachername.Substring(teachername.IndexOf(';') + 2);
                    classname = classname.Substring(classname.IndexOf(';') + 1);
                }

                Tund tund = new Tund()
                {
                    lessonname = lessonname,
                    classname = classname,
                    teachername = teachername,
                    start = item.start,
                    end = item.end
                };
                timetable.Add(tund);
            }

            return timetable;
        }

        public SchoolWeekClass SortByDay(List<Tund> timetable)
        {
            SchoolWeekClass schoolWeek = new SchoolWeekClass()
            {
                Monday = new List<Tund>(),
                Tuesday = new List<Tund>(),
                Wednesday = new List<Tund>(),
                Thursday = new List<Tund>(),
                Friday = new List<Tund>()
            };
            foreach (var item in timetable)
            {
                DayOfWeek day = item.start.DayOfWeek;

                switch (day)
                {
                    case DayOfWeek.Monday:
                        schoolWeek.Monday.Add(item);
                        break;
                    case DayOfWeek.Tuesday:
                        schoolWeek.Tuesday.Add(item);
                        break;
                    case DayOfWeek.Wednesday:
                        schoolWeek.Wednesday.Add(item);
                        break;
                    case DayOfWeek.Thursday:
                        schoolWeek.Thursday.Add(item);
                        break;
                    case DayOfWeek.Friday:
                        schoolWeek.Friday.Add(item);
                        break;
                    default:
                        break;
                }
            }
            return schoolWeek;
        }
    }

    class JsonTund
    {
        public string link { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string className { get; set; }
    }

    public class Tund
    {
        public string lessonname { get; set; }
        public string classname { get; set; }
        public string teachername { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}