using Data.Data;
using Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DataAccess
{
    class BacklogItemDataAccess
    {
        UserDataAccess userDataAccess = new UserDataAccess();
        public void CreateBacklogItem(BackLogItem backlog)
        {
            using (var context = new ThemisContext())
            {
                var backlogItem = new BackLogItem()
                {
                    Name = backlog.Name,
                    SprintId = null,
                    Status = "Not done",
                    Description = backlog.Description,
                    Priority = backlog.Priority,
                    EstimatedTime = backlog.EstimatedTime
                };
                context.BackLogItems.Add(backlogItem);
                context.SaveChanges();
            }
        }
        public BackLogItem getBacklogItemById(int id)
        {
            using (var context = new ThemisContext())
            {
                var item = context.BackLogItems.Find(id);
                return item;
            }
        }
        public BackLogItem[] GetBacklogByProject(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var backlogs = context.BackLogItems.Where(b => b.Name.Equals(projectName)).ToArray();
                foreach(BackLogItem backLog in backlogs)
                {
                    backLog.WorksOnIt = context.Users.Find(context.BackLogItems.Where(b => b.Id.Equals(backLog.Id)).Select(b => b.WorksOnIt.username).FirstOrDefault());
                }
                return backlogs;
            }
        }
        public BackLogItem[] GetBackLogItemsBySprint(string projectName, int sprintId)
        {
            using (var context = new ThemisContext())
            {
                var backlogs = context.BackLogItems.Where(b => b.Name.Equals(projectName) && b.SprintId.Equals(sprintId)).ToArray();
                foreach (BackLogItem backLog in backlogs)
                {
                    backLog.WorksOnIt = context.Users.Find(context.BackLogItems.Where(b => b.Id.Equals(backLog.Id)).Select(b => b.WorksOnIt.username));
                }
                return backlogs;
            }
        }
        public BackLogItem SetSprintId(int backlogId, int sprintId)
        {
            using (var context = new ThemisContext())
            {
                var backlogItem = context.BackLogItems.Find(backlogId);
                if (backlogItem != null)
                {
                    backlogItem.SprintId = sprintId;
                }
                context.Entry(backlogItem).State = EntityState.Modified;
                context.SaveChanges();
                return backlogItem;
            }
        }
        public Sprint SetActualTime(int backlogId, int actualTime)
        {
            using (var context = new ThemisContext())
            {
                var backlog = context.BackLogItems.Find(backlogId);
                if (backlog != null)
                {
                    backlog.ActualTime = actualTime;
                    backlog.Status = "Done";
                }
                context.Entry(backlog).State = EntityState.Modified;
                context.SaveChanges();
                var sprint = AssembleSprintById(backlog.SprintId);
                return sprint;
            }
        }
        public Sprint SetWorkingUser(int backlogId, string username)
        {
            using (var context = new ThemisContext())
            {
                var backlog = context.BackLogItems.Find(backlogId);
                var user = context.Users.Find(username);
                if (backlog != null)
                {
                    backlog.WorksOnIt = user;
                    backlog.Status = "Being done";
                }
                context.Entry(backlog).State = EntityState.Modified;
                context.SaveChanges();
                var sprint = AssembleSprintById(backlog.SprintId);
                return sprint;
            }
        }
        public BackLogItem EditDescription(int backlogId, string newDescription)
        {
            using (var context = new ThemisContext())
            {
                var backlog = context.BackLogItems.Find(backlogId);
                if (backlog != null)
                {
                    backlog.Description = newDescription;
                }
                context.Entry(backlog).State = EntityState.Modified;
                context.SaveChanges();
                return backlog;
            }
        }
        public Sprint AssembleSprintById(int? id)
        {
            using (var context = new ThemisContext())
            {
                var sprint = context.Sprints.Find(id);
                var backlogItems = context.BackLogItems.Where(b => b.SprintId.Equals(sprint.SprintId)).ToList();
                foreach (BackLogItem backLog in backlogItems)
                {
                    backLog.WorksOnIt = context.Users.Find(context.BackLogItems.Where(b => b.Id.Equals(backLog.Id)).Select(b => b.WorksOnIt.username).FirstOrDefault());
                }
                sprint.BacklogItems = backlogItems;
                return sprint;
            }
        }
        public BackLogItem AssembleBackLogItem(string projectName, string description)
        {
            using (var context = new ThemisContext())
            {
                var backlogItem = context.BackLogItems.Where(b => b.Name.Equals(projectName) && b.Description.Equals(description)).FirstOrDefault();
                return backlogItem;
            }
        }
    }
}
