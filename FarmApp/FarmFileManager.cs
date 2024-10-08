﻿using System.Text.Json;

namespace FarmApp
{
    public class FarmFileManager
    {
        private const string _filePath = "farmStorage.json";

        public void SaveToFile(Farm farm)
        {
            var farmData = new FarmData(farm.GetBoxes().ToList()) ;

            using var writer = new StreamWriter(_filePath);
            var json = JsonSerializer.Serialize(farmData);
            writer.Write(json);
        }

        public FarmData LoadFromFile()
        {
            if(File.Exists(_filePath))
            {
                using var reader = new StreamReader(_filePath);
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<FarmData>(json)!;
            }
            else
            {
                var farmData = new FarmData(new List<Box>());
                SaveToFile(new Farm(farmData.Boxes));
                return farmData;
            }
        }
    }
}
