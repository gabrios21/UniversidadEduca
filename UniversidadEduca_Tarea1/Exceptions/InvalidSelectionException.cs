using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class InvalidSelectionException : Exception {
        public InvalidSelectionException(string message) : base(message) {

        }
    }
}
