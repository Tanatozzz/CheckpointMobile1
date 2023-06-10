using CheckpointMobile1.API;
using CheckpointMobile1.Classes;
using CheckpointMobile1.Singletons;
using System.Globalization;

namespace CheckpointMobile1;

public partial class ScannerPage : ContentPage
{

    public class Checkpoint
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string IDOffice { get; set; }
        public bool IsActive { get; set; }
    }
    public ScannerPage()
	{
		InitializeComponent();
        //cameraView.BarCodeOptions = new()
        //{
        //    PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
        //};
        HttpEmployee fff = new HttpEmployee();
    }

    private async void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(500);
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }
    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            string barcodeText = args.Result[0].Text;

            Checkpoint currentCheckpoint = new Checkpoint();

            // Разбор полученных данных из QR-кода
            string[] data = barcodeText.Replace("QR_CODE:", "").Split('\n');
            foreach (string line in data)
            {
                string[] keyValue = line.Split(':');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    // Сохранение данных в экземпляр класса Checkpoint
                    switch (key)
                    {
                        case "ID":
                            int id;
                            if (int.TryParse(value, out id))
                                currentCheckpoint.ID = id;
                            break;
                        case "Title":
                            currentCheckpoint.Title = value;
                            break;
                        case "IDOffice":
                            currentCheckpoint.IDOffice = value;
                            break;
                        case "IsActive":
                            currentCheckpoint.IsActive = Convert.ToBoolean(value);
                            break;
                    }
                }
            }

            string checkpointData = $"ID: {currentCheckpoint.ID}\n" +
                                    $"Title: {currentCheckpoint.Title}\n" +
                                    $"IDOffice: {currentCheckpoint.IDOffice}\n" +
                                    $"IsActive: {currentCheckpoint.IsActive}\n";

            Employee employee = EmployeeSingleton.Instance.Employee;
            HttpQuery httpmanager = new HttpQuery();
            try
            {
                bool a = await httpmanager.CheckAccess(currentCheckpoint.ID, employee.ID);
                if (a)
                {
                    barcodeResult.Text = "Доступ разрешен";

                }
                else if (a == false)
                {
                    barcodeResult.Text = "Доступ запрещен"; 
                }
                else
                {
                    barcodeResult.Text = "Проход не найден";
                }

            }
            catch (Exception)
            {
                throw;
            }
        });


    }

    private async void ProfilePageBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ProfilePage");
    }

    private async void SettingsPageBtn_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("2", "2", "Ок");
    }
}