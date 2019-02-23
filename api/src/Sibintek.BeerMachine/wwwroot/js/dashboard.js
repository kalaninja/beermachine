"use strict";

(function () {
    var timerId = setInterval(function () {
        updateDashboard();

    }, 5000);

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
                app.$data.dashboardData = JSON.parse(req.responseText);

                updateCharts();
            }
        };
        req.send();
    }

    function updateCharts() {
    }
})();

