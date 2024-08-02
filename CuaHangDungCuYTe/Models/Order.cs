namespace CuaHangDungCuYTe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            ChitietOrders = new HashSet<ChitietOrder>();
        }

        [StringLength(100)]
        public string orderId { get; set; }

        public int productId { get; set; }

        public int? soLuongBan { get; set; }

        public double? giaBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChitietOrder> ChitietOrders { get; set; }

        public virtual Product Product { get; set; }
    }
}
