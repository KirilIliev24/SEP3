using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThemisBlazorApp2._0.Data.DataTransferObjects;

namespace ThemisBlazorApp2._0.Data
{
    public class DataModelService
    {
        HttpClient client;
        public User loggedIn { get; set; }
        public Project[] userProjects { get; set; }
        public string[] invitedToProjects { get; set; }
        public DataModelService(IHttpClientFactory clientA)
        {
            client = clientA.CreateClient();
        }
        public Project GetProjectByName(string PName)
        {
            foreach(Project project in userProjects)
            {
                if (project.Name.Equals(PName))
                    return project;
            }
            return null;
        }
        public async Task<bool> attemptLogin(string UserName, string Password)
        {
            bool result = false;
            var jsonString = await client.GetStringAsync($"http://localhost:8080/Logic_API_war_exploded/users?username={UserName}&password={Password}");
            Console.WriteLine(jsonString);
            try
            {
                var tempDataUser = JsonConvert.DeserializeObject<User>(jsonString);
                loggedIn = tempDataUser;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> submitNewUser(string fName, string lName, string username, string password, string email)
        {
            bool result = false;
            User tempUser = new User(fName, lName, username, email, password);
            var json = JsonConvert.SerializeObject(tempUser);
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/users", new StringContent(json, Encoding.UTF8, "application/json"));

            try
            {
                var tempDataUser = JsonConvert.DeserializeObject<User>(jsonString.Content.ReadAsStringAsync().Result);
                if (tempDataUser != null)
                {
                    loggedIn = tempDataUser;
                    result = true;
                }
            }
            catch { return result; }
            return result;
        }
        public async Task<bool> receiveProjects(string UserName)
        {
            bool result = false;
            var jsonString = await client.GetStringAsync($"http://localhost:8080/Logic_API_war_exploded/project?username={UserName}");
            var tempProjects = JsonConvert.DeserializeObject<Project[]>(jsonString);
            if (tempProjects != null)
            {
                userProjects = tempProjects;
                result = true;
            }
            return result;
        }
        public async Task<bool> submitProject(string ProjectName)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(ProjectName);
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/project?username={loggedIn.username}", new StringContent(json, Encoding.UTF8, "application/json"));
            try
            {
                var tempProjects = JsonConvert.DeserializeObject<Project>(jsonString.Content.ReadAsStringAsync().Result);
                if (tempProjects != null)
                {
                    await receiveProjects(loggedIn.username);
                    result = true;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
        public async Task<bool> inviteUserToProject(string projectToAdd, string userToAdd)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(projectToAdd);
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/invitation?username={userToAdd}", new StringContent(json, Encoding.UTF8, "application/json"));
            try
            {
                var tempDataUser = JsonConvert.DeserializeObject<User>(jsonString.Content.ReadAsStringAsync().Result);
                if (tempDataUser != null & tempDataUser.username.Equals(userToAdd))
                {
                    result = true;
                }
            }
            catch
            {
                return result = false;
            }            
            return result;
        }
        public async Task<bool> getUserInvitations()
        {
            bool result = false;
            var jsonString = await client.GetStringAsync($"http://localhost:8080/Logic_API_war_exploded/invitation?username={loggedIn.username}");
            var tempInvitations = JsonConvert.DeserializeObject<string[]>(jsonString);
            if(tempInvitations != null)
            {
                invitedToProjects = tempInvitations;
                result = true;
            }
            return result;
        }
        public async Task<bool> acceptInvitationToProject(string projectName)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(projectName);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/invitation?username={loggedIn.username}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempProject = JsonConvert.DeserializeObject<Project>(jsonString.Content.ReadAsStringAsync().Result);
            if(tempProject != null & tempProject.Name.Equals(projectName))
            {
                result = true;
                var tempStringList = new List<string>(invitedToProjects);
                tempStringList.Remove(projectName);
                invitedToProjects = tempStringList.ToArray();
            }
            return result;
        }
        public async Task<bool> declineInvitationToProject(string projectName)
        {
            bool result = false;
            var jsonString = await client.DeleteAsync($"http://localhost:8080/Logic_API_war_exploded/invitation?username={loggedIn.username}&projectName={projectName}");
            var tempUser = JsonConvert.DeserializeObject<User>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempUser != null & tempUser.username.Equals(loggedIn.username))
            {
                result = true;
                var tempStringList = new List<string>(invitedToProjects);
                tempStringList.Remove(projectName);
                invitedToProjects = tempStringList.ToArray();
            }
            return result;
        }
        public async Task<bool> updateUserRole(string userToUpdate, string projectName, string roleToAssign)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(roleToAssign);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/roles?username={userToUpdate}&projectName={projectName}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempProject = JsonConvert.DeserializeObject<Project>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempProject != null & tempProject.Name.Equals(projectName))
            {
                result = true;
                for (int i = 0; i < userProjects.Length; i++)
                    if (userProjects[i].Name.Equals(tempProject.Name))
                        userProjects[i] = tempProject;
            }
            return result;
        }        
        public async Task<bool> createBackLogItem(string projectName, string itemPriority, string itemDescription, int itemEstimated)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(new BackLogItem(itemPriority, itemEstimated, itemDescription));
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/backlogitems?projectName={projectName}", new StringContent(json, Encoding.UTF8, "application/json"));
            try
            {
                var tempBacklogItemProject = JsonConvert.DeserializeObject<Project>(jsonString.Content.ReadAsStringAsync().Result);
                if (tempBacklogItemProject != null & tempBacklogItemProject.Name.Equals(projectName))
                {
                    result = true;
                    for (int i = 0; i < userProjects.Length; i++)
                        if (userProjects[i].Name.Equals(projectName))
                            userProjects[i] = tempBacklogItemProject;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
        public async Task<bool> createUserStory(string projectName, string userStoryDescription)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(userStoryDescription);
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/userStory?projectName={projectName}", new StringContent(json, Encoding.UTF8, "application/json"));
            try
            {
                var tempUserStory = JsonConvert.DeserializeObject<UserStory>(jsonString.Content.ReadAsStringAsync().Result);
                if (tempUserStory != null & tempUserStory.Description.Equals(userStoryDescription))
                {
                    result = true;
                    for (int i = 0; i < userProjects.Length; i++)
                        if (userProjects[i].Name.Equals(projectName))
                            userProjects[i].UserStories.Add(tempUserStory);
                }
            }
            catch
            {
                return result;
            }
            return result;
        }
        public async Task<bool> updateUserStoryDesc(string projectName, string[] descriptions)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(descriptions);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/userStory?projectName={projectName}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempUserStories = JsonConvert.DeserializeObject<UserStory[]>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempUserStories != null && tempUserStories.Length != 0)
            {
                result = true;
                GetProjectByName(projectName).UserStories = tempUserStories.ToList<UserStory>();
            }
            return result;
        }
        public async Task<bool> createNewSprint(string projectName, int[] itemIds)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(itemIds);
            var jsonString = await client.PostAsync($"http://localhost:8080/Logic_API_war_exploded/sprints?projectName={projectName}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempSprint = JsonConvert.DeserializeObject<Sprint>(jsonString.Content.ReadAsStringAsync().Result);
            if(tempSprint != null)
            {
                result = true;
                for(int i = 0; i<userProjects.Length; i++)
                {
                    if(userProjects[i].Name.Equals(projectName))
                    {
                        userProjects[i].Sprints.Add(tempSprint);
                    }
                }
            }
            return result;
        }
        public async Task<bool> startWorkingOnBacklog(string id)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(id);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/backlogitems?username={loggedIn.username}&id={id}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempSprint = JsonConvert.DeserializeObject<Sprint>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempSprint != null)
            {
                result = true;
                foreach(var project in userProjects)
                    for(int i=0; i<project.Sprints.Count; i++)
                        if (project.Sprints[i].Number == tempSprint.Number)
                            project.Sprints[i] = tempSprint;
            }
            return result;
        }
        public async Task<bool> finishWorkingOnBacklog(string id, string time)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(id);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/backlogitemsActualTime?id={id}&actualTime={time}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempSprint = JsonConvert.DeserializeObject<Sprint>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempSprint != null)
            {
                result = true;
                foreach (var project in userProjects)
                    for (int i = 0; i < project.Sprints.Count; i++)
                        if (project.Sprints[i].Number == tempSprint.Number)
                            project.Sprints[i] = tempSprint;
            }
            return result;
        }
        public async Task<bool> editBacklogDescription(string projectName, string id, string description)
        {
            bool result = false;
            var json = JsonConvert.SerializeObject(description);
            var jsonString = await client.PutAsync($"http://localhost:8080/Logic_API_war_exploded/backlogitemsEditDescription?id={id}", new StringContent(json, Encoding.UTF8, "application/json"));
            var tempBacklog = JsonConvert.DeserializeObject<BackLogItem>(jsonString.Content.ReadAsStringAsync().Result);
            if (tempBacklog != null)
            {
                result = true;
                GetProjectByName(projectName).BackLogItems[GetProjectByName(projectName).BackLogItems.FindIndex(bl => bl.Id.ToString().Equals(id))].Description = tempBacklog.Description;
                foreach(var sprint in GetProjectByName(projectName).Sprints)
                {
                    if(sprint.BacklogItems != null && sprint.BacklogItems.FindIndex(bl => bl.Id.ToString().Equals(id)) != -1)
                    {
                        sprint.BacklogItems[sprint.BacklogItems.FindIndex(bl => bl.Id.ToString().Equals(id))].Description = tempBacklog.Description;
                    }
                }
            }
            return result;
        }
        public void cleanData()
        {
            loggedIn = null;
            userProjects = null;
            invitedToProjects = null;
        }
    }
}
