using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FarmApp
{
    public class FarmFileManager
    {
        private const string _filePath = "farmStorage.json";

        public void SaveToFile(Farm farm)
        {
            var farmData = new { farm.FruitBoxes, farm.VeggieBoxes };

            using (var writer = new StreamWriter(_filePath))
            {
                var json = JsonSerializer.Serialize(farmData);
                writer.Write(json);
            }
        }

        public void LoadFromFile(Farm farm)
        {
            if(File.Exists(_filePath))
            {
                using(var reader = new StreamReader(_filePath))
                {
                    var json = reader.ReadToEnd();
                    var farmData = JsonSerializer.Deserialize<Farm>(json);
                    farm.FruitBoxes = farmData.FruitBoxes;
                    farm.VeggieBoxes = farmData.VeggieBoxes;
                }
            } else
                SaveToFile(farm);

        }
    }
}
