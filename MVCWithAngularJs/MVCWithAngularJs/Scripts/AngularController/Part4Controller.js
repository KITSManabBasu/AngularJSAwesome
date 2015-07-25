angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('Part4Controller', function ($scope, EmployeeService) { // explained in Part 2 about controller
    $scope.Employees = null;
    EmployeeService.GetEmployeeList().then(function (d) {
        $scope.Employees = d.data; //Success callback
    }, function (error) {
        alert('Error!'); // Failed Callback
    });
})
.factory('EmployeeService', function ($http) { // I have explained about factory in the Part 2

    var fac = {};
    fac.GetEmployeeList = function () {
        return $http.get('/Data/GetEmployeeList')
    }
    return fac;
});