
$(document).ready(function () {

    carregaSelectMesDashboard();
    carregaSelectTrimestreDashboard();
    carregaSelectAnoDashboard();
    selectMesAndTrimestreActions();
    carregaSelectAnoGraficos();

});

function carregaCharts() {
    if ($("#inadimplenciaChart").length > 0) {
        fetch('/Home/GetGraficos')
            .then(response => response.json())
            .then(data => {
                var inadimplenciaPorMes = data.inadimplenciaPorMes.map(function (item) {
                    return item.valorInadimplente;
                });

                var receitaPorMes = data.receitaPorMes.map(function (item) {
                    return item.valorReceita;
                });

                
                var ctxInadimplencia = document.getElementById('inadimplenciaChart').getContext('2d');
                var inadimplenciaChart = new Chart(ctxInadimplencia, {
                    type: 'line',
                    data: {
                        labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                        datasets: [{
                            label: 'Inadimplência',
                            data: inadimplenciaPorMes,
                            borderColor: 'rgba(255, 99, 132, 1)',
                            fill: false
                        }]
                    }
                });

                var ctxReceita = document.getElementById('receitaChart').getContext('2d');
                var receitaChart = new Chart(ctxReceita, {
                    type: 'line',
                    data: {
                        labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                        datasets: [{
                            label: 'Receita',
                            data: receitaPorMes,
                            borderColor: 'rgba(75, 192, 192, 1)',
                            fill: false
                        }]
                    }
                });
            });

    }
}

//Indicadores Dashboard
$('#filtro_ano_dashboard, #filtro_mes_dashboard, #filtro_trimestre_dashboard').on('change', function () {
    var ano = $('#filtro_ano_dashboard').val();
    var mes = $('#filtro_mes_dashboard').val();
    var trimestre = $('#filtro_trimestre_dashboard').val();

    $.ajax({
        url: '/Home/GetIndicadores',
        type: 'GET',
        data: { ano: ano, mes: mes, trimestre: trimestre },
        success: function (data) {
            $('span#totalEmitidas').text(data.totalEmitidas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
            $('span#totalInadimplencias').text(data.totalNotasVencidas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
            $('span#totalSemCobrancas').text(data.totalNotasSemCobranca.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
            $('span#totalAVencer').text(data.totalNotasAVencer.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
            $('span#totalPagas').text(data.totalNotasPagas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
        },
        error: function (xhr, status, error) {
            console.error("Erro ao obter os indicadores: ", error);
        }
    });
});


function carregaSelectMesDashboard() {
    if ($('#filtro_mes_dashboard').length > 0) {
        var placeholder = "Mês";

        const meses = [
            { id: '', text: '' },
            { id: 1, text: "Janeiro" },
            { id: 2, text: "Fevereiro" },
            { id: 3, text: "Março" },
            { id: 4, text: "Abril" },
            { id: 5, text: "Maio" },
            { id: 6, text: "Junho" },
            { id: 7, text: "Julho" },
            { id: 8, text: "Agosto" },
            { id: 9, text: "Setembro" },
            { id: 10, text: "Outubro" },
            { id: 11, text: "Novembro" },
            { id: 12, text: "Dezembro" }
        ];

        $('#filtro_mes_dashboard').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            closeOnSelect: false,
            width: '100%',
            data: meses
        });

    }
}

function carregaSelectTrimestreDashboard() {
    if ($('#filtro_trimestre_dashboard').length > 0) {
        var placeholder = "Trimestre";

        const data = [
            { id: '', text: '' },
            { id: 1, text: "1º Trimestre (Jan-Mar)" },
            { id: 2, text: "2º Trimestre (Abr-Jun)" },
            { id: 3, text: "3º Trimestre (Jul-Set)" },
            { id: 4, text: "4º Trimestre (Out-Dez)" },            
        ];

        $('#filtro_trimestre_dashboard').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            closeOnSelect: false,
            width: '100%',
            data: data
        });

    }
}

function carregaSelectAnoDashboard() {
    if ($('#filtro_ano_dashboard').length > 0) {
        var placeholder = "Ano";

        const data = [
            { id: '', text: '' },
            //{ id: 2024, text: "2024" },
            { id: 2023, text: "2023" },
            { id: 2022, text: "2022" },
            { id: 2021, text: "2021" },
            { id: 2020, text: "2020" },
        ];

        $('#filtro_ano_dashboard').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            closeOnSelect: false,
            width: '100%',
            data: data
        });

    }
}

function selectMesAndTrimestreActions() {
    const filtroMes = $("#filtro_mes_dashboard");
    const filtroTrimestre = $("#filtro_trimestre_dashboard");

    if ($(filtroMes).length > 0 || $(filtroTrimestre).length > 0) {
        $(filtroMes).on("change", function () {
            if ($(this).val() !== "") {
                filtroTrimestre.prop("disabled", true);
            } else {
                filtroTrimestre.prop("disabled", false);
            }
        });

        $(filtroTrimestre).on("change", function () {
            if ($(this).val() !== "") {
                filtroMes.prop("disabled", true);
            } else {
                filtroMes.prop("disabled", false);
            }
        });
    }
}

//Graficos
var inadimplenciaChart;
var receitaChart;
function criarGraficos(inadimplenciaPorMes, receitaPorMes) {
    var ctxInadimplencia = document.getElementById('inadimplenciaChart').getContext('2d');
    var ctxReceita = document.getElementById('receitaChart').getContext('2d');

    
    inadimplenciaChart = new Chart(ctxInadimplencia, {
        type: 'line',
        data: {
            labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            datasets: [{
                label: 'Inadimplência',
                data: inadimplenciaPorMes,
                borderColor: 'rgba(255, 99, 132, 1)',
                fill: false
            }]
        }
    });

    
    receitaChart = new Chart(ctxReceita, {
        type: 'line',
        data: {
            labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            datasets: [{
                label: 'Receita',
                data: receitaPorMes,
                borderColor: 'rgba(54, 162, 235, 1)',
                fill: false
            }]
        }
    });
}
function carregarGraficos(ano) {
    $.ajax({
        url: '/Home/GetGraficos',
        type: 'GET',
        data: { ano: ano },
        success: function (data) {
            
            var inadimplenciaPorMes = data.inadimplenciaPorMes.map(item => item.valorInadimplente);
            var receitaPorMes = data.receitaPorMes.map(item => item.valorReceita);
                       

            if (!inadimplenciaChart || !receitaChart) {
                
                criarGraficos(inadimplenciaPorMes, receitaPorMes);
            } else {
                             
                atualizarGrafico('inadimplenciaChart', inadimplenciaPorMes);                
                atualizarGrafico('receitaChart', receitaPorMes);
            }

            
        },
        error: function (err) {
            console.error('Erro ao carregar gráficos: ', err);
        }
    });
}

function atualizarGrafico(chartId, data) {
    var chart = Chart.getChart(chartId);
    if (chart) {
        chart.data.datasets[0].data = data;
        chart.update(); // Re-renderiza o gráfico
    }
}

function carregaSelectAnoGraficos() {
    if ($('#filtro_ano_graficos').length > 0) {
        var placeholder = "Ano";

        const data = [
            { id: '', text: '' },
            //{ id: 2024, text: "2024" },
            { id: 2023, text: "2023" },
            { id: 2022, text: "2022" },
            { id: 2021, text: "2021" },
            { id: 2020, text: "2020" },
        ];

        $('#filtro_ano_graficos').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: false,
            closeOnSelect: true,
            data: data
        });

        const anoInicial = data[1].id;
        carregarGraficos(anoInicial);

        
        $('#filtro_ano_graficos').on('change', function () {
            const ano = $(this).val();
            carregarGraficos(ano);
        });

    }
}




