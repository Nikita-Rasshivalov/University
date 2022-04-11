package com.company.data;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import models.Person;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class PersonRepository implements IRepository<Person> {

    private String _path;

    public PersonRepository(String path) {
        _path = path;
    }

    public int Create(Person entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            int id;
            List<Person> persons = new ArrayList<>();
            if (file.exists()) {
                persons = mapper.readValue(file, new TypeReference<List<Person>>() {
                });
                id = persons.stream().count() > 0 ? persons.stream().max(Comparator.comparing(Person::getId)).get().getId() + 1 : 1;
            } else {
                id = 1;
            }
            entity.setId(id);
            persons.add(entity);
            mapper.writeValue(file, persons);
            return id;
        } catch (Exception e) {
        }
        return -1;
    }

    public boolean Update(Person entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            var persons = mapper.readValue(file, new TypeReference<List<Person>>() {
            });
            var index = GetIndex(persons, entity.getId());
            if (index > 0) {
                persons.remove(index);
                persons.add(index, entity);
                mapper.writeValue(file, persons);
                return true;
            }
        } catch (Exception e) {
        }
        return false;
    }

    public List<Person> Get() {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        try {
            var file = new File(_path);
            return mapper.readValue(file, new TypeReference<List<Person>>() {
            });
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }

        return new ArrayList<>();
    }

}
