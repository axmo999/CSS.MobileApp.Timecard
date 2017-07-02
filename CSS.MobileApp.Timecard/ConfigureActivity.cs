using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using static Android.Widget.TextView;

namespace CSS.MobileApp.Timecard
{
    [Activity(Label = "勤怠システム 設定", ScreenOrientation = ScreenOrientation.Landscape)]
    public class ConfigureActivity : Activity
    {
        private EditText _EditIPAdress;
        private EditText _EditFolderName;
        private EditText _EditUser;
        private EditText _EditPassword;

        private Button _Save;
        private Button _ToMain;

        private string _ConfigFileName = "config.json";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.Configure);

            // ボタン取得
            _Save = FindViewById<Button>(Resource.Id.buttonSave);

            _ToMain = FindViewById<Button>(Resource.Id.buttonMain);

            // テキストエディット取得
            _EditIPAdress = FindViewById<EditText>(Resource.Id.editTextIPAdress);

            _EditFolderName = FindViewById<EditText>(Resource.Id.editTextFolderName);

            _EditUser = FindViewById<EditText>(Resource.Id.editTextUserName);

            _EditPassword = FindViewById<EditText>(Resource.Id.editTextPassword);


            var ConfigFile = IsolatedStorageFile.GetUserStoreForApplication();

            if (!IsolatedStorageFileExists(_ConfigFileName))
            {
                CreateConfigFIle();
            }

            using (IsolatedStorageFileStream StrageFileStream = ConfigFile.OpenFile(_ConfigFileName, FileMode.Open))
            using (StreamReader reader = new StreamReader(StrageFileStream))
            {
                var Config = JsonConvert.DeserializeObject<Entity.Configure>(reader.ReadToEnd());
                _EditIPAdress.SetText(Config.UriAdress.ToString(), BufferType.Normal);
                _EditFolderName.SetText(Config.FolderName.ToString(), BufferType.Normal);
                _EditUser.SetText(Config.User.ToString(), BufferType.Normal);
                _EditPassword.SetText(Config.Password.ToString(), BufferType.Normal);
            }

            _Save.Click += delegate
            {
                Save_onClick();
            };

            _ToMain.Click += delegate
            {
                ToMain_onClick();
            };
        }

        private void ToMain_onClick()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);

            //throw new NotImplementedException();
        }

        private void Save_onClick()
        {
            var Config = JsonConvert.SerializeObject(new Entity.Configure()
            {
                User = _EditUser.Text.ToString(),
                Password = _EditPassword.Text.ToString(),
                UriAdress = _EditIPAdress.Text.ToString(),
                FolderName = _EditFolderName.Text.ToString()
                
            });

            var ConfigFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream StrageFileStream = ConfigFile.CreateFile("config.json"))
            using (StreamWriter writer = new StreamWriter(StrageFileStream))
            {
                writer.Write(Config);
                Toast.MakeText(this, "設定を保存しました。", ToastLength.Short).Show();
            }

            //throw new NotImplementedException();
        }

        private bool IsolatedStorageFileExists(string Name)
        {
            using (var folder = IsolatedStorageFile.GetUserStoreForDomain())
            {
                return folder.FileExists(Name);
            }
        }

        private void CreateConfigFIle()
        {
            var Config = JsonConvert.SerializeObject(new Entity.Configure()
            {
                User = "",
                Password = "",
                UriAdress = "",
                FolderName = ""

            });

            var ConfigFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream StrageFileStream = ConfigFile.CreateFile("config.json"))
            using (StreamWriter writer = new StreamWriter(StrageFileStream))
            {
                writer.Write(Config);
            }
        }
    }
}