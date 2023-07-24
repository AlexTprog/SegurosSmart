listCompania();

$("#dtFechaRen").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

function listCompania() {
    $.get("Compania/GetAll", function (data) {
        
        createListTable(["Id", "Descripcion", "Ruc", "Razon Social", "Contacto",
            "Celular", "Contrato", "Fecha Renovacion"], data);
    });
}

function eliminar(id) {

    if (confirm("Desea eliminar?") == 1) {

        $.get("Compania/Delete/?id=" + id, function (data) {
            if (data == 0) {
                alert("Ocurrio un error");
            } else {
                alert("Se elimino correctamente");
                listCompania();
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
        document.getElementById("lblTitulo").innerHTML = "Agregar Compañia";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Compañia";

        $.get("Compania/Get/?id=" + id, function (data) {

            document.getElementById("txtIdComp").value = data.Id;
            document.getElementById("txtDesc").value = data.Descripcion;
            document.getElementById("txtRuc").value = data.Ruc;

            document.getElementById("txtRazonSocial").value = data.RazonSocial;
            document.getElementById("txtContacto").value = data.Contacto;
            document.getElementById("txtCelular").value = data.Celular;
            document.getElementById("txtContrato").value = data.Contrato;

            document.getElementById("dtFechaRen").value = data.FechaRenovacion;

        });

    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idCompania = document.getElementById("txtIdComp").value;
        var descripcion = document.getElementById("txtDesc").value;
        var ruc = document.getElementById("txtRuc").value;
        var razonSocial = document.getElementById("txtRazonSocial").value;
        var contacto = document.getElementById("txtContacto").value;
        var celular = document.getElementById("txtCelular").value;
        var contrato = document.getElementById("txtContrato").value;

        var fechaRen = document.getElementById("dtFechaRen").value;

        frm.append("Id", idCompania);
        frm.append("Descripcion", descripcion);
        frm.append("Ruc", ruc);
        frm.append("RazonSocial", razonSocial);
        frm.append("Contacto", contacto);
        frm.append("Celular", celular);
        frm.append("Contrato", contrato);

        frm.append("FechaRenovacion", fechaRen);

        frm.append("Estado", 0);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Compania/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data == 0) {
                        alert("Ocurrio un error");
                    } else {
                        alert("Se ejecuto correctamente");
                        listCompania();
                        document.getElementById("btnCancelar").click();
                    }
                }
            })
        }
    }
}