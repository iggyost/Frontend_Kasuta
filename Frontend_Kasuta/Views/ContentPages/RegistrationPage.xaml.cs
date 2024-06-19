using Frontend_Kasuta.ApplicationData;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Frontend_Kasuta.Views.ContentPages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    Regex phoneRegex = new Regex("^((\\+7|7|8)+([0-9]){10})$");

    private void backBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AuthorizationPage();
    }

    private async void regBtn_Clicked(object sender, EventArgs e)
    {
        regBtn.IsEnabled = false;
        if (nameEntry.Text != null)
        {
            if (emailEntry.Text != null)
            {
                if (passwordEntry.Text != null)
                {
                    if (phoneEntry.Text != null)
                    {


                        if (emailRegex.IsMatch(emailEntry.Text))
                        {
                            if (passwordEntry.Text.Length > 3)
                            {
                                if (phoneRegex.IsMatch(phoneEntry.Text))
                                {
                                    HttpClient client = new HttpClient();
                                    var response = await client.GetAsync($"{App.conString}user/reg/{nameEntry.Text}/{emailEntry.Text}/{passwordEntry.Text}/{phoneEntry.Text}");
                                    if (response.IsSuccessStatusCode)
                                    {
                                        string content = await response.Content.ReadAsStringAsync();
                                        App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                                        regBtn.IsEnabled = true;
                                        Application.Current.MainPage = new NavigationPage(new MainPage());
                                    }
                                    else
                                    {
                                        await DisplayAlert("Ошибка!", "Неправильные данные пользователя!", "Закрыть");
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Ошибка!", "Номер телефона не соответствует формату!", "Закрыть");
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
                        await DisplayAlert("Ошибка!", "Поле для номера телефона не может быть пустым", "Закрыть");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка!", "Поле для пароля не может быть пустым", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Поле для E-mail не может быть пустым", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Поле для имени не может быть пустым", "Закрыть");
        }
        regBtn.IsEnabled = true;
    }
}