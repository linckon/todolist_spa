angular.module('todolist').controller('projectController', ['$scope', 'projectService', '$location',
    function ($scope, projectService, $location) {

    $scope.buttonName = 'Create';
    $scope.project = {};
    $scope.save = function() {
        projectService.save($scope.project).then(function(response) {
            console.log(response);
            $scope.project = {};
            $location.path('/projects');
        }, function(error) {
            alert(error.statusText);
        });
    };
    
    
}
]);