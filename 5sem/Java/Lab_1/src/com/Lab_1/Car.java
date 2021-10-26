package com.Lab_1;

import java.util.ArrayList;
import java.util.List;

public class Car {

    int CarId ;
    String Stamp;
    String Model;
    int ReleaseYear;
    String Color;
    double Price;
    String Number;

    public Car(int carId, String stamp, String model, int releaseYear, String color, double price, String number){
        setCarId(carId);
        setStamp(stamp);
        setModel(model);
        setReleaseYear(releaseYear);
        setColor(color);
        setPrice(price);
        setNumber(number);
    }

    public int getCarId() {
        return CarId;
    }

    public void setCarId(int carId) {
        CarId = carId;
    }

    public String getStamp() {
        return Stamp;
    }

    public void setStamp(String stamp) {
        Stamp = stamp;
    }

    public String getModel() {
        return Model;
    }

    public void setModel(String model) {
        Model = model;
    }

    public int getReleaseYear() {
        return ReleaseYear;
    }

    public void setReleaseYear(int releaseYear) {
        ReleaseYear = releaseYear;
    }

    public String getColor() {
        return Color;
    }

    public void setColor(String color) {
        Color = color;
    }

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        Price = price;
    }

    public String getNumber() {
        return Number;
    }

    public void setNumber(String number) {
        Number = number;
    }

}
