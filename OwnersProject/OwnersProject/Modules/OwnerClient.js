var app = angular.module("ownerApp", ["ui.bootstrap"]);


app.controller("ownerCtrl", ['$scope', '$http', function ($scope, $http) {

    $scope.sortType = 'Name'; // set the default sort type
    $scope.reverse = false;

    $scope.sortBy = function (sortType) {
        $scope.reverse = ($scope.sortType === sortType) ? !$scope.reverse : false;
        $scope.sortType = sortType;
    };

    $http.get("/api/Owner").then(function (responce) {

        $scope.owners = responce.data;

        var allCandidates = $scope.owners;
        $scope.totalItems = allCandidates.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = 3;

        $scope.$watch("currentPage", function () {
            setPagingData($scope.currentPage);
        });

        function setPagingData(page) {
            var pagedData = allCandidates.slice(
                (page - 1) * $scope.itemsPerPage,
                page * $scope.itemsPerPage
            );
            $scope.aCandidates = pagedData;
        }
    });

    $scope.redirectToOwner = function (id, name) {
        window.location = "http://localhost:16547/SecondPage/Details/" + id + "?name=" + name;

    }

    $scope.onDelete = function (id) {
        $http.delete("/api/Owner/" + id).then(function () {
            // success callback
            $scope.statuscode = "Deleted";
            window.location = "http://localhost:16547/Home/Index";

            //$route.reload();

        },
            function (responce) {
                // failure call back
            });
    }

    $scope.addOwner = function (name) {
        $http.post("/api/Owner", {
            Name: name
        }).then(function () {
            $scope.statuscode = "Added";
            window.location = "http://localhost:16547/Home/Index";
        }, function (responce) {
            // failure call back
        });
    }

}]);