using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace montest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int nbrUtilisateurs = 0;
            Console.WriteLine("Veuillez entrer le nombre d'utilisateurs à inscrire:");
            string input = Console.ReadLine();

            if (!Int32.TryParse(input, out nbrUtilisateurs))
            {
                Console.WriteLine("Vous n'avez pas inscrit un nombre, veuillez réessayer.");
                System.Environment.Exit(0);
            }

            if (nbrUtilisateurs < 3)
            {
                Console.WriteLine("Veuillez entrer plus de 3 utilisateurs.");
                System.Environment.Exit(0);
            }

            String[] nomUser = new string[nbrUtilisateurs];
            String[] prenomUser = new string[nbrUtilisateurs];
            String[] phoneUser = new string[nbrUtilisateurs];

            int cnt = 0;
            while (cnt < nbrUtilisateurs)
            {
                Console.WriteLine("Veuillez entrer le nom de l'utilisateur #" + cnt);
                nomUser[cnt] = Console.ReadLine();

                Console.WriteLine("Veuillez entrer le prénom de l'utilisateur");
                prenomUser[cnt] = Console.ReadLine();

                Console.WriteLine("Veuillez entrer le # de téléphone de l'utilisateur | (555)555-5555");
                phoneUser[cnt] = Console.ReadLine();
                MatchPhone(phoneUser[cnt]);

                Console.WriteLine("---------------------------------------------------\n");
                cnt++;
            }
            
            Console.WriteLine("******************Tableau d'utilisateurs******************");
            for (int i = 0; i < nbrUtilisateurs; i++)
            {
                Console.WriteLine("* Nom: {0}, Prénom: {1}, #Tel: {2} *",nomUser[i],prenomUser[i],phoneUser[i]);
            }
            Console.WriteLine("**********************************************************\n");

            List<String[]> tableauUtilisateurs = SortMethod(nomUser, prenomUser, phoneUser); //0: Noms, 1: prenoms, 2: phones
            Console.WriteLine("*************Tableau d'utilisateurs AVEC SORT*************");
            for (int i = 0; i < nbrUtilisateurs; i++)
            {
                Console.WriteLine("* Nom: {0}, Prénom: {1}, #Tel: {2} *",tableauUtilisateurs[0][i],tableauUtilisateurs[1][i],tableauUtilisateurs[2][i]);
            }
            Console.WriteLine("**********************************************************");
            
            
            
            void MatchPhone(String phoneNumber)
            {
                Regex phoneNumberRegex = new Regex("^[(][0-9][0-9][0-9][)][0-9][0-9][0-9][-][0-9][0-9][0-9][0-9]$");
                Match match = phoneNumberRegex.Match(phoneNumber);
                if (!match.Success)
                {
                    Console.WriteLine("Le numéro que vous aviez entrer ne correspond pas à la méthode requise");
                    System.Environment.Exit(0);
                }
            }

            List<String[]> SortMethod(String[] noms, String[] prenoms, String[] phones)
            {
                List<string[]> userInfo = new List<string[]>();
                for (int i = 0; i < noms.Length; i++)
                {
                    var tempNom = noms[i];
                    var tempPrenom = prenoms[i];
                    var tempPhone = phones[i];
                    var j = i;
                    
                    while(j > 0 && noms[j-1].CompareTo(tempNom) > 0)
                    {
                        noms[j] = noms[j-1];
                        prenoms[j] = prenoms[j-1];
                        phones[j] = phones[j-1];
                        j = j-1;
                    }
                    
                    noms[j] = tempNom;
                    prenoms[j] = tempPrenom;
                    phones[j] = tempPhone;
                }
                
                userInfo.Add(noms);
                userInfo.Add(prenoms);
                userInfo.Add(phones);

                return userInfo;
            }
        }
    }
}
