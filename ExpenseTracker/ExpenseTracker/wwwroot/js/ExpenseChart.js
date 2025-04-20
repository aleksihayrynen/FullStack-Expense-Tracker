window.renderExpenseChart = (elementId, chartData) => {
    // Sort same way as bar chart (by amount descending)
    const colors = Highcharts.getOptions().colors;

    let formattedChartData;

    // Null or empty check first
    if (!Array.isArray(chartData) || chartData.length === 0) {
        formattedChartData = [{
            name: 'Data not found ;(',
            y: 404,
            color: '#e0e0e0'
        }];
    } else { //Logic to use same colours as the other chart. We have to use same sort algorhytm for same colours
        const topCategories = [...chartData]
            .sort((a, b) => b.amount - a.amount)
            .slice(0, 5);

        formattedChartData = topCategories.map((item, index) => ({
            name: item.category.charAt(0).toUpperCase() + item.category.slice(1), // Capitalize
            y: item.amount,
            color: colors[index % colors.length]
        }));
    }


    Highcharts.chart(elementId, {
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Categories'
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
