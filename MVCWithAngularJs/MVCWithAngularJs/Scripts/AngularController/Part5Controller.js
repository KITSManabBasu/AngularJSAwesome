angular.module('MyApp') // extending from previously created angularJS  module in the First part
.controller('Part5Controller', function ($scope, LocationService) {
    // expained about controller in Part2 // Here LocationService (Service) Injected

    $scope.CountryID = null;
    $scope.StateID = null;
    $scope.CountryList = null;
    $scope.StateList = null;

    $scope.StateTextToShow = "Select State";
    $scope.Result = "";

    // Populate Country
    LocationService.GetCountry().then(function (d) {
        $scope.CountryList = d.data;
    }, function (error) {
        alert('Error!');
    });
    // Function For Populate State  // This function we will call after select change country
    $scope.GetState = function () {
        $scope.StateID = null; // Clear Selected State if any
        $scope.StateList = null; // Clear previously loaded state list
        $scope.StateTextToShow = "Please Wait..."; // this will show until load states from database

        //Load State 
        LocationService.GetState($scope.CountryID).then(function (d) {
            $scope.StateList = d.data;
            $scope.StateTextToShow = "Select State";
        }, function (error) {
            alert('Error!');
        });

    }
    // Function for show result
    $scope.ShowResult = function () {
        $scope.Result = "Selected Country ID : " + $scope.CountryID + " State ID : " + $scope.StateID;
    }

})
.factory('LocationService', function ($http) { // explained about factory in Part2
    var fac = {};
    fac.GetCountry = function () {
        return $http.get('/Data/GetCountries')
    }
    fac.GetState = function (countryID) {
        return $http.get('/Data/GetStates?countryID='+countryID)
    }
    return fac;
});