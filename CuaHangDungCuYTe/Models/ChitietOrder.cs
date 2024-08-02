namespace CuaHangDungCuYTe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChitietOrder
    {
        [Key]
        [StringLength(100)]
        public string chiTietId { get; set; }

        [Required]
        [StringLength(100)]
        public string orderId { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public double? tongGia { get; set; }

        public virtual Account Account { get; set; }

        public virtual Order Order { get; set; }
    }
}
