using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller 
    {
        //ITodoItemServices interface 
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;

         public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //If there is a logged-in user, the User property contains a lightweight object with some (but not all) of the user's information. The UserManager uses this to look up the full user details in the database via the GetUserAsync() method.

            //The value of currentUser should never be null, because the [Authorize] attribute is present on the controller. However, it's a good idea to do a sanity check, just in case. You can use the Challenge() method to force the user to log in again if their information is missing ==>
            if (currentUser == null) return Challenge();

            //Get todo from the database
            var items  = await _todoItemService.GetIncompleteItemsAsync(currentUser);

            //put items into model
             var model = new TodoViewModel()
             {
                 Items = items
             };
    
            //Render view using the model
            return View(model); 
 
            //or
            // return View(new TodoViewModel() { Items = items});

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var successful = await _todoItemService.AddItemAsync(newItem, currentUser);
            if (!successful)
            {
                return BadRequest("could not add item");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
            if(!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}