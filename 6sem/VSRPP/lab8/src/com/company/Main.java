package com.company;

import models.*;

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Main {
    private static Scanner _scanner = new Scanner(System.in);

    public static void main(String[] args) {
        while (true) {
            var choice = getChoice(Arrays.asList("1.Add Metropolis", "2.Add City", "3.Add place", "4.Show", "5.Exit"), 5);
            switch (choice) {
                case 1:
                    var type = "Metropolis";
                    var metropolis = new Metropolis(getString("Input name"), type);
                    metropolis.Add();
                    break;
                case 2:
                    type = "City";
                    var city = new City(getString("Input name"), type);
                    city.Add();
                    break;
                case 3:
                    type = "Place";
                    var place = new Place(getString("Input name"), type, getString("Input address"));
                    place.Add();
                    break;
                case 4:
                    Field.ShowList();
                    break;
                case 5:
                    return;
                default:
                    break;
            }
        }
    }

    private static String getString(String message) {
        System.out.println(message);
        return _scanner.nextLine();
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
