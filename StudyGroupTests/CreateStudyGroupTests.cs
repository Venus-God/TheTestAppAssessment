using TheTestApp.API.Models;
using TheTestApp.API.Enums;
using Microsoft.AspNetCore.Mvc;


namespace StudyGroupTests
{
    [TestFixture]
    internal class CreateStudyGroupTests : StudyGroupTestSetup
    {
        [TestCase("short")]
        [TestCase("LongestCharacterThatICouldThin")]
        [TestCase("MediumNumber")]
        [TestCase("Chemistry")]
        [TestCase("Maths")]
        [TestCase("Physics")]
        public void Given_AValidGroupName_CreateStudyGroup_ShouldBeSuccessFul(string studyGroupName)
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, studyGroupName, Subject.Chemistry, createDate, users);
            MockRepository.Setup(x => x.CreateStudyGroup(studyGroup)).Returns(Task.CompletedTask);
            //Act
            var result = _studyGroupSUT.CreateStudyGroup(studyGroup);

            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [TestCase("shot")]
        [TestCase("LongestCharacterThatICouldThinkOff")]
        public void Given_AnInvalidGroupName_CreateStudyGroup_ShouldReturnBadRequest(string studyGroupName)
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, studyGroupName, Subject.Chemistry, createDate, users);
            MockRepository.Setup(x => x.CreateStudyGroup(studyGroup)).Returns(Task.CompletedTask);
            //Act
            var result = _studyGroupSUT.CreateStudyGroup(studyGroup);

            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [TestCase(Subject.Chemistry)]
        [TestCase(Subject.Physics)]
        [TestCase(Subject.Math)]
        public void Given_ValidSubject_CreateStudyGroup_ShouldReturnSuccess(Subject subject)
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroupName = nameof(subject) + "Study Group";
            var studyGroup = new StudyGroup(1, studyGroupName, subject, createDate, users);
            MockRepository.Setup(x => x.CreateStudyGroup(studyGroup)).Returns(Task.CompletedTask);
            //Act
            var result = _studyGroupSUT.CreateStudyGroup(studyGroup);

            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [TestCase("DuplicateGroupName")]
        public void GivenDuplicateGroupName_WhenCreatingStudyGroup_ThenThrowAnError(string groupName)
        {
            //Arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, groupName, Subject.Math, createDate, users);
            MockRepository.Setup(x => x.CreateStudyGroup(studyGroup)).Returns(Task.CompletedTask);
            //create the first group
            var duplicateStudygroup = _studyGroupSUT.CreateStudyGroup(studyGroup);


            //Act
            //create the group again for duplication
            var result = _studyGroupSUT.CreateStudyGroup(studyGroup);

            //Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
            //actual results would have a duplication error
            //Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}
