using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Settings
    {
        static Settings() { }
        private static Settings instance;
        private static object obj = new object();

        public static Settings GetInstance()
        {
            lock (obj)
            {
                if (instance == null)
                {
                    instance = new Settings();
                    instance.BackfaceCulling = true;
                    instance.ShowMesh = true;
                    instance.ShowNormals = false;
                }
            }

            return instance;
        }

        public bool BackfaceCulling { get; set; }
        public bool ShowNormals { get; set; }
        public bool ShowMesh { get; set; }

    }
}
