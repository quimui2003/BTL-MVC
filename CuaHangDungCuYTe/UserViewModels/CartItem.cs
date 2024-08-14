using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CuaHangDungCuYTe.Models;

namespace CuaHangDungCuYTe.UserViewModels
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int? Quantity { get; set; }
    }
}