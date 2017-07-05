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
    [Activity(Label = "勤怠システム 設定", Theme = "@android:style/Theme.NoTitleBar")]
    public class ConfigureActivity : Activity
    {
        private EditText _EditIPAdress;
        private EditText _EditFolderName;
        private EditText _EditUser;
        private EditText _EditPassword;
        private EditText _EditDomain;

        private Button _Save;
        private Button _ToMain;

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

            _EditDomain = FindViewById<EditText>(Resource.Id.editTextDomain);

            // ローカルストレージインスタンス作成
            DAO.LocalStorage LocalStorage = new DAO.LocalStorage();


            // コンフィグファイル読み込み
            Entity.Configure EntityConfig = new Entity.Configure();
            EntityConfig = LocalStorage.Read();

            _EditIPAdress.SetText(EntityConfig.UriAdress, BufferType.Normal);
            _EditFolderName.SetText(EntityConfig.FolderName, BufferType.Normal);
            _EditUser.SetText(EntityConfig.User, BufferType.Normal);
            _EditPassword.SetText(EntityConfig.Password, BufferType.Normal);
            _EditDomain.SetText(EntityConfig.Domain, BufferType.Normal);

            _Save.Click += delegate
            {
                //Save_onClick();

                EntityConfig.User = _EditUser.Text;
                EntityConfig.Password = _EditPassword.Text;
                EntityConfig.UriAdress = _EditIPAdress.Text;
                EntityConfig.FolderName = _EditFolderName.Text;
                EntityConfig.Domain = _EditDomain.Text;

                try
                {
                    LocalStorage.Write(EntityConfig);
                    Toast.MakeText(this, "設定を保存しました。", ToastLength.Short).Show();
                }catch(Exception e)
                {
                    Toast.MakeText(this, "エラーが発生しました。" + e , ToastLength.Short).Show();
                }
                

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
        }

    }
}