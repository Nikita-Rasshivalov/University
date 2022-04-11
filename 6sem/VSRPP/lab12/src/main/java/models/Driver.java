package models;

public class Driver extends Person {
    private Boolean _isWorked;
    private int _tripId;
    private int _carId;
    public Driver() {
    }

    public Driver(String username, String password, String name, String surname) {
        super(username, password, name, surname);
    }

    public Boolean get_isWorked() {
        return _isWorked;
    }

    public void set_isWorked(Boolean _isWorked) {
        this._isWorked = _isWorked;
    }

    public int get_carId() {
        return _carId;
    }

    public void set_carId(int _carId) {
        this._carId = _carId;
    }

    public int get_tripId() {
        return _tripId;
    }

    public void set_tripId(int _tripId) {
        this._tripId = _tripId;
    }

    @Override
    public String toString() {
        return super.toString() + ", _isWorked: " + _isWorked +  ", _tripId: "+ _tripId+  ", _carId: "+ _carId ;
    }

    @Override
    public PersonType getType() {
        return PersonType.Driver;
    }

}
