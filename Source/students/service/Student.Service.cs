using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.Source.students.repository;
using CRUD_C_SHARP.students.entities;

namespace CRUD_C_SHARP.Source.students.service;

public abstract class StudentService
{
    private static async Task<object> FindByName(string name)
    {
        var studentEntity = await StudentRepository.FindByName(name);

        if (studentEntity == null)
        {
            return studentEntity!;
        }

        return Results.NotFound("Student not found");
    }

    private static async Task<object> FindById(Guid id)
    {
        var studentEntity = await StudentRepository.FindById(id);

        if (studentEntity == null)
        {
            return Results.NotFound("Student not found");
        }

        return studentEntity;
    }

    private static object HandleException(Exception e)
    {
        return Results.Problem(e.Message);
    }

    public static async Task<object> AddStudent(AddStudentRequest? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("Student name is required");

        var student = await FindByName(request.Name);

        try
        {
            if (student == null)
            {
                Student? newStudent = new(request.Name);
                await StudentRepository.AddStudentRepository(newStudent);
                var returnStudent = new ListStudents(newStudent.Id, newStudent.Name);

                return Results.Created($"/student/{returnStudent.Id}", returnStudent);
            }

            return Results.Conflict("Student already exists");
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    public static async Task<object> GetAllStudents()
    {
        try
        {
            var students = await StudentRepository.GetAllStudents();
            return students.Count == 0
                ? Results.NotFound("No students found")
                : Results.Ok(students);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    public static async Task<object> GetStudentById(Guid id)
    {
        try
        {
            var result = await FindById(id);
            if (result is Student student && student != null)
            {
                var responseData = new ListStudents(student.Id, student.Name);
                return Results.Ok(responseData);
            }

            return Results.NotFound("Student not found");
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    public static async Task<object> UpdateStudentRequest(Guid id, UpdateStudentRequest request)
    {
        try
        {
            var result = await FindById(id);

            if (result is Student student && student != null)
            {
                if (student.Name == request.Name)
                    return Results.Conflict("Student name is the same!");


                student.UpdateStudentName(request.Name);
                await StudentRepository.UpdateStudent(student);

                return Results.Ok(new ListStudents(student.Id, student.Name));
            }

            return Results.NotFound("Student not found");
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    public static async Task<object> DeleteStudent(Guid id)
    {
        try
        {
            var result = await FindById(id);

            if (result is Student student && student != null)
            {
                await StudentRepository.DeleteStudent(student);
                return Results.NoContent();
            }

            return Results.NotFound("Student not found");
        }
        catch (Exception e)
        {
            return HandleException(e);

        }
    }
}