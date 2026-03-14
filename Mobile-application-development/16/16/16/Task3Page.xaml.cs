namespace _16
{
    public partial class Task3Page : ContentPage
    {
        private const double G = 9.8; // ускорение свободного падения, м/с²

        public Task3Page()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object? sender, EventArgs e)
        {
            if (!double.TryParse(EntryV.Text?.Replace(',', '.'), 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.CultureInfo.InvariantCulture, out double v) || v <= 0)
            {
                DisplayAlert("Ошибка", "Введите положительную начальную скорость V", "OK");
                return;
            }

            if (!double.TryParse(EntryT.Text?.Replace(',', '.'), 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.CultureInfo.InvariantCulture, out double t) || t <= 0)
            {
                DisplayAlert("Ошибка", "Введите положительное время полёта T", "OK");
                return;
            }

            double argument = G * t / (2 * v);

            if (argument > 1 || argument < -1)
            {
                DisplayAlert("Ошибка", 
                    "Невозможно вычислить угол: gT/(2V) должен быть в диапазоне [-1, 1]. " +
                    "Проверьте введённые значения (для данного T нужна большая скорость V).", "OK");
                return;
            }

            double alphaRadians = Math.Asin(argument);
            double alphaDegrees = alphaRadians * 180 / Math.PI;

            ResultLabel.Text = $"Угол α = {alphaDegrees:F2}°";
            DetailLabel.Text = $"В радианах: {alphaRadians:F4} рад";

            ResultBorder.IsVisible = true;
        }
    }
}
