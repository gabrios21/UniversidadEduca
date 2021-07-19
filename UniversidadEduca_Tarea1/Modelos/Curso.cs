using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    public class Curso {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        public Curso(int id, string name, string description) {
            Id = id;
            Nombre = name;
            Descripcion = description;
        }
    }
}
