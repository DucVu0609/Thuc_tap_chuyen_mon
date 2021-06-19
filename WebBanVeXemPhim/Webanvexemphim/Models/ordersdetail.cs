namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ordersdetail")]
    public partial class ordersdetail
    {
        [Key]
        public int ID { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string time { get; set; }
        public DateTime day { get; set; }
        public int priceSale { get; set; }
        public double amount { get; set; }
    }
}
