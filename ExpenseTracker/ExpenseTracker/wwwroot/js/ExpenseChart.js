window.renderExpenseChart = (elementId, chartData) => {

    let formattedChartData = chartData.map(item => ({
        name: item.category,
        y: item.amount
    }));

    if (formattedChartData.length === 0) {
        console.log("Jippii");
        formattedChartData = [{
            name: 'Data not found ;(',
            y: 404,
            color: '#e0e0e0'
        }];
    }

    console.log(formattedChartData);
    
    Highcharts.chart(elementId, {
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Expense Categories'
        },
        plotOptions: {
            pie: {
                innerSize: '70%',
                dataLabels: {
                    enabled: true
                }
            }
        },
        credits: {
            enabled: false
        },
        series: [{
            name: 'Expenses',
            data: formattedChartData
        }]
    });
};
