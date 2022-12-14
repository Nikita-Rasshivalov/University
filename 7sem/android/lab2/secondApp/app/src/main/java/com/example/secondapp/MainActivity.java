package com.example.secondapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.util.SparseArray;
import android.view.Gravity;
import android.view.View;
import android.view.autofill.AutofillValue;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.secondapp.data.DataProvider;
import com.example.secondapp.model.CarModel;
import com.example.secondapp.model.ShowCarByStampModel;
import com.example.secondapp.model.ShowCarByYearWithPriceModel;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    final int SHOW_CAR_BY_Stamp_CODE = 1;
    final int SHOW_CAR_BY_Y_P = 2;
    private TextView stampFilter;
    private TextView y_p_Filter;
    private static ShowCarByStampModel showCarByStamp;
    private static ShowCarByYearWithPriceModel showCarByYearWithPrice;
    private static boolean isInit = false;
    private static void InitData(){
        if(isInit){
            return;
        }
        isInit = true;
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 10000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 10000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 30000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 20000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 10000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 15000, "6666", 2022));
        DataProvider.addCar(new CarModel("Mers", "CLS", "black", 12000, "6666", 2022));
        DataProvider.addCar(new CarModel("BMW", "m5", "black", 10200, "6666", 2022));
        DataProvider.addCar(new CarModel("BMW", "m5", "black", 10500, "6666", 2022));
        DataProvider.addCar(new CarModel("BMW", "m5", "black", 10600, "6666", 2022));
        DataProvider.addCar(new CarModel("BMW", "m5", "black", 10100, "6666", 2022));
        DataProvider.addCar(new CarModel("BMW", "m5", "black", 10200, "6666", 2022));
        }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        InitData();
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        stampFilter = (TextView) findViewById(R.id.stampFilter);
        y_p_Filter = (TextView) findViewById(R.id.y_p_Filter);
        Button createButton = (Button) findViewById(R.id.createButton);
        createButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent("com.example.secondapp.CreateCar");
                startActivity(intent);
            }
        });

        Button showByNumberButton = (Button) findViewById(R.id.showByNumberButton);
        showByNumberButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent("com.example.secondapp.ShowCarByYearWithPrice");
                startActivityForResult(intent, SHOW_CAR_BY_Y_P);
            }
        });

        Button showByStampButton = (Button) findViewById(R.id.showByStampButton);
        showByStampButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent("com.example.secondapp.ShowCarByStamp");
                startActivityForResult(intent, SHOW_CAR_BY_Stamp_CODE);
            }
        });

        Button clearFiltersButton = (Button) findViewById(R.id.clearFiltersButton);
        clearFiltersButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                showCarByStamp = null;
                showCarByYearWithPrice = null;
                stampFilter.setText("Stamp filter: None");
                y_p_Filter.setText("Number filter: None");
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (data == null) {
            return;
        }
        if(requestCode == SHOW_CAR_BY_Stamp_CODE) {
            showCarByStamp = (ShowCarByStampModel)data.getParcelableExtra("model");
            stampFilter.setText("Stamp filter: " + showCarByStamp.getStamp());
        }

        if(requestCode == SHOW_CAR_BY_Y_P) {
            showCarByYearWithPrice = (ShowCarByYearWithPriceModel) data.getParcelableExtra("model");
            y_p_Filter.setText("Year and price filter: ");
        }
    }

    @Override
    protected void onResume() {
        reloadPatientsTable();
        super.onResume();
    }

    private void reloadPatientsTable(){
        TableLayout carsTable = (TableLayout) findViewById(R.id.carsTable);
        carsTable.removeViews(1, Math.max(0, carsTable.getChildCount() - 1));
        int number = 1;
        SparseArray<AutofillValue> arr = new SparseArray<AutofillValue>();
        List<CarModel> filteredCars = new ArrayList<>();
        for (CarModel car: DataProvider.getCars()) {
            boolean add = true;
            if(showCarByStamp != null
                    && !car.getStamp().equals(showCarByStamp.getStamp())){
                add = false;
            }
            if(showCarByYearWithPrice != null
                    && (car.getReleaseYear() != showCarByYearWithPrice.getReleaseYear()
                    || car.getPrice() < showCarByYearWithPrice.getPrice())){
                add = false;
            }

            if(add){
                filteredCars.add(car);
            }
        }

        for (CarModel car: filteredCars) {
            TableRow tableRow = new TableRow(this);
            tableRow.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Intent intent = new Intent("com.example.secondapp.ViewCar");
                    intent.putExtra("CAR_ID", car.getId());
                    startActivity(intent);
                }
            });
            tableRow.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT));
            tableRow.addView(getRowView(Integer.toString(number++), 1f));
            tableRow.addView(getRowView(car.getStamp(), 2f));
            tableRow.addView(getRowView(car.getModel(), 2f));
            tableRow.addView(getRowView(car.getColor(), 2f));
            tableRow.addView(getRowView(Integer.toString(car.getPrice()), 2f));
            tableRow.addView(getRowView(Integer.toString(car.getReleaseYear()), 2f));
            carsTable.addView(tableRow);
        }
    }

    TextView getRowView(String value, float weight){
        TextView textView = new TextView(this);
        textView.setText(value);
        textView.setLayoutParams(new TableRow.LayoutParams(0, TableRow.LayoutParams.WRAP_CONTENT, weight));
        textView.setGravity(Gravity.CENTER_HORIZONTAL);
        textView.setTextColor(Color.rgb(98, 0, 238));

        return textView;
    }
}