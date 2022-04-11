package models;

public class Dispatcher extends Person {
    public Dispatcher() {
    }

    public Dispatcher(String username, String password, String name, String surname) {
        super(username, password, name, surname);
    }

    @Override
    public PersonType getType() {
        return PersonType.Dispatcher;
    }

}
