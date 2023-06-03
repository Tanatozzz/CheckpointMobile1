using CheckpointMobile1.API;
using CheckpointMobile1.Classes;
using CheckpointMobile1.Singletons;

namespace CheckpointMobile1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string username = txtLogin.Text; // Получение текста из поля ввода для логина
        string password = txtPassword.Text; // Получение текста из поля ввода для пароля

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Ошибка", "Введите данные!", "Ок");
            return;
        }

        // Проверка длины пароля и логина
        if (username.Length > 16 || password.Length > 16)
        {
            await DisplayAlert("Ошибка", "Поля логина и пароля должны содержать не более 16 символов!", "Ок");
            return;
        }

        try
        {
            HttpQuery loginManager = new HttpQuery();
            Employee loggedInEmployee = await loginManager.Login(username, password);
            if (loggedInEmployee != null)
            {
                await DisplayAlert("Успешно", "Доступ разрешен!", "Ок");
                EmployeeSingleton.Instance.Employee = loggedInEmployee;
                await Shell.Current.GoToAsync("ScannerPage");
            }
            else
            {
                // Ошибка аутентификации
                await DisplayAlert("Ошибка", "Неверные учетные данные!", "Ок");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.ToString(), "Ок");
            throw;
        }
    }
}

