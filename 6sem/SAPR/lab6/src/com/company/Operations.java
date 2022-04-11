package com.company;

import java.util.ArrayList;
import java.util.List;

public class Operations {
    List<Project> pareto = new ArrayList<>();
    int[] ci = new int[3];
    double[] vi = new double[3];
    double c = 0.5, d = 0.5; // "ядро" альтернатив
    // поиск лучших альтернатив
    public List<Project> findAlternatives(List<Project> list, int[][] expertsOpinion) {
        paretoSet(list);
        marks();
        rangMethod(expertsOpinion);
        // находим коэффициент согласия и несогласия
        List<Project> bestAlternatives = new ArrayList<>();
        double[][] agrMatrix = agreements();
        double[][] disMatrix = disagreements();
        // в таблице согласия в каждой строке производится поиск минимального значения
        // в таблице несогласия - максимальное значение
        for (int i = 0; i < pareto.size(); i++) {
            double minAgreement = Double.MAX_VALUE;
            double maxDisagreement = Double.MIN_VALUE;

            for (int j = 0; j < pareto.size(); j++) {
                if (i != j) {
                    if (minAgreement > agrMatrix[i][j])
                        minAgreement = agrMatrix[i][j];
                    if (maxDisagreement < disMatrix[i][j])
                        maxDisagreement = disMatrix[i][j];
                }
            }
            // сравнение каждого значения с ядром альтернатив
            if (minAgreement > c && maxDisagreement < d)
                bestAlternatives.add(pareto.get(i));
        }
        // вывод рассчитанных значений
        System.out.println("Коэффициент с = " + c + "; Коэффициент d = " + d);
        System.out.print("\n");
        System.out.println("Множество Парето");
        for (Project project : pareto) {
            System.out.println(project.toString());
        }
        System.out.print("\n");
        System.out.println("Матрица согласия");
        printMatrix(agrMatrix);
        System.out.println("Матрица несогласия");
        printMatrix(disMatrix);

        return bestAlternatives;
    }

    // находим оптимальные решения
    public void paretoSet(List<Project> list) {
        pareto.addAll(list); // записываем в новым массив изначальный масси
        // проверям какие проекты подходят
        for (int i = 0; i < list.size(); i++) {
            for (int j = 0; j < list.size(); j++) {
                boolean flag = false;
                if (i != j) {
                    if (list.get(i).getFirstCriteria() > list.get(j).getFirstCriteria()) {
                        flag = true;
                    } else if (list.get(i).getSecondCriteria() < list.get(j).getSecondCriteria()) {
                        flag = true;
                    } else if (list.get(i).getThirdCriteria() > list.get(j).getThirdCriteria()) {
                        flag = true;
                    }
                    // удаляем "плохие" проекты
                    if (!flag) {
                        pareto.remove(list.get(i));
                    }
                }
            }
        }
    }

    // переход к безразмерному виду
    public void marks() {
        double max = pareto.get(0).getFirstCriteria();  // по 1ому критерию смотрим максимальное значение
        double min = pareto.get(0).getSecondCriteria(); // по 2ому критерию смотрим минимальное значение

        for (Project project : pareto) {
            if (project.getFirstCriteria() > max) {
                max = project.getFirstCriteria();
            }
            if (project.getSecondCriteria() < min) {
                min = project.getSecondCriteria();
            }
        }

        for (Project project : pareto) {
            project.setFirstCriteria(project.getFirstCriteria() / max);   // делим каждый 1ый критерий на максимум
            project.setSecondCriteria(min / project.getSecondCriteria()); // делим минимум на каждый 2ой критерий
        }
    }

    // находим оценки, на основе мнений экспертов
    public void rangMethod(int[][] expertsOpinion) {
        int sum;
        for (int i = 0; i < expertsOpinion[0].length; i++) {
            sum = 0;
            for (int[] expert : expertsOpinion) {
                sum += expert[i];
            }

            ci[i] = sum; // сумма каждого столбца-критерия
        }

        sum = 0;
        for (int i = 0; i < 3; i++) {
            sum += ci[i]; // общая сумму всех столбцов-критериев
        }

        for (int i = 0; i < ci.length; i++) {
            vi[i] = ci[i] / (double) sum; // веса альтернатив
        }
    }

    // находим индексы согласия
    public double[][] agreements() {
        double[][] agreements = new double[pareto.size()][pareto.size()];
        for (int i = 0; i < pareto.size() ; i++) {
            for (int j = 0; j < pareto.size(); j++) {
                double k = 0;
                if (i == j) {
                    agreements[i][j] = Double.MAX_VALUE;
                } else {
                    if (pareto.get(i).getFirstCriteria() > pareto.get(j).getFirstCriteria())
                        k += vi[0];
                    if (pareto.get(i).getSecondCriteria() > pareto.get(j).getSecondCriteria())
                        k += vi[1];
                    if (pareto.get(i).getThirdCriteria() > pareto.get(j).getThirdCriteria())
                        k += vi[2];
                    agreements[i][j] = k;
                }
            }
        }

        return agreements;
    }

    // находим индексы несогласия
    public double[][] disagreements() {
        double[][] disagreements = new double[pareto.size()][pareto.size()];
        List<Double> difference = new ArrayList<>();

        for (int i = 0; i < pareto.size(); i++) {
            for (int j = 0; j < pareto.size(); j++) {
                if (i != j) {
                    if (pareto.get(i).getFirstCriteria() < pareto.get(j).getFirstCriteria()) {
                        difference.add(pareto.get(j).getFirstCriteria() - pareto.get(i).getFirstCriteria());
                    }
                    System.out.println();
                    if (pareto.get(i).getSecondCriteria() < pareto.get(j).getSecondCriteria()) {
                        difference.add(pareto.get(j).getSecondCriteria() - pareto.get(i).getSecondCriteria());
                    }
                    if (pareto.get(i).getThirdCriteria() < pareto.get(j).getThirdCriteria()) {
                        difference.add(pareto.get(j).getThirdCriteria() - pareto.get(i).getThirdCriteria());
                    }

                    double max = 0;
                    for (Double d : difference) {
                        if (d > max)
                            max = d;
                    }

                    difference.clear();
                    disagreements[i][j] = max;
                } else
                    disagreements[i][j] = Double.MAX_VALUE;
            }
        }

        return disagreements;
    }

    // метод для вывода матрицы
    private static void printMatrix(double[][] matrix) {
        StringBuilder sb = new StringBuilder();
        for (double[] line : matrix) {
            StringBuilder row = new StringBuilder();
            for (int j = 0; j < matrix.length; j++) {
                row.append(Math.ceil(line[j] * 100) / 100).append(" ");
            }
            sb.append(row).append("\n");
        }
        System.out.println(sb.toString());
    }
}
