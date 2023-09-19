var app = angular.module("Bionuclear", ["ngRoute"]);

app.service("GlobalServices", function($http){

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
      console.log($scope.email);
      console.log($scope.clave);

      if($scope.email != "" && $scope.clave != ""){
         var respuesta = GlobalServices.login($scope.email, $scope.clave);
         if(respuesta){
            $location.path('/home');
         }else{
            alert("Sus credenciales son incorrectas")
         }
      }
      else{
         alert("Ingrese sus datos.")
      }

      $scope.email = "";
      $scope.clave = "";

    };
    
    });

app.controller("HomeController", function($scope, $http,$location) {

    $scope.url_base = "http://127.0.0.1:5500/frontend/#!/";
    $scope.posts = [];
    $scope.llamarpeticion = function() {
       // Simple GET request example:
    $http({
       method: 'GET',
       url: 'https://jsonplaceholder.typicode.com/posts'
     }).then(function successCallback(response) {
          console.log(response.data)
          $scope.posts = response.data;
       }, function errorCallback(response) {
          console.log(response);
       });
    };
    
    });
app.controller("RegistrarController", function($scope, $http) {

    $scope.url_base = "http://127.0.0.1:5500/frontend/#!/";
    $scope.posts = [];
    $scope.llamarpeticion = function() {
       // Simple GET request example:
    $http({
       method: 'GET',
       url: 'https://jsonplaceholder.typicode.com/posts'
     }).then(function successCallback(response) {
          console.log(response.data)
          $scope.posts = response.data;
       }, function errorCallback(response) {
          console.log(response);
       });
    };

    $scope.sexo = "-1";

    $scope.registrar = function(){
      console.log($scope.nombre);
      console.log($scope.correo);
      console.log($scope.sexo);
      console.log($scope.doctor);
      console.log($scope.comentario);
      console.log($scope.file);

    };
    
    
    });   