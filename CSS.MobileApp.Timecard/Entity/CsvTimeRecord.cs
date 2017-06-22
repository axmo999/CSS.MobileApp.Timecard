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

namespace CSS.MobileApp.Timecard.Entity
{
    /// <summary>
    /// CSVの行とリンクする構造体クラスです
    /// </summary>
    public class CsvTimeRecord
    {
        /// <summary>
        /// コンストラクタです
        /// </summary>
        public CsvTimeRecord()
        {

        }

        /// <summary>
        /// CSVのデータを格納するクラス
        /// </summary>
        public class Record
        {
            public string Date { get; set; }
            public string ArrivalTime { get; set; }
            public string LeaveTime { get; set; }
            public string Note { get; set; }
        }

        /// <summary>
        /// 格納するルール
        /// </summary>
        public class CsvMapper : CsvHelper.Configuration.CsvClassMap<Record>
        {
            public CsvMapper()
            {
                Map(x => x.Date).Index(0);
                Map(x => x.ArrivalTime).Index(1);
                Map(x => x.LeaveTime).Index(2);
                Map(x => x.Note).Index(3);
            }
        }
    }
}