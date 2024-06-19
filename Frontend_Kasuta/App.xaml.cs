using Frontend_Kasuta.ApplicationData;
using Frontend_Kasuta.Views.ContentPages;

namespace Frontend_Kasuta;

public partial class App : Application
{
    public static string conString = "http://192.168.0.10:45455/api/";
    public static User enteredUser;
    public static List<Season> seasonsList = new List<Season>();
    public static List<Category> categoriesList = new List<Category>();
    public static Season selectedSeason;
    public static Category selectedCategory;
    public static List<ClothesView> clothesView = new List<ClothesView>();
    public static ClothesView selectedCloth;
    public static bool isOrdersUpdated = false;
    public App()
	{
		InitializeComponent();

		MainPage = new WelcomePage();
    }
}
