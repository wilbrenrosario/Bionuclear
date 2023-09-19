var app = angular.module("Bionuclear");

app.serivce("GlobalServices", function($http){

    var url_base = "https://bionuclearapi.azurewebsites.net";

    this.login = function(nombre, clave) {
        $http({
            method: 'POST',
            url: url_base + "/api/Usuarios",
            data: {"correo": nombre, "clave": clave}
          }).then(function successCallback(response) {
               console.log(response.data)
               return true;
            }, function errorCallback(response) {
               console.log(response);
               return false;
            });
    }

});