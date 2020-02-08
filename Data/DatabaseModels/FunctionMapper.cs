using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Data.DataAccess;

namespace Data.DatabaseModels
{
    class FunctionMapper
    {
        public string json { get; set; }
        DataModel model = new DataModel();
        string jsonString = "";
        public string Map(jsonObject jsono)
        {
            if (jsono.functionName.Equals("addNewUser"))
            {
                Console.WriteLine("We are adding a new user");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                User tempUser = JsonConvert.DeserializeObject<User>(stringArray[0]);
                User user = model.addUser(tempUser);
                jsonString = JsonConvert.SerializeObject(user);
            }
            else if (jsono.functionName.Equals("searchUserByBasic"))
            {
                Console.WriteLine("We are searching for user");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                User user = model.searchUser(stringArray[0]);
                jsonString = JsonConvert.SerializeObject(user);
            }
            else if (jsono.functionName.Equals("addNewProject"))
            {
                Console.WriteLine("Adding a new project");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project project = model.addNewProject(stringArray[0], stringArray[1]);
                jsonString = JsonConvert.SerializeObject(project);
            }
            else if (jsono.functionName.Equals("getUserProjects"))
            {
                Console.WriteLine("We are searching for projects");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project[] projects = model.getUserProjects(stringArray[0]);
                jsonString = JsonConvert.SerializeObject(projects);
            }
            else if (jsono.functionName.Equals("inviteUserToProject"))
            {
                Console.WriteLine("Adding user to existing project");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                User user = model.inviteUserToProject(stringArray[1], stringArray[0]);
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            }
            else if (jsono.functionName.Equals("deleteUserInvitation"))
            {
                Console.WriteLine("deleteUserInvitation");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                User user = model.deleteInvitation(stringArray[0], stringArray[1]);
                jsonString = JsonConvert.SerializeObject(user);
            }
            else if (jsono.functionName.Equals("getInvitationsByUser"))
            {
                Console.WriteLine("getInvitationsByUser");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                string[] ProjectNamesArray = model.getInvitationsByUser(stringArray[0]);
                jsonString = JsonConvert.SerializeObject(ProjectNamesArray);
            }
            else if (jsono.functionName.Equals("AcceptInvitation"))
            {
                Console.WriteLine("AcceptInvitation");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project projectAccepted = model.AcceptInvitation(stringArray[0], stringArray[1]);
                jsonString = JsonConvert.SerializeObject(projectAccepted);
            }
            else if (jsono.functionName.Equals("getWorkgroup"))
            {
                Console.WriteLine("getWorkgroup");
                string projectName = JsonConvert.DeserializeObject<string>(jsono.argument);
                WorkGroup workGroup = model.GetWorkGroup(projectName);
                jsonString = JsonConvert.SerializeObject(workGroup);
            }
            else if (jsono.functionName.Equals("setScrumMaster"))
            {
                Console.WriteLine("setScrumMaster");
                string[] strinArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project projectWithSM = model.setScrumMaster(strinArray[1], strinArray[0]);
                jsonString = JsonConvert.SerializeObject(projectWithSM);
            }
            else if (jsono.functionName.Equals("updateScrumMaster"))
            {
                Console.WriteLine("updateScrumMaster");
                string[] strinArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project projectWithUpdatedSM = model.updateScrumMaster(strinArray[1], strinArray[0]);
                jsonString = JsonConvert.SerializeObject(projectWithUpdatedSM);
            }
            else if (jsono.functionName.Equals("setProductOwner"))
            {
                Console.WriteLine("setProductOwner");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project projectWithPO = model.setProductOwner(stringArray[1], stringArray[0]);
                jsonString = JsonConvert.SerializeObject(projectWithPO);
            }
            else if (jsono.functionName.Equals("updateProductOwner"))
            {
                Console.WriteLine("updateProductOwner");
                string[] strinArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Project projectWithUpdatedPO = model.updateProductOwner(strinArray[1], strinArray[0]);
                jsonString = JsonConvert.SerializeObject(projectWithUpdatedPO);
            }
            else if (jsono.functionName.Equals("createBacklogItem"))
            {
                Console.WriteLine("createBacklogItem");
                BackLogItem backLogItem = JsonConvert.DeserializeObject<BackLogItem>(jsono.argument);
                Project project = model.createBacklogItem(backLogItem);
                jsonString = JsonConvert.SerializeObject(project);
            }
            else if (jsono.functionName.Equals("getBacklogItemById"))
            {
                Console.WriteLine("getBacklogItemById");
                string backlogItemId = JsonConvert.DeserializeObject<string>(jsono.argument);
                BackLogItem backLogItem = model.getBacklogItemById(Int32.Parse(backlogItemId));
                jsonString = JsonConvert.SerializeObject(backLogItem);
            }
            else if (jsono.functionName.Equals("setUserForBacklog"))
            {
                Console.WriteLine("updateUsername");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Sprint sprint = model.setUserForBacklog(Int32.Parse(stringArray[1]), stringArray[0]);
                jsonString = JsonConvert.SerializeObject(sprint);
            }
            else if (jsono.functionName.Equals("updateActualTime"))
            {
                Console.WriteLine("updateActualTime");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Sprint sprint = model.updateActualTime(Int32.Parse(stringArray[0]), Int32.Parse(stringArray[1]));
                jsonString = JsonConvert.SerializeObject(sprint);
            }
            else if (jsono.functionName.Equals("setSprintId"))
            {
                Console.WriteLine("setSprintId");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                BackLogItem backLogItem = model.setSprintId(Int32.Parse(stringArray[0]), Int32.Parse(stringArray[1]));
                jsonString = JsonConvert.SerializeObject(backLogItem);
            }
            else if (jsono.functionName.Equals("addUserStory"))
            {
                Console.WriteLine("addUserStory");
                UserStory story = JsonConvert.DeserializeObject<UserStory>(jsono.argument);
                UserStory storyCreated = model.AddUserStory(story);
                jsonString = JsonConvert.SerializeObject(storyCreated);
            }
            else if (jsono.functionName.Equals("GetUserStoriesForProject"))
            {
                Console.WriteLine("GetUserStoriesForProject");
                string projectName = JsonConvert.DeserializeObject<string>(jsono.argument);
                UserStory[] userStories = model.GetUserStoriesForProject(projectName);
                jsonString = JsonConvert.SerializeObject(userStories);
            }
            else if (jsono.functionName.Equals("editDescriptionUserStory"))
            {
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                UserStory[] editedUserStory = model.EditUserStoryDescription(stringArray[0], stringArray[1], stringArray[2]);
                jsonString = JsonConvert.SerializeObject(editedUserStory);
            }
            else if (jsono.functionName.Equals("editDescriptionBacklogItem"))
            {
                Console.WriteLine("editDescriptionBacklogItem");
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                BackLogItem backLogItem = model.EditDescriptionBacklogItem(Int32.Parse(stringArray[0]), stringArray[1]);
                jsonString = JsonConvert.SerializeObject(backLogItem);
            }
            else if (jsono.functionName.Equals("GetSprintsForProject"))
            {
                string projectName = JsonConvert.DeserializeObject<string>(jsono.argument);
                Sprint[] getSprints = model.GetSprintsForProject(projectName);
                jsonString = JsonConvert.SerializeObject(getSprints);
            }
            else if (jsono.functionName.Equals("getSprintNumbers"))
            {
                string projectName = JsonConvert.DeserializeObject<string>(jsono.argument);
                int[] getSprintNumbers = model.GetSprintNumbers(projectName);
                jsonString = JsonConvert.SerializeObject(getSprintNumbers);
            }
            else if (jsono.functionName.Equals("createSprint"))
            {
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Sprint newSprint = model.CreateSprint(stringArray[0], Int32.Parse(stringArray[1]));
                jsonString = JsonConvert.SerializeObject(newSprint);
            }
            else if (jsono.functionName.Equals("getSprintId"))
            {
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                int sprintId = model.getSprintId(stringArray[0], Int32.Parse(stringArray[1]));
                jsonString = JsonConvert.SerializeObject(sprintId);
            }
            else if (jsono.functionName.Equals("getSprint"))
            {
                string[] stringArray = JsonConvert.DeserializeObject<string[]>(jsono.argument);
                Sprint sprint = model.GetSprint(stringArray[0], Int32.Parse(stringArray[1]));
                jsonString = JsonConvert.SerializeObject(sprint);
            }
            else if (jsono.functionName.Equals("getAllUsernames"))
            {
                string[] usernames = model.GetAllUsernames();
                jsonString = JsonConvert.SerializeObject(usernames);
            }
            else if (jsono.functionName.Equals("getAllProjectNames"))
            {
                string[] projectNames = model.GetAllProjectNames();
                jsonString = JsonConvert.SerializeObject(projectNames);
            }
            return jsonString;
        }
    }
}
