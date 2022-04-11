package Utils;

import models.Car;
import models.CarType;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import java.util.ArrayList;
import java.util.List;

public class CarHandler extends DefaultHandler {

    private boolean hasStamp = false;
    private boolean hasNumber = false;
    private boolean hasType = false;
    private boolean hasIsBroken = false;

    // List to hold Car object
    private List< Car > carList = null;
    private Car car = null;

    public List <Car> getEmpList() {
        return carList;
    }

    @Override
    public void startElement(String uri, String localName, String qName, Attributes attributes) throws SAXException {

        if (qName.equalsIgnoreCase("Car")) {
            String id = attributes.getValue("id");
            // initialize Car object and set id attribute
            car = new Car() {
                @Override
                public CarType getType() {
                    return null;
                }
            };
            car.setId(Integer.parseInt(id));
            // initialize list
            if (carList == null)
                carList = new ArrayList< >();
        } else if (qName.equalsIgnoreCase("_stamp")) {
            // set boolean values for fields, will be used in setting Car variables
            hasStamp = true;
        } else if (qName.equalsIgnoreCase("_number")) {
            hasNumber = true;
        } else if (qName.equalsIgnoreCase("_isBroken")) {
            hasIsBroken = true;
        } else if (qName.equalsIgnoreCase("_type")) {
            hasType = true;
        }
    }

    @Override
    public void endElement(String uri, String localName, String qName) throws SAXException {
        if (qName.equalsIgnoreCase("Car")) {
            // add User object to list
            carList.add(car);
        }
    }

    @Override
    public void characters(char ch[], int start, int length) throws SAXException {

        if (hasStamp) {
            car.set_stamp((new String(ch, start, length)));
            hasStamp = false;
        } else if (hasNumber) {
            car.set_number(new String(ch, start, length));
            hasNumber = false;
        } else if (hasType) {
            car.set_type(CarType.valueOf(new String(ch, start, length)));
            hasType = false;
        } else if (hasIsBroken) {
            car.set_isBroken(false);
            hasIsBroken = false;
        }
    }
}