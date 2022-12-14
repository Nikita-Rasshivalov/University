package com.example.secondapp.model;

import android.os.Parcel;
import android.os.Parcelable;

public class ShowCarByYearWithPriceModel implements Parcelable {
    private double price;
    private int releaseYear;

    public ShowCarByYearWithPriceModel(double price, int releaseYear){
        this.price = price;
        this.releaseYear = releaseYear;
    }

    public ShowCarByYearWithPriceModel(Parcel in) {
        String[] data = new String[2];
        in.readStringArray(data);
        this.price = Integer.parseInt(data[0]);
        this.releaseYear = Integer.parseInt(data[1]);
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public int getReleaseYear() {
        return releaseYear;
    }

    public void setReleaseYear(int releaseYear) {
        this.releaseYear = releaseYear;
    }


    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeStringArray(new String[] { Integer.toString(releaseYear), Integer.toString(releaseYear) });
    }

    public static final Parcelable.Creator<ShowCarByYearWithPriceModel> CREATOR = new Parcelable.Creator<ShowCarByYearWithPriceModel>() {

        @Override
        public ShowCarByYearWithPriceModel createFromParcel(Parcel source) {
            return new ShowCarByYearWithPriceModel(source);
        }

        @Override
        public ShowCarByYearWithPriceModel[] newArray(int size) {
            return new ShowCarByYearWithPriceModel[size];
        }
    };
}
