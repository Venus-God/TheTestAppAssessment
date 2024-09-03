using Microsoft.AspNetCore.Mvc;
using Moq;
using TheTestApp.API.Enums;
using TheTestApp.API.Models;

namespace StudyGroupTests
{
    internal class GetStudyGroupTests : StudyGroupTestSetup
    {
        [Test]
        public void Given_ThereAreExistingGroups_WhenGetStudyGroup_ThenListOfStudyGroupsReturned()
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, "Maths", Subject.Chemistry, createDate, users);
            var allGroups = new List<StudyGroup>();
            allGroups.Add(studyGroup);
            MockRepository.Setup(x => x.GetStudyGroups()).ReturnsAsync(allGroups);


            //act
            var result = _studyGroupSUT.GetStudyGroups();

            //Assert
            Assert.IsNotNull(result.Result);
        }

        [Test]
        public void Given_ThereAreNoExistingGroups_WhenGetStudyGroup_ThenEmptyListReturned()
        {
            //arrange
            var studyGroups = new List<StudyGroup>();
            MockRepository.Setup(x => x.GetStudyGroups()).ReturnsAsync(studyGroups);

            //act
            var result = _studyGroupSUT.GetStudyGroups();

            //assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);

        }
    }
}
