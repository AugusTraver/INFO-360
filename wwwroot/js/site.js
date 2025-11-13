function mostrarFormularioCrear() {
    const form = document.getElementById("formCrearTarea");
    form.style.display = "inline-block";
}

function mostrarFormularioActualizar(idTarea) {
    const form = document.getElementById(`formActualizarTarea-${idTarea}`);
    if (form.style.display === "none" || form.style.display === "") {
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
}
