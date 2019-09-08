using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;


//this class contained hard-coded to-do items  ===>  Now that the database context exist, we can now create a new service class that will use Entity-Framework-core to get real items from the database

//This will be done by disabling the injected-dependency Service in the ConfigureService(startup.cs) or deleting this class file

namespace AspNetCoreTodo.Services
{

    //FakeTodoItemServices is implementing ITodoitemServices interface
    // public class FakeTodoItemService : ITodoItemService
    // {
    //     public Task<TodoItem[]> GetIncompleteItemsAsync()
    //     {
    //         var item1 = new TodoItem
    //         {
    //             Title = "Learn ASP.NET Core",
    //             DueAt = DateTimeOffset.Now.AddDays(1),
    //         };

    //         var item2 = new TodoItem
    //         {
    //             Title = "Build awesome apps",
    //             DueAt = DateTimeOffset.Now.AddDays(2),
    //         };

    //         return Task.FromResult(new[] { item1, item2 });
    //     }
    // }
}

