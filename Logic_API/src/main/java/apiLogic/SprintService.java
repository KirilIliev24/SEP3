package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.BackLogItem;
import dataTransferObjects.Sprint;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/sprints")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class SprintService
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @PUT
    public String setSprintId(@QueryParam("blId") String backlogId, @QueryParam("sprintId") String sprintId)    {
        int id = gson.fromJson(backlogId,int.class);
        int sprintid = gson.fromJson(sprintId,int.class);
        BackLogItem fromJsonItem = dataModel.setSprintId(id, sprintid);
        String json = gson.toJson(fromJsonItem);
        return json;
    }

    @POST
    public String createSprint(@QueryParam("projectName") String projectName, String backlogIdArray)    {
        int[] ids = gson.fromJson(backlogIdArray, int[].class);
        Sprint sprint = dataModel.createSprint(projectName,ids);
        String json = gson.toJson(sprint);
        System.out.println("8 GB of RAM not enough :'(");
        return json;
    }
}
