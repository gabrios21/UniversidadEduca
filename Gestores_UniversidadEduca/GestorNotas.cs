using AccesoDatos_UniversidadEduca;

namespace Gestores_UniversidadEduca {
    public class GestorNotas {
        public MapaDatos MapaDatos { get; set; }

        public GestorNotas() {
            MapaDatos = new MapaDatos();
        }

        public bool CredencialesSonCorrectas(string usuario, string contrasena) {
            return MapaDatos.CredencialesSonCorrectas(usuario, contrasena);
        }
    }
}
