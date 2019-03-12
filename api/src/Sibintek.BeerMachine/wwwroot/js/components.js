"use strict";

Vue.component('customer-row', {
    props: ['customer', 'index'],
    template: '<tr>\n' +
        '<td>{{index+1}}</td>\n' +
        '<td>{{customer.name}}</td>\n' +
        '<td>{{customer.balance}}</td>\n' +
        '</tr>'
});

Vue.component('buyer-row', {
    props: ['customer', 'index'],
    template: '<tr>\n' +
        '<td>{{index+1}}</td>\n' +
        '<td>{{customer.name}}</td>\n' +
        '<td>{{customer.spent}}</td>\n' +
        '</tr>'
});

Vue.component('cart-header', {
    props: ['count'],
    template: '<h4 class="d-flex justify-content-between align-items-center mb-3">\n' +
        '            <span>Ваша корзина</span>\n' +
        '            <span class="badge badge-secondary badge-pill">{{ count }}</span>\n' +
        '        </h4>'
});

Vue.component('cart-empty', {
    props: ['isEmpty'],
    template: '<div>ваша корзина пуста</div>'
});

Vue.component('cart-empty-dashboard', {
    props: ['isEmpty'],
    template: '<div class="empty-shopping-cart align-self-center">' +
        '<img alt="" class="empty-cart-img" src="/img/empty_cart.png">'+
        '</div>'
});

Vue.component('cart-product', {
    props: ['item'],
    template: '<li class="list-group-item d-flex justify-content-between lh-condensed">\n' +
        '                <div>\n' +
        '                    <h6 class="my-0">{{ item.name }}</h6>\n' +
        '                    <small class="text-muted">x {{ item.count }}</small>\n' +
        '                </div>\n' +
        '                <h6 class="text-muted">{{ item.price}} sib</h6>\n' +
        '</li>'
});

Vue.component('purchase', {
    porps: ['purchaseResult'],
    template: '<table>' +
        '<tr>' +
        '<th>Товар</th>' +
        '<th>Количество</th>' +
        '<th>Стоимость, sibcoin</th>' +
        '</tr>' +
        '<tr is="purchase-product" v-for="item in purchaseResult.shoppingCart.items"></tr>' +
        '</table>' +
        '<div> Баланс кошелька {{purchaseResult.walletBalance}}</div>'
});

Vue.component('purchase-product', {
    props: ['item'],
    template: '<tr>' +
        '<td>{{item.name}}</td>' +
        '<td>x{{item.count}}</td>' +
        '<td>{{item.price}}</td>' +
        '</tr>'
});

Vue.component('cart-total', {
    props: ['total', 'isEmpty'],
    template: '<li class="list-group-item d-flex justify-content-between">' +
        '<h6>Итого</h6>\n' +
        '<h6>{{total}} sib</h6>\n' +
        '</li>'
});

Vue.component('transaction-row', {
    props: ['transaction'],
    template: '<tr>\n' +
        '<td>{{transaction.block}}</td>\n' +
        '<td>{{transaction.hash}}</td>\n' +
        '<td>{{transaction.walletId}}</td>\n' +
        '<td>{{transaction.type === 0 ? "начисление" : "списание"}}</td>\n' +
        '<td>{{transaction.sum}}</td>\n' +
        '</tr>'
});