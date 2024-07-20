function loadChart(months, shipmentCounts) {
    console.log('Months:', months);
    console.log('Shipment Counts:', shipmentCounts);

    // כל החודשים
    var allMonths = Array.from({ length: 12 }, (_, i) => i + 1);
    var monthLabels = allMonths.map(m => `Month ${m}`);

    // צור אובייקט עבור מספר המשלוחים לפי חודש
    var shipmentCountsByMonth = allMonths.map(m => {
        var index = months.indexOf(m);
        return index > -1 ? shipmentCounts[index] : 0;
    });

    console.log('Shipment Counts by Month:', shipmentCountsByMonth);

    var ctx = document.getElementById('shipmentsChart').getContext('2d');
    var shipmentsChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: monthLabels,
            datasets: [{
                label: 'Shipments per Month',
                data: shipmentCountsByMonth,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                x: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Month'
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Number of Shipments'
                    }
                }
            }
        }
    });
}