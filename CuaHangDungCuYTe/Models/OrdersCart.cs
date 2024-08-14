namespace CuaHangDungCuYTe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdersCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CartId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public int? Soluong { get; set; }

        public virtual Account Account { get; set; }

        public virtual Product Product { get; set; }
    }
}
