namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("category")]
    public partial class category
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string slug { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int parentid { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orders { get; set; }

        [StringLength(150)]
        public string metakey { get; set; }

        [StringLength(150)]
        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status { get; set; }
    }
}
