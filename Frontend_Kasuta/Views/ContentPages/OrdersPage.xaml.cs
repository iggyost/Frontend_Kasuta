using Frontend_Kasuta.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_Kasuta.Views.ContentPages;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
	}
	public async Task GetUserOrders()
	{
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{App.conString}ordersview/get/{App.enteredUser.UserId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OrdersView[]>(content).ToList();
            ordersCollectionView.ItemsSource = data;
        }
        else
        {

        }
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetUserOrders();
        while (true)
        {
            if (App.isOrdersUpdated == true)
            {
                await GetUserOrders();
            }
            await Task.Delay(2000);
        }
    }
}