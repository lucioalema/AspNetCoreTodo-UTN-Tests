using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService: ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;

            _context.Items.Add(newItem);

            await _context.SaveChangesAsync();            
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.Items
                .Where(x => !x.IsDone)
                .ToArrayAsync();
        }

        public async Task MarkDoneAsync(Guid id)
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return;
            item.IsDone = true;

            await _context.SaveChangesAsync();
        }
    }
}
