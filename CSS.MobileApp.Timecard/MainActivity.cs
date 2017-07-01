using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;

namespace CSS.MobileApp.Timecard
{
    [Activity(Label = "勤怠システム", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        /// <summary>
        /// クラス変数群です。
        /// </summary>

        /// <summary>
        /// 打刻ボタンです。
        /// </summary>
        private Button _TimeStamp;

        /// <summary>
        /// ユーザーセレクトボックスです。
        /// </summary>
        private Spinner _SpinnerUserLists;

        /// <summary>
        /// 出退勤切り替えボタンです。
        /// </summary>
        private ToggleButton _ToggleAttendance;

        private Button _ToConfig;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // メイン画面からそれぞれのボタンを取得します。
            _SpinnerUserLists = FindViewById<Spinner>(Resource.Id.spinnerUserLists);
            _TimeStamp = FindViewById<Button>(Resource.Id.buttonStamp);
            _ToggleAttendance = FindViewById<ToggleButton>(Resource.Id.toggleAttendance);
            _ToConfig = FindViewById<Button>(Resource.Id.buttonToConfig);

            // ユーザーリスト取得インスタンスです。
            DAO.CsvToUserList UserList = new DAO.CsvToUserList();

            // 取得したユーザーリストをユーザーセレクトボックスに設定します。
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, UserList.GetUserLists());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _SpinnerUserLists.Adapter = adapter;
            
            // 打刻ボタンクリック時の挙動です。
            _TimeStamp.Click += delegate
            {
                TimeStamp_onClick();
            };

            _ToConfig.Click += delegate
            {
                ToConfig_onClick();
            };


        }

        private void ToConfig_onClick()
        {
            var intent = new Intent(this, typeof(ConfigureActivity));
            StartActivity(intent);

            //throw new NotImplementedException();
        }

        /// <summary>
        /// 打刻ボタンクリック
        /// </summary>
        private void TimeStamp_onClick()
        {
            // ユーザーリスト取得インスタンスです。
            DAO.CsvToUserList UserList = new DAO.CsvToUserList();

            // 名前をユーザーセレクトボックスから取得します。
            string Name = _SpinnerUserLists.SelectedItem.ToString();

            // 名前より社員Noを取得します。
            string Id = UserList.GetUserId(Name);

            // 出勤CSV書き込みインスタンスです。IDを設定します。
            DAO.WriteTime WriteTime = new DAO.WriteTime(Id);

            // 出勤対ボタンの状態を取得します。
            string State = string.Empty;
            bool Attendance = _ToggleAttendance.Checked;

            try
            {
                if (Attendance)
                {
                    // 出勤時間を書き込みます。
                    State = "出勤";
                    WriteTime.setArrivalTimeStamp();
                }
                else
                {
                    // 退勤時間を書き込みます。
                    State = "退勤";
                    WriteTime.setLeaveTimeStamp();
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(this, Name + "の" + State + "でエラーが発生しました。\n エラー内容：" + e , ToastLength.Short).Show();
            }
            finally
            {
                Toast.MakeText(this, Name + "の" + State + "を打刻しました。", ToastLength.Short).Show();
                WriteTime = null;
            }
        }
    }
}

