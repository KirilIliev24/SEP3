package model;

import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.Security;
import java.security.spec.InvalidKeySpecException;
import java.util.Base64;
import java.util.StringTokenizer;

public class EncryptPassword {

    private static final int iterations = 20000;
    private static final int saltLength = 32;
    private static final int desiredKeyLength = 256;

    public static String getHashedPassword(String password) {
        SecureRandom secureRandom = new SecureRandom();
        byte[] salt = secureRandom.generateSeed(saltLength);
        try {
            return new String(Base64.getEncoder().encode(salt)) + "$" + hash(password, salt);
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
            return "NoSuchAlgorithmException";
        } catch (InvalidKeySpecException e) {
            e.printStackTrace();
            return "InvalidKeySpecException";
        } catch (IllegalArgumentException e) {
            e.printStackTrace();
            return "IllegalArgumentException";
        }
    }

    public static boolean autenthicate(String password, String storedPassword){
        try {
            StringTokenizer tokenizer = new StringTokenizer(storedPassword, "$");
            final String salt = tokenizer.nextToken();
            final String saltedHash = tokenizer.nextToken();
            String hashOfInput = hash(password, Base64.getDecoder().decode(salt));
            return hashOfInput.equals(saltedHash);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return false;
    }

    private static String hash(String password, byte[] salt) throws IllegalArgumentException, NoSuchAlgorithmException, InvalidKeySpecException {
        if (password == null || password.length() == 0)
            throw new IllegalArgumentException("Empty passwords are not supported.");
        SecretKeyFactory f = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA1");
        SecretKey key = f.generateSecret(new PBEKeySpec(
                password.toCharArray(), salt, iterations, desiredKeyLength));
        return new String(Base64.getEncoder().encode(key.getEncoded()), StandardCharsets.UTF_8);
    }
}
