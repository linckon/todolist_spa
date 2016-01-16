angular.module('todolist').controller('tasksController', [
    '$scope', 'taskService', function ($scope, taskService) {

        $scope.tasks = [];
        var errorFunction = function (error) {
            console.log(error);
        };

        $scope.loadTasks = function () {
            //call another function to fetch data
            taskService.getAllByProject(0).then(function (response) {
                console.log(response);
                if (response.IsSuccess) {
                    $scope.tasks = response.Data;
                } else {
                    alert(response.Message);
                }
            }, errorFunction);
        };

        $scope.delete = function (project) {
            taskService.remove(project.Id).then(function (response) {
                if (response.IsSuccess) {
                    $scope.loadTasks();
                } else {
                    alert(response.Message + '\n Detail: ' + response.Exception.Message);
                }
            }, errorFunction);
        };

        function init() {
            $scope.loadTasks();
        };

        init();
    }]);