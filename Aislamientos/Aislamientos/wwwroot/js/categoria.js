
var dataTable;


function cargarDataTable() {
    dataTable = $("#tblCategorias").DataTable({

        "ajax": {
            "url": "/categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Categoria", "width": "5%" },
            { "data": "nombre", "width": "80%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group btn-group-sm">

<a   class='btn btn-danger text-white' style='cursor:pointer; width:100%' disabled>
<i class='fas fa-trash-alt'></i> 
</a>

                            `;
                }, "width": "30%"
            }

        ],
        "language": {
            "processing": "Procesando...",
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "emptyTable": "Ningún dato disponible en esta tabla",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "search": "Buscar:",
            "infoThousands": ",",
            "loadingRecords": "Cargando...",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
        },
        "width": "100%"
    });
}

function cargarDataTableRead() {
    dataTable = $("#tblCategorias").DataTable({

        "ajax": {
            "url": "/categorias/Start_Repetible_Read",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Categoria", "width": "5%" },
            { "data": "nombre", "width": "80%" },
            {
                "data": "id_Categoria",
                "render": function (data) {
                    
                    return `<div class="btn-group btn-group-sm">
<a href='/Categorias/Get_Repetible_Read_First/${data}' class='btn btn-success text-white editar' style='cursor:pointer; width:100%'>
<i class='fas fa-edit'></i> Editar
</a>

                            `;
                }, "width": "30%"
            }

        ],
        "language": {
            "processing": "Procesando...",
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "emptyTable": "Ningún dato disponible en esta tabla",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "search": "Buscar:",
            "infoThousands": ",",
            "loadingRecords": "Cargando...",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
        },
        "width": "100%"
    });
}

function cargarDataTableSerializable() {
    dataTable = $("#tblCategorias").DataTable({

        "ajax": {
            "url": "/categorias/Star_Serializable",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Categoria", "width": "5%" },
            { "data": "nombre", "width": "80%" },
            {
                "data": "id_Categoria",
                "render": function (data) {

                    return `<div class="btn-group btn-group-sm">
<a href='/Categorias/Get_Serializable_First/${data}' class='btn btn-success text-white editar' style='cursor:pointer; width:100%'>
<i class='fas fa-edit'></i> Editar
</a>

                            `;
                }, "width": "30%"
            }

        ],
        "language": {
            "processing": "Procesando...",
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "emptyTable": "Ningún dato disponible en esta tabla",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "search": "Buscar:",
            "infoThousands": ",",
            "loadingRecords": "Cargando...",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
        },
        "width": "100%"
    });
}

function Delete(url) {

    Swal.fire({
        title: '¿Esta seguro de borrar?',
        text: "No podrá recuperar el cambio!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'SÍ, BORRAR!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {

                    if (data.success) {

                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }

                }
            });

        }
    })


}



// agregar categoria (guardar)

$("#formAddCategory").on("submit", function (ev) {
    if ($(this).valid()) {
        //aqui va el codigo de la llamada ajax para guardar la nueva cadena, si ese json devuelve true entonces manda o a recargar la pagina
        ev.preventDefault();
        var url = $(this).attr("action");
        var datos = $(this).serialize();
        // para agregar cargando borrar este comentario
        $.ajax({
            type: "POST",
            url: url,
            data: datos,
            success: function (data) {
                if (data.success == true) {
                    toastr.success(data.message);
                    $("#categoriaNombre").val("");
                    dataTable.ajax.reload();

                } else {
                    toastr.error(data.message);
                }


            },
            error: function (data) {
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });
    }
});

// read commit
$("#formAddCategory2").on("submit", function (ev) {
    if ($(this).valid()) {
        //aqui va el codigo de la llamada ajax para guardar la nueva cadena, si ese json devuelve true entonces manda o a recargar la pagina
        ev.preventDefault();
        var url = $(this).attr("action");
        var datos = $(this).serialize();
        // para agregar cargando borrar este comentario
        $.ajax({
            type: "POST",
            url: url,
            data: datos,
            success: function (data) {
                if (data.success == true) {
                    toastr.success(data.message);
                    $("#categoriaNombre").val("");
                    dataTable.ajax.reload();

                } else {
                    toastr.error(data.message);
                }


            },
            error: function (data) {
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });
    }
});
 // fin read commit

// ROLL BACK TRANSAC
$("#formAddCategory3").on("submit", function (ev) {
    if ($(this).valid()) {
        //aqui va el codigo de la llamada ajax para guardar la nueva cadena, si ese json devuelve true entonces manda o a recargar la pagina
        ev.preventDefault();
        var url = $(this).attr("action");
        var datos = $(this).serialize();
        // para agregar cargando borrar este comentario
        $.ajax({
            type: "POST",
            url: url,
            data: datos,
            success: function (data) {
                if (data.success == true) {
                    toastr.success(data.message);
                    $("#categoriaNombre").val("");
                    dataTable.ajax.reload();

                } else {
                    toastr.error(data.message);
                }


            },
            error: function (data) {
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });
    }
});
// FIN ROLL BACK TRANSAC


// Mostrar modificar categoria
$(document).on('click', 'a.editar', function (event) {
    event.preventDefault();


    var urlDatos = $(this).attr('href');


    $.ajax({
        url: urlDatos,
        type: 'get',
        success: function (response) {

            $('.formAddCategory4').empty().html(response.html);



            $('.modalAgregarCategoria4').modal({
                backdrop: 'static',
                keyboard: false
            });
        },
        error: function (er) {
            toastr.error(er.message);
        }
    });

});

// Mostrar modificar categoria serializable
$(document).on('click', 'a.editar', function (event) {
    event.preventDefault();


    var urlDatos = $(this).attr('href');


    $.ajax({
        url: urlDatos,
        type: 'get',
        success: function (response) {

            $('.formAddCategory5').empty().html(response.html);



            $('.modalAgregarCategoria5').modal({
                backdrop: 'static',
                keyboard: false
            });
        },
        error: function (er) {
            toastr.error(er.message);
        }
    });

});
//Guardar modificacion categorias
$(document).on('submit', '#formAddCategory4', function (event) {



    if ($(this).valid()) {

        event.preventDefault();

        var url = $(this).attr("action");

        var datos = $(this).serialize();



        $.ajax({
            type: "POST",
            url: url,
            data: datos,

            success: function (data) {
                if (data.success == true) {

                    toastr.success(data.message);
                    $("#categoriaNombre").val("");
                    dataTable.ajax.reload();



                } else {
                    toastr.error(data.message);
                }

            },
            error: function (data) {
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });




    }



});

//Guardar modificacion categorias serializable
$(document).on('submit', '#formAddCategory5', function (event) {



    if ($(this).valid()) {

        event.preventDefault();

        var url = $(this).attr("action");

        var datos = $(this).serialize();



        $.ajax({
            type: "POST",
            url: url,
            data: datos,

            success: function (data) {
                if (data.success == true) {

                    toastr.success(data.message);
                    $("#categoriaNombre").val("");
                    dataTable.ajax.reload();



                } else {
                    toastr.error(data.message);
                }

            },
            error: function (data) {
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });




    }



});

// reset el formulario luego de cerrar el modal
$('.modal').on('hidden.bs.modal', function () {
    $(this).find('form')[0].reset();
});