using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections;
using Org.Json;
using System.Resources;
using Android.Content.Res;
using System.IO;
using System.Collections.Generic;

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            assets = this.Assets;
            string raw;

            using (StreamReader sreader = new StreamReader(assets.Open("data.json")))
            {
                raw = sreader.ReadToEnd();
            }
            JSONObject json = new JSONObject(raw);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //week = json.Get("week");

            initUI(raw);
             


        }

        protected void initUI(string raw)
        {
            JSONObject json = new JSONObject(raw);

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



            goal.Text = "$" + json.GetString("totalGoal");
            current.Text = "$" + json.GetString("total");
            weekGoal.Text = json.GetString("weekGoal") + " Days";
            weekCurrent.Text = "$" + json.GetString("totalGoal") + " Days";

        }
    }
}

