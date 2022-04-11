package models;

public class RepairOrder {
    private int _carId;
    private int _driverId;

    public RepairOrder(int carId, int driverId) {
        this._carId = carId;
        this._driverId = driverId;
    }
    public int get_carId() {
        return _carId;
    }

    public void set_carId(int _carId) {
        this._carId = _carId;
    }

    public int get_driverId() {
        return _driverId;
    }

    public void set_driverId(int _driverId) {
        this._driverId = _driverId;
    }


}
