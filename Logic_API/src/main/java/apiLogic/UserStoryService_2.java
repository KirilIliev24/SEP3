package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.UserStory;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/userStory2")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class UserStoryService_2
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @PUT
    public String editNumber(@QueryParam("projectName") String projectName, String oldNumber, String newNumber) {
        String o = gson.fromJson(oldNumber, String.class);
        String n = gson.fromJson(newNumber, String.class);
        UserStory userStory = dataModel.editNumber(projectName, o, n);
        String json = gson.toJson(userStory);
        return json;
    }
}
