listClientes();


$("#dtFechaAfiliacion").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

$.get("Cliente/GetAll", (data) => {
    fillComboCliente(data, document.getElementById("cboCliente"));
})

$.get("Seguro/GetAll", (data) => {
    fillComboSeguro(data, document.getElementById("cboSeguro"));
})

function fillComboCliente(data, control) {
    var content = "";
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].Id + "'>";
        content += data[i].DocumentoIdentidad + " " + data[i].ApellidoPaterno + " " + data[i].Nombres;
        content += "</option>";
    }
    control.innerHTML = content;
}

function fillComboSeguro(data, control) {
    var content = "";
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].Id + "'>";
        content += data[i].Descripcion;
        content += "</option>";
    }
    control.innerHTML = content;
}

function listClientes() {
    $.get("Afiliacion/GetAll", function (data) {
        createListTable(["Id", "Cliente", "Seguro", "Fecha Afiliacion"], data);
    });
}

function eliminar(id) {

    if (confirm("Desea eliminar?") == 1) {

        $.get("Afiliacion/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listClientes();
            }
        });
    }
}

function openModal(id) {

    var controlesObligatorio = document.getElementsByClassName("obligatorio");
    var ncontroles = controlesObligatorio.length;
    for (var i = 0; i < ncontroles; i++) {
        controlesObligatorio[i].parentNode.classList.remove("error");
    }

    if (id == 0) {
        borrarDatos();
        document.getElementById("lblTitulo").innerHTML = "Agregar Afiliacion";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Afiliacion";

        $.get("Afiliacion/Get/?id=" + id, function (data) {

            document.getElementById("txtIdAfiliacion").value = data[0].Id;
            document.getElementById("cboCliente").value = data[0].Cliente;
            document.getElementById("cboSeguro").value = data[0].Seguro;

            document.getElementById("dtFechaAfiliacion").value = data[0].FechaAfiliacion;

        });

    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idAfiliacion = document.getElementById("txtIdAfiliacion").value;
        var cliente = document.getElementById("cboCliente").value;
        var seguro = document.getElementById("cboSeguro").value;
        var fechaAfiliacion = document.getElementById("dtFechaAfiliacion").value;

        frm.append("Id", idAfiliacion);
        frm.append("Cliente", cliente);
        frm.append("Seguro", seguro);
        frm.append("FechaAfiliacion", fechaAfiliacion);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Afiliacion/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {
                    console.log(data);
                    switch (data) {
                        case '0':
                            alert("Ocurrio un error");
                            break;
                        case '1':
                            alert("Se ejecuto correctamente");
                            listClientes();
                            document.getElementById("btnCancelar").click();
                            break;
                        case '2':
                            alert("La cliente supera la edad permitida para el seguro elegido");
                            break;
                        default:
                            alert("Ocurrio un error inesperado");
                            break;
                    }
                }
            })
        }
    }
}