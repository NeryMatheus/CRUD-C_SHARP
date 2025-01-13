using CRUD_C_SHARP.Source.result;
using CRUD_C_SHARP.Source.students.record;
using CRUD_C_SHARP.Source.students.repository;
using CRUD_C_SHARP.students.entities;

namespace CRUD_C_SHARP.Source.students.service;

public abstract class StudentService
{
    public static async Task<ApiResponse<ListStudents>> AddStudent(AddStudentRequest? request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Name))
            return new ApiResponse<ListStudents>(null, "Insert student first", "Not Content", 204);
        
        var newStudent = new Student(request.Name);
        var studentExist = await StudentRepository.GetStudentByName(newStudent);
        
        if (studentExist != null)
        {
            return new ApiResponse<ListStudents>(null, "Student already exists", "Conflict", 409);
        }

        await StudentRepository.AddStudentRepository(newStudent);
        
        var studentEntity = await StudentRepository.GetStudentByName(newStudent);
        var studentReturn = new ListStudents(studentEntity!.Id, studentEntity.Name);

        return new ApiResponse<ListStudents>(studentReturn, "Student added successfully", "OK", 200);
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
    
    public static async Task<ApiResponse<ListStudents?>> GetStudentByName(string name)
    {
        ListStudents? listStudents = null;
        try
        {
            var students = await StudentRepository.FindByName(new Student(name));

            if (students != null)
            {
                listStudents = new ListStudents(students.Id, students.Name);
            }

            return students == null 
                ? new ApiResponse<ListStudents?>(null, "Student not found", "Not Found", 404) 
                : new ApiResponse<ListStudents?>(listStudents, "Student retrieved successfully", "Success", 200);
        } catch (Exception e)
        {
            return new ApiResponse<ListStudents?>(null, e.Message, "Internal Server Error", 500);
        }
    }
    
    public static async Task<ApiResponse<ListStudents>> UpdateStudentRequest(Guid id, UpdateStudentRequest request)
    {
        try
        {
            var student = await StudentRepository.FindById(id);
            
            if (student == null)
            {
                return new ApiResponse<ListStudents>(null, "Student not found", "Not Found", 404);
            }
            
            student.UpdateStudentName(request.Name);
            
            await StudentRepository.UpdateStudent(student);
            
            return new ApiResponse<ListStudents>(new ListStudents(student.Id, student.Name), "Student updated successfully", "Success", 200);
        }
        catch (Exception e)
        {
            return new ApiResponse<ListStudents>(null, e.Message, "Internal Server Error", 500);
        }
    }
}