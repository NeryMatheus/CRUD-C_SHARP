using CRUD_C_SHARP.Source.result;
using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.Source.students.repository;
using CRUD_C_SHARP.students.entities;

namespace CRUD_C_SHARP.Source.students.service;

public abstract class StudentService
{
    public static async Task<ResultBase> AddStudent(AddStudentRequest request)
    {
        var newStudent = new Student(request.Name);
        var studentEntity = await StudentRepository.GetStudentByName(newStudent);
        
        if (studentEntity != null)
        {
            return new ResultBase("Student already exists", "Conflict", 409);
        }

        await StudentRepository.AddStudentRepository(newStudent);
        
        return new ResultBase("Student added successfully", "OK", 200);
    } 
}