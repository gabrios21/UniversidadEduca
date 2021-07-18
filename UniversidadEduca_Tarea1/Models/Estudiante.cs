using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class Estudiante : UniversityMember {

        public DateTime FechaNacimiento { get; private set; }
        public char Genero { get; private set; }

        public Estudiante(int id, string name, string lastName, string secondLastName, DateTime dob, char gender, Sede campus) : base(id, name, lastName, secondLastName, campus) {
            FechaNacimiento = dob;
            Genero = gender;
        }
    }
}

