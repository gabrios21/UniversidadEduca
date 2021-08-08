using AccesoDatos_UniversidadEduca;
using Modelos_UniversidadEduca.Excepciones;
using System;
using System.Collections.Generic;
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

        public List<Estudiante> ObtenerEstudiantesEnCurso(int idCurso) {
            return MapaDatos.ObtenerEstudiantesEnCurso(idCurso);
        }

        public List<Estudiante> ObtenerEstudiantesPorSede(int idSede)
        {
            return MapaDatos.ObtenerEstudiantesPorSede(idSede);
        }

        public void MatricularCurso(Curso nuevoCurso, Estudiante estudiante) {
            if (EstaMatriculadoEnCurso(nuevoCurso, estudiante)) {
                throw new ObjetoDuplicadoException("El estudiante ya se encuentra matriculado en el curso seleccionado");
            }
            MapaDatos.MatricularEstudiante(nuevoCurso.Id, estudiante.Id);
        }

        private bool EstaMatriculadoEnCurso(Curso curso, Estudiante estudiante) {
            return estudiante.Cursos.Contains(curso);
        }
    }
}
