angular.module('MyApp')  //extending angular module from first part
.controller('Part7Controller', function ($scope, OrderService) { //explained about controller in Part2
    $scope.Orders = [];

    OrderService.GetOrders().then(function (d) {
        $scope.Orders = d.data;
    });
})
.factory('OrderService', function ($http) { //explained about factory in Part2
    var fac = {};
    fac.GetOrders = function () {
        return $http.get('/Data/CustomerOrders');
    }
    return fac;
});