using CRUD_C_SHARP.data;
using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.students.entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C_SHARP.Source.students.repository;

public abstract class StudentRepository
{
    
    private static readonly AppDbContext Db = new();
    private static readonly CancellationToken Ct;
    
    public static async Task<Student?> GetStudentByName(Student student)
    {
        return await Db.Students
            .Where(s => s.Active)
            .FirstOrDefaultAsync(x => x.Name == student.Name, Ct);
    }
    
    public static async Task AddStudentRepository(Student student)
    {
        await Db.Students.AddAsync(student, Ct);
        await Db.SaveChangesAsync(Ct);
    }
    
    public static async Task<List<ListStudents>> GetAllStudents()
    {
        var activeStudents = await Db.Students
            .Where(student => student.Active)
            .Select(student => new { student.Id, student.Name, student.Active })
            .ToListAsync(Ct);

        return activeStudents
            .Select(student => new ListStudents(student.Id, student.Name))
            .ToList();
    }
    
    public static async Task<Student?> FindByName(Student student)
    {
        return await Db.Students
            .Where(x => x.Name.ToUpper() == student.Name.ToUpper())
            .Where(x => x.Active)
            .FirstOrDefaultAsync(Ct);
    }
    
    public static async Task<Student?> FindById(Guid id)
    {
        return await Db.Students.SingleOrDefaultAsync(x => x.Id == id, Ct);
    }
    
    public static async Task UpdateStudent(Student student)
    {
        Db.Students.Update(student);
        await Db.SaveChangesAsync(Ct);
    }

    public static async Task DeleteStudent(Student student)
    {
        student.DeactivateStudent();
        await Db.SaveChangesAsync(Ct);
    }
        
}