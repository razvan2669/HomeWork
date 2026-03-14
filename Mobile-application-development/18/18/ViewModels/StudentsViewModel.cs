using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _18.Models;
using _18.Services;

namespace _18.ViewModels;

public partial class StudentsViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEmpty))]
    [NotifyPropertyChangedFor(nameof(ShowStudentList))]
    [NotifyPropertyChangedFor(nameof(ShowEmptyState))]
    private ObservableCollection<StudentModel> _students = [];

    [ObservableProperty]
    private StudentModel? _selectedStudent;

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FormTitle))]
    private bool _isEditing;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FormTitle))]
    [NotifyPropertyChangedFor(nameof(ShowList))]
    [NotifyPropertyChangedFor(nameof(ShowStudentList))]
    [NotifyPropertyChangedFor(nameof(ShowEmptyState))]
    private bool _isFormVisible;

    public bool IsEmpty => Students.Count == 0;

    /// <summary>Видимость списка (когда форма скрыта)</summary>
    public bool ShowList => !IsFormVisible;

    /// <summary>Показать список студентов (когда есть данные и форма скрыта)</summary>
    public bool ShowStudentList => ShowList && !IsEmpty;

    /// <summary>Показать пустое состояние</summary>
    public bool ShowEmptyState => ShowList && IsEmpty;

    /// <summary>Заголовок формы</summary>
    public string FormTitle => IsEditing ? "Редактирование" : "Новый студент";

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public StudentsViewModel(DatabaseService db)
    {
        _db = db;
        _ = LoadStudentsAsync();
    }

    public async Task LoadAsync() => await LoadStudentsAsync();

    /// <summary>Вызвать сохранение из code-behind (запасной вариант если Command не срабатывает)</summary>
    public async Task ExecuteSaveAsync()
    {
        if (IsEditing)
            await UpdateStudentAsync();
        else
            await AddStudentAsync();
    }

    /// <summary>Вызвать редактирование из code-behind</summary>
    public void ExecuteEditStudent(StudentModel? student) => EditStudent(student);

    /// <summary>Вызвать удаление из code-behind</summary>
    public async Task ExecuteDeleteStudentAsync(StudentModel? student) => await DeleteStudentAsync(student);

    [RelayCommand]
    private async Task LoadStudentsAsync()
    {
        var list = await _db.GetAllStudentsAsync();
        Students = new ObservableCollection<StudentModel>(list);
    }

    [RelayCommand]
    private async Task AddStudentAsync()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email))
        {
            StatusMessage = "Заполните все поля";
            return;
        }

        var email = Email.Trim();
        if (!EmailValidator.IsValid(email))
        {
            StatusMessage = "Введите корректный email (gmail.com, yandex.ru, mail.ru и др.)";
            return;
        }

        var student = new StudentModel
        {
            FirstName = FirstName.Trim(),
            LastName = LastName.Trim(),
            Email = email
        };
        await _db.SaveStudentAsync(student);
        await LoadStudentsAsync();
        ClearForm();
        StatusMessage = "Студент добавлен";
    }

    [RelayCommand]
    private void EditStudent(StudentModel? student)
    {
        if (student == null) return;
        SelectedStudent = student;
        FirstName = student.FirstName;
        LastName = student.LastName;
        Email = student.Email;
        IsEditing = true;
        IsFormVisible = true;
        StatusMessage = "Режим редактирования";
    }

    [RelayCommand]
    private async Task UpdateStudentAsync()
    {
        if (SelectedStudent == null) return;
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email))
        {
            StatusMessage = "Заполните все поля";
            return;
        }

        var email = Email.Trim();
        if (!EmailValidator.IsValid(email))
        {
            StatusMessage = "Введите корректный email (gmail.com, yandex.ru, mail.ru и др.)";
            return;
        }

        SelectedStudent.FirstName = FirstName.Trim();
        SelectedStudent.LastName = LastName.Trim();
        SelectedStudent.Email = email;
        await _db.SaveStudentAsync(SelectedStudent);
        await LoadStudentsAsync();
        ClearForm();
        StatusMessage = "Студент обновлён";
    }

    [RelayCommand]
    private async Task DeleteStudentAsync(StudentModel? student)
    {
        if (student == null) return;
        await _db.DeleteStudentAsync(student);
        if (SelectedStudent?.StudentID == student.StudentID)
            ClearForm();
        await LoadStudentsAsync();
        StatusMessage = "Студент удалён";
    }

    [RelayCommand]
    private void ShowAddForm()
    {
        SelectedStudent = null;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        IsEditing = false;
        IsFormVisible = true;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsEditing)
            await UpdateStudentAsync();
        else
            await AddStudentAsync();
    }

    [RelayCommand]
    private async Task SaveFormAsync()
    {
        if (IsEditing)
            await UpdateStudentAsync();
        else
            await AddStudentAsync();
    }

    [RelayCommand]
    private void CancelEdit()
    {
        ClearForm();
    }

    private void ClearForm()
    {
        SelectedStudent = null;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        IsEditing = false;
        IsFormVisible = false;
    }
}
