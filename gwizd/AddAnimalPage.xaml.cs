using Newtonsoft.Json;
using System.Text;

namespace gwizd;

public class AnimalData
{
    public string TypeOfAnimal { get; set; }
    public string Location { get; set; }
    public string ImageData { get; set; }
    public bool IsWild { get; set; }
    public bool Found { get; set; }
    public string Color { get; set; }
    public string Details { get; set; }
    public DateTime Date { get; set; }
    public string Breed { get; set; }
}

public partial class AddAnimalPage : ContentPage
{
    private readonly FileResult _photo;
    private readonly Location _loc;

    public AddAnimalPage(FileResult photo, Location loc)
    {
        InitializeComponent();
        _photo = photo;
        _loc = loc;

        if (_photo != null)
        {
            animalPhoto.Source = ImageSource.FromStream(() => _photo.OpenReadAsync().Result);
        }

        if (_loc != null)
        {
            location.Text = $"{_loc.Latitude}:{_loc.Longitude}";
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //check data and make post request
        if (string.IsNullOrEmpty(animal.Text))
        {
            DisplayAlert("Error", "Animal name is required", "OK");
            return;
        }
        
        if (string.IsNullOrEmpty(location.Text))
        {
            DisplayAlert("Error", "Location is required", "OK");
            return;
        }

        if (_photo == null)
        {
            DisplayAlert("Error", "Photo is required", "OK");
            return;
        }

        if (foundOrLost.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Found or lost is required", "OK");
            return;
        }

        if (string.IsNullOrEmpty(color.Text))
        {
            DisplayAlert("Error", "Color is required", "OK");
            return;
        }

        if (string.IsNullOrEmpty(details.Text))
        {
            DisplayAlert("Error", "Details are required", "OK");
            return;
        }

        var animalData = new AnimalData
        {
            TypeOfAnimal = animal.Text,
            Location = location.Text,
            ImageData = Convert.ToBase64String(File.ReadAllBytes(_photo.FullPath)),
            IsWild = isItWild.IsToggled,
            Found = foundOrLost.SelectedIndex == 0,
            Color = color.Text,
            Details = details.Text,
            Date = DateTime.Now,
            Breed = "xD"
        };

        // Serialize the data to JSON
        var jsonData = JsonConvert.SerializeObject(animalData);

        var client = new HttpClient();

        // Set the content type to application/json
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        // Create a StringContent with the JSON data
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = client.PostAsync("http://local.tomekb530.me:9091/api/animals", content).Result;

        if (response.IsSuccessStatusCode)
        {
            DisplayAlert("Success", "Animal added", "OK");
            Navigation.PopAsync();
        }
        else
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseString);
            DisplayAlert("Error", "Something went wrong", "OK");
        }
    }
}
