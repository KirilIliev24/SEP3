package sockets;

import java.util.ArrayList;

public class Json
{
    public static byte[] trimByte(byte[] bytes)    {
        ArrayList<Byte> bytelist = new ArrayList<Byte>();
        for (byte responseByte : bytes)
        {
            if (responseByte == 0) break;
            bytelist.add(responseByte);
        }
        byte[] trimmedBytes = new byte[bytelist.size()];
        for (int i = 0; i < bytelist.size(); i++)
        {
            trimmedBytes[i] = bytelist.get(i);
        }
        return trimmedBytes;
    }
}
