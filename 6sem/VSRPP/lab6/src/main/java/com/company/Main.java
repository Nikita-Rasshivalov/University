package com.company;

import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import services.BaseService;

import java.io.IOException;

public class Main {
    private static String personsStoragePath = System.getProperty("user.dir") + "\\files\\persons.json";
    private static String tripsStoragePath = System.getProperty("user.dir") + "\\files\\trips.json";
    private static String carsStoragePath = System.getProperty("user.dir") + "\\files\\cars.json";


    public static void main(String[] args) throws IOException {
        var personRepository = new PersonRepository(personsStoragePath);
        var tripsRepository = new TripRepository(tripsStoragePath);
        var carRepository = new CarRepository(carsStoragePath);
        var service = new BaseService(personRepository, tripsRepository,carRepository);
        service.ShowMenu();
    }
}
