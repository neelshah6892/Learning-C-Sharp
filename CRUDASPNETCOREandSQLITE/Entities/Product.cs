using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDASPNETCOREandSQLITE.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        public double price { get; set; }

        public int quantity { get; set; }

    }
}
