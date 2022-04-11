package com.company;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        // шкала Харрингтона(значения свои)
        HashMap<String, Double> harringtonScale = new HashMap<>();
        harringtonScale.put("средние(хуже П2)", 0.6);
        harringtonScale.put("средние", 0.7);
        harringtonScale.put("хорошие", 0.8);
        harringtonScale.put("очень хорошие", 1.0);

        // характеристики проектов id, прибыль, рабочие места, возможности развития
        List<Project> projects = new ArrayList<>();
        projects.add(new Project(1, 12, 3000, harringtonScale.get("хорошие")));
        projects.add(new Project(2, 10, 3500, harringtonScale.get("средние")));
        projects.add(new Project(3, 13, 3000, harringtonScale.get("средние(хуже П2)")));
        projects.add(new Project(4, 11, 1500, harringtonScale.get("хорошие")));
        projects.add(new Project(5, 15, 2000, harringtonScale.get("очень хорошие")));
        projects.add(new Project(6, 14, 2500, harringtonScale.get("очень хорошие")));

        // мнения экспертов
        int[][] expertsOpinion = new int[][] { { 10, 3, 6 },
                                               { 10, 6, 3 } };

        System.out.println("\u250c---\u252c-----------------\u252c---------------\u252c---------------------\u2510");
        System.out.println("\u2502 № \u2502 Прибыль         \u2502 Новые рабочие \u2502 Возможности         \u2502");
        System.out.println("\u2502   \u2502 млн ден.ед./год \u2502    места      \u2502 развития территории \u2502");
        System.out.println("\u251c---\u253c-----------------\u253c---------------\u253c---------------------\u2524");
        for (Project project : projects) {
            System.out.println(project.toString());
        }
        System.out.print("\u2514---\u2534-----------------\u2534---------------\u2534---------------------\u2518");

        Operations op = new Operations();
        List<Project> bestAlternatives = op.findAlternatives(projects, expertsOpinion);

        System.out.print("Лучшие альтернативы: ");
        for (Project project : bestAlternatives) {
            System.out.print(project.getId() + " ");
        }
    }
}
