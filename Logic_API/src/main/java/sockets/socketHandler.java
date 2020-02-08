package sockets;

import com.google.gson.Gson;
import dataTransferObjects.*;
import dataTransferObjectsDB.BackLogItemDB;
import dataTransferObjectsDB.ProjectDB;
import dataTransferObjectsDB.SprintDB;
import dataTransferObjectsDB.UserStoryDB;

public class socketHandler {
    private Gson gson;
    private SocketConnection socketConnection;

    public socketHandler() {
        gson = new Gson();
        socketConnection = new SocketConnection();
    }

    public User searchUser(String username) {
        String[] arrayString = new String[1];
        arrayString[0] = username;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("searchUserByBasic", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        User user = gson.fromJson(json, User.class);
        return user;
    }

    public User addNewUser(User user) {
        String[] arrayString = new String[1];
        String json = gson.toJson(user);
        arrayString[0] = json;
        json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("addNewUser", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        User userFromSockets = gson.fromJson(json, User.class);
        return userFromSockets;
    }

    public ProjectDB addNewProject(String username, String pName) {
        String[] arrayString = new String[2];
        arrayString[0] = username;
        arrayString[1] = pName;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("addNewProject", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        ProjectDB projectFromSockets = gson.fromJson(json, ProjectDB.class);
        return projectFromSockets;
    }

    public ProjectDB[] getProjects(String username) {
        String[] arrayString = new String[1];
        arrayString[0] = username;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("getUserProjects", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        ProjectDB[] projectsFromSockets = gson.fromJson(json, ProjectDB[].class);
        return projectsFromSockets;
    }

    public User inviteUserToProject(String username, String pName) {
        String[] arrayString = new String[2];
        arrayString[0] = username;
        arrayString[1] = pName;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("inviteUserToProject", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        User user = gson.fromJson(json, User.class);
        return user;
    }

    public User deleteUserInvitation(String username, String pName) {
        String[] arrayString = new String[2];
        arrayString[1] = username;
        arrayString[0] = pName;
        String json = gson.toJson(arrayString);
        System.out.println("Delete invitation was called");
        //This comment is only for confusing you.. but DON'T DELETE IT!!!
        jsonObject jsonObject = new jsonObject("deleteUserInvitation", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        User user = gson.fromJson(json, User.class);
        return user;
    }

    public String[] getInvitations(String username) {
        String[] arrayString = new String[1];
        arrayString[0] = username;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("getInvitationsByUser", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        String[] invitations = gson.fromJson(json, String[].class);
        return invitations;


    }

    public Project getAcceptedInvitation(String username, String pName) {
        System.out.println("we are in socketHandler class");
        String[] arrayString = new String[2];
        arrayString[1] = username;
        arrayString[0] = pName;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("AcceptInvitation", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        Project projectFromSockets = gson.fromJson(json, Project.class);
        return projectFromSockets;

    }

    public WorkGroup getWorkgroup(String pName, String functionName) {

        String json = gson.toJson(pName);
        jsonObject jsonObject = new jsonObject(functionName, json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        WorkGroup workGroup = gson.fromJson(json, WorkGroup.class);
        return workGroup;

    }

    public Project setRole(String username, String pName, String functionName) {
        String[] arrayString = new String[2];
        //TAKE CARE OF THE ORDER, HOW IS IT IN THE DATABASE! PNAME AND USERNAME SHOULD BE IN THE RIGHT ORDER!!!
        arrayString[1] = username;
        arrayString[0] = pName;
        String json = gson.toJson(arrayString);
        //functionName below is either ScrumMAster or ProductOwner
        jsonObject jsonObject = new jsonObject(functionName, json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        Project projectFromSockets = gson.fromJson(json, Project.class);
        return projectFromSockets;

    }

    public ProjectDB createBacklogItem(String projectName, BackLogItem backLogItem) {
        BackLogItemDB bli = new BackLogItemDB(projectName, backLogItem);
        String json = gson.toJson(bli);
        jsonObject jsonObject = new jsonObject("createBacklogItem", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        ProjectDB receivedProject = gson.fromJson(json, ProjectDB.class);
        return receivedProject;
    }

    public BackLogItemDB getBacklogItemById(String id) {
        String json = gson.toJson(id);
        jsonObject jsonObject = new jsonObject("getBacklogItemById", json);
        json = gson.toJson(jsonObject);
        String jsonFromSockets = socketConnection.sendInfo(json);
        BackLogItemDB backLogItem = gson.fromJson(jsonFromSockets, BackLogItemDB.class);
        return backLogItem;

    }

    public int[] getSprintNumbers(String projectName) {
        String json = gson.toJson(projectName);
        jsonObject jsonObject = new jsonObject("getSprintNumbers", json);
        json = gson.toJson(jsonObject);
        String numbersFromSockets = socketConnection.sendInfo(json);
        int[] numbers = gson.fromJson(numbersFromSockets, int[].class);
        return numbers;
    }

    public Sprint createSprint(String projectName, int number) {
        String[] arrayString = new String[2];
        arrayString[0] = projectName;
        arrayString[1] = Integer.toString(number);
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("createSprint", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        SprintDB sprintDB = gson.fromJson(json, SprintDB.class);
        return sprintDB.convertToSprint();

    }

    public int getSprintId(String projectName, int sprintNumber) {
        String[] arrayString = new String[2];
        arrayString[0] = projectName;
        arrayString[1] = Integer.toString(sprintNumber);
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("getSprintId", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        int sprintId = gson.fromJson(json, int.class);
        return sprintId;
    }

    public SprintDB getSprint(String projectName, int sprintNumber) {
        String[] arrayString = new String[2];
        arrayString[0] = projectName;
        arrayString[1] = Integer.toString(sprintNumber);
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("getSprint", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        SprintDB sprintDB = gson.fromJson(json, SprintDB.class);
        return sprintDB;
    }

    public SprintDB setUserForBacklog(String username, String id) {
        String[] arrayString = new String[2];
        arrayString[0] = username;
        arrayString[1] = id;
        String json = gson.toJson(arrayString);

        jsonObject jsonObject = new jsonObject("setUserForBacklog", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        SprintDB sprint = gson.fromJson(json, SprintDB.class);
        return sprint;
    }

    public SprintDB updateActualTime(String id, String actualTime) {
        String[] arrayString = new String[2];
        arrayString[0] = id;
        arrayString[1] = actualTime;
        String json = gson.toJson(arrayString);

        jsonObject jsonObject = new jsonObject("updateActualTime", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        SprintDB sprint = gson.fromJson(json, SprintDB.class);
        return sprint;
    }

    public BackLogItemDB setSprintId(int backlogId, int sprintId) {
        String[] arrayString = new String[2];
        arrayString[0] = Integer.toString(backlogId);
        arrayString[1] = Integer.toString(sprintId);
        String json = gson.toJson(arrayString);

        jsonObject jsonObject = new jsonObject("setSprintId", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        BackLogItemDB bli = gson.fromJson(json, BackLogItemDB.class);
        return bli;
    }

    public UserStory addUserStory(String projectName, String description) {
        UserStoryDB us = new UserStoryDB(projectName, description);
        String json = gson.toJson(us);
        jsonObject jsonObject = new jsonObject("addUserStory", json);
        json = gson.toJson(jsonObject);
        System.out.println("Received json: " + json);
        json = socketConnection.sendInfo(json);
        UserStory receivedUserStory = gson.fromJson(json, UserStory.class);
        return receivedUserStory;
    }

    public UserStoryDB[] getUserStories(String pName) {
        String[] arrayString = new String[1];
        arrayString[0] = pName;
        String json = gson.toJson(arrayString);
        jsonObject jsonObject = new jsonObject("getUserStories", json);
        json = gson.toJson(jsonObject);
        json = socketConnection.sendInfo(json);
        UserStoryDB[] userStoryFromSockets = gson.fromJson(json, UserStoryDB[].class);
        return userStoryFromSockets;
    }

    public UserStoryDB[] editDescription(String projectName, String oldDescription, String newDescription) {
        String[] arrayString = new String[3];
        arrayString[0] = projectName;
        arrayString[1] = oldDescription;
        arrayString[2] = newDescription;
        String json = gson.toJson(arrayString);

        jsonObject jsonObject = new jsonObject("editDescriptionUserStory", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        UserStoryDB[] us = gson.fromJson(json, UserStoryDB[].class);
        return us;
    }

    public BackLogItemDB editDescriptionBacklogItem(String backlogId, String description) {
        String[] stringArray = new String[2];
        stringArray[0] = backlogId;
        stringArray[1] = description;
        String json = gson.toJson(stringArray);
        jsonObject jsonObject = new jsonObject("editDescriptionBacklogItem",json);
        json = gson.toJson(jsonObject);
        System.out.println(jsonObject);
        json= socketConnection.sendInfo(json);
        BackLogItemDB backLogItemDB = gson.fromJson(json,BackLogItemDB.class);
        return  backLogItemDB;


    }

    public UserStory editNumber(String projectName, String o, String n) {
        String[] arrayString = new String[3];
        arrayString[0] = projectName;
        arrayString[1] = o;
        arrayString[2] = n;
        String json = gson.toJson(arrayString);

        jsonObject jsonObject = new jsonObject("editNumber", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        UserStory us = gson.fromJson(json, UserStory.class);
        return us;
    }

    public String[] getAllUsernames()    {
        String json = gson.toJson("An empty json");
        jsonObject jsonObject = new jsonObject("getAllUsernames", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        String[] usernames = gson.fromJson(json, String[].class);
        return usernames;
    }

    public String[] getAllProjectNames()    {
        String json = gson.toJson("An empty json");
        jsonObject jsonObject = new jsonObject("getAllProjectNames", json);
        json = gson.toJson(jsonObject);
        System.out.println(json);
        json = socketConnection.sendInfo(json);
        String[] projectNames = gson.fromJson(json, String[].class);
        return projectNames;
    }
}
