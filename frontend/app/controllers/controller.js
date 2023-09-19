var app = angular.module("App", []);

app.controller("home", function($scope, $http) {
   $scope.nombre = "Wilbren Rosario"; 
   $scope.posts = [];
   $scope.comentarios = [
   {
      comentario: "Hola amor",
      username: "codigofacilito"
   },
   {
      comentario: "Hola amor",
      username: "codigofacilito"
   },
   {
      comentario: "Hola amor",
      username: "codigofacilito"
   },
   {
      comentario: "Hola amor",
      username: "codigofacilito"
   }
];

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