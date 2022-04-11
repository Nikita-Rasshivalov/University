package com.company.data;

import models.Entity;

import java.util.List;

public interface IRepository<T extends Entity> {
    int Create(T entity);

    boolean Update(T entity);

    List<T> Get();

    default int GetIndex(List<T> items, int id) {
        for (int i = 0; i < items.size(); i++) {
            if (id == items.get(i).getId()) {
                return i;
            }
        }

        return -1;
    }
}
