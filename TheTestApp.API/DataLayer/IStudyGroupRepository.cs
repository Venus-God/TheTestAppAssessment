using TheTestApp.API.Models;
using TheTestApp.API.Enums;

namespace TheTestApp.API.DataLayer
{
    public interface IStudyGroupRepository
    {
        Task<bool> CreateStudyGroupAsync(StudyGroup studyGroup);
        Task<IEnumerable<StudyGroup>> GetStudyGroups();
        Task<IEnumerable<StudyGroup>> SearchStudyGroups(Subject subject);
        Task JoinStudyGroup(int studyGroupId, int userId);
        Task<bool> LeaveStudyGroup(int studyGroupId, int userId);
    }

}
