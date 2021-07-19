using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    public class Curso {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Nota { get; set; }

        public Curso(int id, string name, string description, int nota = -1) {
            Id = id;
            Nombre = name;
            Descripcion = description;
            Nota = nota;
        }
    }
}
