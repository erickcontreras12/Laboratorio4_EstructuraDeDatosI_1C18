using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab4EDI_1C18.DBContext;
using System.IO;
using Newtonsoft.Json.Linq;
using Lab4EDI_1C18.Models;
using Newtonsoft.Json;
using System.Net;

namespace Lab4EDI_1C18.Controllers
{
    public class CargaController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        List<int> cambio;
        List<int> faltan;
        List<int> colecc;
        
        // GET: Carga
        public ActionResult Index()
        {

            return View(db.diccionario1.Values.ToList());
        }

        public ActionResult Index2()
        {
            return View(db.diccionario2.Values.ToList());
        }
        // GET: Carga/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Cambios(string id)
        {
            Estampas x = db.diccionario1[id];
            cambio = x.cambios.ToList();
            db.actualtrabajando = x;
            return View(cambio.ToList());
        }
        public ActionResult Coleccionadas(string id)
        {
            Estampas x = db.diccionario1[id];
            colecc = x.coleccionadas.ToList();
            db.actualtrabajando = x;
            return View(colecc.ToList());
        }
        public ActionResult Faltantes(string id)
        {
            Estampas x = db.diccionario1[id];
            faltan = x.faltantes.ToList();
            db.actualtrabajando = x;
            return View(faltan.ToList());
        }


        // GET: Carga/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carga/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase postedFile)
        {

            if (postedFile != null)
            {

                string filepath = string.Empty;

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);

                try
                {

                    JArray json = JArray.Parse(csvData);

                    foreach (JObject jsonOperaciones in json.Children<JObject>())
                    {

                        foreach (JProperty property in jsonOperaciones.Properties())
                        {

                            string x = property.Value.ToString();

                            Estampas y = JsonConvert.DeserializeObject<Estampas>(x);

                            db.diccionario1.Add(y.Nombre, y);
                        }

                    }
                    ViewBag.Message = "Cargado Exitosamente";

                }
                catch
                {
                    ViewBag.Message = "Dato erroneo.";
                }
            }
            return View();
        }

        public ActionResult Create2()
        {
            return View();
        }

        // POST: Carga/Create
        [HttpPost]
        public ActionResult Create2(HttpPostedFileBase postedFile)
        {

            if (postedFile != null)
            {

                string filepath = string.Empty;

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);

                try
                {

                    JArray json = JArray.Parse(csvData);

                    foreach (JObject jsonOperaciones in json.Children<JObject>())
                    {

                        foreach (JProperty property in jsonOperaciones.Properties())
                        {

                            bool x = (bool)property.Value;
                            Estampita aux = new Estampita();
                            aux.Nombre = property.Name;
                            aux.valor = x;
                            db.ListadoFinal.Add(aux);
                            db.diccionario2.Add(aux.Nombre, aux);

                        }

                    }
                    ViewBag.Message = "Cargado Exitosamente";
                }
                catch
                {
                    ViewBag.Message = "Dato erroneo.";
                }
            }
            return View();
        }




        // GET: Carga/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estampita jugadorBuscado = db.diccionario2[id];

            if (jugadorBuscado == null)
            {
                return HttpNotFound();
            }
            if (jugadorBuscado.valor==false)
            {
                jugadorBuscado.valor = true;
            }
            else
            {
                jugadorBuscado.valor = false;
            }


            return RedirectToAction("Index2");
        }

        // POST: Carga/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carga/Delete/5
        public ActionResult Delete(int id)
        {
            db.actualtrabajando.faltantes.Remove(id);
            db.actualtrabajando.coleccionadas.Add(id);
            return RedirectToAction("Index");
        }

        public ActionResult cambiar(int id)
        {
            db.actualtrabajando.cambios.Remove(id);            
            return RedirectToAction("Index");
        }

        // POST: Carga/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
