namespace _19.Pages;

public partial class NumberReversePage : ContentPage
{
    public NumberReversePage()
    {
        InitializeComponent();
    }

    private void OnNumberTextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateResult();
    }

    private void OnReverseClicked(object? sender, EventArgs e)
    {
        UpdateResult();
    }

    private void UpdateResult()
    {
        var text = NumberEntry.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(text))
        {
            ResultLabel.Text = "—";
            return;
        }
        if (!long.TryParse(text, out _))
        {
            ResultLabel.Text = "Введите число";
            return;
        }
        var reversed = new string(text.Reverse().ToArray());
        ResultLabel.Text = reversed;
    }
}
