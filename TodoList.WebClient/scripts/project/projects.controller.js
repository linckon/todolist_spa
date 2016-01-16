angular.module('todolist').controller('projectsController', [
    '$scope', 'projectService', function ($scope, projectService) {
        $scope.pagename = "projets";
        $scope.projects = [];
        var errorFunction = function (error) {
            console.log(error);
        };

        $scope.loadProjects = function () {
            //call another function to fetch data
            projectService.getAll().then(function (response) {
                console.log(response);
                if (response.IsSuccess) {
                    $scope.projects = response.Data;
                } else {
                    alert(response.Message);
                }
            }, errorFunction);
        };

        $scope.delete = function (project) {
            projectService.remove(project.Id).then(function (response) {
                if (response.IsSuccess) {
                    $scope.loadProjects();
                } else {
                    alert(response.Message+'\n Detail: '+response.Exception.Message);
                }
            }, errorFunction);
        };

        function init() {
            $scope.loadProjects();
        };

        init();
    }]);