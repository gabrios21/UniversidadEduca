using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    public class MiembroUniversidad {
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string SegundoApellido { get; set; }
        public Sede Sede { get; set; }
        public List<Curso> Cursos { get; set; }

        public MiembroUniversidad(int id, string name, string lastName, string secondLastName, Sede campus) {
            Id = id;
            Nombre = name; 
            Apellido = lastName;
            SegundoApellido = secondLastName;
            Sede = campus;
            Cursos = new List<Curso>();
        }

        public void Matricular(Curso curso) {
            Cursos.Add(curso);        }
    }
}
