package com.Lab_1;

public class Point implements Rotatable {
    private Pair<Double, Double> coordinates;

    public Point(double x, double y) {
        this.coordinates = new Pair<>(x, y);
    }

    public double getX() {
        return coordinates.getFirst();
    }

    public double getY() {
        return coordinates.getSecond();
    }

    /*
     * NOTE: point has no size, therefore it cannot be scaled. Only it's coordinates can.
     * */
    public void scaleCoordinates(double factor) {
        double x = coordinates.getFirst();
        double y = coordinates.getSecond();

        double newX = x * factor;
        double newY = y * factor;

        coordinates = new Pair<>(newX, newY);
    }

    @Override
    public void rotate(double angle) {
        double x = coordinates.getFirst();
        double y = coordinates.getSecond();
        double newX = x * Math.cos(angle) - y * Math.sin(angle);
        double newY = x * Math.sin(angle) + y * Math.cos(angle);
        coordinates = new Pair<>(newX, newY);
    }
}
