using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab4.Commons;

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
                    instance.Light = LightType.Gouraud;
                }
            }

            return instance;
        }

        public bool BackfaceCulling { get; set; }
        public bool ShowNormals { get; set; }
        public bool ShowMesh { get; set; }
        public LightType Light { get; set; }

    }
}
