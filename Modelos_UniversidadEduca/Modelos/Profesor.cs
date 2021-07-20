namespace UniversidadEduca_Tarea1.Models {
    public class Profesor : MiembroUniversidad {
        public decimal Sueldo { get; set; }
        public AccesoPlataforma Plataforma { get; set; }

        public Profesor(int id, string nombre, string apellido, string segundoApellido, decimal sueldo, Sede sede, AccesoPlataforma infoPlataforma) : base(id, nombre, apellido, segundoApellido, sede) {
            Sueldo = sueldo;
            Plataforma = infoPlataforma;
        }
    }
}
