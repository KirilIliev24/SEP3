package dataTransferObjects;

import java.io.Serializable;

public class User implements Serializable {

    private String firstName;
    private String lastName;
    private String username;
    private String email;
    private String password;

    public User()    {
    }

    public User(String username, String password)    {
        firstName = "not_given";
        lastName = "not_given";
        this.username = username;
        this.password = password;
        email = "not_given";
    }

    public User(String firstName, String lastName, String username, String email, String password)    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.username = username;
        this.email = email;
        this.password = password;
    }

    public String getFirstName()
    {
        return firstName;
    }

    public String getLastName()
    {
        return lastName;
    }

    public String getUsername()
    {
        return username;
    }

    public String getEmail()
    {
        return email;
    }

    public String getPassword()
    {
        return password;
    }

    public void setFirstName(String firstName)
    {
        this.firstName = firstName;
    }

    public void setLastName(String lastName)
    {
        this.lastName = lastName;
    }

    public void setUsername(String username)
    {
        this.username = username;
    }

    public void setEmail(String email)
    {
        this.email = email;
    }

    public void setPassword(String password)
    {
        this.password = password;
    }
}
