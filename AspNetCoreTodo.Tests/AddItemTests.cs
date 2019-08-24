using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class AddItemTests : BaseTodoControllerTests
    {
        private static readonly TodoItem FirstItem = new TodoItem() { Id = Guid.NewGuid(), Title = "First Item" };
        private static readonly TodoItem SecondItem = new TodoItem() { Id = Guid.NewGuid(), Title = "Second Item" };

        public AddItemTests() 
            : base(new TodoItem[] { FirstItem, SecondItem })
        {
        }

        [Fact]
        public async Task AddItemPostShouldReturnBadRequestIfModelIsInvalid()
        {
            var model = new TodoItem();
            ControllerUnderTest.ModelState.AddModelError("error", "testerror");

            var result = await ControllerUnderTest.AddItem(model);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task AddItemPostShouldReturnRedirectToActionIndexIfModelIsValid()
        {
            var model = new TodoItem()
            {
                Id = FirstItem.Id,
                Title = FirstItem.Title
            };

            var result = await ControllerUnderTest.AddItem(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(TodoController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task AddItemPostShouldCallAddItemAsyncOnceIfModelIsValid()
        {
            //Agrego datos al ViewModel
            var model = new TodoItem() {
                Title = FirstItem.Title
            };

            await ControllerUnderTest.AddItem(model);

            MockService.Verify(mock => mock.AddItemAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task AddItemPostShouldCallAddItemAsyncWithCorrectParameterIfModelIsValid()
        {
            var item = new TodoItem() {
                Title = FirstItem.Title
            };
            var model = new TodoItem() { Title = item.Title };

            await ControllerUnderTest.AddItem(model);

            MockService.Verify(mock => 
                mock.AddItemAsync(It.Is<TodoItem>(i => 
                    i.Title.Equals(item.Title))), Times.Once);
        }
    }
}