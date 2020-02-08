package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.Project;
import dataTransferObjects.User;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/invitation")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class InvitationService {
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @GET
    public String getInvitations(@QueryParam("username") String username)    {
        String[] invitations = dataModel.getInvitationsByUsername(username);
        String json = gson.toJson(invitations);
        return json;
    }

    @PUT
    public String acceptInvitation(@QueryParam("username") String username, String projectName)    {
        String pName = gson.fromJson(projectName, String.class);
        Project project = dataModel.getAcceptedInvitation(username, pName);
        String json = gson.toJson(project);
        return json;
    }

    @POST
    public String inviteUserToProject(@QueryParam("username") String username, String projectName)    {
        String pName = gson.fromJson(projectName, String.class);
        User user = dataModel.inviteUserProject(username, pName);
        if(user == null)
        {
            String fail = "No such username in the system";
            String json1 = gson.toJson(fail);
            return json1;
        }
        String json = gson.toJson(user);
        return json;
    }

    @DELETE
    public String deleteUserInvitation(@QueryParam("username") String username, @QueryParam("projectName") String pName)    {
        User user = dataModel.deleteUserInvitation(username, pName);
        String json = gson.toJson(user);
        return json;
    }
}
