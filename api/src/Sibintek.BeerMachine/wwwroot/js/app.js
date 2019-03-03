"use strict";

var app = new Vue({
    el: '#root',
    data: {
        dashboardData: {
            topSavers: [],
            topSpenders: []
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