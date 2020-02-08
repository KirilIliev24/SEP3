package dataTransferObjects;

public class UserStory
{
    private String Description;

    public UserStory(String description)
    {
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
}
