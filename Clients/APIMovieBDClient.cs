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
        private static int _page;
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
                MovieModel movies;
                List<MovieModel> resultado = new List<MovieModel>();

                HttpResponseMessage response = _client.GetAsync(
                            _urlBase + "upcoming?api_key=" + _apiKey + "&page=1").Result;

                if (response.IsSuccessStatusCode)
                {  //GET

                    var json = response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<MovieModel>(json.Result);
                    resultado.Add(movies);

                    if (movies.TotalResults > 0)
                    {
                        var countPages = movies.TotalResults / movies.TotalPages;

                        for (_page = 2; _page <= countPages; _page++)
                        {
                            HttpResponseMessage responseOutros = _client.GetAsync(
                      _urlBase + "upcoming?api_key=" + _apiKey + "&page=" + _page).Result;

                            var jsonOutros = responseOutros.Content.ReadAsStringAsync();
                            movies = JsonConvert.DeserializeObject<MovieModel>(jsonOutros.Result);
                            resultado.Add(movies);
                        }
                    }
                }

                return resultado.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
