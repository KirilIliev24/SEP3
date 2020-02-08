package sockets;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;
import java.nio.charset.StandardCharsets;

import static sockets.Json.*;

public class SocketConnection
{
    public String sendInfo(String jsonString)    {
        String received = null;
        try
        {
            DataOutputStream out;
            DataInputStream in;
            Socket socket = new Socket("localhost", 8888);
            out = new DataOutputStream(socket.getOutputStream());
            in = new DataInputStream(socket.getInputStream());

            byte[] send = new byte[250];
            String sendString = jsonString;
            send = sendString.getBytes("UTF8");
            out.write(send, 0, send.length);

            byte[] receive = new byte[50000];
            in.read(receive,0, receive.length);
            receive = trimByte(receive);
            received = new String(receive, StandardCharsets.UTF_8);
            System.out.println(received);

        } catch (UnknownHostException e)
        {
            e.printStackTrace();
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        return received;
    }
}
