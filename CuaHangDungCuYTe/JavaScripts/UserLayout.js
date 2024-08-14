let navbar = document.querySelector('.navbar');
let searchForm = document.querySelector('.search-form');
let userMenu = document.querySelector('.user-menu');
let cartItem = document.querySelector('.cart-items-container');
let loginForm = document.querySelector('#login-btn');
let closeLogin = document.querySelector('.close-login');
let registerForm = document.querySelector('#register-btn');
let closeRegister = document.querySelector('.close-login1');
let registerForm1 = document.querySelector('#register');
<<<<<<< HEAD
let closepay = document.querySelector('.pay-close');
=======
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf

document.querySelector('#menu-btn').onclick = () => {
    navbar.classList.toggle('active');
    searchForm.classList.remove('active');
    cartItem.classList.remove('active');
    userMenu.classList.remove('active');
};

document.querySelector('#search-btn').onclick = () => {
    searchForm.classList.toggle('active');
    navbar.classList.remove('active');
    cartItem.classList.remove('active');
    userMenu.classList.remove('active');
};

document.querySelector('#user-btn').onclick = () => {
    searchForm.classList.remove('active');
    navbar.classList.remove('active');
    cartItem.classList.remove('active');
    userMenu.classList.toggle('active');
};

document.querySelector('#cart-btn').onclick = toggleCart;

function toggleCart() {
    cartItem.classList.toggle('active');
    navbar.classList.remove('active');
    searchForm.classList.remove('active');
    userMenu.classList.remove('active');
}

window.onscroll = () => {
    navbar.classList.remove('active');
    searchForm.classList.remove('active');
    cartItem.classList.remove('active');
    userMenu.classList.remove('active');
};

$(document).ready(function () {
    // Hàm xử lý sự kiện click cho nút checkout
    function bindCheckoutEvent() {
        $('#checkOrder').off('click').on('click', function () {
            $.ajax({
                url: '/User/CheckOrders',
                type: 'GET',
                success: function (result) {
                    if (result.success) {
                        alert(result.message);
                        pay(); // Gọi hàm pay() khi đăng nhập thành công
                    } else {
                        alert(result.message);
                    }
                },
                error: function (xhr, status, error) {
                    alert('Error: ' + error);
                }
            });
        });
    }

    //// Hàm pay() mở form điền thông tin đặt hàng
    //function pay() {
    //    document.querySelector('.pay-box').style.display = "flex";
    //}

    //// Hàm xử lý sự kiện click cho nút close form điền thông tin
    //$(".pay-close").click(function () {
    //    $(".pay-box").css("display", "none");
    //});

    // Hàm xử lý sự kiện click cho nút add-to-cart
    $('.add-to-cart').click(function () {
        var productId = $(this).data('product-id');
        $.ajax({
            url: '/User/Cart',
            type: 'GET',
            data: { productId: productId },
            success: function (result) {
                if (result.success) {
                    alert(result.message);
                    updateCart();
                } else {
                    alert(result.message);
                }
            },
            error: function (xhr, status, error) {
                alert('Error: ' + error);
            }
        });
    });

    // Hàm cập nhật giỏ hàng
    function updateCart() {
        $.ajax({
            url: '/User/DisplayCart',
            type: 'GET',
            success: function (data) {
                $('#cart-content').html(data);
                bindCheckoutEvent(); // Gắn lại sự kiện cho nút checkout
            },
            error: function (xhr, status, error) {
                alert('Error updating cart: ' + error);
            }
        });
    }

    // Hàm xử lý sự kiện click cho nút xóa sản phẩm
    $(document).on('click', '.xoa-cartitem', function () {
        var productId = $(this).data('product-id');
        $.ajax({
            url: '/User/RemoveCartItem',
            type: 'GET',
            data: { productId: productId },
            success: function (result) {
                if (result.success) {
                    alert(result.message);
                    updateCart();
                } else {
                    alert(result.message);
                }
            },
            error: function (xhr, status, error) {
                alert('Error: ' + error);
            }
        });
    });

    // Gắn sự kiện khi trang được tải lần đầu
    bindCheckoutEvent();
});


function rebindCartButton() {
    document.querySelector('#cart-btn').onclick = toggleCart;
}

function loginFormDisplay() {
    document.querySelector('.wrapper').style.display = "flex";
}

function registerFormDisplay() {
    document.querySelector('.wrapper1').style.display = "flex";
}

loginForm.onclick = loginFormDisplay;
registerForm.onclick = registerFormDisplay;

function registerFormDisplay1() {
    closeLoginForm();
    registerFormDisplay();
}
registerForm1.onclick = registerFormDisplay;

function closeLoginForm() {
    document.querySelector('.wrapper').style.display = "none";
}

function closeRegisterForm() {
    document.querySelector('.wrapper1').style.display = "none";
}

closeLogin.onclick = closeLoginForm;
<<<<<<< HEAD
closeRegister.onclick = closeRegisterForm;

// Đóng/Mở thông tin đặt hàng
var payaddress = {};

function pay() {
    //Mở và hiển thị thông tin lên form
    document.querySelector('.pay-box').style.display = "flex";
}

function orderInfo(email, fullname, address, sdt) {
    payaddress = {
        email: email,
        fullname: fullname,
        address: address,
        sdt: sdt
    };
    document.getElementById('info-email').value = payaddress.email;
    document.getElementById('info-name').value = payaddress.fullname;
    document.getElementById('info-address').value = payaddress.address;
    document.getElementById('info-numbers').value = payaddress.sdt;
}

//Đóng form địa chỉ
closepay.addEventListener('click', function () {
    document.querySelector('.pay-box').style.display = "none";
});
=======
closeRegister.onclick = closeRegisterForm;
>>>>>>> 7ad3da394e5fb84f81242f84ff98bf1476fbeabf
