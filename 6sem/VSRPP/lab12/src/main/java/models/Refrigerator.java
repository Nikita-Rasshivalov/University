package models;

public class Refrigerator extends Car {

    public Refrigerator() {
    }

    public Refrigerator(String stamp, String number, Boolean isBroken) {
        super(stamp, number, isBroken,CarType.Refrigerator);

    }


    @Override
    public CarType getType() {
        return CarType.Refrigerator;
    }
}
