using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuaHangDungCuYTe.Areas.Admin.AdminViewModels
{
    public class HangHoaVM
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Maloai { get; set; }

        public string ProductImage { get; set; }

        public int? Soluongton { get; set; }

        public double? Price { get; set; }
        public string Mota {  get; set; }
    }
}