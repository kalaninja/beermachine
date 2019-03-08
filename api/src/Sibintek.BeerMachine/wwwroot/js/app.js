"use strict";

var app = new Vue({
    el: '#root',
    data: {
        dashboardData: {
            topRich: [],
            topBuyers: [],
            reportDateDisplayString: "09.03.2018 00:00:00"
        },
        purchaseResult:{
            success:true,
            shoppingCart:{
                items:[],
                total: 0,
                isEmpty: true
            },
            customer:""
        },
        successPurchase:{
            success:true,
            customer:"",
            shoppingCart:{
                items:[],
                total: 0,
                isEmpty: true
            }
        }
    }
});