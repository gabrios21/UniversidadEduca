using Modelos_UniversidadEduca.Excepciones;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UniversidadEduca_Tarea1.Models;

namespace AccesoDatos_UniversidadEduca {
    public class MapaDatos {

        public string InformacionConexion { get; private set; }

        public MapaDatos() {
            InformacionConexion = ConfigurationManager.ConnectionStrings["conexionUniversidadEduca"].ConnectionString;
        }

        #region Crear

        public void CrearEstudiante(Estudiante estudiante) {
            
            if (EstudianteExiste(estudiante.Id)) {
                throw new ObjetoDuplicadoException("El estudiante digitado ya está en el sistema");
            }

            string sentenciaSql = @"INSERT INTO Estudiante (IdEstudiante, IdSede, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero)
                                    VALUES (@IdEstudiante, @IdSede, @Nombre, @PrimerApellido, @SegundoApellido, @FechaNacimiento, @Genero)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdEstudiante", estudiante.Id);
                comando.Parameters.AddWithValue("@IdSede", estudiante.Sede.Id);
                comando.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                comando.Parameters.AddWithValue("@PrimerApellido", estudiante.Apellido);
                comando.Parameters.AddWithValue("@SegundoApellido", estudiante.SegundoApellido);
                comando.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                comando.Parameters.AddWithValue("@Genero", estudiante.Genero);

                comando.ExecuteNonQuery();

            }
        }

        public void ActualizarNotas(int idCurso, int idEstudiante, int notaFinal)
        {
            string sentenciaSql = @"UPDATE CursoEstudiante 
                                    SET NotaFinal = @NotaFinal
                                    WHERE IdCurso = @IdCurso AND IdEstudiante = @IdEstudiante";

            using (SqlConnection conexion = new(InformacionConexion))
            {

                SqlCommand comando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", idCurso);
                comando.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                comando.Parameters.AddWithValue("@NotaFinal", notaFinal);

                comando.ExecuteNonQuery();

            }
        }

        public void CrearProfesor(Profesor profesor) {

            if (ProfesorExiste(profesor.Id)) {
                throw new ObjetoDuplicadoException("El profesor digitado ya está en el sistema");
            }

            if (NombreUsuarioExiste(profesor.Plataforma.Usuario)) {
                throw new ObjetoDuplicadoException("El nombre de usuario seleccionado ya existe.");
            }

            string sentenciaSql = @"INSERT INTO Profesor (IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna)
                                    VALUES (@IdProfesor, @IdSede, @Nombre, @PrimerApellido, @SegundoApellido, @Sueldo, @Usuario, @Contrasenna)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdProfesor", profesor.Id);
                comando.Parameters.AddWithValue("@IdSede", profesor.Sede.Id);
                comando.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                comando.Parameters.AddWithValue("@PrimerApellido", profesor.Apellido);
                comando.Parameters.AddWithValue("@SegundoApellido", profesor.SegundoApellido);
                comando.Parameters.AddWithValue("@Sueldo", profesor.Sueldo);
                comando.Parameters.AddWithValue("@Usuario", profesor.Plataforma.Usuario);
                comando.Parameters.AddWithValue("@Contrasenna", profesor.Plataforma.Contrasena);

                comando.ExecuteNonQuery();

            }
        }

        public void CrearCurso(Curso curso) {

            if (CursoExiste(curso.Id)) {
                throw new ObjetoDuplicadoException("El curso digitado ya existe");
            }

            string sentenciaSql = @"INSERT INTO Curso (IdCurso, Nombre, Descripcion)
                                    VALUES (@IdCurso, @Nombre, @Descripcion)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", curso.Id);
                comando.Parameters.AddWithValue("@Nombre", curso.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", curso.Descripcion);

                comando.ExecuteNonQuery();
            }
        }

        public void CrearSede(Sede sede) {

            if (SedeExiste(sede.Id)) {
                throw new ObjetoDuplicadoException("La sede digitada ya existe");
            }

            string sentenciaSql = @"INSERT INTO Sede (IdSede, Descripcion)
                                    VALUES (@IdSede, @Descripcion)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdSede", sede.Id);
                comando.Parameters.AddWithValue("@Descripcion", sede.Descripcion);

                comando.ExecuteNonQuery();
            }
        }

        #endregion

        public bool CredencialesSonCorrectas(string usuario, string contrasena) {
            SqlDataReader lector;

            string sentenciaSql = @$"SELECT Contrasenna
                                    FROM Profesor
                                    WHERE Usuario  = @Usuario";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@Usuario", usuario);
                lector = comando.ExecuteReader();

                if (lector.Read()) {  
                    var contrasenaEnArchivo = lector.GetString(0);
                    return contrasena.Equals(contrasenaEnArchivo);
                }
            }

            return false;
        }

        private bool NombreUsuarioExiste(string usuario) {
            SqlDataReader lector;

            string sentenciaSql = @$"SELECT *
                                    FROM Profesor
                                    WHERE Usuario  = @Usuario";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@Usuario", usuario);
                lector = comando.ExecuteReader();

                if (lector.HasRows) {  //Si el query devuelve resultados ya existe el usuario en la BD
                    return true;
                }
            }

            return false;
        }

        private bool EstudianteExiste(int idEstudiante) {
            SqlDataReader lector;

            string sentenciaSql = @$"SELECT *
                                    FROM Estudiante
                                    WHERE IdEstudiante = @IdEstudiante";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                lector = comando.ExecuteReader();

                if (lector.HasRows) {  //Si el query devuelve resultados ya existe el Id en la BD
                    return true;
                }
            }

            return false;
        }

        private bool ProfesorExiste(int idProfesor) {
            SqlDataReader lector;

            string sentenciaSql = @$"SELECT *
                                    FROM Profesor
                                    WHERE IdProfesor = @IdProfesor";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdProfesor", idProfesor);
                lector = comando.ExecuteReader();

                if (lector.HasRows) {  //Si el query devuelve resultados ya existe el Id en la BD
                    return true;
                }
            }

            return false;
        }

        private bool SedeExiste(int idSede) {

            SqlDataReader lector;

            string sentenciaSql = @$"SELECT *
                                    FROM Sede
                                    WHERE IdSede = @IdSede";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdSede", idSede);
                lector = comando.ExecuteReader();

                if (lector.HasRows) {  //Si el query devuelve resultados ya existe el Id en la BD
                    return true;
                }
            }

            return false;
        }

        private bool CursoExiste(int idCurso) {

            SqlDataReader lector;

            string sentenciaSql = @$"SELECT *
                                    FROM Curso
                                    WHERE IdCurso = @IdCurso";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", idCurso);

                lector = comando.ExecuteReader();

                if (lector.HasRows) {  //Si el query devuelve resultados ya existe el Id en la BD
                    return true;
                }
            }

            return false;
        }

        public void MatricularEstudiante(int cursoId, int estudianteId) {

            string sentenciaSql = @"INSERT INTO CursoEstudiante (IdCurso, IdEstudiante)
                                    VALUES (@IdCurso, @IdEstudiante)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", cursoId);
                comando.Parameters.AddWithValue("@IdEstudiante", estudianteId);

                comando.ExecuteNonQuery();
            }
        }

        public void AsignarProfesorACurso(int cursoId, int profesorId) {

            string sentenciaSql = @"INSERT INTO CursoProfesor (IdCurso, IdProfesor)
                                    VALUES (@IdCurso, @IdProfesor)";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", cursoId);
                comando.Parameters.AddWithValue("@IdProfesor", profesorId);

                comando.ExecuteNonQuery();
            }
        }

        #region Obtener

        public List<Profesor> ObtenerProfesores() {

            List<Profesor> profesores = new List<Profesor>();
            SqlDataReader lector;
            

            string sentenciaSql = @"SELECT IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna
                                    FROM Profesor";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var id = lector.GetInt32(0);
                        var idSede = lector.GetInt32(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //El sueldo puede ser nulo según la BD, así que asignamos 0 en esos casos
                        var sueldo = lector.IsDBNull(5) ? 0 : lector.GetDecimal(5); 
                        var usuario = lector.GetString(6);
                        var contrasena = lector.GetString(7);
                        //Obtener objeto sede
                        Sede sede = ObtenerSede(idSede);
                        AccesoPlataforma infoPlataforma = new(usuario, contrasena);
                        Profesor profesor = new(id, nombre, apellido, segundoApellido, sueldo, sede, infoPlataforma);
                        profesor.Cursos = ObtenerCursosProfesor(profesor.Id);
                        profesores.Add(profesor);
                    }
                }
            }

            return profesores;
        }

        public List<Profesor> ObtenerProfesoresPorSede(int idSede)
        {
            List<Profesor> profesores = new();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna
                                    FROM Profesor
                                    WHERE IdSede = @IdSede";

            using (SqlConnection conexion = new(InformacionConexion))
            {

                SqlCommand comando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdSede", idSede);
                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        var id = lector.GetInt32(0);
                        var sedeId = lector.GetInt32(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //El sueldo puede ser nulo según la BD, así que asignamos 0 en esos casos
                        var sueldo = lector.IsDBNull(5) ? 0 : lector.GetDecimal(5);
                        var usuario = lector.GetString(6);
                        var contrasena = lector.GetString(7);
                        //Obtener objeto sede
                        Sede sede = ObtenerSede(sedeId);
                        AccesoPlataforma infoPlataforma = new(usuario, contrasena);
                        Profesor profesor = new(id, nombre, apellido, segundoApellido, sueldo, sede, infoPlataforma);
                        profesor.Cursos = ObtenerCursosProfesor(profesor.Id);
                        profesores.Add(profesor);
                    }
                }
            }
            return profesores;
        }

        private List<Curso> ObtenerCursosProfesor(int profesorId) {
            List<Curso> cursos = new();
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdCurso
                                    FROM CursoProfesor
                                    WHERE IdProfesor = @IdProfesor";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdProfesor", profesorId);

                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var idCurso = lector.GetInt32(0);
                        var curso = ObtenerCurso(idCurso);
                        cursos.Add(curso);
                    }
                }
            }

            return cursos;
        }

        public Profesor ObtenerProfesor(string usuarioABuscar) {
            Profesor profesor = default;
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna
                                    FROM Profesor
                                    WHERE Usuario = @Usuario";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@Usuario", usuarioABuscar);
                lector = comando.ExecuteReader();

                while (lector.Read()) {
                    var id = lector.GetInt32(0);
                    var idSede = lector.GetInt32(1);
                    var nombre = lector.GetString(2);
                    var apellido = lector.GetString(3);
                    var segundoApellido = lector.GetString(4);
                    //El sueldo puede ser nulo según la BD, así que asignamos 0 en esos casos
                    var sueldo = lector.IsDBNull(5) ? 0 : lector.GetDecimal(5);
                    var usuario = lector.GetString(6);
                    var contrasena = lector.GetString(7);
                    //Obtener objeto sede
                    Sede sede = ObtenerSede(idSede);
                    AccesoPlataforma infoPlataforma = new(usuario, contrasena);
                    profesor = new(id, nombre, apellido, segundoApellido, sueldo, sede, infoPlataforma);
                    profesor.Cursos = ObtenerCursosProfesor(profesor.Id);
                }
            }

            return profesor;
        }

        public List<Estudiante> ObtenerEstudiantes() {

            List<Estudiante> estudiantes = new();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdEstudiante, IdSede, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero
                                    FROM Estudiante";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var id = lector.GetInt32(0);
                        var idSede = lector.GetInt32(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //La fecha de nacimiento puede ser nula según la BD, así que asignamos el valor defecto en esos casos
                        var fechaNacimiento = lector.IsDBNull(5) ? default : lector.GetDateTime(5); 
                        var genero = lector.GetString(6)[0];
                        //Obtener objeto sede
                        Sede sede = ObtenerSede(idSede);
                        Estudiante estudiante = new(id, nombre, apellido, segundoApellido, fechaNacimiento, genero, sede);
                        //Obtener cursos
                        estudiante.Cursos = ObtenerCursosEstudiante(estudiante.Id);
                        estudiantes.Add(estudiante);
                    }
                }
            }

            return estudiantes;
        }

        public List<Estudiante> ObtenerEstudiantesPorSede(int idSede) {
            List<Estudiante> estudiantes = new();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdEstudiante, IdSede, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero
                                    FROM Estudiante
                                    WHERE IdSede = @IdSede";

            using (SqlConnection conexion = new(InformacionConexion))
            {

                SqlCommand comando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();

                //Agregar parametros
                comando.Parameters.AddWithValue("@IdSede", idSede);
                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        var id = lector.GetInt32(0);
                        var sedeId = lector.GetInt32(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //La fecha de nacimiento puede ser nula según la BD, así que asignamos el valor defecto en esos casos
                        var fechaNacimiento = lector.IsDBNull(5) ? default : lector.GetDateTime(5);
                        var genero = lector.GetString(6)[0];
                        //Obtener objeto sede
                        Sede sede = ObtenerSede(sedeId);
                        Estudiante estudiante = new(id, nombre, apellido, segundoApellido, fechaNacimiento, genero, sede);
                        //Obtener cursos
                        estudiante.Cursos = ObtenerCursosEstudiante(estudiante.Id);
                        estudiantes.Add(estudiante);
                    }
                }
            }

            return estudiantes;
        }

        private List<Curso> ObtenerCursosEstudiante(int estudianteId) {
            List<Curso> cursos = new();
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdCurso, NotaFinal
                                    FROM CursoEstudiante
                                    WHERE IdEstudiante = @IdEstudiante";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                //Agregar parametros
                comando.Parameters.AddWithValue("IdEstudiante", estudianteId);

                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var idCurso = lector.GetInt32(0);
                        var curso = ObtenerCurso(idCurso);
                        curso.Nota =  lector.IsDBNull(1) ? 0 : lector.GetInt16(1);
                        cursos.Add(curso);
                    }
                }
            }

            return cursos;

        }

        public List<Estudiante> ObtenerEstudiantesEnCurso(int idCurso)
        {

            List<Estudiante> estudiantesEnCurso = new();
            SqlDataReader lector;

            string sentenciaSql = @"SELECT e.IdEstudiante, e.IdSede, e.Nombre, e.PrimerApellido, e.SegundoApellido, e.FechaNacimiento, e.Genero, c.NotaFinal
                                    FROM Estudiante e
                                    JOIN CursoEstudiante c
                                    ON e.IdEstudiante = c.IdEstudiante 
                                    WHERE c.IdCurso = @IdCurso";

            using (SqlConnection conexion = new(InformacionConexion))
            {

                SqlCommand comando = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", idCurso);

                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        var id = lector.GetInt32(0);
                        var idSede = lector.GetInt32(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //La fecha de nacimiento puede ser nula según la BD, así que asignamos el valor defecto en esos casos
                        var fechaNacimiento = lector.IsDBNull(5) ? default : lector.GetDateTime(5);
                        var genero = lector.GetString(6)[0];
                        //Obtener objeto sede
                        Sede sede = ObtenerSede(idSede);
                        Estudiante estudiante = new(id, nombre, apellido, segundoApellido, fechaNacimiento, genero, sede);
                        //Obtener cursos
                        estudiante.Cursos = ObtenerCursosEstudiante(estudiante.Id);
                        estudiante.Nota = lector.IsDBNull(7) ? default : lector.GetInt16(7);
                        estudiantesEnCurso.Add(estudiante);
                    }
                }
            }

            return estudiantesEnCurso;
        }

        public List<Curso> ObtenerCurriculo() {

            List<Curso> curriculo = new List<Curso>();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdCurso, Nombre, Descripcion
                                    FROM Curso";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var id = lector.GetInt32(0);
                        var nombre = lector.GetString(1);
                        var descripcion = lector.GetString(2);
                        Curso curso = new(id, nombre, descripcion);
                        curriculo.Add(curso);
                    }
                }
            }

            return curriculo;
        }

        public Curso ObtenerCurso(int idCurso) {

            Curso curso = default;
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdCurso, Nombre, Descripcion
                                    FROM Curso
                                    WHERE IdCurso = @IdCurso";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                //Agregar parametros
                comando.Parameters.AddWithValue("@IdCurso", idCurso);

                lector = comando.ExecuteReader();

                while (lector.Read()) {
                    var id = lector.GetInt32(0);
                    var nombre = lector.GetString(1);
                    var descripcion = lector.GetString(2);
                    curso = new(id, nombre, descripcion);
                }
            }

            return curso;
        }

        public List<Sede> ObtenerSedes() {

            List<Sede> sedes = new List<Sede>();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdSede, Descripcion
                                    FROM Sede";

            using (SqlConnection conexion = new(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var id = lector.GetInt32(0);
                        var descripcion = lector.GetString(1);
                        Sede sede = new(id, descripcion);
                        sedes.Add(sede);
                    }
                }
            }

            return sedes;
        }

        private Sede ObtenerSede(int idSede) {
            Sede sede = default;
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand();

            string sentenciaSql = @$"SELECT IdSede, Descripcion
                                    FROM Sede
                                    WHERE IdSede = @IdSede";

            using (SqlConnection conexion = new(InformacionConexion)) {
                comando.CommandType = CommandType.Text;
                comando.CommandText = sentenciaSql;
                comando.Connection = conexion;

                conexion.Open();
                //Agregar parametros
                comando.Parameters.AddWithValue("@IdSede", idSede);

                lector = comando.ExecuteReader();

                while (lector.Read()) {
                    var id = lector.GetInt32(0);
                    var descripcion = lector.GetString(1);
                    sede = new Sede(id, descripcion);
                }
            }

            return sede;
        }

        #endregion
    }
}
