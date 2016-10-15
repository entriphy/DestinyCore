package com.evilarceus.destinycore;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import javax.swing.*;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by matthewfinerty on 10/15/16.
 */
public class Items {



    public static void main(String[] args) throws IOException {
        //Test code here
        infoBox("The item is " + hashToWeapon("2882093969"), "Result");
    }


    //Use this dialog box to test... stuff
    public static void infoBox(String infoMessage, String titleBar)
    {
        JOptionPane.showMessageDialog(null, infoMessage, "InfoBox: " + titleBar, JOptionPane.INFORMATION_MESSAGE);
    }

    public static String hashToWeapon(String hash) throws IOException
    {
        String url = "https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/" + hash + "/";

        URL obj = new URL(url);
        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod("GET");
        //con.setRequestProperty("X-API-KEY", apiKey);

        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
        String inputLine;
        String response = "";

        while ((inputLine = in.readLine()) != null) {
            response += inputLine;
        }

        in.close();

        JsonParser parser = new JsonParser();
        JsonObject json = (JsonObject) parser.parse(response);

        System.out.println(json.getAsJsonObject("Response").getAsJsonObject("data").getAsJsonObject("inventoryItem").get("itemName"));
        JsonElement result = json.getAsJsonObject("Response").getAsJsonObject("data").getAsJsonObject("inventoryItem").get("itemName");

        return result.toString().replaceAll("\"", "");

    }
}
