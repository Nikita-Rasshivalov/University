package com.Lab_1;

import java.util.List;
import java.util.Objects;

public class Root {
    String name;
    List<Product> products;

    public String getName(){return name;}

    public List<Product> getProducts() {return products;}

    public void setName(String name) {
        this.name = name;
    }

    public void setProducts(List<Product> products) {
        this.products = products;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Root root = (Root) o;
        return Objects.equals(name, root.name) && Objects.equals(products, root.products);
    }

    @Override
    public int hashCode() {
        return Objects.hash(name, products);
    }

    @Override
    public String toString() {
        return "Root{" +
                "name='" + name + '\'' +
                ", products=" + products +
                '}';
    }
}
