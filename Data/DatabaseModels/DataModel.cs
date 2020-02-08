using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DatabaseModels
{
    public class DataModel
    {
        
        UserDataAccess userDataAccess = new UserDataAccess();
        ProjectWorkgroupDataAccess projectDataAccess = new ProjectWorkgroupDataAccess();
        InvitationDataAccess invitationDataAccess = new InvitationDataAccess();
        BacklogItemDataAccess backlogItemDataAccess = new BacklogItemDataAccess();
        UserStoryDataAccess userStoryDataAccess = new UserStoryDataAccess();
        SprintDataAccess sprintDataAccess = new SprintDataAccess();
        public User searchUser(string username)
        {
            Console.WriteLine("Searching for user in database");
            User user = userDataAccess.retrieveUserByName(username);
            return user;
        }
        public User addUser(User user)
        {
            Console.WriteLine("Adding new user to database");
            userDataAccess.addUser(user);
            var tempUser = userDataAccess.retrieveUserByName(user.username);
            return tempUser;
        }
        //creates a project in database and returns it
        public Project addNewProject(string username, string projectName)
        {
            Console.WriteLine("Adding new project to database");
            projectDataAccess.addProject(projectName, username);
            Project project = projectDataAccess.getProject(projectName);
            return project;
        }
        //given the username of the user return an array of projects that user is part of
        public Project[] getUserProjects(string username)
        {
            Console.WriteLine("Looking for projects by username");
            Project[] projects = projectDataAccess.getProjectsForUser(username);
            return projects;
        }
        //Invites user to project and gives back the User object fro which an invitation has been created.
        public User inviteUserToProject(string projectName, string username)
        {
            Console.WriteLine("Add user to a project");
            Console.WriteLine(projectName, username);
            User tempUser = null;
            tempUser = invitationDataAccess.AddInvitation(projectName, username);
            return tempUser;
        }
        //Deletes a user invitation and then gives back the User object for which an invitation has been deleted.
        public User deleteInvitation(string projectName, string username)
        {
            Console.WriteLine("Delete invitation for user" + username);
            Console.WriteLine(projectName, username);
            User tempUser = null;
            tempUser = invitationDataAccess.DeleteInvitation(projectName, username);
            return tempUser;
        }
        //Returns a string[] of Project names
        public string[] getInvitationsByUser(string InvitedUser)
        {
            Console.WriteLine("Fetch invitation for user " + InvitedUser);
            string[] stringArrayProjects = invitationDataAccess.GetInvitationsByUser(InvitedUser);
            return stringArrayProjects;
        }
        public Project AcceptInvitation(string projectName, string invitedUser)
        {
            Console.WriteLine("Accepts invitation for project" + projectName + "for user" + invitedUser);
            Project acceptedProjectInvitation = invitationDataAccess.AcceptInvitation(projectName, invitedUser);
            acceptedProjectInvitation = projectDataAccess.getProject(acceptedProjectInvitation.Name);
            return acceptedProjectInvitation;
        }
        public WorkGroup GetWorkGroup(string pName)
        {
            Console.WriteLine("Getting WorkGroup");
            return projectDataAccess.GetWorkGroup(pName);
        }
        public Project setScrumMaster(string username, string pName)
        {
            Console.WriteLine("Setting SM role in project" + pName + "for user" + username);
            projectDataAccess.SetScrumMaster(username, pName);
            return projectDataAccess.getProject(pName);
        }
        public Project updateScrumMaster(string username, string pName)
        {
            Console.WriteLine("Updating SM role in project" + pName + "for user" + username);
            projectDataAccess.UpdateExistingScrumMaster(username, pName);
            return projectDataAccess.getProject(pName);
        }
        public Project setProductOwner(string username, string pName)
        {
            Console.WriteLine("Setting PO role in project" + pName + "for user" + username);
            projectDataAccess.SetProductOwner(username, pName);
            return projectDataAccess.getProject(pName);
        }
        public Project updateProductOwner(string username, string pName)
        {
            Console.WriteLine("Updating PO role in project" + pName + "for user" + username);
            projectDataAccess.UpdateExistingProductOwner(username, pName);
            return projectDataAccess.getProject(pName);
        }
        public Project createBacklogItem(BackLogItem backlogItem)
        {
            Console.WriteLine("Creating backlog item");
            backlogItemDataAccess.CreateBacklogItem(backlogItem);
            Project project = projectDataAccess.assembleProject(backlogItem.Name);
            Console.WriteLine(project.BackLogItems.Count + " Im in DataModel****************************");
            return project;
        }
        public BackLogItem getBacklogItemById(int id)
        {
            return backlogItemDataAccess.getBacklogItemById(id);
        }
        public Sprint setUserForBacklog(int backlogId, string username)
        {
            Console.WriteLine("updateUsername");
            return backlogItemDataAccess.SetWorkingUser(backlogId, username);
        }
        public Sprint updateActualTime(int backlogId, int actualTime)
        {
            Console.WriteLine("updateActualTime");
            return backlogItemDataAccess.SetActualTime(backlogId, actualTime);
        }
        public BackLogItem setSprintId(int backlogId, int sprintId)
        {
            Console.WriteLine("setSprintId");
            return backlogItemDataAccess.SetSprintId(backlogId, sprintId);
        }
        public UserStory AddUserStory(UserStory story)
        {
            Console.WriteLine("AddUserStory");
            return userStoryDataAccess.AddUserStory(story);
        }
        public UserStory[] GetUserStoriesForProject(string projectName)
        {
            Console.WriteLine("GetUserStoriesForProject");
            return userStoryDataAccess.GetUserStoriesForProject(projectName);
        }
        public UserStory[] EditUserStoryDescription(string projectName, string oldDescription, string newDescription)
        {
            Console.WriteLine("EditUserStoryDescription");
            return userStoryDataAccess.EditDescription(projectName, oldDescription, newDescription);
        }
        public BackLogItem EditDescriptionBacklogItem(int backlogId, string description)
        {
            Console.WriteLine("EditDescriptionBacklogItem");
            return backlogItemDataAccess.EditDescription(backlogId, description);
        }
        public Sprint CreateSprint(string projectName, int number)
        {
            Console.WriteLine($"Add sprint {number} to project {projectName}");
            return sprintDataAccess.CreateSprint(projectName,number);
        }
        public Sprint[] GetSprintsForProject(string projectName)
        {
            Console.WriteLine($"Got sprints for project {projectName}");
            return sprintDataAccess.AssembleSprintsForProject(projectName);
        }
        public int getSprintId(string projectName, int sprintNumber)
        {
            Console.WriteLine($"Getting sprint id from {projectName}");
            return sprintDataAccess.GetSprintId(projectName, sprintNumber);
        }
        public int[] GetSprintNumbers(string projectName)
        {
            Console.WriteLine($"Getting sprint numbers for project {projectName}");
            return sprintDataAccess.GetSprintNumbers(projectName);
        }
        public Sprint GetSprint(string projectName,int sprintNumber)
        {
            return sprintDataAccess.GetSprint(projectName, sprintNumber);
        }
        public string[] GetAllUsernames()
        {
            Console.WriteLine("Getting all usernames");
            return userDataAccess.GetAllUsernames();
        }
        public string[] GetAllProjectNames()
        {
            Console.WriteLine("Getting all project names");
            return projectDataAccess.GetAllProjectNames();
        }
    }
}
