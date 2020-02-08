package dataTransferObjects;

public class jsonObject {
    private String functionName;
    private String argument;

    public jsonObject(String functionName, String argument){
        this.functionName = functionName;
        this.argument = argument;
    }

    public String getFunctionName() {
        return functionName;
    }

    public String getArgument() {
        return argument;
    }

    public void setFunctionName(String functionName) {
        this.functionName = functionName;
    }

    public void setArgument(String argument) {
        this.argument = argument;
    }
}
