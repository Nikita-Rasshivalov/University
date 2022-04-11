package services;

import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import models.*;

import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;


public class DriverService extends BaseService {
    private final Person _currentPerson;

    public DriverService
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
            var choice = GetChoice(Arrays.asList("1.Show information", "2.Exit"), 2);
            switch (choice) {
                case 1:
                    Driver driver = (Driver) ShowInfo();
                    var value = GetChoice(Arrays.asList("1.Repair request", "2.Make trip status", "3.Exit"), 3);
                    switch (value) {
                        case 1:
                            RepairRequest(driver);
                            break;
                        case 2:
                            MakeStatus(driver);
                            break;
                        case 3:
                            return;
                        default:
                            break;
                    }
                    break;
                case 2:
                    return;
                default:
                    break;
            }
        }
    }

    public Person ShowInfo() {
        try {
            var driverId = _currentPerson.getId();
            var driver = _personRepository.Get().stream().filter(t -> t.getId() == driverId).findFirst();
            if (driver.isEmpty()) {
                throw new Exception("");
            }
            System.out.println(driver.get().toString());
            return driver.get();
        } catch (Exception exception) {
            System.out.println("Incorrect driver id");
            return null;
        }
    }

    public void RepairRequest(Driver driver) {
        try {
            var car = _carRepository.Get().stream().filter(t -> t.getId() == driver.get_carId()).findFirst().get();
            System.out.println(car.toString());
            if (car.is_isBroken() == true) {
                car.set_isBroken(false);
            }
            System.out.println("Complete");
            System.out.println(car.toString());
            _carRepository.Update(car);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            return;
        }
    }

    public void MakeStatus(Driver driver) {
        try {
            var car = _carRepository.Get().stream().filter(t -> t.getId() == driver.get_carId()).findFirst().get();
            System.out.println(car);
            var trips = _tripRepository.Get().stream().collect(Collectors.toList());
            var tripId = GetTripId(trips);
            var trip = _tripRepository.Get().stream().filter(t -> t.getId() == tripId).findFirst().get();
            System.out.println("Choose status 1-complete or 2 - non-complete");
            var value = Integer.parseInt(_scanner.nextLine());
            switch (value) {
                case 1:
                    trip.set_isCompleted(true);
                    MakeCarStatus(car);
                    break;
                case 2:
                    trip.set_isCompleted(false);
                    MakeCarStatus(car);
                    break;
                default:
                    System.out.println("Input error");
                    return;
            }
            _carRepository.Update(car);
            _tripRepository.Update(trip);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            return;
        }
    }

    public void MakeCarStatus(Car car) {
        System.out.println("Choose status 1-not broken or 2 - broken");
        var value = Integer.parseInt(_scanner.nextLine());
        switch (value) {
            case 1:
                car.set_isBroken(true);
                break;
            case 2:
                car.set_isBroken(false);
                break;
            default:
                System.out.println("Input error");
                return;
        }
    }

    private int GetTripId(List<Trip> items) {
        for (Trip trip : items) {
            System.out.println(trip.getId() + ". " + trip.toString());
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

