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
    }
}