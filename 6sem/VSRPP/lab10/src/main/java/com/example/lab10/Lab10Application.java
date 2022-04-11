package com.example.lab10;

import functions.MulFunction;
import functions.SumFunction;
import org.json.simple.JSONObject;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import javax.management.InvalidApplicationException;

@SpringBootApplication
@RestController
public class Lab10Application {

    public static void main(String[] args) {
        SpringApplication.run(Lab10Application.class, args);
    }
    @GetMapping("/mul")
    public String getMul(String val1, String val2) throws InvalidApplicationException {
       try {
           int value1 = Integer.parseInt(val1);
           int value2 = Integer.parseInt(val2);
           var result = new MulFunction(value1,value2).execute();
           JSONObject jsonObject = new JSONObject();
           jsonObject.put("Result from mul", result);
           return String   .format(jsonObject.toString());
       }
       catch (Exception e ){
           JSONObject jsonObject = new JSONObject();
           jsonObject.put("Something went wrong:", e.getMessage());
           return String   .format(jsonObject.toString()  );
       }
    }
    @GetMapping("/sum")
    public String getSum() throws InvalidApplicationException {
        var result = new SumFunction(1,2,3).execute();
        System.out.println(result);
        return String.format("Result from sum: %s", result);
    }
}
