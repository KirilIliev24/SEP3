using Data.Data;
using Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    class ProjectWorkgroupDataAccess
    {
        BacklogItemDataAccess BackLogItems = new BacklogItemDataAccess();
        UserStoryDataAccess UserStories = new UserStoryDataAccess();
        SprintDataAccess Sprints = new SprintDataAccess();
        public void addProject(string projectName, string username)
        {
            using (var context = new ThemisContext())
            {
                var query = from p in context.Projects where p.Name.Equals(projectName) select p.Name;
                if (!query.Equals(projectName))
                {
                    context.Projects.Add(new Project(projectName)
                    {
                        Name = projectName
                    });
                    var temp = context.Users.Find(username);

                    context.WorkGroups.Add(new WorkGroup()
                    {
                        Name = projectName,
                        Creator = temp
                    });
                    context.SaveChanges();
                }
            }
        }
        public Project getProject(string projectName)
        {
            Project tempProject = assembleProject(projectName);
            return tempProject;
        }
        public Project[] getProjectsForUser(string username)
        {
            using (var context = new ThemisContext())
            {
                var workgroupCreator = context.WorkGroups.Where(w => w.Creator.username.Equals(username)).Select(w => w.Name);
                var workgroupScrumMaster = context.WorkGroups.Where(w => w.ScrumMaster.username.Equals(username)).Select(w => w.Name);
                var workgroupProductOwner = context.WorkGroups.Where(w => w.ProductOwner.username.Equals(username)).Select(w => w.Name);
                var workgroupId = context.WorkGroupDevelopers.Where(w => w.Developer.username.Equals(username)).Select(w => w.WorkGroupId).ToList();
                List<Project> projects = new List<Project>();
                foreach (string project in workgroupCreator)
                {
                    projects.Add(assembleProject(project));
                }
                foreach (string project in workgroupScrumMaster)
                {
                    projects.Add(assembleProject(project));
                }
                foreach (string project in workgroupProductOwner)
                {
                    projects.Add(assembleProject(project));
                }
                foreach (int id in workgroupId)
                {
                    var workgroup = context.WorkGroups.Find(id);
                    projects.Add(assembleProject(workgroup.Name));
                }
                Project[] array = projects.ToArray();
                return array;
            }
        }
        //Set a scrum master and removes the developer from the developers table
        public Project SetScrumMaster(string username, string projectName)
        {
            using (var context = new ThemisContext())
            {
                User user = context.Users.Find(username);
                WorkGroup workgroup = assembleWorkGroup(projectName);
                var wgDevs = context.WorkGroupDevelopers.Where(w => w.Developer.username.Equals(username) && w.WorkGroupId.Equals(workgroup.WorkGroupId)).FirstOrDefault();

                workgroup.ScrumMaster = user;
                context.WorkGroupDevelopers.Remove(wgDevs);
                context.Entry(workgroup).State = EntityState.Modified;
                context.SaveChanges();

                return assembleProject(projectName);
            }
        }
        //Updates exsisting scrum master and returns the old one to the developers table
        public Project UpdateExistingScrumMaster(string username, string projectName)
        {
            using (var context = new ThemisContext())
            {
                User user = context.Users.Find(username);
                WorkGroup workgroup = assembleWorkGroup(projectName);
                var oldScrumMaster = context.Users.Find(workgroup.ScrumMaster.username);
                var wgDevs = context.WorkGroupDevelopers.Where(w => w.Developer.username.Equals(username) && w.WorkGroupId.Equals(workgroup.WorkGroupId)).FirstOrDefault();

                workgroup.ScrumMaster = user;

                context.WorkGroupDevelopers.Remove(wgDevs);
                context.WorkGroupDevelopers.Add(new WorkGroupDeveloper()
                {
                    WorkGroupId = workgroup.WorkGroupId,
                    Developer = oldScrumMaster
                });

                context.Entry(workgroup).State = EntityState.Modified;
                context.SaveChanges();
                return assembleProject(projectName);
            }

        }
        //Updates exsisting product owner and returns the old one to the developers table
        public Project UpdateExistingProductOwner(string username, string projectName)
        {
            using (var context = new ThemisContext())
            {
                User user = context.Users.Find(username);
                WorkGroup workgroup = assembleWorkGroup(projectName);
                var oldProductOwner = context.Users.Find(workgroup.ProductOwner.username);
                var wgDevs = context.WorkGroupDevelopers.Where(w => w.Developer.username.Equals(username) && w.WorkGroupId.Equals(workgroup.WorkGroupId)).FirstOrDefault();

                workgroup.ProductOwner = user;

                context.WorkGroupDevelopers.Remove(wgDevs);
                context.WorkGroupDevelopers.Add(new WorkGroupDeveloper()
                {
                    WorkGroupId = workgroup.WorkGroupId,
                    Developer = oldProductOwner
                });

                context.Entry(workgroup).State = EntityState.Modified;
                context.SaveChanges();
                return assembleProject(projectName);
            }

        }
        //Set a product owner and removes the developer from the developers table
        public Project SetProductOwner(string username, string projectName)
        {
            using (var context = new ThemisContext())
            {
                User user = context.Users.Find(username);
                WorkGroup workgroup = assembleWorkGroup(projectName);
                
                var wgDevs = context.WorkGroupDevelopers.Where(w => w.Developer.username.Equals(username) && w.WorkGroupId.Equals(workgroup.WorkGroupId)).FirstOrDefault();

                workgroup.ProductOwner = user;
                context.WorkGroupDevelopers.Remove(wgDevs);
                context.Entry(workgroup).State = EntityState.Modified;
                context.SaveChanges();

                return assembleProject(projectName);
            }
        }
        public WorkGroup GetWorkGroup(string projectName)
        {
            return assembleWorkGroup(projectName);
        }
        public User assembleUser(string username)
        {
            using (var context = new ThemisContext())
            {
                User user = context.Users.Find(username);
                return user;
            }
        }
        public WorkGroup assembleWorkGroup(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var id = context.WorkGroups.Where(w => w.Name.Equals(projectName)).Select(w => w.WorkGroupId).FirstOrDefault();
                var tempWorkGroup = context.WorkGroups.Find(id);
                var listOfUsers = assembleDeveloper(id);
                if (tempWorkGroup != null)
                {
                    User tempUser = context.WorkGroups.Where(w => w.WorkGroupId.Equals(id)).Select(w => w.Creator).FirstOrDefault();
                    if (tempUser != null)
                        tempWorkGroup.Creator = assembleUser(tempUser.username);
                    tempUser = context.WorkGroups.Where(w => w.WorkGroupId.Equals(id)).Select(w => w.ScrumMaster).FirstOrDefault();
                    if (tempUser != null)
                        tempWorkGroup.ScrumMaster = assembleUser(tempUser.username);
                    tempUser = context.WorkGroups.Where(w => w.WorkGroupId.Equals(id)).Select(w => w.ProductOwner).FirstOrDefault();
                    if (tempUser != null)
                        tempWorkGroup.ProductOwner = assembleUser(tempUser.username);
                    if (listOfUsers != null)
                        tempWorkGroup.Developers = listOfUsers;
                }
                return tempWorkGroup;
            }
        }
        public List<User> assembleDeveloper(int id)
        {
            using (var context = new ThemisContext())
            {
                var listOfUsernames = context.WorkGroupDevelopers.Where(w => w.WorkGroupId.Equals(id)).Select(w => w.Developer.username);
                List<User> returnUserList = new List<User>();
                foreach (string username in listOfUsernames)
                {
                    returnUserList.Add(assembleUser(username));
                }
                return returnUserList;
            }
        }
        public string[] GetAllProjectNames()
        {
            using (var context = new ThemisContext())
            {
                var projectNames = context.Projects.Select(p => p.Name).ToArray();
                return projectNames;
            }
        }
        public Project assembleProject(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var project = context.Projects.Find(projectName);
                if (project != null)
                {
                    project.WorkGroup = assembleWorkGroup(projectName);
                    project.BackLogItems = BackLogItems.GetBacklogByProject(projectName).ToList();
                    project.UserStories = UserStories.GetUserStoriesForProject(projectName).ToList();
                    project.Sprints = Sprints.AssembleSprintsForProject(projectName).ToList();
                }
                return project;
            }
        }
    }
}
