using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_UniversidadEduca.Excepciones {
    public class ParametroVacíoException : ApplicationException {
        public ParametroVacíoException(string mensaje) : base(mensaje) {

        }
    }
}
