namespace CleanCode.Demo.WebAPI.Migrations
{
    using CleanCode.Demo.Core.Entities;
    using CleanCode.Demo.WebAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoItemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoItemContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.ToDoItems.AddOrUpdate(x => x.Id,
                new ToDoItem() { Id = 1, Title = "Clean the cloth", Description = "All the cloth must be watched", IsDone = false },
                new ToDoItem() { Id = 1, Title = "Clean the diskes", Description = "All the disk must be watched", IsDone = false }
            );
        }
    }
}
