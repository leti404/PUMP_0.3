function toggleAdditionalFields() {
    const searchBy = document.getElementById("search-by").value;
    const additionalFieldsContainer = document.getElementById("additionalFieldsContainer");
    const codigoFields = document.getElementById("codigoFields");
    const fabricanteFields = document.getElementById("fabricanteFields");
    additionalFieldsContainer.style.display = "none";
    codigoFields.style.display = "none";
    fabricanteFields.style.display = "none";

    if (searchBy === "2") { // Código
        additionalFieldsContainer.style.display = "block";
        codigoFields.style.display = "block";
    } else if (searchBy === "5") { // Fabricante
        additionalFieldsContainer.style.display = "block";
        fabricanteFields.style.display = "block";
    }
}



document.getElementById("results").addEventListener("change", function () {
    var selectedOption = this.value;
    var parts = selectedOption.split('-');
    var lastPart = parts[parts.length - 1].trim();
});