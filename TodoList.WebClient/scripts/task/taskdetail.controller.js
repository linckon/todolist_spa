angular.module('todolist').controller('taskdetailController', [
    '$scope', 'taskService','projectService', '$routeParams', '$location',
    function ($scope, taskService,projectService, $routeParams, $location) {

        $scope.buttonName = 'Update';
        $scope.task = {};
        var id = $routeParams.id;
        $scope.pagename = 'taskdetail' + id;
        $scope.priorities = [1, 2, 3,4,5];

        $scope.save = function () {
            $scope.task.ProjectId = $scope.task.Project.Id;
            taskService.save($scope.task).then(function (response) {
                console.log(response);
                alert(response.Message);
                $location.path('/tasks');
            }, function (error) {
                alert(error.statusText);
            });
        };
        
        var loadProjects = function () {
            //call another function to fetch data
            projectService.getAll().then(function (response) {
                if (response.IsSuccess) {
                    $scope.projects = response.Data;
                    
                    taskService.getDetailById(id).then(function (r) {
                        console.log(r);
                        $scope.task = r.Data;
                        console.log($scope.task.Priority);
                       // $scope.task.Priority = r.Data.Priority;
                        $scope.task.DueDate = new Date(r.Data.DueDate);
                        for (var i = 0; i < $scope.projects.length; i++) {
                            if ($scope.projects[i].Id === $scope.task.ProjectId) {
                                $scope.task.Project = $scope.projects[i];
                                break;
                            }
                        }
                        for (var j = 0; j < $scope.priorities.length; j++) {
                            if ($scope.priorities[j].Priority === $scope.task.Priority) {
                                $scope.task.Priority = $scope.priorities[j];
                                break;
                            }
                        }
                    }, function (error) {
                        alert(error.statusText);
                    });
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
    }]);