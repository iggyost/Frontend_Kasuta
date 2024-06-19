using Frontend_Kasuta.ApplicationData;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Frontend_Kasuta.Views.ContentPages;

public partial class CatalogPage : ContentPage
{
	public CatalogPage()
	{
		InitializeComponent();
	}
    public CatalogPage(bool isCategory, int id)
    {
        InitializeComponent();

		if (isCategory == true)
        {
            headerBorder.BackgroundColor = Color.FromRgba("#CD005B");
            headerImage.Source = "circle_red.png";
            headerImage.WidthRequest = 224;
            headerImage.HeightRequest = 224;
            headerCategoryImage.Source = App.selectedCategory.CoverImage;
            headerLabel.Text = App.selectedCategory.Name;
            headerLabel.FontSize = 39;

            clothesCollectionView.ItemsSource = App.clothesView.Where(x => x.CategoryId == id).ToList();
        }
        else
		{
            headerBorder.BackgroundColor = Color.FromRgba($"{App.seasonsList.Where(x => x.SeasonId == id).Select(x => x.HexColor).FirstOrDefault()}");
            headerImage.WidthRequest = 86;
            headerImage.HeightRequest = 86;
            headerImage.Source = App.selectedSeason.CoverImage;
            headerLabel.Text = App.selectedSeason.Name;
            headerLabel.FontSize = 32;

            clothesCollectionView.ItemsSource = App.clothesView.Where(x => x.SeasonId == id).ToList();
        }
    }
    
    private async void clothGesture_Tapped(object sender, TappedEventArgs e)
    {
        Border border = sender as Border;
        var clothId = int.Parse(border.AutomationId.ToString());
        App.selectedCloth = App.clothesView.Where(x => x.ClothId == clothId).FirstOrDefault();
        await Navigation.PushModalAsync(new ClothPage());
    }
}