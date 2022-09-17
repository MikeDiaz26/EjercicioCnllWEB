using Microsoft.AspNetCore.Mvc;
using EjercicioCanellaWEB.Models;
using System.Net.Http;
using EjercicioCanellaWEB.Connection;

namespace EjercicioCanellaWEB.Controllers
{
    public class VehiculoController : Controller
    {
        private GestionVehiculo datos = new GestionVehiculo();

        public IActionResult Index()
        {
            IEnumerable<Vehiculo> vehiculos = datos.GetVehiculos();
            return View(vehiculos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Placa,Marca,Modelo,Anio,Color,PrecioRenta")] Vehiculo vehiculo)
        {
            if (!datos.NewVehiculo(vehiculo))
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(string placa)
        {
            ViewBag.vehiculo = datos.GetVehiculo(placa);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Placa,Marca,Modelo,Anio,Color,PrecioRenta")] Vehiculo vehiculo)
        {
            if (!datos.UpdateVehiculo(vehiculo))
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Details), new { Placa = vehiculo.Placa });
        }

        public ActionResult Details(string placa)
        {
            ViewBag.vehiculo = datos.GetVehiculo(placa);
            return View();
        }

        public ActionResult Delete(string placa)
        {
            if (placa == null)
                return BadRequest();

            ViewBag.vehiculo = datos.GetVehiculo(placa);
            if (ViewBag.vehiculo != null)
                return View();

            BadRequest();
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string placa, IFormCollection collection)
        {
            if (!datos.DeleteVehiculo(placa))
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
    }
}
