package com.Lab_1;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.List;

public class JsonParser {
    public Root parse(){
        Root root = new Root();
        JSONParser parser = new JSONParser();
        try( FileReader reader = new FileReader("Target/test.json")) {
           JSONObject rootJsonObject = (JSONObject) parser.parse(reader);
           String name = (String) rootJsonObject.get("name");
            JSONArray productsJsonArray = (JSONArray) rootJsonObject.get("products");
            List<Product> productList = new ArrayList<>();
            for (Object item:productsJsonArray){
                JSONObject productJsonObject = (JSONObject) item;
                String nameProduct = (String) productJsonObject.get("name");
                long cipherProduct = (long) productJsonObject.get("cipher");
            Product product = new Product(nameProduct,(int)cipherProduct);
            productList.add(product);
            }
           root.setName(name);
            root.setProducts(productList);
           return root;

        } catch (Exception e) {
            System.out.println("Parsing error" + e);
        }
        return null;
    }
}
