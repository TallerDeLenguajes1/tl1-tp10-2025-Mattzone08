using System.Text.Json;
using EspacioClases;

HttpClient ClienteConsumo = new HttpClient(); // instancio un elemento de la clase HttpClient para realizar el consumo de la Api
var UrlApi = "https://jsonplaceholder.typicode.com/users/"; // declaro una variable que contendra la url de la api a consumir

HttpResponseMessage respuesta = await ClienteConsumo.GetAsync(UrlApi); // Realiza el consumo de la Api y guarda la respuesta de la api en la variable respuesta 

respuesta.EnsureSuccessStatusCode(); // Comprueba que la respuesta de la api sea valida, caso contrario larga una excepcion :(

string CuerpoRespuesta = await respuesta.Content.ReadAsStringAsync(); // convierte el cuerpo de la respuesta a string para poder deserializarla y instanciarla en la clases necesaria
List<Usuario> listUsuariotemp = JsonSerializer.Deserialize<List<Usuario>>(CuerpoRespuesta); // Deserealiza el CuerpoRespuesta que ya esta en string y los instancia en una lista de la clase Usuario

Usuario[] listUsuario = new Usuario[5]; // declaro un arreglo del la clase usuario para asi tener los 5 que necesito 


for (int i = 0; i < 5; i++)
{
    listUsuario[i] = listUsuariotemp[i];
    Console.WriteLine($"Nombre: {listUsuario[i].name} \n Correo Electronico: {listUsuario[i].email} \n Domicilio: {listUsuario[i].address.city} {listUsuario[i].address.street}");
    Console.WriteLine("/-----------/");

}

var opciones = new JsonSerializerOptions
{

    WriteIndented = true

};


string ListaUsuariosJsoneada = JsonSerializer.Serialize(listUsuario, opciones);
string ruta = "usuarios.json";

await File.WriteAllTextAsync(ruta, ListaUsuariosJsoneada);
Console.WriteLine("json guardado correctamente");

