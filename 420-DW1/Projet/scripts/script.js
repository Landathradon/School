$(document).ready(function () {
    const now = new Date();
    const year = now.getFullYear();
    $('#curDate').text("19/4/2020");
    // $('#curDate').text(`${now.getDate()}/${now.getMonth() + 1}/${year}`);
    $('#curYear').text(year);
    $('#datepicker').datepicker({
        uiLibrary: 'bootstrap4'
    });

    $('#research_date').datepicker({
        uiLibrary: 'bootstrap4'
    });

    $('#datepickerBtn').click(function (e) {
        e.preventDefault();
        dateSelector($('#datepicker').val());
    });

    $('a').click(function (e) {
        if($(this).children().is("img.card-img-top")){
            console.log($(this).children());
        } else {
            e.preventDefault();
            redirectToUrl(e.target.href);
        }
    });

    if(getUrlVars()["research_date"] != null){
        $('#user-info').removeClass('hidden');
        $('#search_name').text(getUrlVars()["research_prenom"]);
        $('#search_lastname').text(getUrlVars()["research_nom"]);
        $('#search_date').text(getUrlVars()["research_date"]);
        $('#search_sign').text(getUrlVars()["research_sign"]);
    }

    $('#research_sign').on('change',function(){
        let selected = $(this).val();
        $('#research_form').prop('action',`signes/${selected}.html`);
    });
    $('#research_date').on('change',function(){
        dateSelector($('#research_date').val(), true);
    });
});

function dateSelector(userDate, switchSign = false) {
    let date = new Date(userDate);

    for (let i = 0; i <= zodiacSignsDates.length; i++) {
        let tmpDateDebut, tmpDateFin;

        if(zodiacSignsDates[i].moisDebut === 12){//Add 1 year if beginning month is December.
            tmpDateFin = new Date(`${zodiacSignsDates[i].moisFin}/${zodiacSignsDates[i].jourFin}/${date.getFullYear()+1}`);
        } else {
            tmpDateFin = new Date(`${zodiacSignsDates[i].moisFin}/${zodiacSignsDates[i].jourFin}/${date.getFullYear()}`);
        }

        if(date.getMonth() === 0 && date.getDate() < zodiacSignsDates[i+1].jourDebut){//Substract 1 year if selected month is January and selected day is smaller than next sign first day.
            i = 11; //Select End of year sign
            tmpDateDebut = new Date(`${zodiacSignsDates[i].moisDebut}/${zodiacSignsDates[i].jourDebut}/${date.getFullYear()-1}`);
            tmpDateFin = new Date(`${zodiacSignsDates[i].moisFin}/${zodiacSignsDates[i].jourFin}/${date.getFullYear()}`);
        } else {
            tmpDateDebut = new Date(`${zodiacSignsDates[i].moisDebut}/${zodiacSignsDates[i].jourDebut}/${date.getFullYear()}`);
        }

        if (tmpDateDebut.getTime() <= date.getTime() && tmpDateFin.getTime() >= date.getTime()) {
            if(switchSign){
                $(`#research_sign`).val(zodiacSignsDates[i].name);
                $('#research_form').prop('action',`signes/${zodiacSignsDates[i].filename}.html`);
                break;
            }

            //Si la date est entre celle du début et celle de fin .. Continuez ici
            redirectToUrl(`signes/${zodiacSignsDates[i].filename}.html`);
            break;
        }
    }
}

function redirectToUrl(url){
    let baseUrl = window.location.toString();

    if(baseUrl.includes("signes/")) {
        if(baseUrl.includes(url.split("/")[0])){
            url = url.split("/");
            url = url[url.length-1];
        }
        if (url === "index.html" || url === "astrologie.html" || url ==="contact.html") {
            url = `../${url}`;
            console.log(url);
        }
    }

    window.location.replace(url);
}

function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
        function(m,key,value) {
            vars[key] = unescape(decodeURI(value));
        });
    return vars;
}

const zodiacSignsDates = [
    {name: "Verseau", filename: "verseau", moisDebut: 1, jourDebut: 20, moisFin: 2, jourFin: 18},
    {name: "Poissons", filename: "poissons", moisDebut: 2, jourDebut: 19, moisFin: 3, jourFin: 20},
    {name: "Belier", filename: "belier", moisDebut: 3, jourDebut: 21, moisFin: 4, jourFin: 19},
    {name: "Taureau", filename: "taureau", moisDebut: 4, jourDebut: 20, moisFin: 5, jourFin: 20},
    {name: "Gemeaux", filename: "gemeaux", moisDebut: 5, jourDebut: 21, moisFin: 6, jourFin: 20},
    {name: "Cancer", filename: "cancer", moisDebut: 6, jourDebut: 21, moisFin: 7, jourFin: 22},
    {name: "Lion", filename: "lion", moisDebut: 7, jourDebut: 23, moisFin: 8, jourFin: 22},
    {name: "Vierge", filename: "vierge", moisDebut: 8, jourDebut: 23, moisFin: 9, jourFin: 22},
    {name: "Balance", filename: "balance", moisDebut: 9, jourDebut: 23, moisFin: 10, jourFin: 22},
    {name: "Scorpion", filename: "scorpion", moisDebut: 10, jourDebut: 23, moisFin: 11, jourFin: 21},
    {name: "Sagittaire", filename: "sagittaire", moisDebut: 11, jourDebut: 22, moisFin: 12, jourFin: 21},
    {name: "Capricorne", filename: "capricorne", moisDebut: 12, jourDebut: 22, moisFin: 1, jourFin: 19},
]