using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller 
    {
        //ITodoItemServices interface 
        private readonly ITodoItemService _todoItemServices;

         public TodoController(ITodoItemService todoItemService)
        {
            _todoItemServices = todoItemService;
        }
        public async Task<IActionResult> index()
        {
            //Get todo from the database
            var items  = await _todoItemServices.GetIncompleteItemsAsync();

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

            var successful = await _todoItemServices.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest(new { error = "could not add item"});
            }

            return RedirectToAction("Index");
        }
    }
}