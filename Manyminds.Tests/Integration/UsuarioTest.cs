using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Application.ViewModels.Response;
using Manyminds.Application.ViewModels.Response.Usuario;
using Manyminds.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace Manyminds.Tests.Integration
{
    public class UsuarioTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public UsuarioTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            Autenticar();
        }

        protected void Autenticar()
        {
            var loginRequest = new LoginRequest
            {
                Email = "ze@gmail.com",
                Senha = "senha123"
            };

            var autenticarResponse = _client.PostAsJsonAsync("https://localhost:7124/api/Autenticacao/Autenticar", loginRequest).Result;
            var content = autenticarResponse.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<LoginResponse>(content);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result!.Data.Token);
        }

        [Fact(DisplayName = "RetornarTodosUsuarios")]
        public async Task GetUsers()
        {
            //arrange
            var url = "https://localhost:7124/api/Usuario/RetornarTodosUsuarios";

            //act
            var response = await _client.GetAsync(url);

            //assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RetornarTodosResponse>(content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result!.Data.Any());
        }
              
        [Theory(DisplayName = "RetornarUsuario")]
        [InlineData(1)]
        [InlineData(2)]
        public async Task RetornarUsuario(int id)
        {
            //arrange
            var url = "https://localhost:7124/api/Usuario/RetornarUsuario?codigo=";

            //act
            var response = await _client.GetAsync($"{url}{id}");

            //assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UsuarioResponse>(content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, result!.Status);
            Assert.True(result!.Success);
            Assert.NotNull(result!.Data);
            Assert.NotEmpty(result.Data.Email);
        }

    }
}
