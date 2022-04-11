package services;

import com.company.data.CarRepository;
import com.company.data.PersonRepository;
import com.company.data.TripRepository;
import models.Person;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;
import java.util.Scanner;

public class BaseService {
    protected PersonRepository _personRepository;
    protected TripRepository _tripRepository;
    protected CarRepository _carRepository;

    protected Scanner _scanner = new Scanner(System.in);

    public BaseService(PersonRepository personRepository, TripRepository tripRepository, CarRepository carRepository) {
        _personRepository = personRepository;
        _tripRepository = tripRepository;
        _carRepository = carRepository;
    }

    public void ShowMenu() {
        while (true) {
            var choice = GetChoice(Arrays.asList("1.Login", "2.Exit"), 2);
            switch (choice) {
                case 1:
                    var optionalPerson = login();
                    if (optionalPerson.isPresent()) {
                        var person = optionalPerson.get();
                        var type = person.getType();
                        switch (type) {
                            case Admin -> new AdminService(person, _personRepository, _tripRepository, _carRepository).ShowMenu();
                            case Dispatcher -> new DispatcherService(person, _personRepository, _tripRepository, _carRepository).ShowMenu();
                            case Driver -> new DriverService(person, _personRepository, _tripRepository, _carRepository).ShowMenu();
                        }
                    }
                    break;
                case 2:
                    return;
                default:
                    break;
            }
        }
    }

    private Optional<Person> login() {
        System.out.println("Input username:");
        String username = _scanner.nextLine();
        var persons = _personRepository.Get();
        var person = persons.stream().filter(p -> p.getUsername().equals(username)).findFirst();
        if (person.isEmpty()) {
            System.out.println("No such person");
            return Optional.empty();
        }

        System.out.println("Input password:");
        String password = _scanner.nextLine();
        if (!person.get().getPassword().equals(password)) {
            System.out.println("Incorrect password");
            return Optional.empty();
        }

        return person;
    }


    public int GetChoice(List<String> menuItems, int maxInput) {
        System.out.println("Menu");
        for (String item :
                menuItems) {
            System.out.println(item);
        }
        System.out.println("Select:");
        int choice;
        while (true) {
            try {
                choice = Integer.parseInt(_scanner.nextLine());
                if (choice < 1 || choice > maxInput) throw new Exception("");
                break;
            } catch (Exception exception) {
                System.out.println("Incorrect input");
            }
        }
        return choice;
    }
}
