namespace CuaHangDungCuYTe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int productId { get; set; }

        [StringLength(100)]
        public string productName { get; set; }

        public int maLoai { get; set; }

        [StringLength(255)]
        public string productImage { get; set; }

        public int? soLuong { get; set; }

        public double? donGia { get; set; }

        [Column(TypeName = "text")]
        public string moTa { get; set; }

        public virtual Loai Loai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
