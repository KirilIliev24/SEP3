package dataTransferObjects;

import java.util.ArrayList;

public class Sprint
{
    private int Number;

    private ArrayList<BackLogItem> BacklogItems;

    public Sprint()    {
    }

    public Sprint(int number)    {
        Number = number;

    }

    public Sprint(int number , ArrayList<BackLogItem> backlogItems)    {
        Number=number;
        BacklogItems=backlogItems;
    }

    public int getNumber()
    {
        return Number;
    }

    public void setNumber(int number)
    {
        Number = number;
    }

    public void addBackLogItem(BackLogItem bli)
    {
        BacklogItems.add(bli);
    }

    public BackLogItem getBackLogIte(int i)
    {
        return BacklogItems.get(i);
    }
}
