package models;

public class Metropolis extends Field {
    public Metropolis(String name, String type) {
        super(name, type);
    }

    @Override
    public void Show() {
        System.out.println(_name + ", " + _type);
    }
}
