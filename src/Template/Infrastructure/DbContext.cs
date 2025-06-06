using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Template.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Template.Infrastructure
{
    public class TemplateDbContextFactory : IDesignTimeDbContextFactory<TemplateDbContext>
    {
        public TemplateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TemplateDbContext>();

            // La tua connection string MySQL
            var connectionString = "Server=localhost;Port=3306;Database=smartwork;User=root;Password=root;";

            // Versione del server MySQL (modifica in base alla tua versione)
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33)); 

            optionsBuilder.UseMySql(connectionString, serverVersion);

            return new TemplateDbContext(optionsBuilder.Options);
        }
    }
}