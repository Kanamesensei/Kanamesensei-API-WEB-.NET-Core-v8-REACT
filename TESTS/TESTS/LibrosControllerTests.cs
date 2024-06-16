using BOOKSTATION.Server.Class;
using BOOKSTATION.Server;
using DB;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using BOOKSTATION.Server.Controllers;

namespace TESTS
{
    public class LibrosControllerTests
    {

        [Fact]
        public async Task GetLibros_ReturnsOkWithLibros()
        {

            var mockService = new Mock<ILibrosService>();
            var libros = new List<Libros>(); 
            mockService.Setup(service => service.GetAllLibrosAsync()).ReturnsAsync(libros);
            var controller = new LibrosController(mockService.Object);

            var response = await controller.GetLibros();

            Xunit.Assert.IsType<OkObjectResult>(response.Result);
            var result = Xunit.Assert.IsAssignableFrom<IEnumerable<Libros>>(((OkObjectResult)response.Result).Value);
            Xunit.Assert.Equal(libros, result);
        }

        [Fact]
        public async Task GetLibro_ReturnsOkWithLibro()
        {
            
            int libroId = 1;
            var mockService = new Mock<ILibrosService>();
            var libro = new Libros { LibroID = libroId, };
            mockService.Setup(service => service.GetLibroByIdAsync(libroId)).ReturnsAsync(libro);
            var controller = new LibrosController(mockService.Object);

            var response = await controller.GetLibro(libroId);

            Xunit.Assert.IsType<OkObjectResult>(response.Result);
            var result = Xunit.Assert.IsType<Libros>(((OkObjectResult)response.Result).Value);
            Xunit.Assert.Equal(libroId, result.LibroID);
        }

        [Fact]
        public async Task PostLibro_ReturnsCreatedAtAction()
        {
       
            var mockService = new Mock<ILibrosService>();
            var libroToAdd = new Libros {  };
            var libroAdded = new Libros { LibroID = 1,  };
            mockService.Setup(service => service.CreateLibroAsync(libroToAdd)).ReturnsAsync(libroAdded);
            var controller = new LibrosController(mockService.Object);

            var response = await controller.PostLibro(libroToAdd);

            var createdAtActionResult = Xunit.Assert.IsType<CreatedAtActionResult>(response.Result);
            Xunit.Assert.Equal(nameof(controller.GetLibro), createdAtActionResult.ActionName);
            Xunit.Assert.Equal(libroAdded.LibroID, createdAtActionResult.RouteValues["id"]);
            Xunit.Assert.Equal(libroAdded, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Editar_ReturnsOkWhenUpdated()
        {

            var mockService = new Mock<ILibrosService>();
            var libroToUpdate = new Libros { };
            mockService.Setup(service => service.UpdateLibroAsync(libroToUpdate)).ReturnsAsync(true);
            var controller = new LibrosController(mockService.Object);

            var response = await controller.Editar(libroToUpdate);

            Xunit.Assert.IsType<OkObjectResult>(response);
            var result = Xunit.Assert.IsType<string>(((OkObjectResult)response).Value);
            Xunit.Assert.Equal("Libro actualizado correctamente", result);
        }

        [Fact]
        public async Task DeleteLibro_ReturnsNoContentWhenDeleted()
        {

            int libroId = 1;
            var mockService = new Mock<ILibrosService>();
            mockService.Setup(service => service.DeleteLibroAsync(libroId)).ReturnsAsync(true);
            var controller = new LibrosController(mockService.Object);

            var response = await controller.DeleteLibro(libroId);

            Xunit.Assert.IsType<NoContentResult>(response);
        }
    }
}
