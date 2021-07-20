using AccesoDatos_UniversidadEduca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Models;

namespace Gestores_UniversidadEduca {
    public class GestorSede {
        public MapaDatos  MapaDatos { get; set; }

        public GestorSede() {
            MapaDatos = new MapaDatos();
        }

        public void AgregarSede(Sede sede) {
            MapaDatos.CrearSede(sede);
        }

        public List<Sede> ObtenerListaSedes() {
            var sedes = MapaDatos.ObtenerSedes();
            return sedes;
        }

    }
}
