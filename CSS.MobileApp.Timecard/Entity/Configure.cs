namespace CSS.MobileApp.Timecard.Entity
{
    /// <summary>
    /// SMB設定用クラス
    /// </summary>
    public class Configure
    {
        /// <summary>
        /// SMBアクセスユーザー名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// SMBアクセスパスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SMBネットワークIPアドレス
        /// </summary>
        public string UriAdress { get; set; }

        /// <summary>
        /// SMB共有フォルダ名
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// SMBドメイン名
        /// </summary>
        public string Domain { get; set; }
    }
}