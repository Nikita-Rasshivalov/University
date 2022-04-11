package com.company.data;

import Utils.CarHandler;
import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import models.Car;
import org.xml.sax.SAXException;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;
import javax.xml.stream.XMLOutputFactory;
import javax.xml.stream.XMLStreamWriter;
import java.io.File;
import java.io.FileWriter;
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


    public int CreateXml(Car entity) {

        var cars = GetXml();
        XMLOutputFactory xof = XMLOutputFactory.newInstance();
        XMLStreamWriter xsw = null;
        try {
            xsw = xof.createXMLStreamWriter(new FileWriter(_path));
            xsw.writeStartDocument();
            xsw.writeStartElement("cars");


            var id = cars.stream().count() > 0 ? cars.stream().max(Comparator.comparing(Car::getId)).get().getId() + 1 : 1;
            entity.setId(id);
            cars.add(entity);

            for (Car car : cars) {
                xsw.writeStartElement("car");
                xsw.writeAttribute("id", String.valueOf(id));
                xsw.writeStartElement("_stamp");
                xsw.writeCharacters(car.get_stamp());
                xsw.writeEndElement();
                xsw.writeStartElement("_number");
                xsw.writeCharacters(car.get_number());
                xsw.writeEndElement();
                xsw.writeStartElement("_isBroken");
                xsw.writeCharacters(String.valueOf(car.is_isBroken()));
                xsw.writeEndElement();
                xsw.writeStartElement("_type");
                xsw.writeCharacters(String.valueOf(car.get_type()));
                xsw.writeEndElement();
                xsw.writeEndElement();
            }

            xsw.writeEndElement();
            xsw.writeEndDocument();
            xsw.flush();
            return id;
        } catch (Exception e) {
            System.err.println("Unable to write the file: " + e.getMessage());
        } finally {
            try {
                if (xsw != null) {
                    xsw.close();
                }
            } catch (Exception e) {
                System.err.println("Unable to close the file: " + e.getMessage());
            }
            return -1;
        }
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

    public List<Car> GetXml() {
        SAXParserFactory saxParserFactory = SAXParserFactory.newInstance();
        try {
            SAXParser saxParser = saxParserFactory.newSAXParser();
            CarHandler handler = new CarHandler();
            saxParser.parse(new File(_path), handler);
            List<Car> carList = handler.getEmpList();
            return carList;
        } catch (ParserConfigurationException | SAXException | IOException e) {
            e.printStackTrace();
            return new ArrayList<>();
        }
    }
}
