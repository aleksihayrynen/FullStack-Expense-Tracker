window.renderExpenseChart = (elementId, chartData) => {
    Highcharts.chart('container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Ferry passengers by vehicle type 2024',
            align: 'left'
        },
        xAxis: {
            categories: [
                'January', 'February', 'March', 'April', 'May'
            ]
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            }
        },
        legend: {
            reversed: true
        },
        plotOptions: {
            series: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true
                }
            }
        },
        series: [{
            name: 'Motorcycles',
            data: [74, 27, 52, 93, 1272]
        };
    }
}
