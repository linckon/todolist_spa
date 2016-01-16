angular.module('todolist').controller('dashboardController', ['$scope', 'dashboardService', 'projectService', 'taskService',
    function ($scope, dashboardService, projectService, taskService) {

    $scope.pagename = 'dashboard';

    $scope.projects = [];
    $scope.tasks = [];
    $scope.selectedProject = {};
    var errorFunction = function(error) {
        console.log(error);
    };
        
    $scope.loadTasks = function (p) {
        $scope.selectedProject = p;
        $scope.tasks = [];
        //call another function to fetch the tasks of this projects
        taskService.getAllByProject(p.Id).then(function(response) {
            if (response.IsSuccess) {
                $scope.tasks = response.Data;
                if ($scope.tasks.length === 0) {
                    alert("No task found for this project");
                }
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };
        
    $scope.loadProjects = function () {
        //call another function to fetch data
        projectService.getAll().then(function (response) {
            console.log(response);
            if (response.IsSuccess) {
                $scope.projects = response.Data;
                if ($scope.projects.length > 0) {
                    $scope.loadTasks($scope.projects[0]);
                }
            } else {
                alert(response.Message);
            }
        }, errorFunction);
    };

        $scope.complete = function(t) {
            t.IsComplete = true;
            taskService.markComolete(t).then(function(response) {
                if (response.IsSuccess) {
                    $scope.loadTasks($scope.selectedProject);
                }
            }, errorFunction);

        };
    
    function init() {
        $scope.loadProjects();
    };

    init();
  }]);