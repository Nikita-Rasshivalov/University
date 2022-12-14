package com.example.secondapp;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
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

public class ViewCar extends AppCompatActivity {

    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_car);
        int carId = (int)getIntent().getSerializableExtra("CAR_ID");
        CarModel car = DataProvider.getById(carId);
        if(car == null){
            AlertDialog.Builder alert = new AlertDialog.Builder(ViewCar.this);
            alert.setMessage("car no longer exist");
            alert.setCancelable(false);
            alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialogInterface, int i) {
                    goToMain();
                }
            });
            alert.show();
        }
        else {
            EditText stampText = (EditText) findViewById(R.id.stamp);
            stampText.setText(car.getStamp());
            EditText colorText = (EditText) findViewById(R.id.color);
            colorText.setText(car.getColor());
            EditText modelText = (EditText) findViewById(R.id.model);
            modelText.setText(car.getModel());
            EditText priceText = (EditText) findViewById(R.id.price);
            priceText.setText(Integer.toString(car.getReleaseYear()));
            EditText reg_numberText = (EditText) findViewById(R.id.reg_number);
            reg_numberText.setText(car.getRegNumber());
            EditText yearText = (EditText) findViewById(R.id.year);
            yearText.setText(Integer.toString(car.getReleaseYear()));

            Button saveButton = (Button) findViewById(R.id.saveButton);
            saveButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    String errorMessage = "";
                    String stamp = stampText.getText().toString();
                    String model = modelText.getText().toString();
                    String color = colorText.getText().toString();
                    String reg = reg_numberText.getText().toString();
                    Integer year = Helper.parseIntOrNull(yearText.getText().toString());
                    Integer price = Helper.parseIntOrNull(priceText.getText().toString());

                    if(stamp.trim().isEmpty()){
                        errorMessage += "stamp cannot be empty\n";
                    }

                    if(model.trim().isEmpty()){
                        errorMessage += "model cannot be empty\n";
                    }

                    if(color.trim().isEmpty()){
                        errorMessage += "color name cannot be empty\n";
                    }

                    if(reg.trim().isEmpty()){
                        errorMessage += "reg number cannot be empty\n";
                    }

                    if(price == null){
                        errorMessage += "price cannot be empty\n";
                    }

                    if(year == null){
                        errorMessage += "year  is not valid number\n";
                    }


                    if(errorMessage.isEmpty()) {
                        try {
                            CarModel carToUpdate = new CarModel(stamp, model, color, price, reg, year);
                            carToUpdate.setId(carId);
                            DataProvider.updateCar(carToUpdate);
                            AlertDialog.Builder alert = new AlertDialog.Builder(ViewCar.this);
                            alert.setMessage("Car updated");
                            alert.setCancelable(false);
                            alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                }
                            });
                            alert.show();
                        }
                        catch(Exception e){
                            AlertDialog.Builder alert = new AlertDialog.Builder(ViewCar.this);
                            alert.setMessage("Car cannot be updated:" + e.getMessage());
                            alert.setCancelable(false);
                            alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                    goToMain();
                                }
                            });
                            alert.show();
                        }
                    }
                    else{
                        AlertDialog.Builder alert = new AlertDialog.Builder(ViewCar.this);
                        alert.setMessage("Car cannot be saved:" + errorMessage);
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

            Button deleteButton = (Button) findViewById(R.id.deleteButton);
            deleteButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    AlertDialog.Builder alert = new AlertDialog.Builder(ViewCar.this);
                    alert.setMessage("Are you sure?");
                    alert.setCancelable(false);
                    alert.setNegativeButton("cancel", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {

                        }
                    });
                    alert.setPositiveButton("yes", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            DataProvider.deleteById(carId);
                            goToMain();
                        }
                    });
                    alert.show();
                }
            });
        }
    }

    private void goToMain(){
        Intent intent = new Intent("com.example.secondapp.MAIN");
        startActivity(intent);
        finish();
    }
}