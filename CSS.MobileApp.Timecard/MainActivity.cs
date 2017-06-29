using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using CSS.Library.Timecard.DAO;
using CSS.Library.Timecard.Entity;
using System.Collections.Generic;
using System.IO;

namespace CSS.MobileApp.Timecard
{
    [Activity(Label = "勤怠システム", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        Button timeStamp;
        Spinner SpinnerUserLists;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            SpinnerUserLists = FindViewById<Spinner>(Resource.Id.spinnerUserLists);

            List<UserList.User> Users = new List<UserList.User>();

            Config.Properties props = new Config.Properties();
            props.User = "administrator";
            props.Password = "chubu#201";
            props.UriAdress = "192.168.250.200";
            props.FolderName = "/share/test/";

            CsvToList csvList = new CsvToList();

            SharedFile smbUserList = new SharedFile(props);
            Users = csvList.Users(smbUserList.SmbReader("UserList.txt"));
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Users);

            SpinnerUserLists.Adapter = adapter;

            timeStamp = FindViewById<Button>(Resource.Id.buttonStamp);

            timeStamp.Click += Stamp;

        }

        private void Stamp(object sender, EventArgs e)
        {
            DateTime nowDate = DateTime.Now;


        }
    }
}

