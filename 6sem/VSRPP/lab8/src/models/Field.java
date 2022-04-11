package models;

import java.util.LinkedList;

public abstract class Field {
    public static LinkedList<Field> Entities = new LinkedList<>();
    protected String _name;
    protected String _type;

    public Field(String name, String type) {
        _name = name;
        _type = type;
    }

    public void Add() {
        if (Entities.indexOf(this) == -1) {
            Entities.add(this);
        } else {
            System.out.println("Already added to list");
        }
    }

    public void Show() {
        System.out.println(_name + ", " + _type);
    }

    public static void ShowList() {
        for (var item : Entities) {
            item.Show();
        }
    }

}
