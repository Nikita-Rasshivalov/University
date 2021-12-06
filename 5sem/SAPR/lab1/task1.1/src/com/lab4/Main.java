package com.lab4;

import java.math.BigInteger;

public class Main {

    public static void main(String[] args) {
        System.out.println(factorial(new BigInteger("7500")));
    }

    private static BigInteger factorial(BigInteger number) {
        return number.intValue() == 0 ? new BigInteger("1") :
                number.multiply(factorial(number.subtract(new BigInteger("1"))));
    }
}
