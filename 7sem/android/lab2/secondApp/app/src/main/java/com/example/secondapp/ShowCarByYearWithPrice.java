package com.example.secondapp;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import com.example.secondapp.helper.Helper;
import com.example.secondapp.model.ShowCarByYearWithPriceModel;

public class ShowCarByYearWithPrice extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_car_by_year_price);
        EditText yearText = (EditText) findViewById(R.id.year);
        EditText priceText = (EditText) findViewById(R.id.price);
        Button showButton = (Button) findViewById(R.id.showButton);
        showButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent();
                Integer year = Helper.parseIntOrNull(yearText.getText().toString());
                Integer price = Helper.parseIntOrNull(priceText.getText().toString());
                if(year == null || price == null){
                    AlertDialog.Builder alert = new AlertDialog.Builder(ShowCarByYearWithPrice.this);
                    alert.setMessage("Invalid input");
                    alert.setCancelable(false);
                    alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {

                        }
                    });
                    alert.show();
                }
                intent.putExtra("model", new ShowCarByYearWithPriceModel(price.intValue(),year.intValue()));
                setResult(RESULT_OK, intent);
                finish();
            }
        });
    }
}