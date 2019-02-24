"use strict";

(function () {
    var timerId = setInterval(function () {
        updateDashboard();

    }, 5000);

    //init pie charts
    var firstChartData = {
        datasets: [{
            data: [100, 20],
            backgroundColor: [
                'rgb(255, 237, 157)',
                'rgb(105, 133, 175)'
            ],
            borderWidth: 0
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: [
            'Свободные баллы',
            'Заработанные баллы'
        ]
    };
    
    var secondChartData ={
        datasets: [{
            data: [100, 20],
            backgroundColor: [
                'rgb(255, 237, 157)',
                'rgb(105, 133, 175)'
            ],
            borderWidth: 0
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: [
            'Потрачено',
            'Накоплено'
        ]
    };
    
    var chartOptions = {
        responsive: true,
        aspectRatio:1.5,
        legend:{
            position:"bottom",
            fullWidth: false,
            labels:{
                boxWidth: 20
            }
        }
    };
    
    var firstChartCtx = document.getElementById("firstChart");
    var firstChart = new Chart(firstChartCtx, {
        type: 'doughnut',
        data: firstChartData,
        options: chartOptions
    });
    
    var secondChartCtx = document.getElementById("secondChart");
    var secondChart = new Chart(secondChartCtx, {
        type: 'doughnut',
        data: secondChartData,
        options: chartOptions
    });

    function updateDashboard() {
        var req = new XMLHttpRequest();
        req.open('GET', '/dashboard/data', true);
        req.onreadystatechange = function () {
            if (req.readyState != 4) {
                return;
            }

            if (req.status != 200) {
                console.log(req.status + ': ' + req.statusText);
            } else {
                var dashboardData = JSON.parse(req.responseText);
                app.$data.dashboardData = dashboardData;

                updateCharts(dashboardData);
            }
        };
        req.send();
    }

    function updateCharts(dashboardData) {
        firstChart.data.datasets[0].data = dashboardData.earnedLostDataSet;
        firstChart.update();
        
        secondChart.data.datasets[0].data = dashboardData.spendAccumulateDataSet;
        secondChart.update();
    }
})();

