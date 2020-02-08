using Data.Data;
using Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DataAccess
{
    class SprintDataAccess
    {
        public Sprint CreateSprint(string projectName, int number)
        {
            using (var context = new ThemisContext())
            {
                context.Sprints.Add(new Sprint() { Name = projectName, Number = number });
                context.SaveChanges();
                return GetSprint(projectName, number);
            }
        }
        //Gets the sprint and puts the apropriate backlog items in it
        public Sprint GetSprint(string projectName, int sprintNumber)
        {
            using (var context = new ThemisContext())
            {
                var sprint = context.Sprints.Where(s => s.Name.Equals(projectName) && s.Number.Equals(sprintNumber)).FirstOrDefault();
                var backlogItems = context.BackLogItems.Where(b => b.SprintId.Equals(sprint.SprintId)).ToList();
                foreach (BackLogItem backLog in backlogItems)
                {
                    backLog.WorksOnIt = context.Users.Find(context.BackLogItems.Where(b => b.Id.Equals(backLog.Id)).Select(b => b.WorksOnIt.username).FirstOrDefault());
                }
                sprint.BacklogItems = backlogItems;
                return sprint;
            }
        }
        public int GetSprintId(string projectName, int sprintNumber)
        {
            using (var context = new ThemisContext())
            {
                var sprintId = context.Sprints.Where(s => s.Name.Equals(projectName) && s.Number.Equals(sprintNumber)).Select(s => s.SprintId).FirstOrDefault();
                return sprintId;
            }
        }
        //Gets all sprint numbers for a specific project 
        public int[] GetSprintNumbers(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var sprintNumbers = context.Sprints.Where(s => s.Name.Equals(projectName)).Select(s => s.Number).ToArray();
                return sprintNumbers;
            }
        }
        //Gets all the sprints for a project and adds the backlog items in the apropriate sprint 
        public Sprint[] AssembleSprintsForProject(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var sprints = context.Sprints.Where(s => s.Name.Equals(projectName)).ToArray();

                foreach (Sprint s in sprints)
                {
                    var backlogItemsInSprints = context.BackLogItems.Where(b => b.SprintId.Equals(s.SprintId)).ToList();
                    foreach (BackLogItem backLog in backlogItemsInSprints)
                    {
                        backLog.WorksOnIt = context.Users.Find(context.BackLogItems.Where(b => b.Id.Equals(backLog.Id)).Select(b => b.WorksOnIt.username).FirstOrDefault());
                    }
                    s.BacklogItems = backlogItemsInSprints;
                }
                return sprints;
            }
        }
    }
}
