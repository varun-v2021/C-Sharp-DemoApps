using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExpressionDemo
{
    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        static int count = 1;
        public Category()
        {
            CategoryId = count++;
        }
    }
}
