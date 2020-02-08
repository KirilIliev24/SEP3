package dataTransferObjectsDB;

import dataTransferObjects.Project;
import dataTransferObjects.UserStory;

public class UserStoryDB
{   private int Id;
    private String Description;
    private String Name;

    public UserStoryDB(String description)
    {
        this.Description = description;
    }

    public UserStoryDB(String projectName, String description)    {
        this.Name = projectName;
        this.Description = description;

    }

    public String getDescription()
    {
        return Description;
    }

    public void setDescription(String description)
    {
        this.Description = description;
    }

    public int getId()
    {
        return Id;
    }

    public void setId(int id)
    {
        Id = id;
    }

    public String getName()
    {
        return Name;
    }

    public void setName(String name)
    {
        Name = name;
    }

    public UserStory convertToUserStory()
    {
        return new UserStory(Description);
    }
}
