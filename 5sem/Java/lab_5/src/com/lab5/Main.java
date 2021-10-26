package com.lab5;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.Random;

public class Main {

    public static void main(String[] args) {

        final ArrayList<String> strings = new ArrayList<>();
        strings.add("Задать движение по экрану строк");
        strings.add("(одна за другой)");
        strings.add("из массива строк");
        strings.add("направление движения по апплету");
        strings.add("и значение каждой строки");
        strings.add("выбирается случайным образом");

        final JFrame frame = new JFrame("Движение по экрану строк");
        frame.setPreferredSize(new Dimension(640, 420));
        frame.setVisible(true);
        frame.setLayout(null);

        final JLabel lbl = new JLabel();
        lbl.setLocation(-1, 0);
        lbl.setSize(300, 20);
        frame.add(lbl);

        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        frame.pack();

        // главный таймер - в нем происходит вся логика движения
        Timer timer = new Timer(50, new ActionListener() {
            int speedX, speedY; // скорость движения текста
            final Random rnd = new Random();
            @Override
            public void actionPerformed(ActionEvent arg0) {
                Point loc = lbl.getLocation();
                var randomString = rnd.nextInt(strings.size()-1);
                var size = strings.get(randomString).length()*7.5;
                // если вышли за пределы, то сброс на новое слов
                if (loc.x > frame.getWidth()-size  || loc.y > frame.getHeight()-45 || loc.x < 0 || loc.y < 0) {
                    lbl.setLocation(frame.getWidth()/2, frame.getHeight()/2);
                    speedX = 1+rnd.nextInt(7); // задаю случайную скорость
                    speedY = 1+rnd.nextInt(7); // задаю случайную скорость
                    lbl.setText(strings.get(randomString)); // задаю случайную строку
                } else {
                    lbl.setLocation(loc.x + speedX, loc.y + speedY);
                }
            }});
        timer.start();
    }
}
