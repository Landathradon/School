package com.company;

import java.text.DecimalFormat;
import java.util.*;

public class SystemPaie {
    int NoSemaine;
    String IdEmployee;
    double Heures;
    double TauxHoraire;
    double VentesBrutes;

    public double TauxSalaire(Employee employe) {
        switch (employe.Departement) {
            case "restaurant":
                return 8.50;
            case "maintenance":
                return 12.50;
            case "commis":
            case "paysagistes":
                return 15.75;
            case "ventes":
                return 15.00;
            default:
                return 0;
        }
    }

    public SystemPaie ajoutPaie(ArrayList<Employee> listeEmployee) {
        Scanner input = new Scanner(System.in);
        SystemPaie paie = new SystemPaie();
        Employee employe;

        System.out.print("Veuillez entrer le # de la semaine: ");
        paie.NoSemaine = input.nextInt();
        System.out.print("Veuillez entrer le ID de l'employé: ");
        paie.IdEmployee = input.next();
        employe = Employee.findEmployeeById(listeEmployee, paie.IdEmployee);
        System.out.print("Veuillez entrer le nombre d'heures travaillées: ");
        paie.Heures = input.nextDouble();
        paie.TauxHoraire = TauxSalaire(employe);
        System.out.print("Veuillez entrer le montant des ventes brutes: ");
        paie.VentesBrutes = input.nextDouble();

        System.out.println("La paie à bien été ajouter à la liste.");
        return paie;
    }

    public static SystemPaie findpaiementByIdEmployee(ArrayList<SystemPaie> listePaies, String Id) {
        int lastWeek = listePaies.stream().filter(paie -> Id.equals(paie.IdEmployee)).mapToInt(paie -> paie.NoSemaine).max().getAsInt();
        return listePaies.stream().filter(paie -> Id.equals(paie.IdEmployee) && paie.NoSemaine == lastWeek).findFirst().orElse(null);
    }

    public static Double calculHeuresSup(Double heures) {
        if (heures > 44.00) {
            return heures - 44.00;
        } else {
            return 0.00;
        }
    }

    public static void calculContributions(ArrayList<Employee> listeEmployee, ArrayList<SystemPaie> listePaies, int getNoSemaine, boolean RRCouASEM) {
        ArrayList<Employee> listeEmployeeFixe = Employee.FilterEmployee(listeEmployee, "Fixe");
        ArrayList<Employee> listeEmployeeCommission = Employee.FilterEmployee(listeEmployee, "Commission");
        double rateRRC = 0.0495;
        double rateASEM = 0.0198;
        double contribution = 0.00;

        for (SystemPaie paie : listePaies) {
            if(paie.NoSemaine == getNoSemaine) {
                for (Employee employeeFixe : listeEmployeeFixe) {
                    contribution += (montantPaie(employeeFixe, paie, "Fixe") * (RRCouASEM? rateRRC: rateASEM));
                }

                for (Employee employeeCommission : listeEmployeeCommission) {
                    contribution += (montantPaie(employeeCommission, paie, "Commission") * (RRCouASEM? rateRRC: rateASEM));
                }
            }
        }
        String result = new DecimalFormat("#.##").format(contribution);

        System.out.println("Total des contributions " + (RRCouASEM? "au Régime de Retraite du Canada":"à l'assurance emploi") + ": " + result + "$");
    }

    public static Double montantPaie(Employee currentEmployee, SystemPaie paie, String taux) {
        double montantSalaireEmployee = 0.00;
        double tauxCommission = 0.015;

            if (paie.IdEmployee.equals(currentEmployee.IdEmployee)) {
                double hrsSup = calculHeuresSup(paie.Heures);
                if (taux.equals("Fixe")) {
                    if (paie.Heures > 44.00) {
                        montantSalaireEmployee = (((paie.Heures - hrsSup) * paie.TauxHoraire) + (hrsSup * paie.TauxHoraire));
                    } else {
                        montantSalaireEmployee = (paie.Heures * paie.TauxHoraire);
                    }
                } else if (taux.equals("Commission")) {
                    if (paie.Heures > 44.00) {
                        montantSalaireEmployee = (((paie.Heures - hrsSup) * paie.TauxHoraire) + (paie.VentesBrutes * tauxCommission));
                    } else {
                        montantSalaireEmployee = ((paie.Heures * paie.TauxHoraire) + (paie.VentesBrutes * tauxCommission));
                    }
                }
            }

        return montantSalaireEmployee;
    }
}