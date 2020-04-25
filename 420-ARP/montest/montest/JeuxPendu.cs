using System;
using System.Linq;

namespace montest
{
    class JeuxPendu
    {
        static void Main(string[] args)
        {
            // Initialisation du tableau de mots à trouver.
            string[] mots = new string[]
            {
            "Carrousel",
            "Digestion",
            "Essence",
            "Gestation",
            "Ivoire",
            "Mutation",
            "Nocturne",
            "Pagination",
            "Soleil",
            "Tentacule"
            };
            // Déclaration des variables. 
            bool[] lettresTrouve;     // Tableau des lettres trouvées.
            bool reponse = true;     // Variable qui mettra fin au jeu.
            bool repLettre = false;     // Variable qui contrôle la saisie de l'utilisateur.
            char rep;     // Variable qui reçoit la réponse du joueur.
            char lettre;     // Variable qui récupère la lettre saisie par l'utilisateur.
            bool motTrouve;     // Variable spécifiant si le mot a été trouvé ou non.
            string motRecherche;     // Variable qui contiendra le mot à trouver.
            string lettres = "";     // Variable qui contient les lettres demandées.
            int tentatives;     // Variable qui compte le nombre de tentatives.
            int tentativesMax = 15;     // Variable qui définie le nombre de tentatives maximal.
            string messageMotCherche = "";     // Variable qui affiche l'état du mot à chercher.
            do
            {
                // L'objet Random détermine ici, de manière aléatoire,
                //l'indice qui sera utilisée pour sélectionner le mot 
                // dans le tableau mots.
                Random rand = new Random();
                int indice = rand.Next(10);
                // Initialisation du mot à chercher.
                motRecherche = mots[indice].ToLower();
                // Initialisation du tableau des lettres trouvées dans le mot.
                lettresTrouve = new bool[motRecherche.Length];
                // Initialisation des variables.
                motTrouve = false;
                tentatives = 1;
                Console.WriteLine();
                Console.WriteLine($" Le mot recherché contient {motRecherche.Length} lettres.");
                // Création de la boucle
                while (tentatives <= tentativesMax) //Erreur, rajout de la variable tentativesMax (étais à 1)
                {
                    Console.WriteLine();
                    Console.Write(" Quelle lettre désirez-vous chercher dans le mot ? ");
                    repLettre = Char.TryParse(Console.ReadLine().ToLower(), out lettre); // Erreur missing , out lettre & .toUpper() -> .toLower()
                    // On efface le contenu de la console.
                    Console.Clear();
                    if (repLettre)
                    {
                        if(!lettres.Contains(lettre))
                        {
                            lettres += " " + lettre;
                        }
                        else
                        {
                            Console.WriteLine(" Vous avez déjà utilisé cette lettre dans votre recherche.");
                            tentatives--;
                            //Console.WriteLine(" Vous venez de perdre une tentative.");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Vous devez saisir qu'une seule lettre à la fois.");
                        tentatives--;
                    }

                    // Si le mot n'a pas été trouvé, on exécute la série d'instructions qui suit.
                    if (!motTrouve)
                    {
                        // Boucle qui compare les lettres du mot avec la lettre demandé.
                        // Si la lettre est trouvée dans le mot, on l'identifie dans le tableau lettresTrouvé.
                        for (int i = 0; i < motRecherche.Length; i++) // Erreur enlever le +1 (out of bounds error)
                        {
                            if (motRecherche[i] == lettre)
                            {
                                lettresTrouve[i] = true;
                            }
                        }
                        // initialisation de la variable messageMotCherche.
                        messageMotCherche = " ";
                        // Boucle qui compose la variable messageMotCherche
                        // qui affichera le résultat après comparaison.
                        for (int i = 0; i < motRecherche.Length; i++)
                        {
                            if (lettresTrouve[i])
                            {
                                messageMotCherche += motRecherche[i];
                            }
                            else
                            {
                                messageMotCherche += " _ ";
                            }
                        }
                        // Initialisation de la variable motTrouve.
                        motTrouve = true;
                        // Boucle qui vérifie si toutes les lettres du mot ont été trouvées.
                        for (int i = 0; i < lettresTrouve.Length; i++)
                        {
                            if (!lettresTrouve[i])
                            {
                                motTrouve = false;
                            }
                        }
                        // Si le mot a été trouvé, on met fin à la boucle.
                        if (motTrouve)
                        {
                            break;
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine(messageMotCherche);
                    Console.WriteLine();
                    Console.WriteLine($" Vous avez utilisé {tentatives} essais sur 15.");
                    Console.WriteLine();
                    // On affiche les lettres utilisées par le joueur.
                    Console.WriteLine($" Vous avez déjà utilisé les lettres suivantes :{lettres}.");
                    tentatives++;
                }
                // Si le mot a été trouvé, on affiche le message au vainqueur.
                if (motTrouve)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($" Félicitation, vous avez trouvé le mot {mots[indice]} en {tentatives} tentatives.");
                }
                // Sinon, on affiche un message au perdant.
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($" Désolé, vous n'avez pas réussi à trouver le mot {mots[indice]}.");
                    Console.WriteLine(" Meilleure chance la prochaine fois !");
                }
                // Boucle qui demande à l'utilisateur s'il désire jouer une autre partie.
                // La réponse doit être 'O' ou 'N'.
                do
                {
                    // On demande au joueur s'il veut jouer une autre partie
                    Console.WriteLine();
                    Console.Write(" Désirez-vous jouer une autre partie (O ou N) ?     ");
                    repLettre = Char.TryParse(Console.ReadLine().ToUpper(), out rep); //Erreur changer lettre to rep
                    // On efface le contenu de la console.
                    Console.Clear();
                    if(repLettre)
                    {
                        if (rep == 'O') 
                        {
                            reponse = true; //Erreur Changer rep par reponse
                            lettres = ""; //Reset letters
                            break; //Erreur ajouter cette ligne
                        }
                        else if (rep == 'N')
                        {
                            reponse = false;
                            break; //Erreur ajouter cette ligne
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Vous devez répondre par Oui (O) ou Non (N).");
                    }
                } while (rep != 'O' || rep != 'N'); //Error while NOT those chars
                // On efface le contenu de la console.
                Console.Clear();
            } while (reponse);
            // Attente de la fin du programme.
            Console.WriteLine();
            Console.WriteLine(" J'espère vous revoir bientôt !");
            Console.WriteLine();
            Console.WriteLine(" Appuyez sur une touche pour terminer le programme.");
            Console.ReadKey();
        }
    }
}