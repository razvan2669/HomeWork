using _18.ViewModels;

namespace _18.Pages;

public partial class StudentsPage : ContentPage
{
    public StudentsPage(StudentsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StudentsViewModel viewModel)
            await viewModel.LoadAsync();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (BindingContext is StudentsViewModel vm)
            await vm.ExecuteSaveAsync();
    }

    private void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Models.StudentModel student
            && BindingContext is StudentsViewModel vm)
        {
            vm.ExecuteEditStudent(student);
        }
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Models.StudentModel student
            && BindingContext is StudentsViewModel vm)
        {
            await vm.ExecuteDeleteStudentAsync(student);
        }
    }
}
