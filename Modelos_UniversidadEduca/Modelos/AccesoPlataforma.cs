namespace UniversidadEduca_Tarea1.Models {
    public class AccesoPlataforma {
        public string Usuario { get; private set; }
        public string Contrasena { get; private set; }

        public AccesoPlataforma(string usuario, string contrasena) {
            Usuario = usuario;
            Contrasena = contrasena;
        }
    }
}