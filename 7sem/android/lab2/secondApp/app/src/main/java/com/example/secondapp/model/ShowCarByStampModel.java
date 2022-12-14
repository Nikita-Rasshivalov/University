package com.example.secondapp.model;

import android.os.Parcel;
import android.os.Parcelable;

public class ShowCarByStampModel implements Parcelable {
    private String stamp;
    public ShowCarByStampModel(String stamp){
        this.stamp = stamp;
    }
    public ShowCarByStampModel(Parcel in) {
        String[] data = new String[1];
        in.readStringArray(data);
        stamp = data[0];
    }

    public String getStamp() {
        return stamp;
    }

    public void setStamp(String stamp) {
        this.stamp = stamp;
    }
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeStringArray(new String[] { stamp });
    }

    public static final Parcelable.Creator<ShowCarByStampModel> CREATOR = new Parcelable.Creator<ShowCarByStampModel>() {

        @Override
        public ShowCarByStampModel createFromParcel(Parcel source) {
            return new ShowCarByStampModel(source);
        }

        @Override
        public ShowCarByStampModel[] newArray(int size) {
            return new ShowCarByStampModel[size];
        }
    };
}
