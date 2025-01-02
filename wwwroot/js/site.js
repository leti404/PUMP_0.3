function toggleAdditionalFields() {
    const searchBy = document.getElementById("search-by").value;
    const additionalFieldsContainer = document.getElementById("additionalFields");
    additionalFieldsContainer.innerHTML = "";
    if (searchBy === "2") { // Código
        additionalFieldsContainer.innerHTML = `
            <label for="codiCorto">Código Corto:</label>
            <select asp-for="CodiCorto" class="inputdatos" id="codiCorto">
            @foreach (string MiniCod in BD.ObtenerListaCodigos())
            {
                <option value="@MiniCod">@MiniCod</option>
            }
            </select>
        `;
    } else if (searchBy === "5") { // Fabricante esto esta en el chat del 12/19/2024 1:45pm first data imput extraction
        additionalFieldsContainer.innerHTML = `
            <label for="fabricanteNomb">Fabricante:</label>
            <select asp-for="FabricanteNomb" class="inputdatos" id="fabricanteNomb">
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