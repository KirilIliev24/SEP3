package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.BackLogItem;
import dataTransferObjects.Project;
import dataTransferObjects.User;
import model.DataModel;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;


@Path("/project")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class ProjectService {
    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @GET
    public String getProjects(@QueryParam("username") String username){
        Project[] projectsForUser = dataModel.getUserProject(username);
        String json = gson.toJson(projectsForUser);
        return json;
    }

    @POST
    public String createProject(@QueryParam("username") String username, String projectName){
        String pName = gson.fromJson(projectName, String.class);
        Project project = dataModel.createUserProject(username, pName);
        if(project == null)
        {
            String json1 = gson.toJson("A project with that name already exists");
            return json1;
        }
        String json = gson.toJson(project);
        return json;
    }
}
