using _18.Models;
using SQLite;

namespace _18.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _db;

    public async Task InitAsync()
    {
        if (_db != null) return;

        var path = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<StudentModel>();
    }

    public async Task<List<StudentModel>> GetAllStudentsAsync()
    {
        await InitAsync();
        return await _db!.Table<StudentModel>().ToListAsync();
    }

    public async Task<StudentModel?> GetStudentAsync(int id)
    {
        await InitAsync();
        return await _db!.Table<StudentModel>().FirstOrDefaultAsync(s => s.StudentID == id);
    }

    public async Task<int> AddStudentAsync(StudentModel student)
    {
        await InitAsync();
        return await _db!.InsertAsync(student);
    }

    public async Task<int> UpdateStudentAsync(StudentModel student)
    {
        await InitAsync();
        return await _db!.UpdateAsync(student);
    }

    public async Task<int> DeleteStudentAsync(StudentModel student)
    {
        await InitAsync();
        return await _db!.DeleteAsync(student);
    }

    public async Task SaveStudentAsync(StudentModel student)
    {
        await InitAsync();
        if (student.StudentID == 0)
            await _db!.InsertAsync(student);
        else
            await _db!.UpdateAsync(student);
    }
}
