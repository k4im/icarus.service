using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.application.models;

namespace icarus.application.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient _http;

        public Repository(HttpClient http)
        {
            _http = http;
        }

        public string LastSearch { get; set; }

        public Task Create(string route, ProjectDTO model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string route, int id)
        {
            throw new NotImplementedException();
        }

        public Task Get(string route)
        {
            throw new NotImplementedException();
        }

        public async Task<String> GetFilter(string route, string search)
        {
            LastSearch = search;
            HttpResponseMessage  reponse = await _http.GetAsync($"{route}/{LastSearch}");
            reponse.EnsureSuccessStatusCode();
            var responseBody = await reponse.Content.ReadAsStringAsync();
            return responseBody;
        }

        public Task Update(string route, int id, ProjectDTO model)
        {
            throw new NotImplementedException();
        }
    }
}