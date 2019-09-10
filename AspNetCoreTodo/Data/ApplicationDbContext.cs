using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        //A DbSet represents a table or collection in the database. By creating a DbSet<TodoItem> property called Items, you're telling Entity Framework Core that you want to store TodoItem entities in a table called Items.
        public DbSet<TodoItem> Items { get; set; }
    }
}
