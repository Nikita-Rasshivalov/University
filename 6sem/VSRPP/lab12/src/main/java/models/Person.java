package models;

import com.fasterxml.jackson.annotation.JsonIgnore;

public abstract class Person extends Entity {
    private String _name;
    private String _surname;
    private String _username;
    private String _password;

    public Person() {
    }

    public Person(String username, String password, String name, String surname) {
        _username = username;
        _password = password;
        _name = name;
        _surname = surname;
    }

    @JsonIgnore
    public abstract PersonType getType();

    @JsonIgnore
    public String getName() {
        return _name;
    }

    public void setName(String name) {
        _name = name;
    }

    @JsonIgnore
    public String getSurname() {
        return _surname;
    }

    public void setSurname(String surname) {
        _surname = surname;
    }

    @JsonIgnore
    public String getUsername() {
        return _username;
    }
    @JsonIgnore
    public void setUsername(String username) {
        _username = username;
    }

    @JsonIgnore
    public String getPassword() {
        return _password;
    }
    @JsonIgnore
    public void setPassword(String password) {
        _password = password;
    }

    @Override
    public String toString() {
        return getType()  + ", " + _name + " " + _surname;
    }
}

