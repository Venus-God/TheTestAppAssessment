using Microsoft.AspNetCore.Mvc;
using Moq;
using TheTestApp.API.Enums;
using TheTestApp.API.Models;

namespace StudyGroupTests
{
    internal class LeaveStudyGroupTests : StudyGroupTestSetup
    {
        [Test]
        public void GivenAGroupThatExists_WhenGroupMemberLeavesTheGroup_ThenSuccessful()
        {
            //arrange
            var user1 = new User();
            user1.UserId = 1;
            user1.Name = "Jane";

            var user2 = new User();
            user2.UserId = 2;
            user2.Name = "John";

            var listOfUsers = new List<User>
            {
                user1,
                user2
            };
            var creationDate = DateTime.Now;
            var subject = Subject.Chemistry;
            var studyGroup = new StudyGroup(1, "Chemistry", subject, creationDate, listOfUsers);
            MockRepository.Setup(x => x.LeaveStudyGroup(It.IsAny<int>(), It.IsAny<int>())).
            Returns(Task.FromResult(true));


            //act
            var result = _studyGroupSUT.LeaveStudyGroup(studyGroup.StudyGroupId, user1.UserId);

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GivenAGroupThatExists_WhenNonGroupMemberLeavesTheGroup_ThenNotFoundError()
        {
            //arrange
            var user1 = new User();
            user1.UserId = 1;
            user1.Name = "Jane";

            var user2 = new User();
            user2.UserId = 2;
            user2.Name = "John";

            var user3 = new User
            {
                UserId = 3,
                Name = "Xanthe",
                Surname = "Canning"
            };

            var listOfUsers = new List<User>
            {
                user1,
                user2
            };
            var creationDate = DateTime.Now;
            var subject = Subject.Chemistry;
            var studyGroup = new StudyGroup(1, "Chemistry", subject, creationDate, listOfUsers);
            MockRepository.Setup(x => x.LeaveStudyGroup(studyGroup.StudyGroupId, user3.UserId)).
                Returns(Task.FromResult(false));

            //act
            var result = _studyGroupSUT.LeaveStudyGroup(studyGroup.StudyGroupId, user3.UserId);

            //assert

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public void GivenAGroupThatDoesNotExists_WhenNonGroupMemberLeavesTheGroup_ThenNotFoundError()
        {
            //arrange
            //false means group not found or student not part of the group
            MockRepository.Setup(x => x.LeaveStudyGroup(It.IsAny<int>(), It.IsAny<int>())).
                Returns(Task.FromResult(false));

            //act
            var result = _studyGroupSUT.LeaveStudyGroup(1, 1);

            //assert

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}
