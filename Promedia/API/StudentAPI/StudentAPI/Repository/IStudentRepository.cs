using Microsoft.EntityFrameworkCore;
using StudentAPI.Domain.Entities;

namespace StudentAPI.Repository
{
	public interface IStudentRepository
	{
		Task<Student?> GetStudentById(int Id);

		Task<IEnumerable<Student>?> GetAllStudnts();

		Task<bool> AddStudentAsync(Student student);

		Task<bool> IsValidNatianlId(string nationalId);

		bool DeleteStudent(int Id);

		bool UpdateStudent(Student student);
	}
}
