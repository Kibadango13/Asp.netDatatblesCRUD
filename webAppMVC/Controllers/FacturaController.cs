using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webAppMVC.Models;

namespace webAppMVC.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura
        public ActionResult Index()
        {
            try
            {
                //SELECT LIST DE LOS CLIENTES AL INICIAR LA PAGINA
                List<SelectListItem> listaCliente;
                using (var bd = new DBPruebaEntities())
                {

                    listaCliente = (from rowCliente in bd.Personas
                                    select new SelectListItem
                                    {
                                        Text = rowCliente.NOMBRE + " " + rowCliente.APELLIDO.ToString(),
                                        Value = rowCliente.IIDPersona.ToString()
                                    }).ToList();
                    listaCliente.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });

                }
                ViewBag.lista = listaCliente;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        // GET: Factura/Create
        public ActionResult CrearFactura(FacturaCLS FacturaCLS)
        {

            int IID = 0;
            if (!ModelState.IsValid)
            {
                return View(FacturaCLS);
            }
            else
            {
                using (var bd = new DBPruebaEntities())
                {
                    //objecto factura
                    FACTURA oFactura = new FACTURA();
                    oFactura.IIDPERSONA = FacturaCLS.IIDPERSONA;
                    oFactura.FECHA = FacturaCLS.FECHA;
                    oFactura.NUMFACTURA = FacturaCLS.NUMFACTURA;
                    oFactura.IVA = FacturaCLS.IVA;
                    oFactura.ESTADO = int.Parse(FacturaCLS.ESTADO);
                    bd.FACTURAs.Add(oFactura);
                    bd.SaveChanges();
                    IID = oFactura.IIDFACTURA;

                    DETALLE oDetalle = new DETALLE();

                    foreach (var x in FacturaCLS.DETALLE)
                    {
                        oDetalle.IIDFACTURA = IID;
                        oDetalle.NOMBREPRODUCTO = x.NOMBREPRODUCTO;
                        oDetalle.CANTIDAD = x.CANTIDAD;
                        oDetalle.PRECIO_UNITARIO = x.PRECIO_UNITARIO;
                        oDetalle.TOTAL =x.TOTAL;
                        oDetalle.ESTADO = 1;
                        bd.DETALLEs.Add(oDetalle);
                        bd.SaveChanges();
                    }
                }
            }

            return Json(new { draw = IID, factura = FacturaCLS });
        }
 
        public ActionResult EditarFactura(FacturaCLS FacturaCLS)
        {
       
            if (!ModelState.IsValid)
            {
                return View(FacturaCLS);
            }
            else
            {
                using (var bd = new DBPruebaEntities())
                {
                    //objecto factura
                    FACTURA oFactura = bd.FACTURAs.Where(p => p.IIDFACTURA.Equals(FacturaCLS.IIDFACTURA)).First();
                    oFactura.IIDPERSONA = FacturaCLS.IIDPERSONA;
                    oFactura.FECHA = FacturaCLS.FECHA;
                    oFactura.NUMFACTURA = FacturaCLS.NUMFACTURA;
                    oFactura.IVA = FacturaCLS.IVA;
                    oFactura.ESTADO = int.Parse(FacturaCLS.ESTADO);
                    bd.SaveChanges();

                    DETALLE oDetalle = new DETALLE();

                    foreach (var x in FacturaCLS.DETALLE)
                    {
                        

                        DETALLE oDet = bd.DETALLEs.Where(p => p.IIDDETALLE.Equals(x.IIDDETALLE)).FirstOrDefault();
                        if (oDet != null)
                        {
                            // update information 
                            oDet.NOMBREPRODUCTO = x.NOMBREPRODUCTO;
                            oDet.CANTIDAD = x.CANTIDAD;
                            oDet.PRECIO_UNITARIO = x.PRECIO_UNITARIO;
                            oDet.TOTAL = x.TOTAL;
                            oDet.ESTADO = x.ESTADO;
                            bd.SaveChanges();
                        }
                        else
                        {
                            // insert new data  
                            oDetalle.IIDFACTURA = FacturaCLS.IIDFACTURA;
                            oDetalle.NOMBREPRODUCTO = x.NOMBREPRODUCTO;
                            oDetalle.CANTIDAD = x.CANTIDAD;
                            oDetalle.PRECIO_UNITARIO = x.PRECIO_UNITARIO;
                            oDetalle.TOTAL = x.TOTAL;
                            oDetalle.ESTADO = 1;
                            bd.DETALLEs.Add(oDetalle);
                            bd.SaveChanges();
                        }

                      
                    }
                }
            }

            return Json(new { factura = FacturaCLS });
        }
    
        public ActionResult EliminarFactura(int IIFACTURA_PA)
        {
            using (var bd = new DBPruebaEntities())
            {
                FACTURA FAC = bd.FACTURAs.Where(p=>p.IIDFACTURA.Equals(IIFACTURA_PA)).First();
                FAC.ESTADO = 0;
                bd.SaveChanges();
            }
            return Json(new { IIFACTURA_PA });
        }

        public ActionResult DatatTableListFacturas()
        {
            try
            {
                List<FacturaCLS> listBus = null;
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //using permits to open and close the conection
                using (var bd = new DBPruebaEntities())
                {
                    listBus = bd.Database.SqlQuery<FacturaCLS>("SELECT P.IIDPersona as IIDPERSONA , F.IIDFACTURA as IIDFACTURA ,P.CEDULA,P.NOMBRE+' ' + P.APELLIDO AS NOMBRE,C.nombre AS CUIDAD,P.TELEFONO,F.NUMFACTURA,IVA, CONVERT(varchar(10),COUNT(NOMBREPRODUCTO)) AS NUMERO_REGISTRO,  CONVERT(varchar(10),sum(TOTAL)) AS TOTAL_FACTURA FROM PERSONA P INNER JOIN FACTURA F ON  P.IIDPersona = F.IIDPersona INNER JOIN DETALLE D ON  D.IIDFACTURA = F.IIDFACTURA INNER JOIN CUIDAD C ON C.IIDCUIDAD = P.IIDCUIDAD where  F.ESTADO = 1 and D.ESTADO = 1 GROUP BY F.IIDFACTURA, P.IIDPersona, P.CEDULA, P.NOMBRE, P.APELLIDO, F.NUMFACTURA, FECHA, IVA, C.nombre, P.TELEFONO").ToList<FacturaCLS>();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listBus = listBus.Where(m => m.NUMFACTURA.Contains(searchValue)).ToList();
                }
                var count = listBus.Count();
                return Json(new { draw, recordsTotal = count, recordsFiltered = count, data = listBus });

            }
            catch (Exception)
            {
                throw;
            }

        }
  
        public ActionResult DatatTableListDetalle(int IIDFACTURA, int IIDPersona)
        {
            try
            {
                List<DetalleCLS> listBus = null;

                //using permits to open and close the conection
                using (var bd = new DBPruebaEntities())
                {
                    listBus = bd.Database.SqlQuery<DetalleCLS>("SELECT    D.IIDDETALLE,NOMBREPRODUCTO,d.PRECIO_UNITARIO as PRECIO_UNITARIO,CANTIDAD,TOTAL,d.ESTADO, 1 as NUMERO_REGISTRO FROM PERSONA P INNER JOIN FACTURA F ON  P.IIDPersona = F.IIDPersona AND  F.ESTADO = 1 INNER JOIN DETALLE D ON  D.IIDFACTURA = F.IIDFACTURA AND D.ESTADO = 1 INNER JOIN CUIDAD C ON C.IIDCUIDAD = P.IIDCUIDAD WHERE f.IIDFACTURA = @IIDFACTURA AND P.IIDPersona = @IIDPersona",
                        new SqlParameter("IIDFACTURA", IIDFACTURA),
                        new SqlParameter("IIDPersona", IIDPersona)).ToList<DetalleCLS>();
                    FACTURA FAC = bd.FACTURAs.Where(p => p.IIDFACTURA.Equals(IIDFACTURA)).First();
                    var count = listBus.Count();
                    return Json(new { IIDFACTURA, IIDPersona, FAC.NUMFACTURA, FAC.FECHA, FAC.IVA, data = listBus });
                }


        

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}