"use strict";

(function(){
    var timerId = setInterval(function() {
        updateDashboard();

    }, 5000);

    Vue.component('customer-row', {
        props: ['customer','index'],
        template:'<tr>\n'+
            '<td>{{index+1}}</td>\n'+
            '<td>{{customer.name}}</td>\n'+
            '<td>{{customer.balance}}</td>\n'+
            '</tr>'
    });

    var app = new Vue({
        el: '#root',
        data: {
            dashboardData: {
                topSavers:[],
                topSpenders: []
            }
        }
    });

    function updateDashboard(){
        var req = new XMLHttpRequest();
        req.open('GET', '/dashboard/data', true);
        req.onreadystatechange = function() { // (3)
            if (req.readyState != 4) {
                return;
            }

            if (req.status != 200) {
                console.log(req.status + ': ' + req.statusText);
            } else {
                console.log(req.responseText);
                app.$data.dashboardData = JSON.parse(req.responseText);

                updateCharts();
            }
        };
        req.send();
    }
    
    function updateCharts(){
    }
})();

