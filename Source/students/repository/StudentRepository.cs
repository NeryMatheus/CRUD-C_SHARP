using CRUD_C_SHARP.data;
using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.students.entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C_SHARP.Source.students.repository;

public abstract class StudentRepository
{
    
    private static readonly AppDbContext Db = new();
    
    public static async Task<Student?> GetStudentByName(Student student)
    {
        return await Db.Students.FirstOrDefaultAsync(x => x.Name == student.Name);
    }
    
    public static async Task AddStudentRepository(Student student)
    {
        await Db.Students.AddAsync(student);
        await Db.SaveChangesAsync();
    }
    
    public static async Task<List<ListAllStudents>> GetAllStudents()
    {
        var activeStudents = await Db.Students
            .Where(student => student.Active)
            .Select(student => new { student.Id, student.Name, student.Active })
            .ToListAsync();

        return activeStudents
            .Select(student => new ListAllStudents(student.Id, student.Name, student.Active))
            .ToList();
    }
}