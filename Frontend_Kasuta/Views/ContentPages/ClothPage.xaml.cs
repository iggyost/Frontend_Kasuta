namespace Frontend_Kasuta.Views.ContentPages;

public partial class ClothPage : ContentPage
{
	public ClothPage()
	{
		InitializeComponent();
	}

    private async void buyButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PayPage());
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        clothNameLabel.Text = App.selectedCloth.Name;
        clothCostLabel.Text = App.selectedCloth.Cost.ToString() + "₽";
        clothGenderLabel.Text = App.selectedCloth.Gender.ToString();
        clothImage.Source = App.selectedCloth.CoverImage;
        clothMaterialLabel.Text = App.selectedCloth.Material;
        authorLabel.Text = App.selectedCloth.UserName;
        clothSizeLabel.Text = App.selectedCloth.SizeRu.ToString();
    }
}