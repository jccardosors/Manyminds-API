using Manyminds.Api.Controllers;
using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Response;
using Manyminds.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Manyminds.Tests.Unit.Controllers
{
    public class UsuarioControllerTest
    {
        private readonly UsuarioController _controller;
        private readonly Mock<ILogger<UsuarioController>> _mocklogger = new();
        private readonly Mock<IUsuarioService> _mockUsuarioService = new();

        public UsuarioControllerTest()
        {
            _controller = new UsuarioController(_mocklogger.Object, _mockUsuarioService.Object);
        }

        [Fact(DisplayName = "RetornarTodosUsuarios")]
        public async Task GetUsers()
        {
            //arrange
            _mockUsuarioService.Setup(p => p.RetornarTodos()).ReturnsAsync(new RetornarTodosResponse
            {
                Data = new List<UsuarioVM>
                 {
                     new UsuarioVM { Codigo = 1, Email = "teste@gmail.com" }
                 },
                Message = "",
                Status = 200,
            });

            //act
            var response = await _controller.RetornarTodosUsuarios();


            //assert
            var responseResult = Assert.IsType<OkObjectResult>(response.Result);   
            var objectResult = Assert.IsAssignableFrom<RetornarTodosResponse>(responseResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, responseResult.StatusCode);
            Assert.True(objectResult.Data.Any());
            Assert.Equal((int)HttpStatusCode.OK, objectResult.Status);
            Assert.True(objectResult.Success);
        }


    }
}
