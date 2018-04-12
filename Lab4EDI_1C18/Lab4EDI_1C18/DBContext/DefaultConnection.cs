using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4EDI_1C18.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public Dictionary<string, Estampas> diccionario1 = new Dictionary<string, Estampas>();
        public Dictionary<string, Estampita> diccionario2 = new Dictionary<string, Estampita>();
        public List<Estampita> ListadoFinal = new List<Estampita>();

        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}