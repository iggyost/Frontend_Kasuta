namespace Frontend_Kasuta.Views.ContentPages;

public partial class PayPage : ContentPage
{
	public PayPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		clothImage.Source = App.selectedCloth.CoverImage;
		clothNameLabel.Text = App.selectedCloth.Name;
    }

    private async void payBtn_Clicked(object sender, EventArgs e)
    {
        if (cvvCodeEntry.Text == null && cardNumEntry.Text == null)
        {
            await DisplayAlert("Ошибка!", "Недопустимы пустые поля для ввода!", "Закрыть");
        }
        else
        {
            if (cardNumEntry.Text.Length == 16)
            {
                if (cvvCodeEntry.Text.Length == 3)
                {

                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync($"{App.conString}order/create/{App.selectedCloth.ClothId}/{App.enteredUser.UserId}");
                    if (response.IsSuccessStatusCode)
                    {
                        App.isOrdersUpdated = true;
                        await successImage.FadeTo(1, 250);
                        await Task.Delay(5000);
                        await successImage.FadeTo(0, 250);
                        await Navigation.PopModalAsync();
                    }

                }
                else
                {
                    await DisplayAlert("Ошибка", "CVV-код должен состоять из 3 цифр", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Номер карты должен состоять из 16 цифры", "Закрыть");
            }
        }
    }
}