using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Aislamientos.Models.Commands
{
    public class CategoriaQuery
    {
        Connection cn = new Connection();
        public CategoriaQuery()
        {
            
        }

        public async Task<List<Categoria>> Read_UnCommited_Start() {


            string _query = @"SELECT id_categoria as Id_Categoria, nombre as Nombre FROM categoria c
                             ";
            List<Categoria> categorias;
            using (var coneccion = new SqlConnection(cn.cadena())) {

           return   coneccion.Query<Categoria>(_query).ToList();
            
            }

           // return categorias;
        }

        public async void Read_UnCommited_Begin_Transac(Categoria categoria) {
            Random r = new Random();

            string _queryTransac = @"
                                     INSERT INTO categoria (id_categoria, nombre) VALUES (@id,@nombre) ";

            try
            {
                using (var coneccion = new SqlConnection(cn.cadena()))
                {
                    coneccion.Open();
                     using (SqlTransaction tran = coneccion.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                    

                    coneccion.Execute(_queryTransac, new { id = r.Next(10, 101) + 5, nombre = categoria.Nombre }, tran);

                        System.Threading.Thread.Sleep(30000);
                   

                        tran.Commit();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

         
        }

        public async void Read_Commited_Begin_Transac(Categoria categoria) {
            Random r = new Random();
            string _queryTransac = @"
                                     INSERT INTO categoria (id_categoria, nombre) VALUES (@id,@nombre) ";
            try
            {
                using (var coneccion = new SqlConnection(cn.cadena()))
                {
                    coneccion.Open();
                    using (SqlTransaction tran = coneccion.BeginTransaction())
                    {


                        coneccion.Execute(_queryTransac, new { id = r.Next(10, 101) + 5, nombre = categoria.Nombre }, tran);

                        System.Threading.Thread.Sleep(30000);


                        tran.Commit();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async void Roll_Back_Transac(Categoria categoria) {

            Random r = new Random();
            string _queryTransac = @"
                                     INSERT INTO categoria (id_categoria, nombre) VALUES (@id,@nombre) ";
            try
            {
                using (var coneccion = new SqlConnection(cn.cadena()))
                {
                    coneccion.Open();
                    using (SqlTransaction tran = coneccion.BeginTransaction())
                    {


                        coneccion.Execute(_queryTransac, new { id = r.Next(100, 500) + 1, nombre = categoria.Nombre }, tran);

                        System.Threading.Thread.Sleep(30000);


                        tran.Rollback();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }



        }
        /// <summary>
        /// repetible read
        /// </summary>
        /// <returns></returns>
        public async Task<List<Categoria>> Roll_Repetible_Read_Transac() {

            string _query = @"
                SELECT id_categoria as Id_Categoria, nombre as Nombre FROM categoria c
                 SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  ";
            List<Categoria> categorias;
            using (var coneccion = new SqlConnection(cn.cadena()))
            {

                return coneccion.Query<Categoria>(_query).ToList();

            }

        }

        // repetible read get editar
        public async Task<Categoria> GetCategoria(int id) {

            string _query = @"SELECT id_categoria as Id_Categoria, nombre as Nombre FROM categoria c
                where c.id_categoria = @idcat ";

            Categoria categoria;
            using (var coneccion = new SqlConnection(cn.cadena()))
            {
                coneccion.Open();
                using (SqlTransaction tran = coneccion.BeginTransaction(IsolationLevel.RepeatableRead))
                {

                    return categoria = coneccion.QueryFirst<Categoria>(_query, new { idcat=id }, tran);
                }
            }
        }

        public async void Update_Read_Transac(Categoria categoria) {

            string _queryTransac = @"
                                     update categoria set nombre = @vnombre where id_categoria = @vid ";
            try
            {
                using (var coneccion = new SqlConnection(cn.cadena()))
                {
                    coneccion.Open();
                    using (SqlTransaction tran = coneccion.BeginTransaction(IsolationLevel.RepeatableRead))
                    {


                        coneccion.Execute(_queryTransac, new { vid=categoria.Id_Categoria, vnombre = categoria.Nombre }, tran);

                        System.Threading.Thread.Sleep(30000);


                        tran.Commit();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Categoria>> Serializable_Transac_Select() {

            string _query = @"
                SELECT id_categoria as Id_Categoria, nombre as Nombre FROM categoria c
                SET TRANSACTION ISOLATION LEVEL SERIALIZABLE  ";
           
            using (var coneccion = new SqlConnection(cn.cadena()))
            {

                return coneccion.Query<Categoria>(_query).ToList();

            }

        }

        // serializable mostrar
        public async Task<Categoria> GetCategoriaSerializable(int id)
        {

            string _query = @"SELECT id_categoria as Id_Categoria, nombre as Nombre FROM categoria c
                where c.id_categoria = @idcat ";

            Categoria categoria;
            using (var coneccion = new SqlConnection(cn.cadena()))
            {
                coneccion.Open();
                using (SqlTransaction tran = coneccion.BeginTransaction(IsolationLevel.Serializable))
                {

                    return categoria = coneccion.QueryFirst<Categoria>(_query, new { idcat = id }, tran);
                }
            }
        }

        public async void Update_Serializable_Transac(Categoria categoria)
        {

            string _queryTransac = @"
                                     update categoria set nombre = @vnombre where id_categoria = @vid ";
            try
            {
                using (var coneccion = new SqlConnection(cn.cadena()))
                {
                    coneccion.Open();
                    using (SqlTransaction tran = coneccion.BeginTransaction(IsolationLevel.Serializable))
                    {


                        coneccion.Execute(_queryTransac, new { vid = categoria.Id_Categoria, vnombre = categoria.Nombre }, tran);

                        System.Threading.Thread.Sleep(30000);


                        tran.Commit();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
