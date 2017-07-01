using CSS.Library.Timecard.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSS.MobileApp.Timecard.DAO
{
    /// <summary>
    /// Csvからユーザーリストへ変換します。
    /// </summary>
    public class CsvToUserList
    {
        /// <summary>
        /// クラス変数群です。
        /// </summary>
        private List<CSS.Library.Timecard.Entity.UserList.User> _Users = new List<CSS.Library.Timecard.Entity.UserList.User>();

        /// <summary>
        /// コンストラクタです。
        /// 環境変数を設定します。
        /// </summary>
        public CsvToUserList()
        {
            CSS.Library.Timecard.Entity.Config.Properties props = new CSS.Library.Timecard.Entity.Config.Properties();
            props.User = "administrator";
            props.Password = "chubu#201";
            props.UriAdress = "192.168.250.200";
            props.FolderName = "/share/test/";

            CsvToList csvList = new CsvToList();

            SharedFile smbUserList = new SharedFile(props);
            _Users = csvList.Users(smbUserList.SmbReader("UserList.txt"));
        }

        /// <summary>
        /// 名前リスト取得します。
        /// </summary>
        /// <returns>名前リスト</returns>
        public List<String> GetUserLists()
        {
            List<String> Users = new List<String>();
            foreach(CSS.Library.Timecard.Entity.UserList.User user in _Users)
            {
                Users.Add(user.Name);
            }

            return Users;
        }

        /// <summary>
        /// 名前より社員Noを取得します。
        /// </summary>
        /// <param name="Name">名前</param>
        /// <returns>社員No</returns>
        public string GetUserId(string Name)
        {
            CSS.Library.Timecard.Entity.UserList.User User = _Users.FirstOrDefault(x => x.Name == Name);
            return User.EmployeeId;
        }
    }
}