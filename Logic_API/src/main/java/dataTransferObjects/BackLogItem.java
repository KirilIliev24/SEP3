package dataTransferObjects;

public class BackLogItem
{
    private int Id;
    private String Priority;
    private String Description;
    private int EstimatedTime;
    private String Status;
    private User WorksOnIt;
    private int ActualTime;

    public BackLogItem()    {
    }

    public BackLogItem(String priority, String status, int estimatedTime, int actualTime)    {
        this.Priority = priority;
        this.Status = status;
        this.EstimatedTime = estimatedTime;
        this.ActualTime = actualTime;
    }

    public BackLogItem(String description, String status, int estimatedTime)    {
        Description = description;
        Status = status;
        EstimatedTime = estimatedTime;
    }

    public BackLogItem(int id, String priority, String description, int estimatedTime, String status, User worksOnIt, int actualTime)    {
        Id = id;
        Priority = priority;
        Description = description;
        EstimatedTime = estimatedTime;
        Status = status;
        WorksOnIt = worksOnIt;
        ActualTime = actualTime;
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
}