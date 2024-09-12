using Moq;
using TheTestApp.API.DataLayer;
using TheTestApp.API.Controllers;
using Microsoft.EntityFrameworkCore;
using TheTestApp.API.Models;

namespace StudyGroupTests
{
    internal class StudyGroupTestSetup
    {
        public StudyGroupController _studyGroupSUT;
        public Mock<IStudyGroupRepository> MockRepository { get; set; }
        public Mock<MyDbContext> MockContext;
        public Mock<DbSet<StudyGroup>> MockDbSetStudyGroup;

        [SetUp]
        public void Setup()
        {
            MockRepository = new Mock<IStudyGroupRepository> ();
            _studyGroupSUT = new StudyGroupController(MockRepository.Object);
            MockContext = new Mock<MyDbContext>();
            MockDbSetStudyGroup = new Mock<DbSet<StudyGroup>>();        }
    }
}