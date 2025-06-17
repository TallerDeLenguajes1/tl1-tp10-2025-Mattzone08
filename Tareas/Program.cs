
using System.Text.Json;
using EspacioTarea;

HttpClient ClienteConsumo = new HttpClient(); // instancio un elemento de la clase HttpClient para realizar el consumo de la Api
var UrlApi = "https://jsonplaceholder.typicode.com/todos/"; // declaro una variable que contendra la url de la api a consumir

HttpResponseMessage respuesta = await ClienteConsumo.GetAsync(UrlApi); // Realiza el consumo de la Api y guarda la respuesta de la api en la variable respuesta 

respuesta.EnsureSuccessStatusCode(); // Comprueba que la respuesta de la api sea valida, caso contrario larga una excepcion :(

string CuerpoRespuesta = await respuesta.Content.ReadAsStringAsync(); // convierte el cuerpo de la respuesta a string para poder deserializarla y instanciarla en la clases necesaria
List<Tarea> listTarea = JsonSerializer.Deserialize<List<Tarea>>(CuerpoRespuesta); // Deserealiza el CuerpoRespuesta que ya esta en string y los instancia en una lista de la clase tarea


foreach (var tareita in listTarea)
{

    Console.WriteLine($"ID de Usuario: {tareita.userId} \n ID de tarea: {tareita.id} \n Tarea: {tareita.title} \n Estado de completada: {tareita.completed}");
    Console.WriteLine("/---------------/");

}

var listTareaOrdenada = listTarea.OrderBy(t => t.completed).ToList(); // Ordena la lista segun pendientes

Console.WriteLine("/---------------/ Aqui las tareas ordenadas segun completadas /---------------/");

foreach (var tareita in listTareaOrdenada)
{

    Console.WriteLine($"ID de Usuario: {tareita.userId} \n ID de tarea: {tareita.id} \n Tarea: {tareita.title} \n Estado de completada: {tareita.completed}");
    Console.WriteLine("/---------------/");

}

var opciones = new JsonSerializerOptions  // Bloque que permite identar el json asi se ve mas bonito
{
    WriteIndented = true
};

string ListaJsoneada = JsonSerializer.Serialize(listTareaOrdenada, opciones); // serealizo la lista a json (se guarda en formato string)

string ruta = "tareas.json";

        if (File.Exists(ruta))
        {
            // Si el archivo existe, agregar una coma y el nuevo JSON
            File.AppendAllText(ruta, "," + ListaJsoneada);
            Console.WriteLine("Contenido agregado al archivo existente.");
        }
        else
        {
            // Si no existe, crear un archivo JSON con un array
            File.WriteAllText(ruta, "[" + ListaJsoneada + "]");
            Console.WriteLine("Archivo creado con contenido inicial.");
        }






