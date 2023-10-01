using CommunityToolkit.Maui.Views;
using gwizd.Classes;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace gwizd
{
    public partial class MainPage : ContentPage
    {
        UserLocation locationService;
        UserPhotos photos;
        DataController dataController;
        public MainPage()
        {
            InitializeComponent();
            locationService = new UserLocation();
            photos = new UserPhotos();
            dataController = new DataController();

            LoadLocation();
            
        }
        
        public void DisplayPopup(AnimalData data)
        {
            var popup = new PetPopup(data);

            this.ShowPopup(popup);
        }

        public async void LoadLocation()
        {
            Location loc = await locationService.GetCurrentLocation();
            MapSpan mapSpan = new MapSpan(loc, 0.01, 0.01);
            mainMap.MoveToRegion(mapSpan);
            Pin pin = new Pin()
            {
                Label = "You are here",
                Location = loc
            };
            mainMap.Pins.Add(pin);

            pin.InfoWindowClicked += (s, args) =>
            {
                DisplayAlert("test", "test", "tuguzikhir");
            };
            
            List<AnimalData> pins = await dataController.GetAnimals();

            pins.ForEach(animal =>
            {
                string Location = animal.Location;
                string[] LocationSplit = Location.Split(':');
                double Latitude = Double.Parse(LocationSplit[0]);
                double Longitude = Double.Parse(LocationSplit[1]);
                Pin pin = new Pin()
                {
                    Label = animal.TypeOfAnimal,
                    Location = new Location(Latitude, Longitude)

                };
                pin.InfoWindowClicked += (s, args) =>
                {
                    DisplayPopup(animal);
                };
                mainMap.Pins.Add(pin);
            });


        }

        private async void ReportButton_Clicked(object sender, EventArgs e)
        {
            FileResult photo = await photos.TakePhoto();
            Location loc = await locationService.GetCachedLocation();
            await Navigation.PushAsync(new AddAnimalPage(photo, loc));
        }
    }
}