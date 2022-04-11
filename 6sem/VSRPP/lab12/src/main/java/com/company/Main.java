package com.company;

import Utils.CarHandler;
import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import models.Car;
import org.xml.sax.SAXException;
import services.BaseService;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;
import java.io.File;
import java.io.IOException;
import java.util.List;

public class Main {
    private static String personsStoragePath = System.getProperty("user.dir") + "\\files\\persons.json";
    private static String tripsStoragePath = System.getProperty("user.dir") + "\\files\\trips.json";
    private static String carsStoragePath = System.getProperty("user.dir") + "\\files\\cars.json";

    private static String carsStorageXPath = System.getProperty("user.dir") + "\\files\\cars.xml";


    public static void main(String[] args) throws IOException {
        var personRepository = new PersonRepository(personsStoragePath);
        var tripsRepository = new TripRepository(tripsStoragePath);
        var carRepository = new CarRepository(carsStorageXPath);
        var service = new BaseService(personRepository, tripsRepository,carRepository);
        service.ShowMenu();
    }
}
