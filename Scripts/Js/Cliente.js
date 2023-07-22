listClientes();

$("#dtFechaNacimiento").datepicker(
    {
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true
    }
);

$.get("Cliente/GetGeneros", (data) => {
    fillComboEnum(data, document.getElementById("cboGenero"));
});

$.get("Cliente/GetTipoDocumento", (data) => {
    fillComboEnum(data, document.getElementById("cboTipodoc"));
});

function listClientes() {
    $.get("Cliente/GetAll", (data) => {
        createListTable(["Id", "Nombres", "Apellido Paterno", "Apellido Materno", "Fecha Nacimiento",
            "Genero", "Telefono", "Direccion", "Email", "Tipo Documento", "DNI",], data);
    });
}

function eliminar(id) {

    if (confirm("Desea eliminar?") == 1) {

        $.get("Cliente/Delete/?id=" + id, function (data) {
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
        document.getElementById("lblTitulo").innerHTML = "Agregar Cliente";
    } else {
        document.getElementById("lblTitulo").innerHTML = "Editar Cliente";

        $.get("Cliente/Get/?id=" + id, function (data) {

            document.getElementById("txtIdCliente").value = data[0].Id;
            document.getElementById("txtnombre").value = data[0].Nombres;
            document.getElementById("txtapPaterno").value = data[0].ApellidoPaterno;

            document.getElementById("txtapMaterno").value = data[0].ApellidoMaterno;

            document.getElementById("dtFechaNacimiento").value = data[0].FechaNacimiento;

            document.getElementById("cboGenero").value = data[0].Genero;

            document.getElementById("txttelefono").value = data[0].Telefono;
            document.getElementById("txtDireccion").value = data[0].Direccion;
            document.getElementById("txtEmail").value = data[0].Email;

            document.getElementById("cboTipodoc").value = data[0].TipoDocumento;
            document.getElementById("txtDocumentoIdentidad").value = data[0].DocumentoIdentidad;
        });

    }
}

function Agregar() {

    if (datosObligarios() == true) {

        var frm = new FormData();
        var idCliente = document.getElementById("txtIdCliente").value;
        var nombre = document.getElementById("txtnombre").value;
        var apPaterno = document.getElementById("txtapPaterno").value;
        var apMaterno = document.getElementById("txtapMaterno").value;

        var fechaNac = document.getElementById("dtFechaNacimiento").value;

        var idGenero = document.getElementById("cboGenero").value;
        var idTipoDocumento = document.getElementById("cboTipodoc").value;


        var telefono = document.getElementById("txttelefono").value;
        var direccion = document.getElementById("txtDireccion").value;
        var Email = document.getElementById("txtEmail").value;
        var documentoIdentidad = document.getElementById("txtDocumentoIdentidad").value;

        frm.append("Id", idCliente);
        frm.append("Nombres", nombre);
        frm.append("ApellidoPaterno", apPaterno);
        frm.append("ApellidoMaterno", apMaterno);

        frm.append("FechaNacimiento", fechaNac);
        frm.append("Genero", idGenero);
        frm.append("Telefono", telefono);
        frm.append("Direccion", direccion);
        frm.append("Email", Email);
        frm.append("TipoDocumento", idTipoDocumento);
        frm.append("DocumentoIdentidad", documentoIdentidad);

        frm.append("Estado", 0);

        if (confirm("Desea guardar los cambios?") == 1) {

            $.ajax({
                type: "POST",
                url: "Cliente/SaveOrUpdate",
                data: frm,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data == 0) {
                        alert("Ocurrio un error");
                    } else {
                        alert("Se ejecuto correctamente");
                        listClientes();
                        document.getElementById("btnCancelar").click();
                    }
                }
            })
        }
    }
}