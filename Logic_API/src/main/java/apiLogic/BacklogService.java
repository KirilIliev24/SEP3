package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.BackLogItem;
import dataTransferObjects.Sprint;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/backlogitems")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class BacklogService
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @POST
    public String createBacklogItem(@QueryParam("projectName") String projectName, String backLogItem)    {
        BackLogItem newBackLogItem = gson.fromJson(backLogItem, BackLogItem.class);
        String receivedJson = gson.toJson(dataModel.createBacklogItem(projectName, newBackLogItem));
        if(receivedJson == null)
        {
            return gson.toJson("Not all fields are filled");
        }
        return receivedJson;
    }

    @PUT
    public String setUserForBacklog(@QueryParam("username") String username, @QueryParam("id") String id, String body)    {
        String aa = gson.fromJson(body, String.class);
        Sprint fromJsonItem = dataModel.setUserForBacklog(username, id);
        String json = gson.toJson(fromJsonItem);
        System.out.println(json);
        return json;
    }
}
