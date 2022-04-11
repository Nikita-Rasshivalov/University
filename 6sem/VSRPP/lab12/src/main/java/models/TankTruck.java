package models;

public class TankTruck extends Car {

    public TankTruck(){}
    public TankTruck(String stamp, String number, Boolean isBroken) {
        super(stamp, number, isBroken,CarType.TankTruck);

    }


    @Override
    public CarType getType() {
        return CarType.TankTruck;
    }
}
