using Aislamientos.Helpers;
using Aislamientos.Models;
using Aislamientos.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aislamientos.Controllers
{
    public class CategoriasController : Controller
    {
        private  CategoriaQuery _categoriaQuery;

      
        public IActionResult Index()
        {
            return View();
        }

        #region dataTables js
        [HttpGet]
        public IActionResult GetAll()
        {
            _categoriaQuery = new CategoriaQuery();
            var datos =_categoriaQuery.Read_UnCommited_Start();
            return Json(new { data = datos.Result });
        }
        #endregion

        #region begin transacion Read uncomited
        [HttpPost]
        public IActionResult Start_Transaction_Read_Uncommited(Categoria categoria) {
            _categoriaQuery = new CategoriaQuery();
            _categoriaQuery.Read_UnCommited_Begin_Transac(categoria);
            return Json(new { success = true, message = "CATEGORÍA AGREGA CORRECTAMENTE!" });

        }
        #endregion

        #region read comited
        [HttpPost]
        public IActionResult Start_Transaction_Read_Commited(Categoria categoria) {

            _categoriaQuery = new CategoriaQuery();
            _categoriaQuery.Read_Commited_Begin_Transac(categoria);
            return Json(new { success = true, message = "CATEGORÍA AGREGA CORRECTAMENTE!" });

        }
        #endregion

        #region roll back tran
        [HttpPost]
        public IActionResult Start_Rollback_Transaction(Categoria categoria) {

            _categoriaQuery = new CategoriaQuery();
            _categoriaQuery.Roll_Back_Transac(categoria);
            return Json(new { success = true, message = "CATEGORÍA ROLL BACK TRANSACTION REALIZADO!" });
        }
        #endregion

        #region Repetible read
        // muestra la tabla principal
        [HttpGet]
        public IActionResult Start_Repetible_Read() {

            _categoriaQuery = new CategoriaQuery();
            var datos = _categoriaQuery.Roll_Repetible_Read_Transac();
            return Json(new { data = datos.Result });

        }
        #endregion

        #region obtener registro a editar para repetible read
        [HttpGet]
        public async Task< IActionResult> Get_Repetible_Read_First(int id) {

            Categoria categoria;// = new Categoria();
            _categoriaQuery = new CategoriaQuery();
            categoria = _categoriaQuery.GetCategoria(id).Result;

            if (categoria == null)
            {
                return NotFound();

            }

            string html = await this.RenderViewAsync("_EditarRead", categoria, true);

            return Json(new { html = html });


        }
        #endregion
        #region update repetible read transac
        [HttpPost]
        public IActionResult Update_Repetible_Read(Categoria categoria) {

            _categoriaQuery = new CategoriaQuery();
            _categoriaQuery.Update_Read_Transac(categoria);

            return Json(new { success = true, message = "CATEGORÍA REPETIBLE READ REALIZADO!" });


        }
        #endregion

        #region serializable
        // muestra la tabla principal
        [HttpGet]
        public IActionResult Star_Serializable()
        {

            _categoriaQuery = new CategoriaQuery();
            var datos = _categoriaQuery.Serializable_Transac_Select();
            return Json(new { data = datos.Result });

        }

        //mostrar al darle editar
        [HttpGet]
        public async Task<IActionResult> Get_Serializable_First(int id)
        {

            Categoria categoria;// = new Categoria();
            _categoriaQuery = new CategoriaQuery();
            categoria = _categoriaQuery.GetCategoriaSerializable(id).Result;

            if (categoria == null)
            {
                return NotFound();

            }

            string html = await this.RenderViewAsync("_EditarSerializable", categoria, true);

            return Json(new { html = html });


        }


        [HttpPost]
        public IActionResult Update_Serializable(Categoria categoria)
        {

            _categoriaQuery = new CategoriaQuery();
            _categoriaQuery.Update_Read_Transac(categoria);

            return Json(new { success = true, message = "CATEGORÍA SERIALIZABLE REALIZADO!" });


        }
        #endregion
    }
}
