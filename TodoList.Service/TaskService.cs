using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.DataAccess;
using TodoList.ViewModel;

namespace TodoList.Service
{
    public class TaskService
    {
        public List<TaskViewModel> GetAll()
        {
            TodoListDbEntities db = new TodoListDbEntities();
            IQueryable<Task> asQueryable = db.Tasks.AsQueryable();
            List<TaskViewModel> list = asQueryable.Select(p => new TaskViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ProjectId = p.ProjectId,
                ProjectName = p.Project.Name,
                Priority = p.Priority,
                DueDate = p.DueDate,
                IsCompleted = p.IsCompleted
            }).ToList();
            return list;
        }

        public int Save(Task task)
        {
            TodoListDbEntities db = new TodoListDbEntities();
            Task dbTask;
            if (task.Id > 0)
            {
                dbTask = db.Tasks.Find(task.Id);
                if (dbTask != null)
                {
                    dbTask.Name = task.Name;
                    dbTask.DueDate = task.DueDate;
                    dbTask.IsCompleted = task.IsCompleted;
                    dbTask.Priority = task.Priority;
                    dbTask.ProjectId = task.ProjectId;
                    dbTask.Changed = DateTime.Now;
                }
            }
            else
            {
                task.Created = DateTime.Now;
                task.Changed = DateTime.Now;
                dbTask = db.Tasks.Add(task);
            }

            db.SaveChanges();
            UpdateProjectCount(task.ProjectId, db);
            return dbTask.Id;
        }

        public List<TaskViewModel> GetAllByProject(int projectId)
        {
            TodoListDbEntities db = new TodoListDbEntities();
            IQueryable<Task> asQueryable = db.Tasks.Where(x=>x.ProjectId == projectId).AsQueryable();
            List<TaskViewModel> list = asQueryable.Select(p => new TaskViewModel() { Id = p.Id, Name = p.Name,IsCompleted = p.IsCompleted,DueDate = p.DueDate}).ToList();
            return list;

        }

        public bool Delete(int id)
        {
            TodoListDbEntities db = new TodoListDbEntities();
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            return true;
        }

        public Task GetById(int id)
        {
            TodoListDbEntities db = new TodoListDbEntities();
            Task task = db.Tasks.Find(id);
            return new Task
            {
                ProjectId = task.ProjectId,
                Name = task.Name,
                Id = task.Id,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                
            };
        }

        public bool MarkComplete(Task task)
        {
            TodoListDbEntities db = new TodoListDbEntities();
            Task dbTask = db.Tasks.Find(task.Id);
            if (dbTask != null)
            {
                dbTask.IsCompleted = true;
                db.SaveChanges();
                int projectId = dbTask.ProjectId;
                UpdateProjectCount(projectId, db);
            }
            return true;
        }

        private static void UpdateProjectCount(int projectId, TodoListDbEntities db)
        {
            int unfinished = db.Tasks.Count(x => x.ProjectId == projectId && x.IsCompleted == false);
            Project dbProject = db.Projects.Find(projectId);
            dbProject.Count = unfinished;
            db.SaveChanges();
        }
    }
}