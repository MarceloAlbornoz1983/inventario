﻿using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.DAL
{
    public class RepositorioDeEmpleados : IRepositorio<Empleado>
    {
        private string DBName = "Inventario.db";
        private string TableName = "Empleadosb";
        public List<Empleado> Read
        {
            get
            {
                List<Empleado> datos = new List<Empleado>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Empleado>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Empleado entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();

            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Empleado entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
