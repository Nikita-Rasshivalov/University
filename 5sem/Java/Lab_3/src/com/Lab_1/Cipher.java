package com.Lab_1;

public class Cipher {
    CipherStrategy strategy;

    public Cipher(CipherStrategy strategy) {
        this.strategy = strategy;
    }

    public String encrypt(String text) {
        return strategy.encrypt(text);
    }

    public String decrypt(String text) {
        return strategy.decrypt(text);
    }
}

