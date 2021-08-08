namespace UniversidadEduca_Tarea1.Models {
    public class Sede {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        public Sede(int id, string description) {
            Id = id;
            Descripcion = description;
        }

        public override string ToString() {
            var sedeEnTexto = $"{Id} - {Descripcion}";
            return sedeEnTexto;
        }
        public override bool Equals(object obj) {
            return obj is Sede sede &&
                   Id == sede.Id;
        }

        public override int GetHashCode() {
            return System.HashCode.Combine(Id);
        }
    }
}
