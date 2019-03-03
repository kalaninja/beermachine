"use strict";

(function(){
    var connection = new signalR.HubConnectionBuilder().withUrl("/cartHub").build();

    connection.on("UpdatePurchaseResult", function (purchaseResult) {
        if(purchaseResult.shoppingCart){
            purchaseResult.shoppingCart.isEmpty = (!purchaseResult.shoppingCart.items || purchaseResult.shoppingCart.items === 0);
            purchaseResult.shoppingCart.count = purchaseResult.shoppingCart.isEmpty 
                ? 0 
                : purchaseResult.shoppingCart.items.length;
        }
        
        app.$data.purchaseResult = purchaseResult;
        
        if(purchaseResult.success){
            app.$data.successPurchase = purchaseResult
        }
    });

    connection.start();

})();



