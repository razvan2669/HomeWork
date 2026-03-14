using Microsoft.Maui.Controls.Shapes;

namespace _16;

public partial class Task1Page : ContentPage
{
    private readonly List<(Entry Z, Entry B, Entry A, Entry Phi)> _inputFields = [];

    public Task1Page()
    {
        InitializeComponent();
    }

    private void OnCreateFormClicked(object? sender, EventArgs e)
    {
        if (!int.TryParse(CountEntry.Text, out var n) || n < 1 || n > 50)
        {
            DisplayAlert("Ошибка", "Введите корректное число n (1-50)", "OK");
            return;
        }

        InputsContainer.Children.Clear();
        _inputFields.Clear();

        for (var i = 0; i < n; i++)
        {
            var frame = new Border
            {
                Stroke = Colors.LightGray,
                StrokeThickness = 1,
                Padding = new Thickness(12),
                StrokeShape = new RoundRectangle { CornerRadius = 6 },
                BackgroundColor = Application.Current?.RequestedTheme == AppTheme.Dark 
                    ? Color.FromArgb("#2a2a2a") 
                    : Color.FromArgb("#f5f5f5")
            };

            var stack = new VerticalStackLayout { Spacing = 6 };

            var label = new Label
            {
                Text = $"Значения для X{i + 1}:",
                FontAttributes = FontAttributes.Bold
            };

            var zEntry = new Entry { Placeholder = "Z", Keyboard = Keyboard.Numeric };
            var bEntry = new Entry { Placeholder = "B", Keyboard = Keyboard.Numeric };
            var aEntry = new Entry { Placeholder = "A", Keyboard = Keyboard.Numeric };
            var phiEntry = new Entry { Placeholder = "Угол φ (градусы)", Keyboard = Keyboard.Numeric };

            stack.Children.Add(label);
            stack.Children.Add(zEntry);
            stack.Children.Add(bEntry);
            stack.Children.Add(aEntry);
            stack.Children.Add(phiEntry);

            frame.Content = stack;
            InputsContainer.Children.Add(frame);
            _inputFields.Add((zEntry, bEntry, aEntry, phiEntry));
        }

        InputsBorder.IsVisible = true;
        CalculateBtn.IsVisible = true;
        ResultBorder.IsVisible = false;
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        double y = 0;

        for (var i = 0; i < _inputFields.Count; i++)
        {
            var (zEntry, bEntry, aEntry, phiEntry) = _inputFields[i];

            if (!double.TryParse(zEntry.Text, out var z) ||
                !double.TryParse(bEntry.Text, out var b) ||
                !double.TryParse(aEntry.Text, out var a) ||
                !double.TryParse(phiEntry.Text, out var phiDeg))
            {
                DisplayAlert("Ошибка", $"Заполните все поля для X{i + 1}", "OK");
                return;
            }

            // tg²φ - тангенс в квадрате, угол вводится в градусах
            var phiRad = phiDeg * Math.PI / 180;
            var tgSquared = Math.Pow(Math.Tan(phiRad), 2);

            if (Math.Abs(tgSquared) < 1e-10)
            {
                DisplayAlert("Ошибка", $"Угол φ для X{i + 1} не должен быть 90° или -90° (tg не определён)", "OK");
                return;
            }

            // X = Z³ - B + A²/tg²φ
            var x = Math.Pow(z, 3) - b + (a * a) / tgSquared;
            y += x;
        }

        ResultLabel.Text = $"Y = {y:F4}";
        ResultBorder.IsVisible = true;
    }
}
