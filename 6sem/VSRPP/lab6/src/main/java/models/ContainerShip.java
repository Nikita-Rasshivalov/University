package models;

public class ContainerShip extends Car {
    private int _daysNumber;

    public ContainerShip() {
    }

    public ContainerShip(String stamp, String number, Boolean isBroken) {
        super(stamp, number, isBroken, CarType.ContainerShip);

    }


    @Override
    public CarType getType() {
        return CarType.ContainerShip;
    }
}
