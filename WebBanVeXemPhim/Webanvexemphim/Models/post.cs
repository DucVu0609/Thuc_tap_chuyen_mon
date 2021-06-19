namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("post")]
    public partial class post
    {
        [Key]
      
        public int ID { get; set; }

        public int? topid { get; set; }
        public string title { get; set; }
        public string slug { get; set; }

        public string detail { get; set; }
        public string img { get; set; }
        public string descriptionShort { get; set; }
        
        public string type { get; set; }

        public string metakey { get; set; }

        public string metadesc { get; set; }
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public DateTime updated_at { get; set; }
        public int updated_by { get; set; }
        public int status { get; set; }
    }
}
