using Tarea2._4.Models;

namespace Tarea2._4.Views;

public partial class PageVideoPlay : ContentPage
{

	private Videos videoData;

    public PageVideoPlay()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        videoData = PageVideoList.videoSeleccionado;

        reporoducir();

        labNombreVideo.Text = videoData.nombre;
    }

    public async void reporoducir()
	{
        string tempFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tempVideo.mp4");

        File.WriteAllBytes(tempFileName, videoData.video);

        videoControl.Source = new Uri(tempFileName);

    }
}