package com.example.secondapp.model;

public class CarModel {
    private int id;
    private String model;
    private String color;
    private int price;
    private String regNumber;
    private int releaseYear;

    public CarModel(String stamp, String model, String color, int price, String regNumber, int releaseYear) {
        this.stamp = stamp;
        this.model = model;
        this.color = color;
        this.price = price;
        this.regNumber = regNumber;
        this.releaseYear = releaseYear;
    }

    private String stamp;

    public String getStamp() {
        return stamp;
    }

    public void setStamp(String stamp) {
        this.stamp = stamp;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public String getRegNumber() {
        return regNumber;
    }

    public void setRegNumber(String regNumber) {
        this.regNumber = regNumber;
    }

    public int getReleaseYear() {
        return releaseYear;
    }

    public void setReleaseYear(int releaseYear) {
        this.releaseYear = releaseYear;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
