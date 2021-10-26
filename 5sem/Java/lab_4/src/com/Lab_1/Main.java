package com.Lab_1;

import java.util.*;

public class Main {

    public static void main(String[] args) {
	JsonParser parser = new JsonParser();
    Root root = parser.parse();
        List<Product> productList = root.getProducts();
        System.out.println("Initial list");
        System.out.println("Root " + root.toString());
        System.out.println("Sorted list on cipher");
        Collections.sort(productList, (o1, o2) -> o1.getCipher() - o2.getCipher());
        for (var prod:productList) {
            System.out.println(String.format("Product name:%s      Product cipher:%d",prod.name,prod.cipher));
        }
        System.out.println("Delete duplicates");
        DeleteDuplicates(productList);
    }

    public static List<Product> DeleteDuplicates(List<Product> productList){
        Set<Product> set = new HashSet<Product>(productList);
        productList.clear();
        productList.addAll(set);
        for (var prod:productList) {
            System.out.println(String.format("Product name:%s      Product cipher:%d",prod.name,prod.cipher));
        }
        return productList;
    }
}
