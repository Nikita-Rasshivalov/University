package models;

public class Place extends Field {
    protected String _address;

    public Place(String name, String type, String address) {
        super(name, type);
        this._address = address;
    }

    @Override
    public void Show() {
        System.out.println(_name + ", " + _type + " Address " + _address);
    }
}
