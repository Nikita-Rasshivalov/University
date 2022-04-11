
package models;

import com.fasterxml.jackson.annotation.JsonIgnore;

public abstract class Car extends Entity {
    private String _stamp;
    private String _number;
    private boolean _isBroken;
    private CarType _type;


    public Car() {
    }

    public Car(String stamp, String number, Boolean isBroken, CarType type) {
        _stamp = stamp;
        _number = number;
        _isBroken = isBroken;
        _type = type;
    }

    @JsonIgnore
    public abstract CarType getType();

    public String get_stamp() {
        return _stamp;
    }

    public void set_stamp(String _stamp) {
        this._stamp = _stamp;
    }

    public String get_number() {
        return _number;
    }

    public void set_number(String _number) {
        this._number = _number;
    }

    public boolean is_isBroken() {
        return _isBroken;
    }

    public void set_isBroken(boolean _isBroken) {
        this._isBroken = _isBroken;
    }

    public CarType get_type() {
        return _type;
    }

    public void set_type(CarType _type) {
        this._type = _type;
    }


    @Override
    public String toString() {

        return _type + ", " + _stamp + ", " + _number + ", " + _isBroken;
    }
}
