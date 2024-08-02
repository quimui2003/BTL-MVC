namespace CuaHangDungCuYTe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            ChitietOrders = new HashSet<ChitietOrder>();
        }

        [Key]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string passWord { get; set; }

        [StringLength(200)]
        public string fullName { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }

        [StringLength(10)]
        public string role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChitietOrder> ChitietOrders { get; set; }
    }
}
