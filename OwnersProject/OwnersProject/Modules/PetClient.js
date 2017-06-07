var app = angular.module("petApp", ["ui.bootstrap"]);

function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [null, ''])[1].replace(/\+/g, '%20')) || null;
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

app.controller("petCtrl", ['$scope', '$http', function ($scope, $http) {

    $scope.urlId = location.pathname.split('/')[3];
    $scope.ownerName = getParameterByName('name');

    $scope.sortType = 'Name';
    $scope.reverse = false;

    $scope.sortBy = function (sortType) {
        $scope.reverse = ($scope.sortType === sortType) ? !$scope.reverse : false;
        $scope.sortType = sortType;
    };

    $http.get("/api/Pet/GetPetsByOwner/" + angular.copy($scope.urlId)).then(function (responce) {

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

    $scope.onDelete = function (id) {
        $http.delete("/api/Pet/" + id).then(function () {
            // success callback
            $scope.statuscode = "Deleted";
            window.location = "http://localhost:16547/SecondPage/Details/" + angular.copy($scope.urlId) + "?name=" + angular.copy($scope.ownerName);
        },
            function (responce) {
                $scope.statuscode = "sd";
                // failure call back
            });
    }

    $scope.addPet = function (name) {
        $http.post("/api/Pet", {
            Name: name,
            OwnerId: $scope.urlId
        }).then(function () {
            $scope.statuscode = "Added";
            window.location = "http://localhost:16547/SecondPage/Details/" + angular.copy($scope.urlId) + "?name=" + angular.copy($scope.ownerName);
        }, function (responce) {
            // failure call back
        });
    }

}]);