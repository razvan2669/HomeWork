namespace _16
{
    public partial class Task2Page : ContentPage
    {
        public Task2Page()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object? sender, EventArgs e)
        {
            if (!int.TryParse(EntryN.Text, out int n) || n < 1)
            {
                DisplayAlert("Ошибка", "Введите целое положительное число N", "OK");
                return;
            }

            if (!int.TryParse(EntryK.Text, out int k) || k < 1)
            {
                DisplayAlert("Ошибка", "Введите целое положительное число k", "OK");
                return;
            }

            double sum = 0;
            var terms = new List<string>();

            for (int i = 1; i <= n; i++)
            {
                double term = Math.Pow(i, k);
                sum += term;
                terms.Add($"{i}^{k} = {term:N0}");
            }

            ResultLabel.Text = $"Сумма = {sum:N2}";
            DetailLabel.Text = string.Join(" + ", Enumerable.Range(1, Math.Min(n, 10)).Select(i => $"{i}^{k}")) +
                (n > 10 ? $" + ... + {n}^{k}" : "");

            ResultBorder.IsVisible = true;
        }
    }
}
