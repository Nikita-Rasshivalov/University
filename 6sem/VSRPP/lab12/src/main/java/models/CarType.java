package models;

public enum CarType {
    ContainerShip,
    Refrigerator,
    TankTruck;

    public static CarType fromInteger(int x) {
        switch (x) {
            case 1:
                return ContainerShip;
            case 2:
                return Refrigerator;
            case 3:
                return TankTruck;
        }
        return null;
    }
}

