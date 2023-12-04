
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Direccón de la API de la que se obtendrá la información.
        string url = "https://rickandmortyapi.com/api/character";

        // Se realiza la solicitud a la URL proporcionada.
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserializar el JSON a objetos C#
                    RootObject rickAndMortyData = JsonSerializer.Deserialize<RootObject>(content);

                    // Se itera a través de los resultados (personajes).

                    foreach (var character in rickAndMortyData.results)
                    {
                        if(character.name.Equals("Summer Smith"))
                        {
                            Console.WriteLine($"ID: {character.id}\nNombre: {character.name}\nEstatus: {character.status}\nEspecie: {character.species}\nTipo: {character.type}\nGénero: {character.gender}\nOrigen: {character.origin}\nLocalización: {character.location}\nImagen: {character.image}\nEpispdio: {character.episode}\nURL: {character.url}\nCreación: {character.created}");
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Excepción: {e.Message}");
            }
        }
    }


    // A continucaión se definen las clases que representan la estrucuta de datos JSON devueltos por la API.

    [Serializable]
    public class Info
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }

    [Serializable]
    public class Origin
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [Serializable]
    public class Location
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [Serializable]

    public class Character
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public Origin origin { get; set; }
        public Location location { get; set; }
        public string image { get; set; }
        public List<string> episode { get; set; }
        public string url { get; set; }
        public DateTimeOffset created { get; set; }
    }

    // Utilzamos esta clase para deserializar la estructura JSON con los elementos que tenga el JSON, en este caso son dos, info y results.

    [Serializable]
    public class RootObject
    {
        public Info info { get; set; }
        public List<Character> results { get; set; }
    }
}