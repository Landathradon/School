using System;
using System.Collections.Generic;
using System.Linq;

namespace integrateur.Classes
{
    class TypeDeCompte
    {
        private static BanqueEntities _BanqueEntities;

        private static List<type_compte> _infoTypeCompte = new List<type_compte>();

        private static int FindType(string description)
        {
            _BanqueEntities = new BanqueEntities();
            _infoTypeCompte = _BanqueEntities.type_compte.ToList();
            return _infoTypeCompte.Find(x => x.description == description).idtype_compte;
        }
        public static string ReturnName(int idtype_compte)
        {
            _BanqueEntities = new BanqueEntities();
            _infoTypeCompte = _BanqueEntities.type_compte.ToList();
            return _infoTypeCompte.Find(x => x.idtype_compte == idtype_compte).description;
        }

        public static int Cheque = FindType("Chèque");
        public static int Epargne = FindType("Épargne");
        public static int Hypothecaire = FindType("Hypothécaire");
        public static int MargeDeCredit = FindType("Marge de crédit");
    }
}
