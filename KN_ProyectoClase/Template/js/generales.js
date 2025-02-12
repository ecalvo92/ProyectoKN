function ConsultarNombre() {

  let identificacion = $("#Identificacion").val();
  $("#Nombre").val("");

  if (identificacion.length >= 9) {
    $.ajax({
      url: 'https://apis.gometa.org/cedulas/' + identificacion,
      method: 'GET',
      dataType: 'json',
      success: function (data) {
        $("#Nombre").val(data.nombre);
      }
    });
  }

}