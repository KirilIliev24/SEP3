package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.BackLogItem;
import dataTransferObjects.Sprint;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/backlogitemsEditDescription")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class BackLogServiceEditDescription
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @PUT
    public String editDescriptionBacklogItem(@QueryParam("id") String backlogId, String description)    {
        String desc = gson.fromJson(description, String.class);
        BackLogItem  backLogItem = dataModel.editDescriptionBacklogItem(backlogId, desc);
        return gson.toJson(backLogItem);
    }
}
