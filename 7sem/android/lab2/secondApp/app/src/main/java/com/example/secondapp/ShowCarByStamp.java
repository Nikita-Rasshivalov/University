package com.example.secondapp;

import androidx.appcompat.app.AppCompatActivity;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.secondapp.model.ShowCarByStampModel;

public class ShowCarByStamp extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_car_by_stamp);
        EditText stampText = (EditText) findViewById(R.id.stamp);
        Button showButton = (Button) findViewById(R.id.showButton);
        showButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent();
                String stamp = stampText.getText().toString();
                if(stamp.trim().isEmpty()) {
                    AlertDialog.Builder alert = new AlertDialog.Builder(ShowCarByStamp.this);
                    alert.setMessage("Stamp name cannot be null or empty");
                    alert.setCancelable(false);
                    alert.setPositiveButton("ok", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {

                        }
                    });
                    alert.show();
                }
                intent.putExtra("model", new ShowCarByStampModel(stamp));
                setResult(RESULT_OK, intent);
                finish();
            }
        });
    }
}