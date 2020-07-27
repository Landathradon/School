$( document ).ready(function() {
    let modif = false;
    $("#showAll").click( function () {
        $(".note").each(function () {
            $(this).closest("tr").show();
        })
        showMoyenne(false);
    });
    $("#showDone").click( function () {
        $(".note").each(function () {
            if(parseInt($(this).text()) === 0 || parseInt($(this).val()) === 0){
                $(this).closest("tr").hide();
            }
        })
        showMoyenne(true);
    });
    $("#modifDesc").click( function () {
        $(".desc").each(function () {
            if(modif){
                let textVal = $(this).contents()[0].value;
                $(this).html(textVal);
            } else {
                let textVal = $(this).text();
                let idVal = $(this).prev().text();
                $(this).html("<input type='text' class='form-control' name='desc_"+idVal+"' value='"+textVal+"'>");
            }
        })
        if(modif) {
            modif = false;
        } else {
            modif = true;
        }
    });
});

function showMoyenne(hidden){
    let i = 0;
    let total = 0;
    $(".note-cour").each(function () {
        if(hidden) {
            if (parseInt($(this).text()) === 0) {
                return;
            }
        }
        total += parseInt($(this).text());
        i++;
    })

    const calcul = (total/i);
    $("#moyenne").text(calcul+"%");
}