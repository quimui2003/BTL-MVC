let navbar = document.querySelector('.navbar');
let searchForm = document.querySelector('.search-form');
let userMenu = document.querySelector('.user-menu');
let cartItem = document.querySelector('.cart-items-container');

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
});

function updateCart() {
    $.ajax({
        url: '/User/DisplayCart',
        type: 'GET',
        success: function (data) {
            $('#cart-content').html(data);
            rebindCartButton(); // Re-bind cart button after updating the cart
        },
        error: function (xhr, status, error) {
            alert('Error updating cart: ' + error);
        }
    });
}

function rebindCartButton() {
    document.querySelector('#cart-btn').onclick = toggleCart;
}
