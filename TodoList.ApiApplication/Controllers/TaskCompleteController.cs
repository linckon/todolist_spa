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
    public class TaskCompleteController : ApiController
    {
        public ResponseModel Post(Task task)
        {
            TaskService taskService = new TaskService();
            ResponseModel responseModel;
            try
            {
                bool completed = taskService.MarkComplete(task);
                responseModel = new ResponseModel(isSuccess:completed);
            }
            catch (Exception exception)
            {

                responseModel = new ResponseModel(isSuccess:false,exception:exception,message:"Couldn't mark complete the task");
            }

            return responseModel;
        }
    }
}
