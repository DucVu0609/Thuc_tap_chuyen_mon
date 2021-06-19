namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("movies")]
    public partial class movies
    {
        [Key]
        public int ID { get; set; }
        public int catid { get; set; }

        public string name { get; set; }
        public string slug { get; set; }
        public string img { get; set; }

        public string detail { get; set; }

        public string directors { get; set; }
        public double price { get; set; }
        public int time { get; set; }
        public string LinkTrailer { get; set; }
        [DisplayFormat(DataFormatString = "{dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public int status { get; set; }
    }
}
