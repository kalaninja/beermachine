"use strict";

Vue.component('cart-header',{
    props: ['count'],
    template: '<h4 class="d-flex justify-content-between align-items-center mb-3">\n' +
        '            <span>Ваша корзина</span>\n' +
        '            <span class="badge badge-secondary badge-pill">{{ count }}</span>\n' +
        '        </h4>'
});

Vue.component('cart-empty',{
    props:['isEmpty'],
    template: '<div>ваша корзина пуста</div>'
});

Vue.component('cart-product', {
    props: ['item'],
    template:'<li class="list-group-item d-flex justify-content-between lh-condensed">\n' +
        '                <div>\n' +
        '                    <h6 class="my-0">{{ item.name }}</h6>\n' +
        '                    <small class="text-muted">x {{ item.count }}</small>\n' +
        '                </div>\n' +
        '                <span class="text-muted">{{ item.price}} ₽</span>\n' +
        '</li>'
});

Vue.component('cart-total', {
    props: ['total','isEmpty'],
    template:'<li class="list-group-item d-flex justify-content-between">'+
        '<span>Итого (RUB)</span>\n'+
        '<strong>{{total}}.00 ₽</strong>\n' +
        '</li>'
});

var app = new Vue({
    el: '#cart',
    data: {
        shoppingCart: {
            items:[],
            total: 0,
            isEmpty: true
        }
    }
});

var connection = new signalR.HubConnectionBuilder().withUrl("/cartHub").build();

connection.on("UpdateShoppingCart", function (shoppingCart) {
    shoppingCart.isEmpty = (!shoppingCart.items || shoppingCart.items === 0);
    shoppingCart.count = shoppingCart.isEmpty ? 0 : shoppingCart.items.length;
    
    app.$data.shoppingCart = shoppingCart
});

connection.start();
