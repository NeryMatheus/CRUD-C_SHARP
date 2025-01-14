using CRUD_C_SHARP.data;
using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.students.entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C_SHARP.Source.students.repository;

public abstract class StudentRepository
{
    private static readonly AppDbContext Db = new();

    public static async Task AddStudentRepository(Student student, CancellationToken Ct = default)
    {
        await Db.Students.AddAsync(student, Ct);
        await Db.SaveChangesAsync(Ct);
    }

    public static async Task<List<ListStudents>> GetAllStudents(CancellationToken Ct = default)
    {
        var activeStudents = await Db.Students
            .Where(student => student.Active)
            .Select(student => new { student.Id, student.Name })
            .ToListAsync(Ct);

        return [.. activeStudents.Select(student => new ListStudents(student.Id, student.Name))];
    }

    public static async Task<Student?> FindByName(string name, CancellationToken Ct = default) => await Db.Students
        .Where(x => x.Active)
        .SingleOrDefaultAsync(x => x.Name == name, Ct);

    public static async Task<Student?> FindById(Guid id, CancellationToken Ct = default) => await Db.Students
        .Where(x => x.Active)
        .SingleOrDefaultAsync(x => x.Id == id, Ct);

    public static async Task UpdateStudent(Student student, CancellationToken Ct = default)
    {
        Db.Students.Update(student);
        await Db.SaveChangesAsync(Ct);
    }

    public static async Task DeleteStudent(Student student, CancellationToken Ct = default)
    {
        student.DeactivateStudent();
        await Db.SaveChangesAsync(Ct);
    }

}