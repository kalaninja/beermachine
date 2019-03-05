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
        
        if(purchaseResult.status === 0){
            app.$data.successPurchase = purchaseResult
        }
        
        if(purchaseResult.exceptionText){
            console.log("Ошибка api");
            console.log(purchaseResult);
        }
    });

    connection.start();

})();



