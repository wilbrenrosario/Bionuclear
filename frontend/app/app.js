var app = angular.module("Bionuclear", ["ngRoute"]);

app.factory('superCache', ['$cacheFactory', function($cacheFactory) {
   return $cacheFactory('super-cache');
 }]);

app.factory("GlobalServices", function($http, superCache){
   var url_base = "https://bionuclearapi.azurewebsites.net";
   
   
   var getData = function(nombre, clave) {
      return $http({method:"POST", url: url_base + "/api/Usuarios", data: {"correo": nombre, "clave": clave}}).then(function(result){
         superCache.put('token', result.data.token);
         superCache.put('tipo_usuario', 0);
         return result.data;
      });
  };

  var registrar = function(comentario, nombre_paciente, correo_electroncio_paciente, nombre_doctor, sexo_paciente, expdiente) {
   console.log("Buscando el token: " + superCache.get("token"));
   $http.defaults.headers.common.Authorization = 'Bearer '+ superCache.get("token");  
   return $http({method:"POST", url: url_base + "/api/Resultados", data: {
      "comentario": comentario,
      "nombre_paciente": nombre_paciente,
      "correo_electroncio_paciente": correo_electroncio_paciente,
      "nombre_doctor": nombre_doctor,
      "sexo_paciente": sexo_paciente,
      "numero_expediente": expdiente
    }}).then(function(result){
      alert("Resultados Registrados!!")
       return result.data;
   });
};

  return { getData: getData, registrar: registrar};
});



app.config(function($routeProvider){
    $routeProvider
        .when("/", {
            controller: "LoginController",
            templateUrl: "app/views/login.html"
        })
        .when("/home", {
         controller: "HomeController",
         templateUrl: "app/views/home.html"
         })
         .when("/registrar", {
            controller: "RegistrarController",
            templateUrl: "app/views/admin/registro_resultados.html"
            })
        .otherwise({
         redirecTo: '/'
        });
});


app.controller("LoginController", function($scope, $http, $window, $location, GlobalServices) {
    $scope.email = "";
    $scope.clave = "";

    $scope.loguear = function(){
      if($scope.email != "" && $scope.clave != ""){   
         var respuesta = GlobalServices.getData($scope.email, $scope.clave);
         respuesta.then(function(result) { 
            console.log(result);
            $location.path('/home');
         });
         /*else{
            alert("Sus credenciales son incorrectas")
         }*/
      }
      else{
         alert("Ingrese sus datos.")
      }

      $scope.clave = "";

    };
    
    });

app.controller("HomeController", function($scope, $http,$location, superCache) {

    $scope.url_base = "https://master--incandescent-sunburst-c9c837.netlify.app/#!/";
    if(superCache.get("token") == undefined){
      $location.path('/');
    }
    });
app.controller("RegistrarController", function($scope, $http,$location, GlobalServices, superCache) {

   if(superCache.get("token") == undefined){
      $location.path('/');
    }
    $scope.url_base = "https://master--incandescent-sunburst-c9c837.netlify.app/#!/";
    $scope.sexo = "-1";
    $scope.expediente = "";
    $scope.registrar = function(){

      var formData = new FormData($('#formulario')[0]);
      formData.append('FileDetail', $('input[type=file]')[0].files[0]); 
    
      $.ajax({
        url: 'https://bionuclearapi.azurewebsites.net/api/Files/Upload',
        data: formData,
        type: 'POST',
        contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
        processData: false, // NEEDED, DON'T OMIT THIS
        beforeSend: function() {
           
        },
        success: function(msg) {
           console.log(msg);
           $scope.expediente = msg;
           var respuesta = GlobalServices.registrar($scope.comentario, $scope.nombre, $scope.correo, $scope.doctor, $scope.sexo, $scope.expediente);
           respuesta.then(function(result) { 
              console.log(result);
           });
        },
        error: function() {
           console.log("error");
        }
    });
    };
    });   