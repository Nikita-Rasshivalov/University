package com.Lab_1;

public class Main {
    public static void main(String[] args) {
        Cipher cipher = new Cipher(new TaskCipherStrategy());
        String text = "qwerty";
        System.out.println("Initial text:" + text);
        System.out.println("Encrypted text");
        text  = cipher.encrypt(text);
        System.out.println(text);
        System.out.println("Decrypted text");
        System.out.println(cipher.decrypt( text));


        String text2 = "12345";
        System.out.println("Initial text:" + text2);
        System.out.println("Encrypted text");
        text2  = cipher.encrypt(text2);
        System.out.println(text2);
        System.out.println("Decrypted text");
        System.out.println(cipher.decrypt( text2));
    }
}
