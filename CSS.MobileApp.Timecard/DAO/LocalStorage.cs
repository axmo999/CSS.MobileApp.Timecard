using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace CSS.MobileApp.Timecard.DAO
{
    public class LocalStorage
    {
		private string _ConfigFileName = "config.json";

        public LocalStorage()
        {
        }

        public void Read()
        {
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
