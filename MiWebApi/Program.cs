using System.Text.Json;
using EspacioClases;

HttpClient ClienteConsumo = new HttpClient(); // instancio un elemento de la clase HttpClient para realizar el consumo de la Api
var UrlApi = "https://v2.jokeapi.dev/joke/Programming,Miscellaneous,Dark,Pun,Spooky,Christmas?lang=es&type=single&amount=10"; // declaro una variable que contendra la url de la api a consumir

HttpResponseMessage respuesta = await ClienteConsumo.GetAsync(UrlApi); // Realiza el consumo de la Api y guarda la respuesta de la api en la variable respuesta 

respuesta.EnsureSuccessStatusCode(); // Comprueba que la respuesta de la api sea valida, caso contrario larga una excepcion :(

string CuerpoRespuesta = await respuesta.Content.ReadAsStringAsync(); // convierte el cuerpo de la respuesta a string para poder deserializarla y instanciarla en la clases necesaria
Bromas listbromas = JsonSerializer.Deserialize<Bromas>(CuerpoRespuesta); // Deserealiza el CuerpoRespuesta que ya esta en string y los instancia en una variable de la clase bromas


foreach (var bromita in listbromas.jokes)
{
    
    Console.WriteLine($"Categoria de la Broma: {bromita.category} \n Broma: {bromita.joke} \n idioma de la broma: {bromita.lang}");
    Console.WriteLine("/---------------/");

}


var opciones = new JsonSerializerOptions  // Bloque que permite indentar el json asi se ve mas bonito
{
    WriteIndented = true
};

string ListaJsoneada = JsonSerializer.Serialize(listbromas, opciones); // serealizo la lista a json (se guarda en formato string)

string ruta = "Bromas.json";

if (File.Exists(ruta))
{
    // Si el archivo existe, agregar una coma y el nuevo JSON
    File.AppendAllText(ruta, "," + ListaJsoneada);
    Console.WriteLine("Contenido agregado al archivo existente.");
}
else
{
    // Si no existe, crear un archivo JSON con un array
    File.WriteAllText(ruta,ListaJsoneada);
    Console.WriteLine("Archivo creado con contenido inicial.");
}
