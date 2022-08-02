using Microsoft.EntityFrameworkCore;

namespace EcomenderApi.Data
{
public class DataContext: DbContext
{
    public DataContext (DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<TodoItem> TodoList {get;set;}
    public DbSet<TodoItem> Aux {get;set;}
}

}