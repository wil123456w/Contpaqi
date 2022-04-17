using Contpaqi.Models;
using Contpaqi.Models.Empleado;
using Contpaqi.Servicio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contpaqi.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: EmpleadoController
        public async Task<ActionResult> Index(string nombre = null,string rfc=null, int? status = null)
        {
            List<EmpleadoObj> lstEmpleado = new List<EmpleadoObj>();
            List<CatalogoObj> lstEstatus = new List<CatalogoObj>();
            List<SelectListItem> SelectEstatus = new List<SelectListItem>();

            Response rs = await EmpleadoGet(null, "Empleado/getEmpleado?nombre=" + nombre + "&rfc=" + rfc + "&status=" + status);
            if (rs.IsCorrect == "true")
            {
                lstEmpleado = JsonConvert.DeserializeObject<List<EmpleadoObj>>(rs.Data.Info.result.ToString());
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Response rsp = await EmpleadoGet(null, "Empleado/getEstatus");
            if (rsp.IsCorrect == "true")
            {
                lstEstatus = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsp.Data.Info.result.ToString());

                lstEstatus.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectEstatus.Add(g);
                });
            }
            ViewBag.LstEstatus = SelectEstatus;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           

            return View(lstEmpleado);
        }

        // GET: EmpleadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmpleadoController/Create
        public async Task<ActionResult> Create()
        {
            List<CatalogoObj> lstGenero = new List<CatalogoObj>();
            List<CatalogoObj> lstEstadoCivil = new List<CatalogoObj>();
            List<CatalogoObj> lstPuesto = new List<CatalogoObj>();
            List<SelectListItem> SelectGenero = new List<SelectListItem>();
            List<SelectListItem> SelectEstadoCivil = new List<SelectListItem>();
            List<SelectListItem> SelectPuesto = new List<SelectListItem>();

            Response rsg = await EmpleadoGet(null, "Empleado/getGenero");
            if (rsg.IsCorrect == "true")
            {
                lstGenero = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsg.Data.Info.result.ToString());

                lstGenero.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectGenero.Add(g);
                });
            }
            ViewBag.LstGenero = SelectGenero;

            Response rsec = await EmpleadoGet(null, "Empleado/getEstadoCivil");
            if (rsec.IsCorrect == "true")
            {
                lstEstadoCivil = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsec.Data.Info.result.ToString());

                lstEstadoCivil.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectEstadoCivil.Add(g);
                });
            }
            ViewBag.LstEstadoCivil = SelectEstadoCivil;

            Response rsp = await EmpleadoGet(null, "Empleado/getPuesto");
            if (rsp.IsCorrect == "true")
            {
                lstPuesto = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsp.Data.Info.result.ToString());

                lstPuesto.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectPuesto.Add(g);
                });
            }
            ViewBag.LstPuesto = SelectPuesto;



            return View();
        }

        // POST: EmpleadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                EmpleadoObj empleado = new EmpleadoObj();
                empleado.nombre = collection["nombre"];
                empleado.apellidoPaterno = collection["apellidoPaterno"];
                empleado.apellidoMaterno = collection["apellidoMaterno"];
                empleado.edad = Convert.ToInt32(collection["edad"]);
                empleado.fechaNacimiento = Convert.ToDateTime(collection["fechaNacimiento"]);
                empleado.generoId = Convert.ToInt32(collection["LstGenero"]);
                empleado.estadoCivilId = Convert.ToInt32(collection["LstEstadoCivil"]);
                empleado.rfc = collection["rfc"];
                empleado.direccion = collection["direccion"];
                empleado.email = collection["email"];
                empleado.telefono = collection["telefono"];
                empleado.puestoId = Convert.ToInt32(collection["LstPuesto"]);
                //empleado.estatusId = Convert.ToInt32(collection["estatusId"]);

                Response rs = await EmpleadoPost(empleado, "Empleado/setEmpleado");

                if (rs.IsCorrect == "true")
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpleadoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            EmpleadoObj empleado = new EmpleadoObj();

            Response rs = await EmpleadoGet(null, "Empleado/getEmpleadoXId?empleadoId=" + id);
            if (rs.IsCorrect == "true")
            {
                empleado = JsonConvert.DeserializeObject<EmpleadoObj>(rs.Data.Info.result.ToString());
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<CatalogoObj> lstGenero = new List<CatalogoObj>();
            List<CatalogoObj> lstEstadoCivil = new List<CatalogoObj>();
            List<CatalogoObj> lstPuesto = new List<CatalogoObj>();
            List<SelectListItem> SelectGenero = new List<SelectListItem>();
            List<SelectListItem> SelectEstadoCivil = new List<SelectListItem>();
            List<SelectListItem> SelectPuesto = new List<SelectListItem>();

            Response rsg = await EmpleadoGet(null, "Empleado/getGenero");
            if (rsg.IsCorrect == "true")
            {
                lstGenero = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsg.Data.Info.result.ToString());

                lstGenero.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectGenero.Add(g);
                });
            }
            var SelectGeneroP = new SelectList(SelectGenero, "Value", "Text", empleado.generoId);
            ViewBag.LstGenero = SelectGeneroP;

            Response rsec = await EmpleadoGet(null, "Empleado/getEstadoCivil");
            if (rsec.IsCorrect == "true")
            {
                lstEstadoCivil = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsec.Data.Info.result.ToString());

                lstEstadoCivil.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectEstadoCivil.Add(g);
                });
            }
            var SelectEstadoCivilP = new SelectList(SelectEstadoCivil, "Value", "Text", empleado.estadoCivilId);
            ViewBag.LstEstadoCivil = SelectEstadoCivilP;

            Response rsp = await EmpleadoGet(null, "Empleado/getPuesto");
            if (rsp.IsCorrect == "true")
            {
                lstPuesto = JsonConvert.DeserializeObject<List<CatalogoObj>>(rsp.Data.Info.result.ToString());

                lstPuesto.ForEach(x =>
                {
                    SelectListItem g = new SelectListItem();
                    g.Text = x.descripcion;
                    g.Value = x.id.ToString();
                    SelectPuesto.Add(g);
                });
            }
            var SelectPuestoP = new SelectList(SelectPuesto, "Value", "Text", empleado.puestoId);
            ViewBag.LstPuesto = SelectPuestoP;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            return View(empleado);
        }

        // POST: EmpleadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                EmpleadoObj empleado = new EmpleadoObj();
                empleado.empleadoId = id;
                //empleado.nombre = collection["nombre"];
                //empleado.apellidoPaterno = collection["apellidoPaterno"];
                //empleado.apellidoMaterno = collection["apellidoMaterno"];
                //empleado.edad = Convert.ToInt32(collection["edad"]);
                //empleado.fechaNacimiento = Convert.ToDateTime(collection["fechaNacimiento"]);
                //empleado.generoId = Convert.ToInt32(collection["LstGenero"]);
                empleado.estadoCivilId = Convert.ToInt32(collection["LstEstadoCivil"]);
                //empleado.rfc = collection["rfc"];
                empleado.direccion = collection["direccion"];
                empleado.email = collection["email"];
                //empleado.telefono = collection["telefono"];
                empleado.puestoId = Convert.ToInt32(collection["LstPuesto"]);
                empleado.fechaBaja= Convert.ToDateTime(collection["fechaBaja"]);

                
                //empleado.estatusId = Convert.ToInt32(collection["estatusId"]);

                Response rs = await EmpleadoPost(empleado, "Empleado/updateEmpleado");

                if (rs.IsCorrect == "true")
                {
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpleadoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            EmpleadoObj empleado = new EmpleadoObj();

            Response rs = await EmpleadoGet(null, "Empleado/getEmpleadoXId?empleadoId=" + id);
            if (rs.IsCorrect == "true")
            {
                empleado = JsonConvert.DeserializeObject<EmpleadoObj>(rs.Data.Info.result.ToString());
            }
            return View(empleado);
        }

        // POST: EmpleadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                EmpleadoObj empleado = new EmpleadoObj();
                empleado.empleadoId = id;
                

                Response rs = await EmpleadoPost(empleado, "Empleado/deleteEmpleado");

                if (rs.IsCorrect == "true")
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> _ViewEmpleado(string nombre = null, string rfc = null, int? status = null)
        {
            List<EmpleadoObj> lstEmpleado = new List<EmpleadoObj>();
            Response rs = await EmpleadoGet(null, "Empleado/getEmpleado?nombre=" + nombre + "&rfc=" + rfc + "&estatusId=" + status);
            if (rs.IsCorrect == "true")
            {
                lstEmpleado = JsonConvert.DeserializeObject<List<EmpleadoObj>>(rs.Data.Info.result.ToString());
            }

            return PartialView("../_ViewEmpleado", lstEmpleado);
        }

        public async Task<Response> EmpleadoPost(EmpleadoObj request, string url)
        {
            try
            {
                var client = new EmpleadoResponse<EmpleadoObj, Response>();
                var response = await client.PostAsJsonAsync(url, null, request);
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<Response> EmpleadoGet(EmpleadoObj request, string url)
        {
            try
            {
                var client = new EmpleadoResponse<EmpleadoObj, Response>();
                var response = await client.GetAsJson(url);
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
