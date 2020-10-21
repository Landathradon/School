var Cour = /** @class */ (function () {
    function Cour(code, nom) {
        this.evaluations = [];
        this.code = code;
        this.nom = nom;
    }
    Cour.prototype.getCode = function () {
        return this.code;
    };
    Cour.prototype.getNom = function () {
        return this.nom;
    };
    Cour.prototype.getEvaluation = function () {
        return this.evaluations;
    };
    Cour.prototype.setCode = function (code) {
        this.code = code;
    };
    Cour.prototype.setNom = function (nom) {
        this.nom = nom;
    };
    Cour.prototype.setEvaluation = function (evaluations) {
        this.evaluations.push(evaluations);
    };
    return Cour;
}());
var Evaluation = /** @class */ (function () {
    function Evaluation(code, note) {
        if (note === void 0) { note = 0; }
        this.note = 0;
        this.code = code;
        this.note = note;
    }
    Evaluation.prototype.getCode = function () {
        return this.code;
    };
    Evaluation.prototype.getNote = function () {
        return this.note;
    };
    Evaluation.prototype.setCode = function (code) {
        this.code = code;
    };
    Evaluation.prototype.setNote = function (note) {
        this.note = note;
    };
    return Evaluation;
}());
var Etudiant = /** @class */ (function () {
    function Etudiant(nom, id) {
        this.listeCours = [];
        this.nom = nom;
        this.id = id;
    }
    Etudiant.prototype.getNom = function () {
        return this.nom;
    };
    Etudiant.prototype.getId = function () {
        return this.id;
    };
    Etudiant.prototype.setNom = function (nom) {
        this.nom = nom;
    };
    Etudiant.prototype.setId = function (id) {
        this.id = id;
    };
    Etudiant.prototype.setCour = function (cour) {
        this.listeCours.push(cour);
    };
    Etudiant.prototype.showCours = function () {
        var message = "ID: " + this.id + "\nNom: " + this.nom + "\n";
        for (var _i = 0, _a = this.listeCours; _i < _a.length; _i++) {
            var cour = _a[_i];
            message += "Code du cours: " + cour.getCode() + " | Nom du cours: " + cour.getNom() + "\n";
            for (var _b = 0, _c = cour.getEvaluation(); _b < _c.length; _b++) {
                var evaluation = _c[_b];
                message += "Code d'\u00E9valuation: " + evaluation.getCode() + " | Note: " + evaluation.getNote() + "\n";
            }
        }
        return message;
    };
    return Etudiant;
}());
var etudiant = new Etudiant("Joe Bleau", "645-12345");
var courDev = new Cour("DW1F", "Developpement Web 1");
var courNT = new Cour("NTEF", "Nouvelles technologies");
courDev.setEvaluation(new Evaluation("DW1FX1", 80));
courDev.setEvaluation(new Evaluation("DW1FX2", 70));
courDev.setEvaluation(new Evaluation("DW1FP1", 0));
courNT.setEvaluation(new Evaluation("NTEFX1", 95));
courNT.setEvaluation(new Evaluation("NTEFP1", 85));
courNT.setEvaluation(new Evaluation("NTEFP2", 70));
etudiant.setCour(courDev);
etudiant.setCour(courNT);
var message = etudiant.showCours();
console.log(message);
var app = document.getElementById("app");
var regex = RegExp("\n");
var msg = message.split(regex);
for (var i = 0; i < msg.length; i++) {
    var p = document.createElement("p");
    p.textContent = msg[i].toString();
    app === null || app === void 0 ? void 0 : app.append(p);
}
