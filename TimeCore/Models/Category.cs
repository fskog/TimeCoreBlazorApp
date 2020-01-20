using System;

namespace TimeCore.Models
{
    public class Category
    {
        public Guid SystemId { get; private set; }
        public string Name { get; private set; }

        public Category(string name = "New category")
        {
            Name = name;
            SystemId = Guid.NewGuid();
        }

        public void SetName(string name)
        {
            Name = name;
        }

    }
}