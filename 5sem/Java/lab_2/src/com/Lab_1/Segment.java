package com.Lab_1;

public class Segment implements Rotatable, Scalable {
    private Pair<Point, Point> ends;

    public Segment(double x1, double y1, double x2, double y2) {
        ends = new Pair<>(new Point(x1, y1), new Point(x2, y2));
    }

    public double length() {
        double x1 = ends.getFirst().getX();
        double y1 = ends.getFirst().getY();
        double x2 = ends.getSecond().getX();
        double y2 = ends.getSecond().getY();
        return Math.sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
    }

    @Override
    public void rotate(double angle) {
        ends.getFirst().rotate(angle);
        ends.getSecond().rotate(angle);
    }

    @Override
    public void scale(double factor) {
        ends.getFirst().scaleCoordinates(factor);
        ends.getSecond().scaleCoordinates(factor);
    }
}
