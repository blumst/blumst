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
            var farmData = new FarmData(farm.FruitBoxes, farm.VeggieBoxes);

            using (var writer = new StreamWriter(_filePath))
            {
                var json = JsonSerializer.Serialize(farmData);
                writer.Write(json);
            }
        }

        public FarmData LoadFromFile()
        {
            if(File.Exists(_filePath))
            {
                using(var reader = new StreamReader(_filePath))
                {
                    var json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<FarmData>(json);
                }
            }
            else
            {
                var farmData = new FarmData(0, 0);
                SaveToFile(new Farm(farmData.FruitBoxes, farmData.VeggieBoxes));
                return farmData;
            }
        }
    }
}
