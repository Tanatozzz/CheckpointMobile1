using CheckpointMobile1.API;
using CheckpointMobile1.Singletons;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace CheckpointMobile1;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        Employee employee = EmployeeSingleton.Instance.Employee;
        FIOLabel.Text = "ФИО: " + employee.FirstName + " " + employee.LastName + " " + employee.Patronomyc;
        switch (employee.IDRole)
        {
            case 1:
                RoleLabel.Text = "Должность: Администратор";
                break;
            case 2:
                RoleLabel.Text = "Должность: Продавец";
                break;
            case 3:
                RoleLabel.Text = "Должность: Консультант";
                break;
            default:
                break;
        }
        PassportLabel.Text = "Паспортные данные: " + employee.PassportSeries + employee.PassportNumber;
        INNLabel.Text = "ИНН: " + employee.INN;
        switch (employee.isInside)
        {
            case true:
                isInsideImage.Source = "Resources/Images/online.png";
                isInsideLabel.Text = "На работе";
                isInsideLabel.TextColor = Colors.GreenYellow; // Зеленый цвет текста
                break;
            case false:
                isInsideImage.Source = "Resources/Images/offline.png";
                isInsideLabel.Text = "Не на работе";
                isInsideLabel.TextColor = Colors.Red; // Красный цвет текста
                break;
        }
    }

    private async void ScannerPageBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ScannerPage");
    }

    private async void SettingsPageBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SettingsPage");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Employee employee = EmployeeSingleton.Instance.Employee;
        HttpQuery loginManager = new HttpQuery();

        IEnumerable<CheckpointAccessedList> checkpoints = await loginManager.GetEmployeeAccessibleCheckpoints(employee.ID);

        if (checkpoints != null)
        {
            lvAccessedCheckpoints.ItemsSource = checkpoints;
        }
        else
        {
            // Ошибка аутентификации
            await DisplayAlert("Ошибка", "Ошибка получения списка доступных дверей!", "Ок");
        }
    }
}