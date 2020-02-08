package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.BackLogItem;
import dataTransferObjects.Project;
import dataTransferObjects.UserStory;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;

@Path("/roles")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class RolesService
{
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @PUT
    public String newRole(@QueryParam("username") String username, @QueryParam("projectName") String projectName, String role)    {
        String pRole = gson.fromJson(role, String.class);
        System.out.println(username + "-" + projectName + "-" + role);
        Project project = dataModel.setRole(username, projectName, pRole);
        String json = gson.toJson(project);
        return json;
    }
}
