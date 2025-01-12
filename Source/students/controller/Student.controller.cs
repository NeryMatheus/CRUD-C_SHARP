using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.Source.students.service;

namespace CRUD_C_SHARP.Source.students.controller;

public static class StudentController
{
    public static void MapStudentRoutes(this WebApplication app)
    {
        var studentRoutes = app.MapGroup("student");

        studentRoutes.MapPost("", async (AddStudentRequest request) =>
        {
            var newStudent = await StudentService.AddStudent(request);
            return newStudent;
        });
        
        studentRoutes.MapGet("/all", async () =>
        {
            var students = await StudentService.GetAllStudents();
            return students;
        });
        
        studentRoutes.MapGet("/{name}", async (string name) =>
        {
            var student = await StudentService.GetStudentByName(name);
            return student;
        });
        
        studentRoutes.MapPatch("/{id:guid}", async (Guid id, UpdateStudentRequest request) =>
        {
            var student = await StudentService.UpdateStudentRequest(id, request);
            return student;
        });
    }
}