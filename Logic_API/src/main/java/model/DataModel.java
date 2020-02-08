package model;

import dataTransferObjects.*;
import dataTransferObjectsDB.*;
import sockets.socketHandler;

import javax.jws.soap.SOAPBinding;
import java.util.ArrayList;
import java.util.Arrays;


public class DataModel {
    private socketHandler socket;

    public DataModel() {
        socket = new socketHandler();
    }

    public User addUser(User user) {
        User newUser = null;
        String[] usernames = getAllUsernames();
        for(int i = 0; i < usernames.length; i++)
        {
            if(usernames[i].equals(user.getUsername()))
            {
                return null;
            }
        }

        if (user != null) {
            System.out.println("Model received new user: " + user.getFirstName() + " (" + user.getUsername() + ")");
            String hashedPass = EncryptPassword.getHashedPassword(user.getPassword());
            System.out.println("Users password is hashed from \"" + user.getPassword() +"\" to \"" + hashedPass + "\"");
            if(hashedPass.equals("NoSuchAlgorithmException") || hashedPass.equals("InvalidKeySpecException") || hashedPass.equals("IllegalArgumentException"))
                return null;
            user.setPassword(hashedPass);
            newUser = socket.addNewUser(user);
        }
        return newUser;
    }

    public User getUserObject(String username, String password) {
        try {
            User user = socket.searchUser(username);
            if(user.getPassword().equals(password) || EncryptPassword.autenthicate(password, user.getPassword()))
                return user;
        }
        catch (NullPointerException e)
        {
            System.out.printf("User not found");
        }
        return null;
    }

    public Project[] getUserProject(String username) {
        ProjectDB[] projectsDB = socket.getProjects(username);
        if (projectsDB != null) {
            Project[] projects = new Project[projectsDB.length];
            for (int i = 0; i < projectsDB.length; i++)
                projects[i] = projectsDB[i].convertToProject();
            return projects;
        } else
            return new Project[0];
    }

    public Project createUserProject(String username, String pName) {
        String[] projectNames = getAllProjectNames();
        for(String s: projectNames)
        {
            if(s.equals(pName))
            {
                return null;
            }
        }
        ProjectDB projectDB = socket.addNewProject(username, pName);
        Project project = projectDB.convertToProject();
        System.out.println("New Project added: " + project.getName());
        return project;
    }

    public User inviteUserProject(String username, String pName) {
        String[] usernames = getAllUsernames();
        for(int i = 0; i < usernames.length; i++)
        {
            if(usernames[i].equals(username) && isUserInProject(username, pName))
            {
                User user = socket.inviteUserToProject(username, pName);
                System.out.println("New user added to project " + pName + ": " + user.getUsername());
                return user;
            }
        }
        return null;
    }

    private boolean isUserInProject(String username, String pName)    {
        WorkGroup workGroup = socket.getWorkgroup(pName, "getWorkgroup");
        ArrayList<User> developers = workGroup.getAllDevelopers();
        // This is working for Kiril
        if((workGroup.getCreator() != null && workGroup.getCreator().getUsername().equals(username)) ||
                (workGroup.getScrumMaster() != null && workGroup.getScrumMaster().getUsername().equals(username)) ||
                (workGroup.getProductOwner() != null && workGroup.getProductOwner().getUsername().equals(username)))
            return false;

        for (User u: developers
             ) {
            if (u.getUsername().equals(username))
                return false;
        }

        return true;
    }

    public User deleteUserInvitation(String username, String pName) {
        User user = socket.deleteUserInvitation(username, pName);
        System.out.println("Invitation is deleted from project " + pName + ": " + user.getUsername());
        return user;
    }

    public String[] getInvitationsByUsername(String username) {
        String[] invitations = socket.getInvitations(username);
        System.out.println("Invitations received...");
        return invitations;
    }

    public Project getAcceptedInvitation(String username, String pName) {
        System.out.println("We are in DataModelClass");
        Project project = socket.getAcceptedInvitation(username, pName);
        System.out.println("User: " + username + " has accepted the " + pName + " project' invitation!");
        return project;
    }

    public Project setRole(String username, String pName, String pRole) {
        Project project = new Project();
        WorkGroup workGroup = socket.getWorkgroup(pName, "getWorkgroup");
        if (pRole.equals("SCRUM Master")) {


            if (workGroup.getScrumMaster() == null) {

                project = socket.setRole(username, pName, "setScrumMaster");
            } else {
                project = socket.setRole(username, pName, "updateScrumMaster");
            }


            System.out.println("User: " + username + " has the new role: " + pRole + " in project: " + pName);
            return project;
        } else if (pRole.equals("Product Owner")) {

            if (workGroup.getProductOwner() == null) {
                project = socket.setRole(username, pName, "setProductOwner");
            } else {
                project = socket.setRole(username, pName, "updateProductOwner");
            }

            System.out.println("User: " + username + " has the new role: " + pRole + " in project: " + pName);
            return project;
        }

        return null;
    }

    public Project createBacklogItem(String projectName, BackLogItem backLogItem) {
        if(backLogItem.getDescription() == null || backLogItem.getDescription().equals("") || backLogItem.getPriority() == null || backLogItem.getPriority().equals("")
        || backLogItem.getEstimatedTime() == 0)
        {
            return null;
        }
        ProjectDB projectDB = socket.createBacklogItem(projectName, backLogItem);
        return projectDB.convertToProject();
    }

    public Sprint setUserForBacklog(String username, String id) {
        BackLogItemDB initialBacklogItem = socket.getBacklogItemById(id);

        if (initialBacklogItem.getWorksOnIt()==null)
        {
            SprintDB sprintDB = socket.setUserForBacklog(username, id);
            System.out.println(username + " is to be set as working in backlog item! Id: " + id);
            return sprintDB.convertToSprint();
        }
        else
            return
                null;


    }

    public Sprint updateActualTime(String id, String actualTime) {
        //Sounds weird, but DO NOT change the name of the method!!!S
        SprintDB sprint = socket.updateActualTime(id, actualTime);
        System.out.println(actualTime + " hours had been set for the backlog item, by id: " + id + "!");
        return sprint.convertToSprint();
    }

    public BackLogItem setSprintId(int backlogId, int sprintId) {
        BackLogItemDB bli = socket.setSprintId(backlogId, sprintId);
        System.out.println("Set sprint " + sprintId + " with backlog ID: " + backlogId);
        return bli.convertToBackLogItem();
    }

    public Sprint createSprint(String projectName, int[] backlogIdArray)    {
        int number=0;
        int[] sprintNumbers = socket.getSprintNumbers(projectName);

        if (sprintNumbers==null)
        {
            number=1;
            socket.createSprint(projectName,number);

        }
       else
        {
            number = sprintNumbers.length+1;

           socket.createSprint(projectName,number);
        }
            int sprintId = socket.getSprintId(projectName,number);
            for (int i=0; i<backlogIdArray.length;i++)
            {
                socket.setSprintId(backlogIdArray[i],sprintId);
            }
            SprintDB sprintDB =  socket.getSprint(projectName,number);
            return sprintDB.convertToSprint();
    }

    public UserStory addUserStory(String projectName, String description) {
        UserStory us = socket.addUserStory(projectName, description);
        System.out.println("New user story: " + description);
        return us;

    }

    public UserStory[] GetUserStoriesForProject(String pName) {
        UserStoryDB[] storiesDB = socket.getUserStories(pName);
        if (storiesDB != null) {
            UserStory[] stories = new UserStory[storiesDB.length];
            for (int i = 0; i < storiesDB.length; i++)
                stories[i] = storiesDB[i].convertToUserStory();
            return stories;
        } else
            return new UserStory[0];
    }

    public UserStory[] editUserStoryDescription(String projectName, String oldDescrition, String newDescriptino) {
        socket.editDescription(projectName, oldDescrition,newDescriptino);
        System.out.println("Edit user story description in project: " + projectName + " With new description: " + newDescriptino);
        return GetUserStoriesForProject(projectName);
    }

    public BackLogItem editDescriptionBacklogItem(String backlogId, String description)    {
        BackLogItemDB backLogItemDB = socket.editDescriptionBacklogItem(backlogId,description);
        return  backLogItemDB.convertToBackLogItem();

    }

    public UserStory editNumber(String projectName, String o, String n) {
        UserStory us = socket.editNumber(projectName, o, n);
        System.out.println("Edit number in project: " + projectName + "Old: " + o + " New: " + n);
        return us;
    }

    public String[] getAllUsernames()    {
        String[] usenames = socket.getAllUsernames();
        return  usenames;
    }

    public String[] getAllProjectNames()    {
        String[] projectNames = socket.getAllProjectNames();
        return  projectNames;
    }
}
