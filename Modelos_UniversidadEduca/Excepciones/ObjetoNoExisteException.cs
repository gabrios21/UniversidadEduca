using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_UniversidadEduca.Excepciones {
    public class ObjetoNoExisteException : ApplicationException {
        public ObjetoNoExisteException(string mensaje) : base(mensaje) {

        }
    }
}
