package models;

public class City extends Field {

    public City(String name, String type) {
        super(name, type);
    }

    @Override
    public void Show() {
        System.out.println(_name + ", " + _type);
    }
}
