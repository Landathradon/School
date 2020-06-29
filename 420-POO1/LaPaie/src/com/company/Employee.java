package com.company;

import java.util.ArrayList;
import java.util.Scanner;

public class Employee {
    String IdEmployee;
    String Nom;
    String Departement;

    public static ArrayList<Employee> FilterEmployee(ArrayList<Employee> listeEmployee, String taux){
        ArrayList<Employee> newList = new ArrayList<>();

        for (Employee employe: listeEmployee) {
            if (taux.equals("Fixe")) {
                if(employe.Departement.matches("restaurant|maintenance|commis|paysagistes")){
                    newList.add(employe);
                }
            } else if (taux.equals("Commission")) {
                if (employe.Departement.equals("ventes")) {
                    newList.add(employe);
                }
            }
        }

        return newList;
    }

    public Employee AjoutEmployee(){
        Scanner input = new Scanner(System.in);
        Employee employe = new Employee();
        System.out.print("Veuillez entrer le ID de l'employé: ");
        employe.IdEmployee = input.next();
        System.out.print("Veuillez entrer le Nom de l'employé: ");
        employe.Nom = input.next();
        System.out.print("Veuillez entrer le Département de l'employé: ");
        employe.Departement = input.next();
        System.out.println("L'employé à bien été ajouter à la liste.");
        return employe;
    }

    public static void ShowEmployee(ArrayList<Employee> listeEmployee, ArrayList<SystemPaie> listePaies, String taux){
        listeEmployee = FilterEmployee(listeEmployee, taux);

        for (Employee employe : listeEmployee) {
            SystemPaie paie = SystemPaie.findpaiementByIdEmployee(listePaies, employe.IdEmployee);
            if(taux.equals("Fixe")) {
                System.out.println("ID: " + employe.IdEmployee + " | Nom: " + employe.Nom + " | Heures Sup: " + SystemPaie.calculHeuresSup(paie.Heures));
            }
            else if(taux.equals("Commission")){
                System.out.println("ID: " + employe.IdEmployee + " | Nom: " + employe.Nom + " | Ventes Brutes: " + paie.VentesBrutes + "$");
            }
        }
    }

    public static Employee findEmployeeById(ArrayList<Employee> listEmployees, String Id) {
        return listEmployees.stream().filter(employee -> Id.equals(employee.IdEmployee)).findFirst().orElse(null);
    }
}