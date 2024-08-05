using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuaHangDungCuYTe.UserViewModels
{
    public class LoginViewModels
    {
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(50)]
        public string passWord { get; set; }

        [StringLength(50)]
        public string passWord2 { get; set; }

        [StringLength(200)]
        public string fullName { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }

        [StringLength(10)]
        public string role { get; set; }
    }
}