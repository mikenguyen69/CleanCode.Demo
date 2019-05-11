using CleanCode.Demo.Core.Entities;
using System;
using System.Data.Entity;

namespace CleanCode.Demo.Infrastructure.Data
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<ToDoItem> ToDoItems { get; }
        int SaveChanges();
        void MarkAsModified<T>(T item);
        void MarkAsDeleted<T>(T item);
    }
}
