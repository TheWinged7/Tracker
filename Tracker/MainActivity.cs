using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using Org.Json;

namespace Tracker
{
    [Activity(Label = "@string/app_name", Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected List<bool> week;
        protected AssetManager assets;
        protected TextView goal, current, weekGoal, weekCurrent;
        protected CheckBox mon, tue, wed, thu, fri, sat, sun;
        protected Button setGoal, dayCompleted;
        protected View.IOnClickListener setGoalListener;
        protected JSONObject json;
        private static Context context;
        protected int count;
        protected string filePath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            assets = this.Assets;


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //week = json.Get("week");


            //initUI(raw);

            context = this.BaseContext;
            string raw;
            filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);


            if (!File.Exists(Path.Combine(filePath, "data.json")))
            {
                using (StreamReader sreader = new StreamReader(assets.Open("data.json")))
                {
                    raw = sreader.ReadToEnd();
                }
                File.WriteAllText(Path.Combine(filePath, "data.json"), raw);

            }
            else
            {
                raw = File.ReadAllText(Path.Combine(filePath, "data.json"));
            }

            json = new JSONObject(raw);

            initUI();

        }

        protected string getDayOfWeek()
        {
            return DateTime.Now.DayOfWeek.ToString();
        }


        protected void setDayCompleted()
        {
            string day = getDayOfWeek();
            JSONArray jweek = json.GetJSONArray("week");
            switch (day)
            {
                case "Monday":
                    if (mon.Checked.Equals(false))
                    {
                        mon.Checked = true;

                        jweek.Put(0, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Tuesday":
                    if (tue.Checked.Equals(false))
                    {
                        tue.Checked = true;

                        jweek.Put(1, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Wednesday":
                    if (wed.Checked.Equals(false))
                    {
                        wed.Checked = true;

                        jweek.Put(2, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Thursday":
                    if (thu.Checked.Equals(false))
                    {
                        thu.Checked = true;

                        jweek.Put(3, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Friday":
                    if (fri.Checked.Equals(false))
                    {
                        fri.Checked = true;

                        jweek.Put(4, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Saturday":
                    if (sat.Checked.Equals(false))
                    {
                        sat.Checked = true;

                        jweek.Put(5, true);
                        json.Put("week", jweek);
                    }
                    break;

                case "Sunday":
                    if (sun.Checked.Equals(false))
                    {
                        sun.Checked = true;

                        jweek.Put(6, true);
                        json.Put("week", jweek);
                    }
                    break;

                default:
                    throw new IndexOutOfRangeException(string.Format("ERROR:\t {0} is not a valid day.", day));
            }
            saveData();

        }

        protected override void OnStop()
        {
            base.OnStop();
            saveData();
        }

        protected void saveData()
        {
            File.WriteAllText(Path.Combine(filePath, "data.json"), json.ToString());
        }

        static protected void toasty(string text, ToastLength length)
        {
            Toast toast = Toast.MakeText(context, text, length);
            toast.Show();
        }

        protected void initUI()
        {
            count = 0;
            goal = (TextView)FindViewById(Resource.Id.goal_total);
            current = (TextView)FindViewById(Resource.Id.current_total);
            weekGoal = (TextView)FindViewById(Resource.Id.week_goal);
            weekCurrent = (TextView)FindViewById(Resource.Id.week_total);

            mon = (CheckBox)FindViewById(Resource.Id.mon_check);
            tue = (CheckBox)FindViewById(Resource.Id.tue_check);
            wed = (CheckBox)FindViewById(Resource.Id.wed_check);
            thu = (CheckBox)FindViewById(Resource.Id.thu_check);
            fri = (CheckBox)FindViewById(Resource.Id.fri_check);
            sat = (CheckBox)FindViewById(Resource.Id.sat_check);
            sun = (CheckBox)FindViewById(Resource.Id.sun_check);

            JSONArray jweek = json.GetJSONArray("week");
            mon.Checked = jweek.GetBoolean(0);
            tue.Checked = jweek.GetBoolean(1);
            wed.Checked = jweek.GetBoolean(2);
            thu.Checked = jweek.GetBoolean(3);
            fri.Checked = jweek.GetBoolean(4);
            sat.Checked = jweek.GetBoolean(5);
            sun.Checked = jweek.GetBoolean(6);

            mon.Clickable = false;
            tue.Clickable = false;
            wed.Clickable = false;
            thu.Clickable = false;
            fri.Clickable = false;
            sat.Clickable = false;
            sun.Clickable = false;



            goal.Text = "$" + json.GetString("totalGoal");
            current.Text = "$" + json.GetString("total");
            weekGoal.Text = json.GetString("weekGoal") + " Days";
            for (int i = 0; i < jweek.Length(); i++)
            {
                if (jweek.GetBoolean(i))
                    count++;
            }
            weekCurrent.Text = +count + " Days";

            setGoal = (Button)FindViewById(Resource.Id.set_goal_button);
            dayCompleted = (Button)FindViewById(Resource.Id.day_ding);

            Button clear = (Button)FindViewById(Resource.Id.debug);

            clear.Click += (sender, e) =>
            {
                JSONArray jw = json.GetJSONArray("week");
                for (int i = 0; i < jw.Length(); i++)
                {
                    jw.Put(i, false);
                }
                mon.Checked = jw.GetBoolean(0);
                tue.Checked = jw.GetBoolean(1);
                wed.Checked = jw.GetBoolean(2);
                thu.Checked = jw.GetBoolean(3);
                fri.Checked = jw.GetBoolean(4);
                sat.Checked = jw.GetBoolean(5);
                sun.Checked = jw.GetBoolean(6);
                json.Put("week", jw);
                File.WriteAllText(Path.Combine(filePath, "data.json"), json.ToString());
                toasty("Reset data file...", ToastLength.Long);
            };

            setGoal.Click += (sender, e) =>
            {
                toasty("Test", ToastLength.Long);
            };


            dayCompleted.Click += (sender, e) =>
            {
                setDayCompleted();
            };
        }
    }
}

