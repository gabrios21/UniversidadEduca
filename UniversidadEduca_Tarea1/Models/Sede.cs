using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class Sede {
        public int Id { get; private set; }
        public string Description { get; private set; }

        public Sede(int id, string description) {
            Id = id;
            Description = description;
        }
    }
}
