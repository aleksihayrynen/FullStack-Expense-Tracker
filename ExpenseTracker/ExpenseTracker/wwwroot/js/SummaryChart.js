window.renderHighchartsSummaryChart = (containerId, chartData, isMonthly) => {
    const categories = chartData.map(d => d.label);
    const expenses = chartData.map(d => d.expense);
    const incomes = chartData.map(d => d.income);

    Highcharts.chart(containerId, {
        chart: {
            type: 'column'
        },
        title: {
            text: isMonthly ? 'Daily Overview' : 'Monthly Overview'
        },
        legend: {
            enabled: false
        },
        credits: {
            enabled: false
        },
        xAxis: {
            categories: categories,
            title: {
                text: isMonthly ? 'Day of Month' : 'Month'
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Amount'
            }
        },
        tooltip: {
            shared: true,
            valuePrefix: '$'
        },
        plotOptions: {
            column: {
                grouping: true,
                shadow: false
            }
        },
        series: [
            {
                name: 'Expenses',
                data: expenses,
                color: '#e74c3c'
            },
            {
                name: 'Income',
                data: incomes,
                color: '#2ecc71'
            }
        ]
    });
};