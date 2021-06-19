namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int userid { get; set; }

        public DateTime created_ate { get; set; }

        public DateTime? exportdate { get; set; }

        public string deliveryaddress { get; set; }

        public string deliveryname { get; set; }

        public string deliveryphone { get; set; }

        public string deliveryemail { get; set; }

        public double sum { get; set; }

        public int status { get; set; }
    }
}
