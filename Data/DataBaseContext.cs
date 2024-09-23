using api.Models;
using api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataBaseContext : DbContext
    {
        //Inserir Todos os Models
        public DbSet<LoginViewModel> Login { get; set; }

        string connectionString = Configuration.ConnectionString;
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseSqlServer(connectionString);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Inserir todos os Maps para criar as tabelas através do Migration
            modelBuilder.Entity<LoginViewModel>().HasNoKey();
        }
    }
}
