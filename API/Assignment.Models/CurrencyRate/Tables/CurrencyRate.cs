using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.CurrencyRate.Tables
{
    [Table("CurrencyRate")]
    public class CurrencyRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CurrencyDescription { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }

        [Required]
        [Column(TypeName = "timestamp")]
        [DefaultValue("now()")]
        public DateTime EffectiveDate { get; set; }
    }
}