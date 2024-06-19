using Frontend_Kasuta.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Kasuta.Views.ContentPages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void seasonsCv_Loaded(object sender, EventArgs e)
    {
    }
    public async void LoadSeasons()
    {
        try
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}season/view");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                App.seasonsList = JsonConvert.DeserializeObject<Season[]>(content).ToList();
                seasonsCv.ItemsSource = App.seasonsList;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Ошибка!", "Ошибка при загрузке сезонов!", "Закрыть");
        }
    }
    private void categoriesCv_Loaded(object sender, EventArgs e)
    {

    }
    public async void LoadCategories()
    {
        try
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{App.conString}category/view");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                App.categoriesList = JsonConvert.DeserializeObject<Category[]>(content).ToList();
                categoriesCv.ItemsSource = App.categoriesList;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Ошибка!", "Ошибка при загрузке категорий!", "Закрыть");
        }
    }
    private void Border_Loaded(object sender, EventArgs e)
    {
        Border border = (Border)sender;
        var borderId = int.Parse(border.AutomationId.ToString());
        if (borderId != 0)
        {
            border.BackgroundColor = Color.FromRgba($"{App.seasonsList.Where(x => x.SeasonId == borderId).Select(x => x.HexColor).FirstOrDefault()}");
        }
    }

    private void borderCategory_Loaded(object sender, EventArgs e)
    {
        Border Border = (Border)sender;
        var BorderId = int.Parse(Border.AutomationId.ToString());
        if (BorderId != 0)
        {
            Border.BackgroundColor = Color.FromRgba($"{App.categoriesList.Where(x => x.CategoryId == BorderId).Select(x => x.HexColor).FirstOrDefault()}");
        }
    }
    public async Task GetClothes()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}clothesview/get");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            App.clothesView = JsonConvert.DeserializeObject<ClothesView[]>(content).ToList();
        }
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        LoadSeasons();
        LoadCategories();
        await GetClothes();
    }

    private async void seasonGesture_Tapped(object sender, TappedEventArgs e)
    {
        Border border = sender as Border;
        var seasonId = int.Parse(border.AutomationId.ToString());
        App.selectedSeason = App.seasonsList.Where(x => x.SeasonId ==  seasonId).FirstOrDefault();
        await Navigation.PushModalAsync(new CatalogPage(false,seasonId));
    }

    private async void categoryGesture_Tapped(object sender, TappedEventArgs e)
    {
        Border border = sender as Border;
        var categoryId = int.Parse(border.AutomationId.ToString());
        App.selectedCategory = App.categoriesList.Where(x => x.CategoryId == categoryId).FirstOrDefault();
        await Navigation.PushModalAsync(new CatalogPage(true, categoryId));
    }

    private async void ordersButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new OrdersPage());
    }
}