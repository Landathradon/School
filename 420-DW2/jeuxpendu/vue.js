var app = new Vue({
    el: '#app',
    data: {
        message: 'jeux du pendu!',
        lettre: "",
        lettres: [],
        mot: "-",
        motTrouver: "",
        tentatives: 0,
        tentativesMax: 15,
        tentativesErreur: 0,
        gameIsOver: false,
        gameWin: false,
        gameRunning: false,
        menuBool: true,
        listMots: [
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
        ],
    },
    methods: {
        startGame(){
            this.gameRunning = true;
            this.menuBool = false;
            let random = Math.floor(Math.random() * this.listMots.length);
            this.mot = this.listMots[random].toLowerCase();
            for (let i = 0;i < this.mot.length;i++){
                this.motTrouver += "_";
            }
        },
        gameOver(){
            this.gameIsOver = true;
        },
        restartGame(){
            this.tentatives = 0;
            this.tentativesErreur = 0;
            this.lettres = [];
            this.motTrouver = "";
            this.gameIsOver = false;
            this.startGame();
        },
        endGame(){
            this.tentatives = 0;
            this.tentativesErreur = 0;
            this.lettres = [];
            this.motTrouver = "";
            this.gameRunning = false;
        },
        addLetters(lettre){
            if(!this.checkLength(lettre)){
                let lettreExistante = this.lettres.filter(function(elem){
                    if(elem.value === lettre.value.toLowerCase().trim()) return elem.value;
                });
                if(lettreExistante.length <= 0){
                    this.lettres.push({value: lettre.value.toLowerCase().trim()});
                    this.checkWord();
                    this.tentatives++;
                }
            }

            this.lettre = "";
        },
        checkLength(){
            return this.lettre.length > 1;
        },
        checkWord(){
            let derniereLettre = this.lettres[this.lettres.length - 1].value;

            if(this.mot.includes(derniereLettre)){
                let tempMot = this.mot.split("");
                let indexList = [];

                tempMot.forEach(function (val, i) {

                    if(val === derniereLettre){
                        indexList.push(i);
                    }

                })

                let tempMotTrouver = this.motTrouver.split("");
                indexList.forEach(function (val) {
                    tempMotTrouver[val] = derniereLettre;
                })
                this.motTrouver = tempMotTrouver.join("");
            } else {
                this.tentativesErreur++;
            }

            if(this.mot === this.motTrouver){
                this.gameWin = true;
                this.gameIsOver = true;
            }
        }
    },
    beforeMount(){
        // this.startGame()
    },
    watch: {
        tentatives: function () {
            if (this.tentatives >= this.tentativesMax){
                this.gameOver();
            }
        }
    }
})