//here I am separating each angular controller to separate file for make it manageable  

angular.module('MyApp') //extending from previously created angular module in the previous part
.controller('Part2Controller', function ($scope, ContactService) { //inject ContactService
    $scope.Contact = null;
    ContactService.GetLastContact().then(function (d) {
        $scope.Contact = d.data; // Success
    }, function () {
        alert('Failed'); // Failed
    });
})
.factory('ContactService', function ($http) { // here I have created a factory which is a populer way to create and configure services
    var fac = {};
    fac.GetLastContact = function () {
        return $http.get('/Data/GetLastContact');
    }
    return fac;
});
