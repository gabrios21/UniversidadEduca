using System;

namespace UniversidadEduca_Tarea1.Models {
    public class Estudiante : MiembroUniversidad {

        public DateTime FechaNacimiento { get; private set; }
        public char Genero { get; private set; }
        public int Nota { get; set; } //Espacio para guardar la nota del estudiante, necesario para visualización.
                                      //No me encanta esta solución, tengo que revisarla 

        public Estudiante(int id, string nombre, string apellido, string segundoApellido, DateTime fechaNacimiento, char genero, Sede sede) : base(id, nombre, apellido, segundoApellido, sede) {
            FechaNacimiento = fechaNacimiento;
            Genero = genero;
        }

        public override string ToString() {
            var estudianteEnTexto = $"{Nombre} {Apellido} {SegundoApellido}";
            return estudianteEnTexto;
        }
        public override bool Equals(object obj) {
            return obj is Estudiante estudiante &&
                   Id == estudiante.Id;
        }

        public override int GetHashCode() {
            return System.HashCode.Combine(Id);
        }
    }
}

