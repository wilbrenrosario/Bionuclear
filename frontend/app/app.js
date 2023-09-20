var app = angular.module("Bionuclear", ["ngRoute"]);

app.factory('superCache', ['$cacheFactory', function($cacheFactory) {
   return $cacheFactory('super-cache');
 }]);

app.factory("GlobalServices", function($http, superCache){
   var url_base = "https://bionuclearapi.azurewebsites.net";
   
   
   var getData = function(nombre, clave) {
      return $http({method:"POST", url: url_base + "/api/Usuarios", data: {"correo": nombre, "clave": clave}}).then(function(result){
         superCache.put('token', result.data.token);
         superCache.put('tipo_usuario', result.data.tipo); //0 admin 1 cliente
         superCache.put('id', result.data.id); 
         return result.data;
      });
  };

  var registrar = function(comentario, nombre_paciente, correo_electroncio_paciente, nombre_doctor, sexo_paciente, expdiente) {
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


var resultados = function() {
   $http.defaults.headers.common.Authorization = 'Bearer '+ superCache.get("token");  
   return $http({method:"GET", url: url_base + "/api/Resultados"}).then(function(result){
       return result.data;
   });
};

var misresultados = function(id) {
   $http.defaults.headers.common.Authorization = 'Bearer '+ superCache.get("token");  
   return $http({method:"GET", url: url_base + "/api/Resultados/Me?id=" + id}).then(function(result){
       return result.data;
   });
};


var buscar_resultado = function(id) {
   $http.defaults.headers.common.Authorization = 'Bearer '+ superCache.get("token");  
   return $http({method:"GET", url: url_base + "/api/Resultados/ById?id=" + id}).then(function(result){
       return result.data;
   });
};

var updateregistro = function(comentario, nombre_paciente, correo_electroncio_paciente, nombre_doctor, sexo_paciente, id, numero_expediente) {
   $http.defaults.headers.common.Authorization = 'Bearer '+ superCache.get("token");  
   return $http({method:"POST", url: url_base + "/api/Resultados/updateresultados", data: {
      "comentario": comentario,
      "nombre_paciente": nombre_paciente,
      "correo_electroncio_paciente": correo_electroncio_paciente,
      "nombre_doctor": nombre_doctor,
      "sexo_paciente": sexo_paciente,
      "numero_expediente": numero_expediente,
      "id": id
    }}).then(function(result){
      alert("Resultados Registrados!!")
       return result.data;
   });
};

  return { getData: getData, registrar: registrar, resultados: resultados, buscar_resultado: buscar_resultado, updateregistro: updateregistro, misresultados: misresultados};
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
            .when("/ver-registros/:id", {
               controller: "VerRegistrosController",
               templateUrl: "app/views/admin/ver_registro_resultados.html"
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
    };
    
    });

app.controller("HomeController", function($scope, $http,$location, superCache, GlobalServices) {

    $scope.isAdmin = superCache.get("tipo_usuario") == "0" ? true : false;
    $scope.url_base = "https://master--incandescent-sunburst-c9c837.netlify.app/#!/";
    $scope.resultados = {};

    if(superCache.get("token") == undefined){
      $location.path('/');
    }

    if($scope.isAdmin){
      var respuesta = GlobalServices.resultados();
      respuesta.then(function(result) { 
         $scope.resultados = result;
      });
    }else{
      var respuesta = GlobalServices.misresultados(superCache.get("id"));
      respuesta.then(function(result) { 
         $scope.resultados = result;
      });
    }

   

    });
app.controller("RegistrarController", function($scope, $http,$location, GlobalServices, superCache) {

   $scope.isAdmin = superCache.get("tipo_usuario") == "0" ? true : false;

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
        headers: {'Authorization': 'Bearer ' +superCache.get("token")},
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
           console.log("El token actual es: " + superCache.get("token"));
        }
    });
    };
    });   
app.controller("VerRegistrosController", function($scope, $http,$location, GlobalServices, superCache, $routeParams) {


   $scope.isAdmin = superCache.get("tipo_usuario") == "0" ? true : false;

   if(superCache.get("token") == undefined){
      $location.path('/');
    }
    $scope.url_base = "https://master--incandescent-sunburst-c9c837.netlify.app/#!/";
    $scope.id = "";
    $scope.correo = "";
    $scope.doctor = "";
    $scope.comentario = "";
    $scope.sexo = "";
    $scope.expediente = "";

    var respuesta = GlobalServices.buscar_resultado($routeParams.id);
    respuesta.then(function(result) { 
       console.log(result);
       $scope.id = result[0].id;
       $scope.nombre = result[0].nombre_paciente;
       $scope.correo = result[0].correo_electroncio_paciente;
       $scope.doctor = result[0].nombre_doctor;
       $scope.comentario = result[0].comentario;
       $scope.sexo = result[0].sexo_paciente;
    });

    
    $scope.updateregistrar = function(){

      var respuesta = GlobalServices.updateregistro($scope.comentario, $scope.nombre, $scope.correo, $scope.doctor, $scope.sexo, $routeParams.id, "0");
      respuesta.then(function(result) { 
         console.log(result);
         alert("Resultados Actualizados")
         var formData = new FormData($('#formulario')[0]);
         formData.append('FileDetail', $('input[type=file]')[0].files[0]); 

         var sd = $('input[type=file]')[0].files[0];
         if(sd != undefined){
            //subir archivo
            $.ajax({
               headers: {'Authorization': 'Bearer ' +superCache.get("token")},
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
                  GlobalServices.updateregistro($scope.comentario, $scope.nombre, $scope.correo, $scope.doctor, $scope.sexo, $routeParams.id, msg);
               },
               error: function() {
                  console.log("error");
                  console.log("El token actual es: " + superCache.get("token"));
               }
           });
         }
      });
    }

    });   