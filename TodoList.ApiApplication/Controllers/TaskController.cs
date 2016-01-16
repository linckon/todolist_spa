using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.DataAccess;
using TodoList.Service;
using TodoList.ViewModel;

namespace TodoList.ApiApplication.Controllers
{
    public class TaskController : ApiController
    {
         TaskService _taskService = new TaskService();

        public ResponseModel Get(int projectId)
        {
            ResponseModel response;
            try
            {
                List<TaskViewModel> tasks = projectId == 0 ? _taskService.GetAll():_taskService.GetAllByProject(projectId);
                response = new ResponseModel(tasks);
            }
            catch (Exception exception)
            {
                response = new ResponseModel(null, false, "Error occurred", exception);
            }
            return response;
        }

        public ResponseModel GetDetail(int id)
        {
            ResponseModel response;
            try
            {
                if (id > 0)
                {
                    Task task = _taskService.GetById(id);
                    response = new ResponseModel(task);
                }
                else
                {
                    response = new ResponseModel(isSuccess:false,message:"Id can't be zero.");
                }
            }
            catch (Exception exception)
            {
                response = new ResponseModel(null, false, "Error occurred", exception);
            }
            return response;
        }


        public ResponseModel Post(Task task)
        {
            ResponseModel response;
            try
            {
                task.Project = null;
                int id = _taskService.Save(task);
                response = id > 0 ? new ResponseModel(id) : new ResponseModel(null, false, "Couldn't save");
            }
            catch (Exception exception)
            {
                response = new ResponseModel(null, false, "Error occurred", exception);
            }
            return response;
        }

        public ResponseModel Delete(int id)
        {
            ResponseModel response;
            try
            {
                bool deleted = _taskService.Delete(id);
                response = deleted ? new ResponseModel(id) : new ResponseModel(null, false, "Couldn't delete");
            }
            catch (Exception exception)
            {
                response = new ResponseModel(null, false, "Error occurred", exception);
            }
            return response;
        }

    }
}
