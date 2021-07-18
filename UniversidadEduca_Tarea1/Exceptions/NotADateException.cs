using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversidadEduca_Tarea1.Exceptions {
    class NotADateException : Exception {
        public NotADateException(string message) : base(message) {

        }
    }
}
