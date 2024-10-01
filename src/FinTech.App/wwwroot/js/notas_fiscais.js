
$(document).ready(function () {

    carregaSelectStatus();
    carregaSelectMes();
    carregaTableNotasFiscais();    

});

//TableList
 function carregaTableNotasFiscais() {
    if ($('#tb_notasFiscais').length > 0) {

        var tb = $('#tb_notasFiscais').DataTable({
            processing: true,
            serverSide: true,
            paging: true,
            autoWidth: false,
            searching: false,
            ordering: false,
            pageLength: 10,
            pageChange: false,
            language: {
                url: "/lib/json/datatable-pt-br.json"
            },
            lengthMenu: [[5, 10, 25], [5, 10, 25]],
            ajax: {
                url: "/NotasFiscais/GetNotasFiscaisList",
                data: function (d) {

                    d.status = $("#filtro_status").val();                    
                    d.mesEmissao = $("#filtro_mes_emissao").val();                    
                    d.mesCobranca = $("#filtro_mes_cobranca").val();                    
                    d.mesPagamento = $("#filtro_mes_pagamento").val();                    

                }
            },
            columns: [
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {
                        return '<div class="text-left">' + f.dataEmissao + '</div>';
                    }
                },
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {

                        return '<div class="text-left">' + f.nomePagador + '</div>';
                    }
                },
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {
                        return '<div class="text-left">' + f.documentoNotaFiscal + '</div>';
                    }
                },
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {
                        return '<div class="text-left">' + f.valor + '</div>';
                    }
                },
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {
                        return '<div class="text-left">' + f.status + '</div>';
                    }
                },              
                                
                {
                    'data': null, orderable: false,
                    render: function (d, t, f, m) {
                        var r = '<a id="visualizar-notaFiscal" class="btn btn-primary btn-xs pe-none" href="/NotaFiscal/Detalhe/' + f.id + '" ><i class="fas fa-search"></i></a >';
                        r += '<a id="editar-notaFiscal" class="btn btn-success btn-xs pe-none" href="/NotaFiscal/Editar/' + f.id + '" target="_blank" ><i class="fas fa-pen"></i></a >';
                        r += '<a id="btn-delete-notaFiscal" class="btn btn-danger btn-xs pe-none" onclick="deleteNotaFiscal(\'' + f.id + '\');"><i class="fas fa-trash"></i></a>';

                        return r;
                    }
                }
            ]
        });
        function atualizarTabela() {
            tb.ajax.reload();
        }

        $('#filtro_status, #filtro_mes_emissao, #filtro_mes_cobranca, #filtro_mes_pagamento').on('change', function () {
            atualizarTabela();
        });   
    }
}


// Filtros
function carregaSelectStatus() {
    if ($('#filtro_status').length > 0) {
        var placeholder = "Status";

        $('#filtro_status').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            closeOnSelect: false,
            width: '100%',
            ajax: {
                url: "/StatusNotaFiscal/StatusNotaFiscalComboBox",
                type: "GET",
                dataType: 'json',
                processResults: function (data) {
                    return {
                        results: data
                    };
                },
                cache: true
            }
        });

    }
}

function carregaSelectMes() {
    if ($('.filtro_mes').length > 0) {       
        var placeholder = "Selecione o Mês";

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
        
        $('.filtro_mes').select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            closeOnSelect: false,
            width: '100%',
            data: meses
        });       

    }
}


