using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class MarksService(ApplicationDbContext _context,ILogger<MarksService> _logger) : IMarksService
    {
        public IQueryable<MarksView> GetStudentAllMArks(int id)
        {
            try
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
            catch(Exception ex) 
            {
                    _logger.LogError(ex, "Error during getting the Marks");
                    throw;
            }
        }
        public async Task<Marks> AddSubjectMarks(int studentId, int subjectId, int marks)
        {
            try
            {
                var record = _context.Marks.FirstOrDefault(marks => marks.StudentId == studentId && marks.SubjectId == subjectId);
                if (record == null)
                {
                    record = new Marks
                    {
                        StudentId = studentId,
                        SubjectId = subjectId,
                        StudentMarks = marks
                    };
                    await _context.Marks.AddAsync(record);
                    _logger.LogInformation("New Marks added", studentId);
                }
                else
                {
                    record.StudentMarks = marks;
                    _context.Marks.Update(record);
                    _logger.LogInformation("Updated the Marks for Student id: ", studentId, "Subject id: ", subjectId);
                }
                await _context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during Student marks Adding/Updating");
                throw;
            }
        }
        public bool DeleteStudentMarks(int studentId, int subjectId)
        {
           try
            {
                bool result = false;
                var record = _context.Marks.FirstOrDefault(m => m.StudentId == studentId && m.SubjectId == subjectId);
                if (record != null)
                {
                    _context.Marks.Remove(record);
                    _context.SaveChanges();
                    _logger.LogInformation(studentId, "Student Marks Deleted for subject id: ", subjectId);
                    result = true;
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured during Student marks deletion");
                throw;
            }
        }
    }
}
