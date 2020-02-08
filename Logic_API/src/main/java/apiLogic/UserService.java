package apiLogic;

import com.google.gson.Gson;
import dataTransferObjects.User;
import model.DataModel;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@Path("/users")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class UserService {

    private DataModel dataModel = new DataModel();
    private Gson gson = new Gson();

    @GET
    public String getUser(@QueryParam("username") String username, @QueryParam("password") String password){
        User user = dataModel.getUserObject(username, password);
        if(user == null)
        {
            String json1 = gson.toJson("User not found");
            return json1;
        }
        String json = gson.toJson(user);
        return json;
    }

    @POST
    public String newUser(String user)    {
        User newUser = gson.fromJson(user, User.class);
        newUser = dataModel.addUser(newUser);
        if(newUser == null)
        {
            String json1 = gson.toJson("Username already exists");
            return json1;
        }
        String json = gson.toJson(newUser);
        return json;
    }
}