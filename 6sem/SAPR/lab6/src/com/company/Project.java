package com.company;

public class Project {
    private int id;
    private double firstCriteria;
    private double secondCriteria;
    private double thirdCriteria;

    public Project(int id, double firstCriteria, double secondCriteria, double thirdCriteria) {
        this.id = id;
        this.firstCriteria = firstCriteria;
        this.secondCriteria = secondCriteria;
        this.thirdCriteria = thirdCriteria;
    }

    public int getId() {
        return id;
    }

    public double getFirstCriteria() {
        return firstCriteria;
    }

    public void setFirstCriteria(double firstCriteria) {
        this.firstCriteria = firstCriteria;
    }

    public double getSecondCriteria() {
        return secondCriteria;
    }

    public void setSecondCriteria(double secondCriteria) {
        this.secondCriteria = secondCriteria;
    }

    public double getThirdCriteria() {
        return thirdCriteria;
    }

    @Override
    public String toString() {
        return "| " + id + " | " +
                firstCriteria + "            | " +
                secondCriteria + "        | " +
                thirdCriteria + "                 |";
    }
}
