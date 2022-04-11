package com.company.data;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import models.Car;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class CarRepository implements IRepository<Car>
{
    private String _path;

    public CarRepository(String path) {
        _path = path;
    }

    public int Create(Car entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            int id;
            List<Car> cars = new ArrayList<>();
            if (file.exists()) {
                cars = mapper.readValue(file, new TypeReference<List<Car>>() {
                });
                id = cars.stream().count() > 0 ? cars.stream().max(Comparator.comparing(Car::getId)).get().getId() + 1 : 1;
            } else {
                id = 1;
            }
            entity.setId(id);
            cars.add(entity);
            mapper.writeValue(file, cars);
            return id;
        } catch (Exception e) {
        }
        return -1;
    }

    public boolean Update(Car entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            var cars = mapper.readValue(file, new TypeReference<List<Car>>() {
            });
            var index = GetIndex(cars, entity.getId());
            if (index > 0) {
                cars.remove(index);
                cars.add(index, entity);
                mapper.writeValue(file, cars);
                return true;
            }
        } catch (Exception e) {
        }
        return false;
    }

    public List<Car> Get() {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        try {
            var file = new File(_path);
            return mapper.readValue(file, new TypeReference<List<Car>>() {
            });
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }

        return new ArrayList<>();
    }
}
