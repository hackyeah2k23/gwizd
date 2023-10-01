using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Newtonsoft.Json;

namespace gwizd.Classes
{
    public class DataController
    {
        public async Task<List<AnimalData>> GetAnimals()
        {
            List<AnimalData> pins = new List<AnimalData>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://local.tomekb530.me:9091/api/animals");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<AnimalData>>(content);
                pins = animals;
                return pins;
            }
            return pins;
        }
    }
}
