using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Helpers;
using CuaHangDungCuYTe.Models;

namespace CuaHangDungCuYTe.Areas.Admin.AdminViewModels
{
    public class HoaDonVM
    {
        [DisplayName("Mã hóa đơn")]
        public string OrderId { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Ngày đặt hàng")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("Tổng tiền")]
        public double? TotalPrice { get; set; }
        [DisplayName("Trạng thái")]
        public string status { get; set; }
    }
}