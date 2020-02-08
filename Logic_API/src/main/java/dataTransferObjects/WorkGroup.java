package dataTransferObjects;

import java.util.ArrayList;

public class WorkGroup
{
    private User Creator;
    private User ScrumMaster;
    private User ProductOwner;
    private String Name;
    private ArrayList<User> Developers;

    public WorkGroup(String projectname, User creator)    {
        Creator = creator;
        Name = projectname;
    }

    public WorkGroup(String projectname)
    {
        Name = projectname;
    }

    public WorkGroup()    {
    }

    public WorkGroup(User creator, User scrumMaster, User productOwner, String name, ArrayList<User> devs)  {
        Creator = creator;
        ScrumMaster = scrumMaster;
        ProductOwner = productOwner;
        Name = name;
        Developers = devs;
    }

    public User getCreator()
    {
        return Creator;
    }

    public User getScrumMaster()
    {
        return ScrumMaster;
    }

    public User getProductOwner()
    {
        return ProductOwner;
    }

    public String getName()
    {
        return Name;
    }

    public void setCreator(User creator)
    {
        this.Creator = creator;
    }

    public void setScrumMaster(User scrumMaster)
    {
        this.ScrumMaster = scrumMaster;
    }

    public void setProductOwner(User productOwner)
    {
        this.ProductOwner = productOwner;
    }

    public void setName(String name)
    {
        this.Name = name;
    }

    public void addDeveloper(User e)
    {
        Developers.add(e);
    }

    public ArrayList<User> getAllDevelopers()
    {
        return Developers;
    }

    public User getDeveloper(int i)
    {
       return Developers.get(i);
    }
}
