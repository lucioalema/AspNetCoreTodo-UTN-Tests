using System;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Moq;
using Xunit;

namespace AspNetCoreTodo.Tests
{
    public abstract class BaseTodoControllerTests
    {
        protected readonly TodoItem[] Items;
        protected readonly Mock<ITodoItemService> MockService;
        protected readonly TodoController ControllerUnderTest;

        protected BaseTodoControllerTests(TodoItem[] items)
        {
            Items = items;
            MockService = new Mock<ITodoItemService>();
            MockService
                .Setup(svc => svc.GetIncompleteItemsAsync())
                .ReturnsAsync(Items);
            ControllerUnderTest = new TodoController(MockService.Object);
        }
    }
}
