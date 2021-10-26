package com.Lab_1;
public class Main {
    public static void main(String[] args) {
        Segment segment1 = new Segment(1,1,1,5);
        Segment segment2 = new Segment(1,5,5,5);
        Segment segment3 = new Segment(5,5,5,1);
        Segment segment4 = new Segment(5,1,1,1);
        segment3.scale(12);
        if (segment1.length() == segment2.length() & segment2.length() == segment3.length() &
                segment3.length() == segment4.length() & segment4.length() == segment1.length()){
            System.out.println("This is square!!");
        }else {
            System.out.println("This is no square!!");
        }
        Square square = new Square(1,1,1,5,5,5);
    }
}
