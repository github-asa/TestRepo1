using System.Collections.Generic;

namespace J2BIOverseasOps.Models
{
    public class CategoryApi
    {
        public class Category
        {
            public Identifier CategoryId { get; set; }

            public string Name { get; set; }

            public bool IsParent { get; set; }

            public List<Category> Children { get; set; }
        }

        public class Identifier
        {
            public string Id { get; set; }
        }
    }
}