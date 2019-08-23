using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();
        Task AddItemAsync(TodoItem newItem);
        Task MarkDoneAsync(Guid id);
    }
}