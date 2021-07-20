using System;

namespace UniversidadEduca_Tarea1.Models {
    public class Estudiante : MiembroUniversidad {

        public DateTime FechaNacimiento { get; private set; }
        public char Genero { get; private set; }

        public Estudiante(int id, string nombre, string apellido, string segundoApellido, DateTime fechaNacimiento, char genero, Sede sede) : base(id, nombre, apellido, segundoApellido, sede) {
            FechaNacimiento = fechaNacimiento;
            Genero = genero;
        }
    }
}

