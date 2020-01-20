using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TimeCore.Models;


namespace TimeCore.Services
{
    public class CategoryService
    {
        private List<Category> Categories;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ILogger<CategoryService> logger)
        {
            _logger = logger;
            Categories = new List<Category>();
            LoadCategories();
        }

        public List<Category> GetAll() => Categories ?? new List<Category>();

        public Category Get(string name) => Categories.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public Category Get(Guid systemId) => Categories.FirstOrDefault(x => x.SystemId.Equals(systemId));

        public void Add(string name)
        {
            var newCategory = new Category(name);
            _logger.LogInformation($"Adding new category: {JsonSerializer.Serialize(newCategory)}");
            Categories.Add(newCategory);
        }

        public void Remove(Guid systemId)
        {
            _logger.LogInformation($"Removing category with id {systemId}");
            Categories.RemoveAll(x => x.SystemId.Equals(systemId));
        }

        private void LoadCategories()
        {
            Categories = new List<Category>();

            string[] MockCategories = { "Category 1", "Category 2", "Category 3" };

            Categories.AddRange(MockCategories.Select(x => new Category(x)));

            _logger.LogInformation($"Loaded categories: {string.Join(",",Categories.Select(x => $"{x.Name} [{x.SystemId}]"))}");
        }


    }
}
