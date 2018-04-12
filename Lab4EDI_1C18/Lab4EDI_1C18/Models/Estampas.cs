using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4EDI_1C18.Models
{
    public class Estampas
    {
        public string Nombre { get; set; }
        public List<int> faltantes = new List<int>();
        public List<int> coleccionadas = new List<int>();
        public List<int> cambios = new List<int>();
    }
}