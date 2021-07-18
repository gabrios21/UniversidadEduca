using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class Profesor : UniversityMember {
        public decimal Sueldo { get; private set; }
        public LogIn Plataforma { get; private set; }

        public Profesor(int id, string name, string lastName, string secondLastName, decimal salary, Sede campus, LogIn loginInfo) : base(id, name, lastName, secondLastName, campus) {
            Sueldo = salary;
            Plataforma = loginInfo;
        }
    }
}
