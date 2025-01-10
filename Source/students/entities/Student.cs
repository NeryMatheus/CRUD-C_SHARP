using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_C_SHARP.students.entities;

[Table("students")]
public class Student
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public bool Active { get; set; }

    public Student(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        Active = true;
    }
    
}