namespace CheckpointMobile1;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void ProfilePageBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ProfilePage");
    }

    private async void ScannerPageBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ScannerPage");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MainPage");
    }
}