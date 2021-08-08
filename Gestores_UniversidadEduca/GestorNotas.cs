using AccesoDatos_UniversidadEduca;
using System;
using System.Collections.Generic;
using UniversidadEduca_Tarea1.Models;

namespace Gestores_UniversidadEduca {
    public class GestorNotas {
        public MapaDatos MapaDatos { get; set; }

        public GestorNotas() {
            MapaDatos = new MapaDatos();
        }

        public bool CredencialesSonCorrectas(string usuario, string contrasena) {
            return MapaDatos.CredencialesSonCorrectas(usuario, contrasena);
        }

        public void ActualizarNotas((List<Estudiante>, int) estudiantesEnCurso)
        {
            var idCurso = estudiantesEnCurso.Item2; //Extraemos el id del curso de la tupla

            foreach (Estudiante estudiante in estudiantesEnCurso.Item1) {
                MapaDatos.ActualizarNotas(idCurso, estudiante.Id, estudiante.Nota);
            }
            
        }
    }
}
