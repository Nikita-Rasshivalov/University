package models;


public class Admin extends Person {
    public Admin() {
    }

    public Admin(String username, String password, String name, String surname) {
        super(username, password, name, surname);
    }

    @Override
    public PersonType getType() {
        return PersonType.Admin;
    }
}
