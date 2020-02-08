package dataTransferObjectsDB;

import dataTransferObjects.BackLogItem;
import dataTransferObjects.Sprint;

import java.util.ArrayList;

public class SprintDB {
    public int SprintId;
    public int Number;
    public ArrayList<BackLogItemDB> BacklogItems = new ArrayList<>();
    public String Name;

    public SprintDB() {

    }

    public SprintDB(String name, int number, ArrayList<BackLogItemDB> backlogItems) {
        Name = name;
        Number = number;
        BacklogItems = backlogItems;
    }

    public int getNumber() {
        return Number;
    }

    public String getName() {
        return Name;
    }

    public void setNumber(int number) {
        Number = number;
    }

    public void setName(String name) {
        Name = name;
    }

    public Sprint convertToSprint()    {
        ArrayList<BackLogItem> backLogItemArrayList = new ArrayList<>();
        if(BacklogItems != null)
            for(int i=0; i<BacklogItems.size(); i++)
                backLogItemArrayList.add(BacklogItems.get(i).convertToBackLogItem());

        return new Sprint(Number,backLogItemArrayList);
    }
}