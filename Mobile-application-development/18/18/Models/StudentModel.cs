using SQLite;

namespace _18.Models;

[Table("Students")]
public class StudentModel
{
    [PrimaryKey, AutoIncrement]
    public int StudentID { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Ignore]
    public string FullName => $"{LastName} {FirstName}".Trim();
}
