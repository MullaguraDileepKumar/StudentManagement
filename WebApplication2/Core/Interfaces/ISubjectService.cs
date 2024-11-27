using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject> AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetSubjectList();
        Task<Subject?> GetSubject(int id);
        Task<Subject> EditSubject(int id, Subject subject);
        Task<bool> DeleteSubject(int id);
    }
}
