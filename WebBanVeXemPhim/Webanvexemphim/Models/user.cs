namespace Webanvexemphim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string fullname { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(225)]
        public string username { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string password { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string email { get; set; }

        [StringLength(5)]
        public string gender { get; set; }

        public string address { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(100)]
        public string img { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int access { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int created_by { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int updated_by { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status { get; set; }
    }
}
