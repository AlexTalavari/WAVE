//home-index.js
var module = angular.module("homeIndex", ['ngRoute']);


//module.config(function($routeProvider) {
//    $routeProvider.when("/",
//        {            
//            controller: "userProfileController",
//            templateUrl:"/Templates/userProfileView.html"
//        });

//    $routeProvider.when("/messages",
//        {
//            controller: "userMessageController",
//            templateUrl: "/Templates/userMessageView.html"
//        });

//    $routeProvider.when("/userEditor",
//        {
//            controller: "userEditorController",
//            templateUrl: "/Templates/userEditorView.html"
//        });
//    $routeProvider.otherwise({ redirectTo: "/" });
//});

function userEditorController($scope, $http) {

}

function userDataController($scope, $http) {
    $scope.data = [];

    $scope.init = function(name) {
        //This function is sort of private constructor for controller
        $scope.name = name;
        var homepageUrl = "http://localhost:17923/api/UserData/" + $scope.name;
        $http.get(homepageUrl)
            .then(function(result) {
                    //Success      
                    angular.copy(result.data, $scope.data);
                },
                function() {
                    //Error
                    alert("Something went wrong");
                }
            );
    };

}

function userMessageController($scope, $http) {
    $scope.model = {};
    $scope.model.messages = [
        {
            imageUrl: "http://placekitten.com/200/300",
            senderName: "Alex",
            messageTitle: "Lorem Ipsum",
            days: "10"
        },
        {
            imageUrl: "http://placekitten.com/200/300",
            senderName: "Anax",
            messageTitle: "Kati Kati",
            days: "1"
        },
        {
            imageUrl: "http://placekitten.com/200/300",
            senderName: "Mitsos",
            messageTitle: "Microsoft Forever",
            days: "12"
        }
    ];
}

function homeIndexController($scope, $http) {

    $scope.data = [];

    $scope.setWidth = function(w) {
        return {
            width: w + "%"
        };
    };
    $scope.setRatingWidth = function(w) {
        return {
            width: 23 * w + "px"
        };
    };
    var homepageUrl = "http://localhost:17923/api/homepage";
    $http.get(homepageUrl)
        .then(function(result) {
                //Success
                var index;
                for (index = 0; index < result.data.length; ++index) {
                    result.data[index].width = 100 * result.data[index].currentValueBar / result.data[index].maxValueBar;
                }

                angular.copy(result.data, $scope.data);
            },
            function() {
                //Error
                alert("Something went wrong");
            }
        );
}