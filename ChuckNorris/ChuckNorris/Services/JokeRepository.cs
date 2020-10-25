using ChuckNorris.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChuckNorris.Services.DTO;
using System.Linq;

namespace ChuckNorris.Services
{
    public class JokeRepository : IRepository<Joke>
    {
        private HttpClient _client;

        private FavoriteService _favoriteService;
        public JokeRepository()
        {
            _client = new HttpClient() { BaseAddress = new Uri("https://api.chucknorris.io/jokes/") };
            _favoriteService = Xamarin.Forms.DependencyService.Get<FavoriteService>();
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var url = "categories";
            var categories = await GetTAsync<IEnumerable<string>>(url);
            categories = FormatStrings(categories);
            return categories;
        }
        public async Task<Joke> GetFromCategory(string category)
        {
            var url = "random?category=" + category.ToLower();
            Joke joke = await GetTAsync<Joke>(url);
            // Add IsFavorite to Joke
            joke.IsFavorite = _favoriteService.IsFavorite(joke);
            joke.ImageSource = _favoriteService.GetIcon(joke);

            return joke;
        }
        public async Task<IEnumerable<Joke>> GetFromSearch(string query, string filter = null)
        {
            var response = await _client.GetAsync("search?query=" + query);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                QueryDTO jokeDTO = JsonConvert.DeserializeObject<QueryDTO>(responseString);
                var jokes = ConvertFromDTO(jokeDTO);
                
                if(filter == null ^ filter != "No filter")
                {
                    var favoreiteService = Xamarin.Forms.DependencyService.Get<FavoriteService>();
                    jokes = jokes.Where(joke => favoreiteService.IsFavorite(joke));
                }

                return jokes;

            }

            throw new Exception("Error retriving random joke from search");
        }

        

        #region Private Methods
        private async Task<T> GetTAsync<T>(string uri) 
        {
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var deserializedObject = JsonConvert.DeserializeObject<T>(responseString);
                return deserializedObject;
            }

            throw new Exception("Error retriving random joke from category");
        }

        private IEnumerable<string> FormatStrings(IEnumerable<string> strings)
        {
            List<string> returnStrings = strings.ToList();
            for (int i = 0; i < returnStrings.Count(); i++)
            {
                var charToUpper = Char.ToUpper(returnStrings[i][0]);
                var tempstring = charToUpper + returnStrings[i].Substring(1);
                returnStrings[i] = tempstring;
            }
            return returnStrings;
        }
        private IEnumerable<Joke> ConvertFromDTO(QueryDTO queryDTO)
        {
            var query = queryDTO.result;
            var values = query.Select(joke => joke.value).ToArray();
            var ids = query.Select(joke => joke.id).ToArray();


            Joke[] jokes = new Joke[values.Length];

            for(int i = 0; i < values.Count(); i++)
            {
                Joke joke = new Joke() {
                    Value = values[i], 
                    ID = ids[i]
                };
                joke.IsFavorite = _favoriteService.IsFavorite(joke);
                joke.ImageSource = _favoriteService.GetIcon(joke);
                jokes[i] = joke;
            }


            return jokes;
        }
        #endregion
    }
}
