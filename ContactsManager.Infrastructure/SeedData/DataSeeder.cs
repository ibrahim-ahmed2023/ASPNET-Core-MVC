using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactsManager.Core.Domain;
using ContactsManager.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Infrastructure.SeedData
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate(); 

            if (!dbContext.countries.Any())
            {
                var countriesJson = File.ReadAllText("countries.json");
                var countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);
                dbContext.countries.AddRange(countries!);
                dbContext.SaveChanges();
            }

            if (!dbContext.Persons.Any())
            {
                var personsJson = File.ReadAllText("persons.json");
                var persons = JsonSerializer.Deserialize<List<Person>>(personsJson);
                dbContext.Persons.AddRange(persons!);
                dbContext.SaveChanges();
            }
        }
    }
}