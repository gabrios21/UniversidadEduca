namespace UniversidadEduca_Tarea1.Models {
    public class Profesor : MiembroUniversidad {
        public decimal Sueldo { get; set; }
        public AccesoPlataforma Plataforma { get; set; }

        public Profesor(int id, string nombre, string apellido, string segundoApellido, decimal sueldo, Sede sede, AccesoPlataforma infoPlataforma) : base(id, nombre, apellido, segundoApellido, sede) {
            Sueldo = sueldo;
            Plataforma = infoPlataforma;
        }

        public override string ToString() {
            var profesorEnTexto = $"{Nombre} {Apellido} {SegundoApellido}";
            return profesorEnTexto;
        }
        public override bool Equals(object obj) {
            return obj is Profesor profesor &&
                   Id == profesor.Id;
        }

        public override int GetHashCode() {
            return System.HashCode.Combine(Id);
        }
    }
}
