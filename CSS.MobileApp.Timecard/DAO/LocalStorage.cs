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

        public Entity.Configure Read()
        {
            Entity.Configure EntityConfig = new Entity.Configure();

			var ConfigFile = IsolatedStorageFile.GetUserStoreForApplication();

			if (!IsolatedStorageFileExists(_ConfigFileName))
			{
				CreateConfigFIle();
			}

			using (IsolatedStorageFileStream StrageFileStream = ConfigFile.OpenFile(_ConfigFileName, FileMode.Open))
			using (StreamReader reader = new StreamReader(StrageFileStream))
			{
				var JsonConfig = JsonConvert.DeserializeObject<Entity.Configure>(reader.ReadToEnd());
                EntityConfig.UriAdress = JsonConfig.UriAdress.ToString();
                EntityConfig.FolderName = JsonConfig.FolderName.ToString();
                EntityConfig.User = JsonConfig.User.ToString();
                EntityConfig.Password = JsonConfig.Password.ToString();
			}

            return EntityConfig;
        }

        public void Write(Entity.Configure EntityConfig)
        {
            var Config = JsonConvert.SerializeObject(EntityConfig);

            var ConfigFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream StrageFileStream = ConfigFile.CreateFile("config.json"))
            using (StreamWriter writer = new StreamWriter(StrageFileStream))
            {
                writer.Write(Config);
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
