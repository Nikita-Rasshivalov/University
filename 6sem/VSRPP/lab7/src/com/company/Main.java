package com.company;

import Accounting.City;

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Main {
    private static Scanner _scanner = new Scanner(System.in);
    private static City _city = new City();

    public static void main(String[] args) {
        while (true) {
            var choice = getChoice(Arrays.asList("1.Street", "2.Square", "3.Avenue", "4.Info", "5.Exit"), 5);
            switch (choice) {
                case 1:
                    try {
                        String name = "Kashatanovaja";
                        _city.SetStreet(name);
                    } catch (Exception e) {
                        System.out.println(e.getMessage());
                    }
                    break;
                case 2:
                    try {
                        String name = "Lenina sq";
                        _city.SetSquare(name);
                    } catch (Exception e) {
                        System.out.println(e.getMessage());
                    }
                    break;
                case 3:
                    try {
                        String name = "Avenue";
                        _city.SetAvenue(name);
                    } catch (Exception e) {
                        System.out.println(e.getMessage());
                    }
                    break;
                case 4:
                    if (_city.getInformation().size() > 0) {
                        for (var info : _city.getInformation()) {
                            System.out.println(info.toStringRus());
                        }
                    } else {
                        System.out.println("is empty");
                    }
                    break;
                case 5:
                    return;
                default:
                    break;
            }
        }
    }


    private static int getChoice(List<String> menuItems, int maxInput) {
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
