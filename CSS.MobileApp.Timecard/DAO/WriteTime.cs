using CSS.Library.Timecard.DAO;
using CSS.Library.Timecard.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSS.MobileApp.Timecard.DAO
{
    /// <summary>
    /// 時間書き込みのクラスです。
    /// </summary>
    class WriteTime
    {
        /// <summary>
        /// クラス変数群です。
        /// </summary>
        private DateTime _NowTime = DateTime.Now;
        private CsvToList _CsvList = new CsvToList();
        private SharedFile _SmbRecordFile;
        private StringBuilder _FileName = new StringBuilder();
        private List<CsvTimeRecord.Record> _Records;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="Id">社員No</param>
        public WriteTime(string Id)
        {
            // 初期設定
            Config.Properties props = new Config.Properties();
            props.User = "administrator";
            props.Password = "chubu#201";
            props.UriAdress = "192.168.250.200";
            props.FolderName = "/share/test/";

            // ファイル名設定
            _FileName.Length = 0;
            _FileName.Append(_NowTime.ToString("yyyyMM"));
            _FileName.Append("_");
            _FileName.Append(Id);
            _FileName.Append(".txt");

            // クラス変数へ設定
            _SmbRecordFile = new SharedFile(props);
            TextReader txtRead = _SmbRecordFile.SmbReader(_FileName.ToString());
            _Records = _CsvList.ReadCsv(txtRead);
            txtRead.Close();
            txtRead.Dispose();
        }

        /// <summary>
        /// 出勤時間書き込み
        /// </summary>
        public void setArrivalTimeStamp()
        {
            var RecordToday = _Records.FirstOrDefault(record => record.Date.ToString() == _NowTime.ToString("yyyy/MM/dd"));

            int index = _Records.IndexOf(RecordToday);

            RecordToday.ArrivalTime = _NowTime.ToString("HH:mm");

            _Records[index] = RecordToday;

            TextWriter txtWrite = _SmbRecordFile.SmbWriter(_FileName.ToString());

            _CsvList.WriteCsv(txtWrite, _Records);

            txtWrite.Close();
            txtWrite.Dispose();
        }


        /// <summary>
        /// 退勤時間書き込み
        /// </summary>
        public void setLeaveTimeStamp()
        {
            var RecordToday = _Records.FirstOrDefault(record => record.Date.ToString() == _NowTime.ToString("yyyy/MM/dd"));

            int index = _Records.IndexOf(RecordToday);

            RecordToday.LeaveTime = _NowTime.ToString("HH:mm");

            _Records[index] = RecordToday;

            TextWriter txtWrite = _SmbRecordFile.SmbWriter(_FileName.ToString());

            _CsvList.WriteCsv(txtWrite, _Records);
            txtWrite.Close();
            txtWrite.Dispose();
        }
    }
}