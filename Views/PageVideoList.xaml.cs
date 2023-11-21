using Tarea2._4.Models;

namespace Tarea2._4.Views;

public partial class PageVideoList : ContentPage
{

    public static Videos videoSeleccionado;
    
    public PageVideoList()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        list.ItemsSource = await App.Instancia.ListVideos();
    }

    private async void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Videos selectedVideo)
        {
            if (selectedVideo != null)
            {
                videoSeleccionado = selectedVideo;
                mostrarVideo();
            }
        }
    }

    private async void mostrarVideo()
    {
        await Navigation.PushAsync(new PageVideoPlay());
    }
}