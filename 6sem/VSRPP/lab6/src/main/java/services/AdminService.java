package services;

import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import models.*;

import java.util.Arrays;

public class AdminService extends BaseService {
    private Person _currentPerson;

    public AdminService
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
            var choice = GetChoice(Arrays.asList("1.Add dispatcher", "2.Add driver", "3.Show users", "4.Add car","5.Exit"), 5);
            switch (choice) {
                case 1:
                    AddDispatcher();
                    break;
                case 2:
                    AddDriver();
                    break;
                case 3:
                    ShowUsers();
                    break;
                case 4:
                    AddCar();
                case  5:return;
                default:
                    break;
            }
        }
    }

    public void AddDispatcher() {
        System.out.println("Input username:");
        String username = _scanner.nextLine();
        if (!CheckUsername(username)) {
            return;
        }
        System.out.println("Input password:");
        String password = _scanner.nextLine();
        System.out.println("Input name:");
        String name = _scanner.nextLine();
        System.out.println("Input surname:");
        String surname = _scanner.nextLine();
        var dispatcher = new Dispatcher(username, password, name, surname);
        _personRepository.Create(dispatcher);
    }

    public void AddCar() {
        System.out.println("Input stamp:");
        String stamp = _scanner.nextLine();

        System.out.println("Input number:");
        String number = _scanner.nextLine();

        System.out.println("Input isBroken:");
        Boolean isBroken = false;

        System.out.println("Choose car type:\n1 - Tank truck; 2 - Refrigerator; 3 - Container ship ");
        int value = _scanner.nextInt();
        switch (value) {
            case 1:
                _carRepository.Create(new TankTruck(stamp, number, isBroken));
                break;
            case 2:
                _carRepository.Create(new Refrigerator(stamp, number, isBroken));
                break;
            case 3:
                _carRepository.Create(new ContainerShip(stamp, number, isBroken));
                break;
            default:
                return;
        }
    }


    public void AddDriver() {
        System.out.println("Input username:");
        String username = _scanner.nextLine();
        if (!CheckUsername(username)) {
            return;
        }
        System.out.println("Input password:");
        String password = _scanner.nextLine();
        System.out.println("Input name:");
        String name = _scanner.nextLine();
        System.out.println("Input surname:");
        String surname = _scanner.nextLine();
        var driver = new Driver(username, password, name, surname);
        _personRepository.Create(driver);
    }

    public void ShowUsers() {
        int i = 1;
        for (Person person : _personRepository.Get()) {
            System.out.println(i + ". " + person.toString());
            i++;
        }
    }


    private boolean CheckUsername(String username) {
        if (username == null) {
            System.out.println("Incorrect username");
            return false;
        }
        var existPerson = _personRepository.Get().stream().filter(p -> p.getUsername().equals(username)).findFirst();
        if (existPerson.isPresent()) {
            System.out.println("Such user already exists");
            return false;
        }

        return true;
    }


}
