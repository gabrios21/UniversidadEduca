using AccesoDatos_UniversidadEduca;
using Modelos_UniversidadEduca.Excepciones;
using System;
using System.Collections.Generic;
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

        public List<Profesor> ObtenerProfesores() {
            return MapaDatos.ObtenerProfesores();
        }

        public List<Profesor> ObtenerProfesoresPorSede(int idSede) {
            return MapaDatos.ObtenerProfesoresPorSede(idSede);
        }

        public void AsignarCurso(Curso nuevoCurso, Profesor profesor) {
            if (EstaAsignadoEnCurso(nuevoCurso, profesor)) {
                throw new ObjetoDuplicadoException("El profesor ya se encuentra asignado al curso seleccionado");
            }
            MapaDatos.AsignarProfesorACurso(nuevoCurso.Id, profesor.Id);
        }

        private bool EstaAsignadoEnCurso(Curso curso, Profesor profesor) {
            return profesor.Cursos.Contains(curso);
        }
    }
}
