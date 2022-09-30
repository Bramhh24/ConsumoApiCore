using ConsumoApiCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ConsumoApiCore.Servicios;

namespace ConsumoApiCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;

        public HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Libro> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Libro(int id)
        {
            Libro modelo = new Libro();
            ViewBag.Accion = "Nuevo libro";

            if (id != 0)
            {
                modelo = await _servicioApi.Obtener(id);
                ViewBag.Accion = "Editar libro";
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Libro libro)
        {
            bool respuesta;
            if(libro.Id == 0)
            {
                respuesta = await _servicioApi.Crear(libro);
            }
            else
            {
                respuesta = await _servicioApi.Editar(libro);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicioApi.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}