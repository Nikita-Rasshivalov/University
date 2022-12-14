package com.example.secondapp.data;

import com.example.secondapp.model.CarModel;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class DataProvider {

    private static ArrayList<CarModel> cars = new ArrayList<>();

    public static void addCar(CarModel car){
        cars.add(car);
        car.setId(getNextId());
    }

    public static void updateCar(CarModel car){
        CarModel existingCar = getById(car.getId());
        if(existingCar == null){
            throw new IllegalArgumentException("This car no longer exist");
        }
        existingCar.setStamp(car.getStamp());
        existingCar.setColor(car.getColor());
        existingCar.setModel(car.getModel());
        existingCar.setPrice(car.getPrice());
        existingCar.setRegNumber(car.getRegNumber());
        existingCar.setReleaseYear(car.getReleaseYear());
    }

    public static List<CarModel> getCars(){
        return Collections.unmodifiableList(cars);
    }

    public static CarModel getById(int id){
        for (CarModel car: cars) {
            if(car.getId() == id){
                return car;
            }
        }

        return null;
    }

    public static void deleteById(int id){
        for(int i = 0; i<cars.size(); i++) {
            if(cars.get(i).getId() == id) {
                cars.remove(i);
                return;
            }
        }
    }



    private static int getNextId(){
        int max = cars.get(0).getId();
        for(int i = 1; i < cars.size(); i++){
            int currentId = cars.get(i).getId();
            if(currentId > max){
                max = currentId;
            }
        }

        return max + 1;
    }

}
