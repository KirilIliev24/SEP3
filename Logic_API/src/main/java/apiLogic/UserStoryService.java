package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.UserStory;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/userStory")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class UserStoryService
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @POST
    public String addUserStory(@QueryParam("projectName") String projectName, String description) {
        String newUserStory = gson.fromJson(description, String.class);
        if (newUserStory == null)
        {
            return gson.toJson("Description is empty");
        }
        String receivedJson = gson.toJson(dataModel.addUserStory(projectName, newUserStory));
        return receivedJson;
    }

    @GET
    public String getUserStoriesForProject(String projectName) {
        String pName = gson.fromJson(projectName, String.class);
        UserStory[] userStories = dataModel.GetUserStoriesForProject(pName);
        String json = gson.toJson(userStories);
        return json;
    }

    @PUT
    public String editDescription(@QueryParam("projectName") String projectName, String description) {
        String[] descriptions = gson.fromJson(description,String[].class);
        UserStory[] userStories = dataModel.editUserStoryDescription(projectName,descriptions[0],descriptions[1]);
        String json = gson.toJson(userStories);
        return json;
    }


}
