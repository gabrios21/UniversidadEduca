using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class NegativeNumberException : Exception {
        public NegativeNumberException(string message) : base(message) {

        }
    }
}
