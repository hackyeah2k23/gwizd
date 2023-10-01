namespace gwizd;

using CommunityToolkit.Maui.Views;

public partial class PetPopup : Popup
{
    public PetPopup(AnimalData animalData)
    {
        InitializeComponent();
        //image from base64
        byte[] image = Convert.FromBase64String(animalData.ImageData);
        animalPhoto.Source = ImageSource.FromStream(() => new MemoryStream(image));

        animalName.Text = animalData.TypeOfAnimal;
        animalBreed.Text = animalData.Breed;
    }
}