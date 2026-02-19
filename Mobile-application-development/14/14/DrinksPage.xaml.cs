using System.Collections.ObjectModel;
using _14.Models;

namespace _14;

public partial class DrinksPage : ContentPage
{
    private readonly ObservableCollection<Drink> _drinks = new();

    public DrinksPage()
    {
        InitializeComponent();
        DrinksList.ItemsSource = _drinks;

        // Примеры для демонстрации
        _drinks.Add(new Tea("Эрл Грей", 300, "Горячий", "Чёрный"));
        _drinks.Add(new Coffee("Капучино", 200, "Горячий", "Капучино"));
        _drinks.Add(new Juice("Яблочный сок", 250, "Яблочный"));
    }

    private void OnDrinkSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Drink drink)
            LblDrinkInfo.Text = drink.GetInfo();
    }

    private async void OnAddDrinkClicked(object? sender, EventArgs e)
    {
        var type = await DisplayActionSheetAsync("Тип напитка", "Отмена", null, "Чай", "Кофе", "Сок");
        if (string.IsNullOrEmpty(type) || type == "Отмена") return;

        if (type == "Чай")
        {
            _drinks.Add(new Tea("Новый чай", 250, "Горячий", "Чёрный"));
        }
        else if (type == "Кофе")
        {
            _drinks.Add(new Coffee("Новый кофе", 200, "Горячий", "Эспрессо"));
        }
        else if (type == "Сок")
        {
            _drinks.Add(new Juice("Новый сок", 300, "Яблочный"));
        }
    }

    private async void OnRemoveDrinkClicked(object? sender, EventArgs e)
    {
        var selected = DrinksList.SelectedItem as Drink;
        if (selected != null)
        {
            _drinks.Remove(selected);
            LblDrinkInfo.Text = "Выберите напиток из списка";
        }
        else
        {
            await DisplayAlertAsync("Подсказка", "Сначала выберите напиток в списке.", "OK");
        }
    }
}
