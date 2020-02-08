package dataTransferObjectsDB;

import dataTransferObjects.User;
import dataTransferObjects.WorkGroup;

import java.util.ArrayList;

public class WorkGroupDB {
    private int WorkGroupId;
    private ProjectDB Project;
    private User Creator;
    private User ScrumMaster;
    private User ProductOwner;
    private String Name;

    public ArrayList<User> Developers = new ArrayList<User>();

    public WorkGroupDB()    {

    }

    public WorkGroupDB(String projectname)
    {
        Name = projectname;
    }

    public WorkGroupDB(String projectname, User creator)    {
        Name = projectname;
        Creator = creator;
    }

    public User getCreator() {
        return Creator;
    }

    public void setCreator(User creator) {
        Creator = creator;
    }

    public User getScrumMaster() {
        return ScrumMaster;
    }

    public void setScrumMaster(User scrumMaster) {
        ScrumMaster = scrumMaster;
    }

    public User getProductOwner() {
        return ProductOwner;
    }

    public void setProductOwner(User productOwner) {
        ProductOwner = productOwner;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public ArrayList<User> getDevelopers() {
        return Developers;
    }

    public void setDevelopers(ArrayList<User> developers) {
        Developers = developers;
    }

    public WorkGroup convertToWG()
    {
        return new WorkGroup(Creator, ScrumMaster, ProductOwner, Name, Developers);
    }
}
