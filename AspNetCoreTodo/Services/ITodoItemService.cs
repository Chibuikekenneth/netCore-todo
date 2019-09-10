using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Controllers;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    //activated ITodoItemService interface to use Any class (implementation type) in the startup.cs ConfigureServices method
    // services.AddSingleton<ITodoItemService, FakeTodoItemService>();
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
    }
}