package com.example.secondapp;

import androidx.appcompat.app.AppCompatActivity;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.secondapp.data.DataProvider;
import com.example.secondapp.helper.Helper;
import com.example.secondapp.model.CarModel;

public class CreateCar extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_car);

        Button createButton = (Button) findViewById(R.id.createButton);
        EditText stampText = (EditText) findViewById(R.id.stamp);
        EditText colorText = (EditText) findViewById(R.id.color);
        EditText modelText = (EditText) findViewById(R.id.model);
        EditText priceText = (EditText) findViewById(R.id.price);
        EditText regNumText = (EditText) findViewById(R.id.reg_number);
        EditText yearText = (EditText) findViewById(R.id.year);

        createButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String errorMessage = "";
                String stamp = stampText.getText().toString();
                String color = colorText.getText().toString();
                String model = modelText.getText().toString();
                String reg_num = regNumText.getText().toString();
                Integer price = Helper.parseIntOrNull(priceText.getText().toString());
                Integer year = Helper.parseIntOrNull(yearText.getText().toString());

                if(stamp.trim().isEmpty()){
                    errorMessage += "stamp cannot be empty\n";
                }

                if(color.trim().isEmpty()){
                    errorMessage += "color cannot be empty\n";
                }

                if(model.trim().isEmpty()){
                    errorMessage += "model name cannot be empty\n";
                }

                if(price  == null){
                    errorMessage += "price cannot be empty\n";
                }

                if(reg_num.trim().isEmpty()){
                    errorMessage += "reg number cannot be empty\n";
                }

                if(year == null){
                    errorMessage += "Card number is not valid number\n";
                }
                if(errorMessage.isEmpty()) {
                    DataProvider.addCar(new CarModel(stamp,model,color,price,reg_num,year));
                    Intent intent = new Intent("com.example.secondapp.MAIN");
                    startActivity(intent);
                    finish();
                }
                else{
                    AlertDialog.Builder alert = new AlertDialog.Builder(CreateCar.this);
                    alert.setMessage("Car cannot be created:" + errorMessage);
                    alert.setCancelable(false);
                    alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {

                        }
                    });
                    alert.show();
                }
            }
        });
    }
}