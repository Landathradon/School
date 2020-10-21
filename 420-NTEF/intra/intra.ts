class Cour {
    code: String
    nom: String
    evaluations: Evaluation[] = []

    constructor(code: String, nom: String) {
        this.code = code
        this.nom = nom
    }

    getCode(): String{
        return this.code
    }
    getNom(): String{
        return this.nom
    }
    getEvaluation(): Evaluation[]{
        return this.evaluations
    }
    setCode(code: String){
        this.code = code
    }
    setNom(nom: String){
        this.nom = nom
    }
    setEvaluation(evaluations: Evaluation){
        this.evaluations.push(evaluations)
    }
}

class Evaluation {
    code: String
    note: number = 0

    constructor(code: String, note: number = 0) {
        this.code = code
        this.note = note
    }

    getCode(): String{
        return this.code
    }
    getNote(): number{
        return this.note
    }
    setCode(code: String){
        this.code = code
    }
    setNote(note: number){
        this.note = note
    }
}

class Etudiant {
    nom: String
    id: String
    listeCours: Cour[] = []

    constructor(nom, id) {
        this.nom = nom
        this.id = id
    }

    getNom(): String{
        return this.nom
    }
    getId(): String{
        return this.id
    }
    setNom(nom: String){
        this.nom = nom
    }
    setId(id: String){
        this.id = id
    }
    setCour(cour: Cour){
        this.listeCours.push(cour)
    }
    showCours(): String{
        let message: String = `ID: ${this.id}\nNom: ${this.nom}\n`
        for (let cour of this.listeCours) {
            message += `Code du cours: ${cour.getCode()} | Nom du cours: ${cour.getNom()}\n`
            for(const evaluation of cour.getEvaluation()){
                message += `Code d'évaluation: ${evaluation.getCode()} | Note: ${evaluation.getNote()}\n`
            }
        }
        return message
    }
}

let etudiant = new Etudiant("Joe Bleau", "645-12345")
let courDev = new Cour("DW1F", "Developpement Web 1")
let courNT = new Cour("NTEF", "Nouvelles technologies")

courDev.setEvaluation(new Evaluation("DW1FX1", 80))
courDev.setEvaluation(new Evaluation("DW1FX2", 70))
courDev.setEvaluation(new Evaluation("DW1FP1", 0))

courNT.setEvaluation(new Evaluation("NTEFX1", 95))
courNT.setEvaluation(new Evaluation("NTEFP1", 85))
courNT.setEvaluation(new Evaluation("NTEFP2", 70))

etudiant.setCour(courDev)
etudiant.setCour(courNT)

const message = etudiant.showCours()

console.log(message)

const app = document.getElementById("app")

const regex = RegExp("\n")
const msg = message.split(regex)

for (let i = 0; i < msg.length; i++) {
    const p = document.createElement("p")
    p.textContent = msg[i].toString()
    app?.append(p)
}
