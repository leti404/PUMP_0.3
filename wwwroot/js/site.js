function toggleAdditionalFields() {
    const searchBy = document.getElementById("search-by").value;
    const additionalFieldsContainer = document.getElementById("additionalFields");
    additionalFieldsContainer.innerHTML = ""; // Clear existing fields

    if (searchBy === "2") { // Código
        additionalFieldsContainer.innerHTML = `
            <label for="codiCorto">Código Corto:</label>
            <select name="codiCorto" class="inputdatos" id="codiCorto">
                @foreach (string MiniCod in BD.ObtenerListaCodigos())
                {
                    <option value="@MiniCod">@MiniCod</option>
                }
            </select>
        `;
    } else if (searchBy === "5") { // Fabricante
        additionalFieldsContainer.innerHTML = `
            <label for="fabricanteNomb">Fabricante:</label>
            <select name="fabricanteNomb" class="inputdatos" id="fabricanteNomb">
                <option value="X">Fabricante X</option>
                <option value="Y">Fabricante Y</option>
                <option value="Z">Fabricante Z</option>
            </select>
        `;
    }
}


document.getElementById("results").addEventListener("change", function () {
    var selectedOption = this.value;
    var parts = selectedOption.split('-');
    var lastPart = parts[parts.length - 1].trim();
});