using System.Collections.ObjectModel;
using _14.Models;

namespace _14;

public partial class SongsPage : ContentPage
{
    private readonly ObservableCollection<Song> _songs = new();

    public SongsPage()
    {
        InitializeComponent();
        SongsList.ItemsSource = _songs;

        // Пример для демонстрации
        _songs.Add(new Song(
            "Imagine",
            "John Lennon",
            1971,
            "Imagine there's no heaven\nIt's easy if you try\nNo hell below us\nAbove us only sky..."));
    }

    private void OnSongSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Song song)
            LblSongInfo.Text = song.GetInfo();
    }

    private async void OnAddSongClicked(object? sender, EventArgs e)
    {
        var title = await DisplayPromptAsync("Новая песня", "Название песни:");
        if (string.IsNullOrWhiteSpace(title)) return;

        var artist = await DisplayPromptAsync("Новая песня", "Исполнитель:");
        if (string.IsNullOrWhiteSpace(artist)) return;

        var yearStr = await DisplayPromptAsync("Новая песня", "Год выпуска:", initialValue: DateTime.Now.Year.ToString());
        if (string.IsNullOrWhiteSpace(yearStr) || !int.TryParse(yearStr, out var year))
            year = DateTime.Now.Year;

        var lyrics = await DisplayPromptAsync("Новая песня", "Текст песни (кратко):", initialValue: "(без текста)");
        if (string.IsNullOrWhiteSpace(lyrics)) lyrics = "(без текста)";

        _songs.Add(new Song(title, artist, year, lyrics));
    }

    private async void OnRemoveSongClicked(object? sender, EventArgs e)
    {
        var selected = SongsList.SelectedItem as Song;
        if (selected != null)
        {
            _songs.Remove(selected);
            LblSongInfo.Text = "Выберите песню из списка";
        }
        else
        {
            await DisplayAlertAsync("Подсказка", "Сначала выберите песню в списке.", "OK");
        }
    }
}
