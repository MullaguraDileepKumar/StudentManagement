using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class SubjectService(ApplicationDbContext _context) : ISubjectService
    {
        public async Task<Subject> AddSubject(Subject subject)
        {
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return subject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<Subject>> GetSubjectList()
        {
            try
            {
                var subjectList = await _context.Subjects
                    .Select(q => new Subject
                    {
                        Id = q.Id,
                        Name = q.Name,
                        SubjectCode = q.SubjectCode,
                        CollegeId = q.CollegeId,
                    }).ToListAsync();

                return subjectList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Subject?> GetSubject(int id)
        {
            try
            {
                var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
                if (subject == null)
                    return null;
                return subject;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Subject> EditSubject(int id, Subject subject)
        {
            try
            {
                var prevSubject = await _context.Subjects.FirstOrDefaultAsync(c => c.Id == id);
                if (prevSubject != null)
                {
                    prevSubject.Name = subject.Name;
                    prevSubject.SubjectCode = subject.SubjectCode;
                    await _context.SaveChangesAsync();
                }
                return prevSubject;
            }
            catch (Exception ex) { throw ex; }
        }


        public async Task<bool> DeleteSubject(int id)
        {
            try
            {
                bool result = false;
                var subject = await _context.College.FirstOrDefaultAsync(m => m.Id == id);
                if (subject != null)
                {
                    _context.College.Remove(subject);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                return result;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
