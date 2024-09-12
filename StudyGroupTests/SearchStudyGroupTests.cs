using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTestApp.API.Enums;
using TheTestApp.API.Models;

namespace StudyGroupTests
{
    internal class SearchStudyGroupTests : StudyGroupTestSetup
    {
        [TestCase(Subject.Chemistry, "Chemistry")]
        [TestCase(Subject.Physics, "Physics")]
        [TestCase(Subject.Math, "Maths")]
        public void GivenAValidSubjectWithStudyGroup_WhenSearchStudyGroup_Success(Subject subject, string studyGroupName)
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, studyGroupName, subject, createDate, users);
            var searchedGroups = new List<StudyGroup>
            {
                studyGroup
            };
            MockRepository.Setup(a => a.CreateStudyGroupAsync(studyGroup));
            MockRepository.Setup(x => x.SearchStudyGroups(subject)).ReturnsAsync(searchedGroups);

            //act

            var result = _studyGroupSUT.SearchStudyGroups(subject);

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [TestCase(Subject.Chemistry, "Chemistry", Subject.Physics)]
        [TestCase(Subject.Physics, "Physics", Subject.Chemistry)]
        [TestCase(Subject.Math, "Maths", Subject.Chemistry)]
        public void GivenAValidSubjectWithoutAStudyGroup_WhenSearchStudyGroup_Success(Subject subject, string studyGroupName, Subject secondSubject)
        {
            //arrange
            var createDate = DateTime.Now;
            var users = new List<User>();
            var studyGroup = new StudyGroup(1, studyGroupName, subject, createDate, users);
            var searchedGroups = new List<StudyGroup>
            {
                studyGroup
            };
            MockRepository.Setup(a => a.CreateStudyGroupAsync(studyGroup));
            MockRepository.Setup(x => x.SearchStudyGroups(subject)).ReturnsAsync(searchedGroups);

            //act

            var result = _studyGroupSUT.SearchStudyGroups(secondSubject);

            //assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}
