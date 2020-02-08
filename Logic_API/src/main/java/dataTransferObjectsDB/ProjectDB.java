package dataTransferObjectsDB;

import dataTransferObjects.*;

import java.util.ArrayList;

public class ProjectDB {
    public WorkGroupDB WorkGroup;
    public String Name;
    public ArrayList<BackLogItemDB> BackLogItems;
    public ArrayList<UserStoryDB> UserStories;
    public ArrayList<SprintDB> Sprints;

    public ProjectDB()    {

    }

    public ProjectDB(String name)    {
        Name = name;

    }

    public ProjectDB(String name, WorkGroupDB workgroup)    {
        Name = name;
        WorkGroup = workgroup;
    }

    public ProjectDB(String name, WorkGroupDB workgroup,ArrayList<BackLogItemDB> backLogItems, ArrayList<UserStoryDB> userStories,ArrayList<SprintDB> sprints) {
        Name = name;
        WorkGroup = workgroup;
        BackLogItems = backLogItems;
        UserStories = userStories;
        Sprints = sprints;
    }

    public WorkGroupDB getWorkGroup() {
        return WorkGroup;
    }

    public void setWorkGroup(WorkGroupDB workGroup) {
        WorkGroup = workGroup;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public Project convertToProject()    {
        ArrayList<BackLogItem> backLogItemArrayList = new ArrayList<>();
        if(BackLogItems != null)
        for(int i=0; i<BackLogItems.size(); i++)
            backLogItemArrayList.add(BackLogItems.get(i).convertToBackLogItem());

        ArrayList<UserStory> userStoryArrayList = new ArrayList<>();
        if(UserStories!= null)
        for(int i=0; i< UserStories.size(); i++)
            userStoryArrayList.add(UserStories.get(i).convertToUserStory());

        ArrayList<Sprint> sprintArrayList = new ArrayList<>();
        if(Sprints!= null)
            for(int i=0; i<Sprints.size(); i++)
                sprintArrayList.add(Sprints.get(i).convertToSprint());

        return  new Project(Name, WorkGroup.convertToWG(), backLogItemArrayList, userStoryArrayList, sprintArrayList);
    }
}
