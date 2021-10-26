package com.Lab_1;

public class Square implements Rotatable, Scalable {
    private Pair<Segment, Segment> sides;

    public Square(double x1, double y1, double x2, double y2, double x3, double y3) {
        Segment side1 = new Segment(x1, y1, x2, y2);
        Segment side2 = new Segment(x2, y2, x3, y3);
        sides = new Pair<>(side1, side2);
    }

    @Override
    public void rotate(double angle) {
        sides.getFirst().rotate(angle);
        sides.getSecond().rotate(angle);
    }

    @Override
    public void scale(double factor) {
        sides.getFirst().scale(factor);
        sides.getSecond().scale(factor);
    }
}
