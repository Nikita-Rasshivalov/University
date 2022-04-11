package com.company.data;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import models.Trip;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class TripRepository implements IRepository<Trip> {
    private String _path;

    public TripRepository(String path) {
        _path = path;
    }

    public int Create(Trip entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            int id;
            List<Trip> trips = new ArrayList<>();
            if (file.exists()) {
                trips = mapper.readValue(file, new TypeReference<List<Trip>>() {
                });
                id = trips.stream().count() > 0 ? trips.stream().max(Comparator.comparing(Trip::getId)).get().getId() + 1 : 1;
            } else {
                id = 1;
            }
            entity.setId(id);
            trips.add(entity);
            mapper.writeValue(file, trips);
            return id;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return -1;
    }

    public boolean Update(Trip entity) {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        var file = new File(_path);
        try {
            var trips = mapper.readValue(file, new TypeReference<List<Trip>>() {
            });
            var index = GetIndex(trips, entity.getId());
            if (index >= 0) {
                trips.remove(index);
                trips.add(index, entity);
                mapper.writeValue(file, trips);
                return true;
            }
        } catch (Exception e) {
            e.printStackTrace();
            System.out.println(e.getMessage());
        }
        return false;
    }

    public List<Trip> Get() {
        var mapper = new ObjectMapper();
        mapper.setVisibility(PropertyAccessor.FIELD, JsonAutoDetect.Visibility.ANY);
        try {
            var file = new File(_path);
            return mapper.readValue(file, new TypeReference<List<Trip>>() {
            });
        } catch (IOException e) {
        }

        return new ArrayList<>();
    }
}
