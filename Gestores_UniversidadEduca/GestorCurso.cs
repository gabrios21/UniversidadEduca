using AccesoDatos_UniversidadEduca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Models;

namespace Gestores_UniversidadEduca {
    public class GestorCurso {
        public MapaDatos MapaDatos { get; set; }

        public GestorCurso() {
            MapaDatos = new MapaDatos();
        }

        public void AgregarCurso(Curso curso) {
            MapaDatos.CrearCurso(curso);
        }
    }
}
