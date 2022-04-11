package models;

public class Trip extends Entity {
    private Boolean _isCompleted;
    private String _date;
    private int _driverId;
    private int _carId;

    public Trip(){}
    public Trip(int carId,String date,int _driverId) {

        this._date = date;
        this._carId=carId;
        this._driverId = _driverId;
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

    public String get_date() {
        return _date;
    }

    public void set_date(String _date) {
        this._date = _date;
    }

    public Boolean get_isCompleted() {
        return _isCompleted;
    }

    public void set_isCompleted(Boolean _isCompleted) {
        this._isCompleted = _isCompleted;
    }

    @Override
    public String toString() {
        return  _date + ", " + _carId + ", " + _driverId;
    }
}
