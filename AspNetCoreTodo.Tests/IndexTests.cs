using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspNetCoreTodo.Tests
{
    public class IndexTests : BaseTodoControllerTests
    {
        private static readonly TodoItem FirstItem = new TodoItem()
        {
            Id = Guid.NewGuid(),
            Title = "First Item",
            IsDone = false
        };
        private static readonly TodoItem SecondItem = new TodoItem()
        {
            Id = Guid.NewGuid(),
            Title = "Second Item",
            IsDone = false
        };
        public IndexTests()
            : base(new TodoItem[] { FirstItem, SecondItem })
        {
        }

        [Fact]
        public async Task IndexGetViewModelShouldBeOfTypeTodoViewModel()
        {
            //Act
            var result = await ControllerUnderTest.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsAssignableFrom<TodoViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task IndexGetShouldReturnListOfToDoItems()
        {
            var result = await ControllerUnderTest.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<TodoViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Items.Length);
        }
    }
}