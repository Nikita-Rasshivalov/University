package services;

import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import models.Car;
import models.Driver;
import models.Person;
import models.Trip;

import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

public class DispatcherService extends BaseService {
    private Person _currentPerson;

    public DispatcherService
            (
                    Person currentPerson,
                    PersonRepository personRepository,
                    TripRepository tripRepository,
                    CarRepository carRepository
            ) {
        super(personRepository, tripRepository, carRepository);
        _currentPerson = currentPerson;
    }

    @Override
    public void ShowMenu() {
        while (true) {
            var choice = GetChoice(Arrays.asList("1.Add trip", "2.Suspend the driver", "3.Exit"), 4);
            switch (choice) {
                case 1:
                    AddTrip();
                    break;
                case 2:
                    SetIsWorked();
                    break;
                case 3:
                    return;
                default:
                    break;
            }
        }
    }

    public void AddTrip() {
        var drivers = _personRepository.Get().stream().filter(t -> t.getClassName().equals("models.Driver")).collect(Collectors.toList());
        var driverId = GetDriverId(drivers);
        var cars = _carRepository.GetXml().stream().collect(Collectors.toList());
        var carId = GetCarId(cars);
        Calendar calendar = Calendar.getInstance();
        SimpleDateFormat formatter = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss");
        var trip = new Trip(carId, formatter.format(calendar.getTime()), driverId);
        trip.set_isCompleted(false);
        _tripRepository.Create(trip);
    }

    public void SetIsWorked() {
        try {
            var drivers = _personRepository.Get().stream().filter(t -> t.getClassName().equals("models.Driver")).collect(Collectors.toList());
            var driverId = GetDriverId(drivers);
            var driver = _personRepository.Get().stream().filter(t -> t.getId() == driverId).findFirst();
            if (driver.isEmpty()) {
                throw new Exception("");
            }
            var currentDriver = (Driver) driver.get();
            if (currentDriver.get_isWorked() == false) {
                currentDriver.set_isWorked(true);
            } else {
                currentDriver.set_isWorked(false);
            }
            _personRepository.Update(driver.get());
        } catch (Exception exception) {
            System.out.println("Incorrect driver id");
            return;
        }
    }

    private int GetCarId(List<Car> items) {
        for (Car car : items) {
            System.out.println(car.getId() + ". " + car.toString());
        }
        System.out.println("Select:");
        try {
            var id = Integer.parseInt(_scanner.nextLine());
            if (items.stream().filter(p -> p.getId() == id).count() == 0) {
                throw new Exception("");
            }
            return id;
        } catch (Exception exception) {
            System.out.println("Incorrect person id");
        }

        return -1;
    }

    private int GetDriverId(List<Person> items) {
        for (Person driver : items) {
            System.out.println(driver.getId() + ". " + driver.toString());
        }
        System.out.println("Select:");
        try {
            var id = Integer.parseInt(_scanner.nextLine());
            if (items.stream().filter(p -> p.getId() == id).count() == 0) {
                throw new Exception("");
            }
            return id;
        } catch (Exception exception) {
            System.out.println("Incorrect person id");
        }

        return -1;
    }
}
