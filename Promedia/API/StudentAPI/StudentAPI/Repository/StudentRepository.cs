using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Domain;
using StudentAPI.Domain.Entities;
using StudentAPI.Repository;
using System.Net;
using System.Reflection;

namespace StudentAPI.Services
{
	public class StudentRepository : IStudentRepository
	{
		// open connection with DpContext
		private readonly PromediaContext _dbContext;
		public StudentRepository(PromediaContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Student?> GetStudentById(int Id)
		=> (_dbContext.Students.FromSqlRaw("SP_GetStudentById @Id={0}", Id).AsEnumerable().FirstOrDefault())!;

		public async Task<IEnumerable<Student>?> GetAllStudnts()
			=> await _dbContext.Students.FromSqlRaw("SP_GetAllStudents").ToListAsync();

		public async Task<bool> IsValidNatianlId(string nationalId)
		{
			var result = await _dbContext.Database.ExecuteSqlRawAsync("SP_ValidNationalId @NationalId = {0}", nationalId);
			return result <= 0;
		}
		public async Task<bool> AddStudentAsync(Student student)
		{
			try
			{

				await _dbContext.Database.ExecuteSqlRawAsync(
				"exec SP_StudentCommands @command = {0}, @Id = {1}, @FName = {2}, @LName = {3}, @NationalId = {4}, @BirthDate = {5}, @Address = {6}, @Gender = {7}",
				"Insert",
				0,
				student.FirstName,
				student.LastName,
				student.NationalId,
				student.BirthOfDate,
				student.Address,
				student.Gender
				);
				return true;
			}
			catch
			{
				{
					return false;
				}
			}
		}

		public bool DeleteStudent(int Id)
		{
			try
			{
				_dbContext.Database.ExecuteSqlRaw(
					"exec SP_StudentCommands @command = {0}, @Id = {1}, @FName = {2}, @LName = {3}, @NationalId = {4}, @BirthDate = {5}, @Address = {6}, @Gender = {7}",
				string.Empty,
				Id,
				string.Empty,
				string.Empty,
				string.Empty,
				DateTime.Now,
				string.Empty,
				string.Empty
				);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateStudent(Student student)
		{
			try
			{
				_dbContext.Database.ExecuteSqlRaw(
				 "exec SP_StudentCommands @command = {0}, @Id = {1}, @FName = {2}, @LName = {3}, @NationalId = {4}, @BirthDate = {5}, @Address = {6}, @Gender = {7}",
				  "Update",
				  student.Id,
				  student.FirstName,
				  student.LastName,
				  student.NationalId,
				  student.BirthOfDate,
				  student.Address,
				  student.Gender
				  );
				return true;
			}
			catch
			{
				return false;
			}

		}
	}
}