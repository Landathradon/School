using System;
using System.Collections.Generic;
using System.Linq;

namespace integrateur.Classes
{
    class TypeDeTransaction
    {
        private static BanqueEntities _BanqueEntities;

        private static List<type_transaction> _infoTypeTransaction = new List<type_transaction>();

        private static int FindType(string description)
        {
            _BanqueEntities = new BanqueEntities();
            _infoTypeTransaction = _BanqueEntities.type_transaction.ToList();
            return _infoTypeTransaction.Find(x => x.description == description).idtype_transaction;
        }

        public static int Depot = FindType("Dépot");
        public static int Retrait = FindType("Retrait");
        public static int Transfert = FindType("Transfert");
        public static int Paiement = FindType("Paiement");
    }
}
