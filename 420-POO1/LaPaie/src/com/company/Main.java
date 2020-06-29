package com.company;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

import static javafx.application.Platform.exit;

public class Main{

    public static void main(String[] args){
        boolean AppRun = true;
        ArrayList<Employee> listeEmployee = new ArrayList<>(PrepareListEmployee());
        ArrayList<SystemPaie> listePaies = new ArrayList<>(PrepareListPaiements(listeEmployee));

        while(AppRun) {

            System.out.println("\n--- Veuillez choisir un des éléments du menu ci-dessous ---\n" +
                    "1- Ajouter un employé\n" +
                    "2- Ajouter une paie\n" +
                    "3- Afficher le total des contributions au Régime de Retraite du Canada\n" +
                    "4- Afficher le total des contributions à l'assurance emploi\n" +
                    "5- Liste des employés à taux fixe\n" +
                    "6- Liste des employés à commissions\n" +
                    "7- Quitter\n" +
                    "-----------------------------------------------------------");
            Scanner input = new Scanner(System.in);
            Employee employe = new Employee();
            SystemPaie paie = new SystemPaie();
            int getNoSemaine;

            switch (input.nextInt()) {
                case 1:
                    listeEmployee.add(employe.AjoutEmployee());
                    break;
                case 2:
                    listePaies.add(paie.ajoutPaie(listeEmployee));
                    break;
                case 3:
                    System.out.print("Veuillez choisir la semaine: ");
                    getNoSemaine = input.nextInt();
                    SystemPaie.calculContributions(listeEmployee, listePaies, getNoSemaine, true);
                    break;
                case 4:
                    System.out.print("Veuillez choisir la semaine: ");
                    getNoSemaine = input.nextInt();
                    SystemPaie.calculContributions(listeEmployee, listePaies, getNoSemaine, false);
                    break;
                case 5:
                    Employee.ShowEmployee(listeEmployee, listePaies, "Fixe");
                    break;
                case 6:
                    Employee.ShowEmployee(listeEmployee, listePaies, "Commission");
                    break;
                case 7:
                    System.out.print("Le programme vas maintenant se fermer..");
                    AppRun = false;
                    break;
                default:
                    System.out.print("Veuillez entrer un chiffre de 1 à 7 selon votre choix.");
                    break;
            }
        }
        exit();
    }

    public static List<Employee> PrepareListEmployee(){
        List<Employee> list = new ArrayList<>();
        String [] Noms = {"John", "Melanie", "Travis", "Robert"};
        String [] Depts = {"restaurant", "maintenance", "commis", "ventes"};

        for (int i = 0; i < Noms.length; i++) {
            Employee employe = new Employee();
            employe.IdEmployee = String.valueOf(i+1);
            employe.Nom = Noms[i];
            employe.Departement = Depts[i];
            list.add(employe);
        }

        return list;
    }

    public static List<SystemPaie> PrepareListPaiements(ArrayList<Employee> listeEmployee){
        List<SystemPaie> list = new ArrayList<>();
        String [] Ids = {"1", "2", "3", "4", "1"};
        int [] NoSemaines = {23, 23, 23, 23, 24};
        Double [] Heures = {32.00 , 42.50, 50.32, 38.00, 16.00};
        Double [] Ventes = {385.00 , 422.50, 570.32, 398.00, 126.37};

        for (int i = 0; i < Ids.length; i++) {
            SystemPaie paie = new SystemPaie();
            Employee employe;

            paie.NoSemaine = NoSemaines[i];
            paie.IdEmployee = Ids[i];
            employe = Employee.findEmployeeById(listeEmployee, Ids[i]);
            paie.Heures = Heures[i];
            paie.TauxHoraire = paie.TauxSalaire(employe);
            paie.VentesBrutes = Ventes[i];

            list.add(paie);
        }

        return list;
    }
}