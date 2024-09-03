using Moq;
using TheTestApp.API.DataLayer;
using TheTestApp.API.Controllers;

namespace StudyGroupTests
{
    internal class StudyGroupTestSetup
    {
        public StudyGroupController _studyGroupSUT;
        public Mock<IStudyGroupRepository> MockRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            MockRepository = new Mock<IStudyGroupRepository>();
            _studyGroupSUT = new StudyGroupController(MockRepository.Object);
        }
    }
}