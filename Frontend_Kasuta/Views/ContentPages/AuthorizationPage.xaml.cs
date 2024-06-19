using Frontend_Kasuta.ApplicationData;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Frontend_Kasuta.Views.ContentPages;

public partial class AuthorizationPage : ContentPage
{
    public AuthorizationPage()
    {
        InitializeComponent();
    }

    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    private void regBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegistrationPage();
    }

    private async void enterBtn_Clicked(object sender, EventArgs e)
    {
        enterBtn.IsEnabled = false;
        if (emailEntry.Text != null)
        {
            if (passwordEntry.Text != null)
            {
                if (emailRegex.IsMatch(emailEntry.Text))
                {
                    if (passwordEntry.Text.Length > 3)
                    {
                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync($"{App.conString}user/enter/{emailEntry.Text}/{passwordEntry.Text}");
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                            enterBtn.IsEnabled = true;
                            Application.Current.MainPage = new NavigationPage(new MainPage());
                        }
                        else
                        {
                            await DisplayAlert("Ошибка!", "Неправильные данные пользователя!", "Закрыть");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Ошибка!", "Минимальная длина пароля - 4 символа!", "Закрыть");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка!", "E-mail не соответствует формату!", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Введите пароль!", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Введите E-mail!", "Закрыть");
        }
        enterBtn.IsEnabled = true;
    }
}