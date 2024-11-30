using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class CollegeService(ApplicationDbContext _context, ILogger<CollegeService> _logger) : ICollegeService
    {
        public async Task<College> AddCollege(College college)
        {
            try
            {
                    await _context.College.AddAsync(college);
                    await _context.SaveChangesAsync();
                _logger.LogInformation("New College is Added Name: ",college.Name);
                    return college;
            }
            catch(Exception ex){
                _logger.LogError(ex,"Error during the adding the college");
                throw;
            }
        }

        public async Task<IEnumerable<College>> GetCollegeList()
        {
            try
            {
                var collegeList = await _context.College
                    .Select(q => new College { 
                    Id = q.Id,
                    Name = q.Name,
                    Address = q.Address,
                    EstablishedYear = q.EstablishedYear,
                    }).ToListAsync();
                _logger.LogInformation("Retriived the College List");
                return collegeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<College?> GetCollege(int id)
        {
            try
            {
                var college = await _context.College
                .FirstOrDefaultAsync(m => m.Id == id);
                if (college == null)
                    return null;
                return college;
            }catch (Exception ex) { throw ex; }
        }

        public async Task<College> EditCollege(int id,College college)
        {
            try
            {
                var prevCollege = await _context.College.FirstOrDefaultAsync(c => c.Id == id);
                if (prevCollege != null)
                {
                    prevCollege.Name = college.Name == null || college.Name == "string" ? prevCollege.Name : college.Name;
                    prevCollege.Address = college.Address == null || college.Address == "string" ? prevCollege.Address : college.Address;
                    prevCollege.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"college got updated Id : {id} , College : {college.Name}");
                }
                return prevCollege;
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "Error occured during editing the college");
                throw; 
            }
        }

        
        public async Task<bool> DeleteCollege(int id)
        {
            try
            {
                bool result = false;
                var college = await _context.College.FirstOrDefaultAsync(m => m.Id == id);
                if (college != null)
                {
                    /*college.IsActive = false;
                    college.IsDeleted = true;*/
                    _context.College.Remove(college);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("College : ",college.Name ,$"Id : {id} got deleted");
                    result = true;
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured during deleting the college");
                throw;
            }
        }
        public async Task<IEnumerable<StudentView>> GetStudentsInCollege(int id)
        {
           try
            {
                var studentsList = await (from c in _context.College
                                          join s in _context.Students
                                          on c.Id equals s.CollegeId
                                          where c.Id == id
                                          select new StudentView
                                          {
                                              Student = new Student
                                              {
                                                  Id = s.Id,
                                                  Name = s.Name,
                                                  PhoneNumber = s.PhoneNumber,
                                              },
                                              College = new College
                                              {
                                                  Id = c.Id,
                                                  Name = c.Name,
                                                  Address = c.Address,
                                                  EstablishedYear = c.EstablishedYear,
                                              },
                                          }).ToListAsync();
                return studentsList;
            }catch(Exception ex){ throw ex; }
        }
    }
}
