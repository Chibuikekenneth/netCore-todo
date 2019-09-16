using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetCoreTodo.UnitTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            //Set up a context (connection to the "DB") for writing
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);

                var fakeUser = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@example.com"
                };

                await service.AddItemAsync(new TodoItem {Title = "Testing?"}, fakeUser);
            }

            //use a seperate context to read data back from the "DB"
              using (var inMemoryContext = new ApplicationDbContext(options))
            {
                var itemsIndatabase = await inMemoryContext.Items.CountAsync();
                Assert.Equal(1, itemsIndatabase);
                
                var item = await inMemoryContext.Items.FirstAsync();
                Assert.Equal("fake-000", item.UserId);
                Assert.Equal("Testing?", item.Title);
                Assert.Equal(false, item.IsDone);
                var difference = DateTimeOffset.Now.AddDays(3) - item.DueAt;
                Assert.True(difference < TimeSpan.FromSeconds(2));
            }
            
        }
    }
}



//As an extra challenge, try writing unit tests that ensure: The MarkDoneAsync() method returns false if it's passed an ID that doesn't exist
//The MarkDoneAsync() method returns true when it makes a valid item as complete
//The GetIncompleteItemsAsync() method returns only the items owned by a particular user