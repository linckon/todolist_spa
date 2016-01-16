angular.module('todolist').controller('taskController', ['$scope', 'taskService', 'projectService'
    ,'$location', function ($scope, taskService, projectService, $location) {

    $scope.buttonName = 'Create';
    $scope.task = {Project:null};
    $scope.projects = [];
    $scope.priorities = [1, 2, 3, 4, 5];
    $scope.save = function () {
        $scope.task.DueDate = $scope.task.DueDate.toDateString();
        $scope.task.ProjectId = $scope.task.Project.Id;
        console.log($scope.task);
        taskService.save($scope.task).then(function (response) {
            console.log(response);
            $scope.task = {};
            alert(response.Message);
            $location.path('/tasks');
        }, function(error) {
            alert(error.statusText);
        });
    };
        
    var loadProjects = function () {
        //call another function to fetch data
        projectService.getAll().then(function (response) {
            if (response.IsSuccess) {
                $scope.projects = response.Data;
            } else {
                alert(response.Message);
            }
        }, function (error) {
            console.log(error);
        });
    };

    function init() {
        loadProjects();
    };

    init();
}
]);