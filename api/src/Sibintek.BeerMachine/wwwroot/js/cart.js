"use strict";

(function(){
    var connection = new signalR.HubConnectionBuilder().withUrl("/cartHub").build();

    connection.on("UpdateShoppingCart", function (shoppingCart) {
        shoppingCart.isEmpty = (!shoppingCart.items || shoppingCart.items === 0);
        shoppingCart.count = shoppingCart.isEmpty ? 0 : shoppingCart.items.length;

        app.$data.shoppingCart = shoppingCart
    });

    connection.start();

})();



