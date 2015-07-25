angular.module('MyApp', ['ngRoute']) //extending from previously created angularjs module in part1
// here ['ngRoute'] is not required, I have just added to make you understand in a single place
.config(function ($routeProvider, $locationProvider) {
    //here we will write code for implement routing 
    $routeProvider
    .when('/', { // This is for reditect to another route
        redirectTo: function () {
            return '/home';
        }
    })
    .when('/home', {
        templateUrl: '/Template/Home.html',
        controller: 'HomeController'
    })
    .when('/about', {
        templateUrl: '/Template/About.html',
        controller: 'AboutController'
    })
    .when('/order/:id', {
        templateUrl: '/Template/Order.html',
        controller: 'OrderController'
    })
    .otherwise({   // This is when any route not matched
        templateUrl: '/Template/Error.html',
        controller: 'ErrorController'
    })

    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})
.controller('HomeController', function ($scope) {
    $scope.Message = "This is HOME page";
})
.controller('AboutController', function ($scope) {
    $scope.Message = "This is ABOUT page";
})
.controller('OrderController', function ($scope, $routeParams) {
    // $routeParams used for get query string value
    $scope.Message = "This is ORDER Page with query string id value " + $routeParams.id;
})
.controller('ErrorController', function ($scope) {
    $scope.Message = "404 Not Found!";
});
