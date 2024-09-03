using TheTestApp.API.Models;

namespace TheTestApp.API.DataLayer
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly List<StudyGroup> _studyGroups = new List<StudyGroup>();
        private readonly List<User> _users = new List<User>();

        public Task CreateStudyGroup(StudyGroup studyGroup)
        {
            _studyGroups.Add(studyGroup);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<StudyGroup>> GetStudyGroups()
        {
            return Task.FromResult<IEnumerable<StudyGroup>>(_studyGroups);
        }

        public Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject)
        {
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

        public Task LeaveStudyGroup(int studyGroupId, int userId)
        {
            var studyGroup = _studyGroups.FirstOrDefault(sg => sg.StudyGroupId == studyGroupId);
            var user = _users.FirstOrDefault(u => u.UserId == userId);

            if (studyGroup != null && user != null)
            {
                studyGroup.RemoveUser(user);
            }

            return Task.CompletedTask;
        }
    }

}
