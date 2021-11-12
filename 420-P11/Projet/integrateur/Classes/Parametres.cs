using System;
using System.Collections.Generic;
using System.Linq;

namespace integrateur.Classes
{
    public class Parametres
    {
        private static BanqueEntities _BanqueEntities;

        private static List<parametre> _infoParametres = new List<parametre>();

        private static string FindValue(Params param)
        {
            _BanqueEntities = new BanqueEntities();
            _infoParametres = _BanqueEntities.parametres.ToList();
            return _infoParametres.Find(x => x.description == param.ToString()).valeur;
        }
        public static parametre FindParam(Params param)
        {
            _BanqueEntities = new BanqueEntities();
            _infoParametres = _BanqueEntities.parametres.ToList();
            return _infoParametres.Find(x => x.description == param.ToString());
        }

        public decimal argent_max_guichet = decimal.Parse(FindValue(Params.argent_max_guichet));
        public decimal retrait_max_guichet = decimal.Parse(FindValue(Params.retrait_max_guichet));
        public bool guichet_ouvert = FindValue(Params.guichet_ouvert) == "1" ? true : false;
        public string compte_admin_code = FindValue(Params.compte_admin_code);
        public string compte_admin_pass = FindValue(Params.compte_admin_pass);
        public decimal argent_courrant_guichet = decimal.Parse(FindValue(Params.argent_courrant_guichet));
        public decimal frais_transaction = decimal.Parse(FindValue(Params.frais_transaction));

        public enum Params
        {
            argent_max_guichet,
            retrait_max_guichet,
            guichet_ouvert,
            compte_admin_code,
            compte_admin_pass,
            argent_courrant_guichet,
            frais_transaction
        }

        public void UpdateParam(Params param, string valeur)
        {
            parametre parametre = FindParam(param);
            _BanqueEntities.parametres.ToList().Find(x => x.id_param == parametre.id_param).valeur = valeur;
            _BanqueEntities.SaveChanges();
        }
    }
}
