﻿window.renderExpenseBarChart = (elementId, chartData) => {
    const totalAmount = chartData.reduce((sum, item) => sum + item.amount, 0);

    const topCategories = [...chartData]
        .sort((a, b) => b.amount - a.amount)
        .slice(0, 10);

    const categories = topCategories.map(item => item.category);
    const percentages = topCategories.map(item => (item.amount / totalAmount * 100).toFixed(2));

    // Generate colored data points (using Highcharts default colors)
    const colors = Highcharts.getOptions().colors;
    const dataWithColors = topCategories.map((item, index) => ({
        y: item.amount,
        color: colors[index % colors.length],
        category: item.category
    }));

    Highcharts.chart(elementId, {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Top 10 Categories by Spending'
        },
        xAxis: {
            categories: categories,
            title: { text: null }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Amount ($)',
                align: 'high'
            },
            labels: {
                overflow: 'justify'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            formatter: function () {
                return `<b>${this.point.category}</b><br/>Amount: $${this.y}<br/>Percent: ${percentages[this.point.index]}%`;
            }
        },
        plotOptions: {
            bar: {
                dataLabels: {
                    enabled: true,
                    style: {
                        textOutline: 'none',
                        color: '#333'
                    },
                    formatter: function () {
                        const percent = percentages[this.point.index];
                        return `$${this.y} (${percent}%)`;
                    }
                }
            }
        },
        series: [{
            name: 'Amount',
            data: dataWithColors
        }],
        credits: {
            enabled: false
        }
    });
};
