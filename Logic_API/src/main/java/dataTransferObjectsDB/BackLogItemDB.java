package dataTransferObjectsDB;


import dataTransferObjects.BackLogItem;
import dataTransferObjects.Project;
import dataTransferObjects.Sprint;
import dataTransferObjects.User;

public class BackLogItemDB
{


    private int Id;
    private int SprintId;
    private String Name;
    private String Description;
    private String Priority;
    private String Status;
    private User WorksOnIt;
    private int EstimatedTime;
    private int ActualTime;


    public BackLogItemDB() {
    }

    public BackLogItemDB(String priority, String status, int estimatedTime, int actualTime)    {
        this.Priority = priority;
        this.Status = status;
        this.EstimatedTime = estimatedTime;
        this.ActualTime = actualTime;
    }

    public BackLogItemDB(String projectName, BackLogItem bli)    {
        Name = projectName;
        Description = bli.getDescription();
        Priority = bli.getPriority();
        EstimatedTime = bli.getEstimatedTime();
    }

    public BackLogItemDB(String description, String priority, int estimatedTime)     {
        Description = description;
        Priority=priority;
        EstimatedTime = estimatedTime;
    }

    public String getPriority()
    {
        return Priority;
    }

    public void setPriority(String priority)
    {
        Priority = priority;
    }

    public String getStatus()
    {
        return Status;
    }

    public void setStatus(String status)
    {
        Status = status;
    }

    public int getEstimatedTime()
    {
        return EstimatedTime;
    }

    public void setEstimatedTime(int estimatedTime)
    {
        EstimatedTime = estimatedTime;
    }

    public int getActualTime()
    {
        return ActualTime;
    }

    public void setActualTime(int actualTime)
    {
        ActualTime = actualTime;
    }

    public int getId()
    {
        return Id;
    }

    public void setId(int id)
    {
        Id = id;
    }

    public int getSprintId()
    {
        return SprintId;
    }

    public void setSprintId(int sprintId)
    {
        SprintId = sprintId;
    }

    public String getName()
    {
        return Name;
    }

    public void setName(String name)
    {
        Name = name;
    }

    public String getDescription()
    {
        return Description;
    }

    public void setDescription(String description)
    {
        Description = description;
    }

    public User getWorksOnIt()
    {
        return WorksOnIt;
    }

    public void setWorksOnIt(User worksOnIt)
    {
        WorksOnIt = worksOnIt;
    }

    public BackLogItem convertToBackLogItem() {
        return new BackLogItem(Id, Priority, Description, EstimatedTime, Status, WorksOnIt, ActualTime);
    }
}
