function mostrarFormularioCrear() {
    const form = document.getElementById("formCrearTarea");

    if(form.style.display == "inline-block"){

        form.style.display = "none";

    } else{

        form.style.display = "inline-block";

    }
}

function mostrarFormularioActualizar(idTarea) {
    const form = document.getElementById(`formActualizarTarea-${idTarea}`);
    if (form.style.display === "none" || form.style.display === "") {
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
}
