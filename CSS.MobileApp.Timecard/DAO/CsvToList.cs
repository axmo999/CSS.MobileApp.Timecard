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
using System.IO;
using SharpCifs.Smb;
using CsvHelper;

namespace CSS.MobileApp.Timecard.DAO
{
    public class CsvToList
    {
        ///
        /// クラス変数群
        ///

        /// <summary>
        /// ファイルエンコーディングです
        /// </summary>
        System.Text.Encoding _encoding = System.Text.Encoding.GetEncoding(932);

        /// <summary>
        /// コンストラクタです
        /// </summary>
        public CsvToList()
        {

        }

        /// <summary>
        /// SMB上のCSVファイルをList構造体に変換します
        /// </summary>
        /// <param name="smbFile">SMBファイル</param>
        /// <returns>List<Record></returns>
        public List<Entity.CsvTimeRecord.Record> ReadCsv(SmbFile smbFile)
        {
            // Recordリストを作成します
            List<Entity.CsvTimeRecord.Record> records = new List<Entity.CsvTimeRecord.Record>();

            // SMBファイルからストリーム作成、文字コード指定
            using (TextReader txtRead = new StreamReader(smbFile.GetInputStream(), _encoding))
            {
                // CSVとして読み込み開始
                var csvRead = new CsvReader(txtRead);

                // CSVファイル設定
                // ヘッダーなし
                csvRead.Configuration.HasHeaderRecord = false;
                // CsvMapper通りにマッピングする
                csvRead.Configuration.RegisterClassMap<Entity.CsvTimeRecord.CsvMapper>();
                // 文字コード設定
                csvRead.Configuration.Encoding = _encoding;

                // Recordリストに流し込み
                records = csvRead.GetRecords<Entity.CsvTimeRecord.Record>().ToList();

                // コネクション破棄
                smbFile.GetInputStream().Dispose();
                txtRead.Dispose();
                csvRead.Dispose();

                // List形式で返す
                return records;
            }
        }

        public bool WriteCsv(SmbFile smbFile, List<Entity.CsvTimeRecord.Record> records)
        {

            return true;
        }
    }
}