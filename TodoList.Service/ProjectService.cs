using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TodoList.DataAccess;
using TodoList.ViewModel;

namespace TodoList.Service
{
   public class ProjectService
    {
       public List<ProjectViewModel> GetAll()
       {
           TodoListDbEntities db = new TodoListDbEntities();
           IQueryable<Project> asQueryable = db.Projects.AsQueryable();
           List<ProjectViewModel> list = asQueryable.Select(p => new ProjectViewModel() {Id = p.Id, Name = p.Name, Count = p.Count}).ToList();
           return list;
       }

       public int Save(Project project)
       {
           TodoListDbEntities db = new TodoListDbEntities();
           Project dbProject ;
           if (project.Id > 0)
           {
               dbProject = db.Projects.Find(project.Id);
               if (dbProject != null)
               {
                   dbProject.Name = project.Name;
                   dbProject.Changed = DateTime.Now;
               }
           }
           else
           {
               project.Created = DateTime.Now;
               project.Changed = DateTime.Now;
               dbProject = db.Projects.Add(project);  
           }
         
           db.SaveChanges();
           return dbProject.Id;
       }

       public Project GetById(int id)
       {
           TodoListDbEntities db = new TodoListDbEntities();
           Project project = db.Projects.Find(id);
           return project;
       }

       public bool Delete(int id)
       {
           TodoListDbEntities db = new TodoListDbEntities();
           Project project = db.Projects.Find(id);
           if (project !=null)
           {
               db.Projects.Remove(project);
               db.SaveChanges();
           }
           return true;
       }
    }
}
