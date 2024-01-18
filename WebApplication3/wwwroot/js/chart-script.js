namespace WebApplication3.wwwroot.js
{
    public class chart_script
    {
        fetch('/api/CubeData/query1')
    .then(response => response.json())
    .then(data => {
            new Chart('chart1', {
                type: 'bar',
                data: {
                    labels: data.labels,
                    datasets: [{
                        label: 'Sent, Dispatched, Deliv',
                        data: data.values,
                        backgroundColor: 'rgba(54, 162, 235, 0.5)' // Example color
                    }]
                },
                options: {
                    // Chart options
                }
            });
        });
    }
}
