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
    
    connection.onclose(function(){
        console.log('connection closed try to reconnect');
        var interval = setInterval(function(){
            connection.start().then(
                function(){
                    console.log('reconnect success!');
                    clearInterval(interval);
                },
                function(){
                    console.log('reconnect failed')
                });
        },2000);
        
        
    });

    connection.start();

})();



