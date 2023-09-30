using gwizd.Classes;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace gwizd
{
    public partial class MainPage : ContentPage
    {
        UserLocation locationService;
        public MainPage()
        {
            InitializeComponent();
            locationService = new UserLocation();


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

    }
}