using Microsoft.EntityFrameworkCore;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.DAL.DBContext
{
    public class ProjectDBContext : DbContext
    {
        public virtual DbSet<Recipe> recipes { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
