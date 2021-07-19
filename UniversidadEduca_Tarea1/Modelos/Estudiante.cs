using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    public class Estudiante : MiembroUniversidad {

        public DateTime FechaNacimiento { get; private set; }
        public char Genero { get; private set; }

        public Estudiante(int id, string nombre, string apellido, string segundoApellido, DateTime fechaNacimineto, char genero, Sede sede) : base(id, nombre, apellido, segundoApellido, sede) {
            FechaNacimiento = fechaNacimineto;
            Genero = genero;
        }
    }
}

