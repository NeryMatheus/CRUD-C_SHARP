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
    
    public static async Task<ApiResponse<List<ListStudents>>> GetAllStudents()
    {
        try
        {
            var students = await StudentRepository.GetAllStudents();
            return students.Count == 0 
                ? new ApiResponse<List<ListStudents>>(null, "No students found", "Not Content", 204) 
                : new ApiResponse<List<ListStudents>>(students, "Students retrieved successfully", "Success", 200);
        }
        catch (Exception e)
        {
            return new ApiResponse<List<ListStudents>>(null, e.Message, "Internal Server Error", 500);
        }
    }
    
    public static async Task<ApiResponse<ListStudents>> GetStudentByName(string name)
    {
        try
        {
            var students = await StudentRepository.FindByName(new Student(name));
            
            return students == null 
                ? new ApiResponse<ListStudents>(null, "Student not found", "Not Found", 404) 
                : new ApiResponse<ListStudents>(students, "Student retrieved successfully", "Success", 200);
        } catch (Exception e)
        {
            return new ApiResponse<ListStudents>(null, e.Message, "Internal Server Error", 500);
        }
    }
}