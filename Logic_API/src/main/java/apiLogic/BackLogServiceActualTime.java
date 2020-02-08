package apiLogic;

import com.google.gson.Gson;

import dataTransferObjects.Sprint;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/backlogitemsActualTime")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)

public class BackLogServiceActualTime {
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @PUT
    public String updateActualTime(@QueryParam("id") String id, @QueryParam("actualTime") String actualTime, String body)    {
        Sprint fromJsonItem = dataModel.updateActualTime(id, actualTime);
        String json = gson.toJson(fromJsonItem);
        return json;
    }
}
