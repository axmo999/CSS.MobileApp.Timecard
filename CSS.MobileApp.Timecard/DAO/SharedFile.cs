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
using SharpCifs.Smb;

namespace CSS.MobileApp.Timecard.DAO
{
    /// <summary>
    /// Samba接続用クラスです
    /// </summary>
    public class SharedFile
    {
        ///
        /// クラス変数群
        ///

        /// <summary>
        /// ローカルホストネームです
        /// </summary>
        private string _hostName = "xamarin.mobile";

        /// <summary>
        /// ローカルIPです
        /// </summary>
        private string _localAddress = "192.168.250.41";

        /// <summary>
        /// SMB接続用ドメイン名です
        /// </summary>
        private string _domain = "chubu-ishikai.local";

        /// <summary>
        /// SMB接続用ユーザー名です
        /// </summary>
        private string _user = "localadmin";

        /// <summary>
        /// SMB接続用パスワードです
        /// </summary>
        private string _password = "chubu#82OO";

        /// <summary>
        /// SMBサーバーのIPアドレスです（テスト用）
        /// </summary>
        private string _uriAdress = "192.168.250.200";

        /// <summary>
        /// SMBサーバー接続先フォルダ名です（テスト用）
        /// </summary>
        private string _folderName = "/share/test/";

        /// <summary>
        /// SMBサーバー接続先ファイル名です（テスト用）
        /// </summary>
        private string _fileName = "201706_0043108.txt";

        /// <summary>
        /// コンストラクタです
        /// </summary>
        public SharedFile()
        {
            // Samba接続プロパティ
            SharpCifs.Util.Sharpen.Properties properties = new SharpCifs.Util.Sharpen.Properties();

            properties.SetProperty("jcifs.smb.client.useExtendedSecurity", "true");
            properties.SetProperty("jcifs.smb.lmCompatibility", "3");
            properties.SetProperty("jcifs.netbios.cachePolicy", "180");
            // cache timeout: cache names
            properties.SetProperty("jcifs.netbios.hostname", _hostName);
            properties.SetProperty("jcifs.netbios.retryCount", "3");
            properties.SetProperty("jcifs.netbios.retryTimeout", "5000");
            // Name query timeout
            properties.SetProperty("jcifs.smb.client.responseTimeout", "10000");
            // increased for NAS where HDD is off!
            properties.SetProperty("jcifs.netbios.laddr", _localAddress);
            properties.SetProperty("jcifs.netbios.baddr", "255.255.255.255");
            properties.SetProperty("jcifs.resolveOrder", "LMHOSTS,BCAST,DNS");
            // ローカルIPセット
            properties.SetProperty("jcifs.smb.client.laddr", _localAddress);

            //You can store authentication information in SharpCifs.Std.
            SharpCifs.Config.SetProperty("jcifs.smb.client.username", _user);
            SharpCifs.Config.SetProperty("jcifs.smb.client.password", _password);

            SharpCifs.Config.SetProperties(properties);
        }

        /// <summary>
        /// SMBコネクション
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>SmbFile</returns>
        public SmbFile Connect(string fileName)
        {
            string uri = "smb://" + _uriAdress + "/" + _folderName + "/" + fileName;
            return new SmbFile(uri);
        }

    }
}