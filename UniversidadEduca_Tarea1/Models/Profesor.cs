using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Models {
    class Profesor : UniversityMember {
        public decimal Salary { get; private set; }
        public LogIn LoginInfo { get; private set; }

        public Profesor(int id, string name, string lastName, string secondLastName, decimal salary, Sede campus, LogIn loginInfo) : base(id, name, lastName, secondLastName, campus) {
            Salary = salary;
            LoginInfo = loginInfo;
        }
    }
}
