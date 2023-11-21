using CommunityToolkit.Maui.Views;

namespace Tarea2._4
{
    public partial class MainPage : ContentPage
    {
        private byte[] videoByte;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            await grabarVideo();
        }


        public async Task grabarVideo()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult video = await MediaPicker.Default.CaptureVideoAsync();

                if (video != null)
                {
                    try
                    {

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            Stream st = await video.OpenReadAsync();
                            await st.CopyToAsync(memoryStream);
                            videoByte = memoryStream.ToArray();
                        }


                        string fileLocalPath = Path.Combine(FileSystem.CacheDirectory, video.FileName);
                        using (FileStream fileStream = File.OpenWrite(fileLocalPath))
                        {
                            Stream stream = new MemoryStream(videoByte);
                            await stream.CopyToAsync(fileStream);
                        }

                        videoControl.Source = MediaSource.FromFile(fileLocalPath);

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Ok");
                    }
                }
            }
        }


        public async void btnGuardar_Clicked(object sender, EventArgs args)
        {
            try
            {
                var nom = DateTime.Now;
                var video = new Models.Videos
                {
                    nombre = nom.Year.ToString() + nom.Month.ToString() + nom.Day.ToString() + nom.ToShortTimeString(),
                    video = videoByte
                };

                if (await App.Instancia.AddVideo(video) > 0)
                {
                    videoByte = new byte[0];
                    videoControl.Source = null;
                    await DisplayAlert("Aviso", "Video guardado correctamente", "Ok");

                }
                else
                {
                    await DisplayAlert("Aviso", "Ocurrio un error", "Ok");
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public async void btnLista_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Views.PageVideoList());
        }
    }
}