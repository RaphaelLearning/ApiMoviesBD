using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIMoviesBD.Domain.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using APIMoviesBD.Domain.Repositories;

namespace APIMoviesBD.Clients
{
    public class APIMovieBDClient : IAPIMovieBDClient
    {
        private static string _urlBase;
        private static string _apiKey;
        private HttpClient _client = new HttpClient();
        private IConfiguration _configuration;

        public void Config(HttpClient client,
           IConfiguration configuration)
        {
            _client = client;
            _client.BaseAddress = new Uri(
                configuration.GetSection("API_Access:UrlBase").Value);
            _urlBase = _client.BaseAddress.ToString();
            _apiKey = configuration.GetSection("API_Access:APIKey").Value;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _configuration = configuration;
        }

        public async Task<IEnumerable<MovieModel>> ListAsync()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"appsettings.json");
            var config = builder.Build();

            Config(_client, config);

            return Get();
        }


        public IEnumerable<MovieModel> Get()
        {
            try
            {

                #region ENCAIXAR LÓGICA PARA TRAZER RESULTADOS DE TODAS AS PÁGINAS
                //Movie movies;

                //        HttpResponseMessage response = client.GetAsync(
                //                    _urlBase + "upcoming?api_key=" + _apiKey + "&page=1").Result;

                //        if (response.IsSuccessStatusCode)
                //        {  //GET

                //            var json = await response.Content.ReadAsStringAsync();
                //            movies = JsonConvert.DeserializeObject<Movie>(json);

                //            //foreach (PSObject result in ps.Invoke())
                //            if (movies.TotalResults > 0)
                //            {
                //                // calculo de loops
                //                var count = movies.TotalResults / movies.TotalPages;

                //                List<Movie> lista = new List<Movie>();
                //                movies.ListMovies = new List<Movie>();

                //                for (_page = 1; _page <= count; _page++)
                //                {
                //                    HttpResponseMessage responseOutros = client.GetAsync(
                //              _urlBase + "upcoming?api_key=" + _apiKey + "&page=" + _page).Result;

                //                    var jsonOutros = await responseOutros.Content.ReadAsStringAsync();
                //                    lista.Add((JsonConvert.DeserializeObject<Movie>(jsonOutros)));
                //                }

                //                return movies;
                #endregion

                MovieModel movies;
                List<MovieModel> resultado = new List<MovieModel>();

                HttpResponseMessage response = _client.GetAsync(
                            _urlBase + "upcoming?api_key=" + _apiKey + "&page=1").Result;

                var json = response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<MovieModel>(json.Result);
                resultado.Add(movies);

                //return Enumerable.Range(1, 5).Select(index => new MovieModel { }).ToArray();
                //return resultado.ToList();
                return resultado.ToList();

            }

            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
