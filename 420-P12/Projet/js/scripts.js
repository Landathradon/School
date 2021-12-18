$( document ).ready(function() {
    $(".datepicker").datepicker();
});

function editDatePicker(date) {
    $(".datepicker").datepicker("option", "minDate", new Date(date));
}

$('#reserverModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    var code_livre = button.data('code_livre')

    var modal = $(this)
    let date = $('#docDateFin')[0].textContent;
    modal.find('.modal-body #code_livre').val(code_livre)
    modal.find('#dateDebut').val(date);

    date = new Date(date);
    date = date.setDate(date.getDate() + 1);
    editDatePicker(date)
})
$('#pretModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    var code_livre = button.data('code_livre')

    var modal = $(this)
    let date = $('#docDateDebut')[0].textContent;
    modal.find('.modal-body #code_livre').val(code_livre)
    modal.find('#datePret').val(date);
    modal.find('#dateFin').val(date);

    hideUsers()
    editDatePicker(date)
})

new Vue({
    el: '#VueApp',
    data: {
        searchQuery: '',
        arrayOfDocuments: [],
        selectedBook: -1,
        isLoggedOn: 0
    },
    methods: {
        loadList() {
            hideUsers(false)
            var self = this;
            self.selectedBook = -1;
            $('#book-card').width("350px").height("350px");

            if(arrayData != null || arrayData !== []){
                self.arrayOfDocuments = arrayData;
            }
        },
        filteredList(code = '') {
            return this.arrayOfDocuments.filter(Document => {
                if(code !== ''){
                    if(Document.Code === code) {return Document}
                } else {
                    return Document.Titre.toLowerCase().includes(this.searchQuery.toLowerCase())
                }
            })
        },
        showBook(code) {
            this.selectedBook = code;
            this.arrayOfDocuments = this.filteredList(code);
            $('#book-card').width("700px").height("500px");
        },
        UpdateSearchBar(){
            const Livre = new URL(location.href).searchParams.get('Livre')
            if(Livre !== null){
                this.searchQuery = Livre.replace('%20',' ')
            }
        }
    },
    mounted: function () {
        this.$nextTick(function () {
            if(estConnecter != null || estConnecter !== []){
                this.isLoggedOn = estConnecter;
            }
            this.loadList();
            this.UpdateSearchBar();
        });
    }
})

function hideUsers(hideBool = true){
    let Nom_membre_reserver = ""
    if($('#Nom_membre_reserver')[0] !== undefined){
        Nom_membre_reserver = $('#Nom_membre_reserver')[0].textContent
    }

    $('#membreRechercher option').each( (index, value) => {
        if (!hideBool){
            $('#membreRechercher option')[index].hidden = false;
        } else if (hideBool && Nom_membre_reserver !== "" && value.textContent !== Nom_membre_reserver){
            $('#membreRechercher option')[index].hidden = true;
        }
    })
}

function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
        function(m,key,value) {
            vars[key] = value;
        });
    return vars;
}