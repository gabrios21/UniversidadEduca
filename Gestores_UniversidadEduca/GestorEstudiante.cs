using AccesoDatos_UniversidadEduca;
using UniversidadEduca_Tarea1.Models;

namespace Gestores_UniversidadEduca {
    public class GestorEstudiante {
        public MapaDatos MapaDatos { get; set; }

        public GestorEstudiante() {
            MapaDatos = new MapaDatos();
        }

        public void AgregarEstudiante(Estudiante estudiante) {
            MapaDatos.CrearEstudiante(estudiante);
        }
    }
}
