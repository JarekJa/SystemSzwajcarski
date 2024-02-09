function Addaplayer(i) {
    var button = document.getElementById("tournamentbutton/" + i);
    var hidden = document.getElementById("ToAdd_"+i+"_");
    if (button.innerHTML =="Dodaj")
    {
        button.innerHTML = "Usuń";
        hidden.value = "True";
    }
    else
    {
        hidden.value = "False";
        button.innerHTML = "Dodaj";
    }
}
