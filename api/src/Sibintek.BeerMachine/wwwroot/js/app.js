"use strict";

var app = new Vue({
    el: '#root',
    data: {
        dashboardData: {
            topSavers: [],
            topSpenders: []
        },
        shoppingCart:{
            items:[],
            total: 0,
            isEmpty: true
        }
    }
});