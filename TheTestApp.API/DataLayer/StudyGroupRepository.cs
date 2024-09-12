using TheTestApp.API.Enums;
using TheTestApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TheTestApp.API.DataLayer
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly List<StudyGroup> _studyGroups = new List<StudyGroup>();
        private readonly List<User> _users = new List<User>();
        private readonly MyDbContext _context;

        public StudyGroupRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStudyGroupAsync(StudyGroup studyGroup)
        {
            var existingGroups = _context.StudyGroups
                .FirstOrDefault(x => x.Name == studyGroup.Name);
            if (existingGroups != null)
            {
                return false;
            }
            _studyGroups.Add(studyGroup);
            return true;
        }

        public Task<IEnumerable<StudyGroup>> GetStudyGroups()
        {
            return Task.FromResult<IEnumerable<StudyGroup>>(_studyGroups);
        }

        public Task<IEnumerable<StudyGroup>> SearchStudyGroups(Subject theSubject)
        {
            var subject = theSubject.ToString();
            var result = _studyGroups.Where(sg => sg.Subject.ToString().Equals(subject, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult<IEnumerable<StudyGroup>>(result);
        }

        public Task JoinStudyGroup(int studyGroupId, int userId)
        {
            var studyGroup = _studyGroups.FirstOrDefault(sg => sg.StudyGroupId == studyGroupId);
            var user = _users.FirstOrDefault(u => u.UserId == userId);

            if (studyGroup != null && user != null)
            {
                studyGroup.AddUser(user);
            }

            return Task.CompletedTask;
        }

        public Task<bool> LeaveStudyGroup(int studyGroupId, int userId)
        {
            var studyGroup = _studyGroups.FirstOrDefault(sg => sg.StudyGroupId == studyGroupId);
            var user = _users.FirstOrDefault(u => u.UserId == userId);

            if (studyGroup != null && user != null)
            {
                studyGroup.RemoveUser(user);
                return Task.FromResult(true);
            }
            else return Task.FromResult(false);
        }
    }

}
