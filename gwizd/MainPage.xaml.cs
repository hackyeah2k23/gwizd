using gwizd.Classes;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace gwizd
{
    public partial class MainPage : ContentPage
    {
        UserLocation locationService;
        UserPhotos photos;
        public MainPage()
        {
            InitializeComponent();
            locationService = new UserLocation();
            photos = new UserPhotos();

            LoadLocation();
        }

        public async void LoadLocation()
        {
            Location loc = await locationService.GetCurrentLocation();
            MapSpan mapSpan = new MapSpan(loc, 0.01, 0.01);
            mainMap.MoveToRegion(mapSpan);
            Pin pin = new Pin()
            {
                Label = "Jesteś tutaj",
                Location = loc
            };
            mainMap.Pins.Add(pin);

            pin.InfoWindowClicked += (s, args) =>
            {
                DisplayAlert("test", "test", "tuguzikhir");
            };
        }

        private void ReportButton_Clicked(object sender, EventArgs e)
        {
            photos.TakePhoto();
        }
    }
}