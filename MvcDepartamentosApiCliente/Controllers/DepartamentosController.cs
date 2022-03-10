using Microsoft.AspNetCore.Mvc;
using MvcDepartamentosApiCliente.Models;
using MvcDepartamentosApiCliente.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDepartamentosApiCliente.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceApiDepartamentos service;

        public DepartamentosController(ServiceApiDepartamentos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos =
                await this.service.GetDepartamentosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Departamento departamento = await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            await this.service.InsertDepartamentoAsync
                (departamento.IdDepartamento, departamento.Nombre
                , departamento.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento departamento =
                await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento departamento)
        {
            await this.service.UpdateDepartamentoAsync(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("Index");
        }
    }
}
