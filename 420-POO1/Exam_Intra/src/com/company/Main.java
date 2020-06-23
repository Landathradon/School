package com.company;

import java.util.Random;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        while (true) {
            Random r = new Random();
            int de1 = r.nextInt((6 - 1) + 1) + 1;
            int de2 = r.nextInt((6 - 1) + 1) + 1;
            int de3 = r.nextInt((6 - 1) + 1) + 1;

            int pointage = de1 + de2 + de3;

            System.out.print("--- Valeur des dés ---\n" +
                    "Dé 1: " + de1 + "\n" +
                    "Dé 2: " + de2 + "\n" +
                    "Dé 3: " + de3 + "\n" +
                    "----------------------\n");

            if (de1 == de2 && de1 == de3) {
                //S'ils sont tous égaux + 10 points
                pointage += 10;
            } else if (de1 == de2 || de1 == de3 || de2 == de3) {
                //Si un seul des group est égaux + 5 points
                pointage += 5;
            }

            System.out.print("--- Valeur des dés ---\n" +
                    "Dé 1: " + de1 + "\n" +
                    "Dé 2: " + de2 + "\n" +
                    "Dé 3: " + de3 + "\n" +
                    "Pointage: " + pointage + "\n" +
                    "----------------------\n");

            Scanner input = new Scanner(System.in);
            System.out.println("Voulez vous recommencé? (o/n)");
            String result = input.nextLine();

            if(result.toLowerCase().equals("o") || result.toLowerCase().equals("oui")){
                continue;
            } else if (result.toLowerCase().equals("n") || result.toLowerCase().equals("non")){
                break;
            } else { break; }
        }
    }
}
