package dataTransferObjects;

import java.util.ArrayList;

public class Project
{
    private ArrayList<UserStory> UserStories;
    private ArrayList<Sprint> Sprints;
    private WorkGroup WorkGroup;
    private String Name;
    private ArrayList<BackLogItem> BackLogItems;

    public Project()    {
    }

    public Project(String name)
    {
        this.Name = name;
    }

    public Project(String name, WorkGroup workGroup , ArrayList<BackLogItem> BackLogItems, ArrayList<UserStory> userStories, ArrayList<Sprint> sprints) {
        Name = name;
        WorkGroup = workGroup;
        this.BackLogItems = BackLogItems;
        this.UserStories = userStories;
        Sprints = sprints;
    }

    public void addUserStory(UserStory us)
    {
        UserStories.add(us);
    }

    public UserStory getUserStory(int i)
    {
        return UserStories.get(i);
    }

    public void addSprint(Sprint s)
    {
        Sprints.add(s);
    }

    public Sprint getSprint(int i)
    {
        return Sprints.get(i);
    }

    public WorkGroup getWorkGroup()
    {
        return WorkGroup;
    }

    public void setWorkGroup(WorkGroup workGroup)
    {
        this.WorkGroup = workGroup;
    }

    public String getName()
    {
        return Name;
    }

    public void setName(String name)
    {
        this.Name = name;
    }
}
