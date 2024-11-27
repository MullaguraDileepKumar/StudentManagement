using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class MarksService(ApplicationDbContext _context) : IMarksService
    {
        public IQueryable<MarksView> GetStudentAllMArks(int id)
        {
            var studentAllMarks = from st in _context.Students
                                  join m in _context.Marks on st.Id equals m.StudentId
                                  join sub in _context.Subjects on m.SubjectId equals sub.Id
                                  join c in _context.College on st.CollegeId equals c.Id
                                  select new MarksView
                                  {
                                      CollegeId = new MarksView.College
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                          Address = c.Address,
                                          
                                      },
                                      Student = new Model.Student
                                      {
                                          Id = st.Id,
                                          Name = st.Name,
                                      },
                                      Marks = new Model.Marks
                                      {
                                         Id = m.Id,
                                         SubjectId = m.SubjectId,
                                         StudentMarks = m.StudentMarks,
                                      },
                                      SubjectId = new MarksView.Subject
                                      {
                                          Id = sub.Id,
                                          Name = sub.Name,
                                          SubjectCode = sub.SubjectCode,
                                      },
                                  };

            return studentAllMarks;
        }
        public async Task<Marks> AddSubjectMarks(int studentId, int subjectId, int marks)
        {
            var record =  _context.Marks.FirstOrDefault(marks =>  marks.StudentId == studentId && marks.SubjectId == subjectId);
            if (record == null)
            {
                record = new Marks { 
                    StudentId = studentId,
                    SubjectId = subjectId,
                    StudentMarks = marks
                };
               await _context.Marks.AddAsync(record);
            }
            else
            {
               record.StudentMarks = marks;
                _context.Update(record);
            }
           await _context.SaveChangesAsync();
            return record;
        }
        public bool DeleteStudentMarks(int studentId, int subjectId)
        {
            bool result = false;
            var record = _context.Marks.FirstOrDefault(m => m.StudentId == studentId && m.SubjectId == subjectId);
            if (record != null)
            {
                _context.Remove(record);
                result = true;
            }
            _context.SaveChanges();
            return result;
        }
    }
}
