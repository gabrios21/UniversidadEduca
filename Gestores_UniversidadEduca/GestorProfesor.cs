using AccesoDatos_UniversidadEduca;
using Modelos_UniversidadEduca.Excepciones;
using UniversidadEduca_Tarea1.Models;

namespace Gestores_UniversidadEduca {
    public class GestorProfesor {
        public MapaDatos MapaDatos { get; set; }

        public GestorProfesor() {
            MapaDatos = new MapaDatos();
        }

        public void AgregarProfesor(Profesor profesor) {
            MapaDatos.CrearProfesor(profesor);
        }

        public Profesor ObtenerProfesor(string usuario) {
            var profesor = MapaDatos.ObtenerProfesor(usuario);
            return profesor ?? throw new ObjetoNoExisteException("Profesor no fue encontrado en la base de datos");
        }
    }
}
