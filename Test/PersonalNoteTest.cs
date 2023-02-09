using AutoMapper;
using Bogus;
using Contracts;
using Entities;
using Entities.DTO;
using Entities.Helper;
using Entities.Models;
using HttpContextMoq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonalNotes.Controllers;
using Repository;
using System.Reflection.Metadata;

namespace Test
{
    public class NoteControllerTest
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly NoteController _controller;

        public NoteControllerTest()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();

            _controller = new NoteController(_mockMapper.Object, _mockRepo.Object);

        }
        //Arrange. With this action, you prepare all the required data and preconditions.
        // Act.This action performs the actual test.
        //Assert. This final action checks if the expected result has occurred.
        /*Mocking frameworks are used to generate replacement objects like Stubs and Mocks.
         Mocking frameworks complement unit testing frameworks by isolating dependencies 
         but are not substitutes for unit testing frameworks.*/

        //getby id
        [Fact]
        public void GetByIdNote()
        {
            var note = new Note()
            {
                ID = 1,
                Title = "Test",
                Description = "Test",
                UserID = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            Mock<INoteRepository> mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(m => m.GetNoteById(1)).Returns(note);
            var result = mockRepo.Object.GetNoteById(1);
            Assert.Equal(1, result.ID);
        }


        [Fact]
        public void CreateNote()
        {
            var note = new Note()
            {
                ID = 1,
                Title = "Test",
                Description = "Test",
                UserID = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            Mock<INoteRepository> mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(m => m.GetNoteById(1)).Returns(note);

            //create object
            mockRepo.Object.Create(note);

            var result = mockRepo.Object.GetNoteById(1);
            Assert.Equal(1, result.ID);
        }

        //get all
        [Fact]
        public void GetAll()
        {
            // Arrange
            var noteObject = new Note() { ID = 1 };
            var context = new Mock<DbContext>();
            Mock<INoteRepository> mockRepo = new Mock<INoteRepository>();
            var testList = new PagedList<Note>(new List<Note> { noteObject }, 1, 1, 1);

            var dbSetMock = new Mock<DbSet<Note>>();
            dbSetMock.As<IQueryable<Note>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Note>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Note>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Note>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            
            
            context.Setup(x => x.Set<Note>()).Returns(dbSetMock.Object);

            // Act
            mockRepo.Setup(m => m.GetAllNotes(new Entities.Helper.PagesParameter
            {
                PageNumber = 1,
                PageSize = 1
            })).Returns(testList);

            Assert.Equal(1, testList[0].ID);
        }
    }
}