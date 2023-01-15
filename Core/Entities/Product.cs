using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Rol { get; set; }
        public string ProductName { get; set; }
    }
}
