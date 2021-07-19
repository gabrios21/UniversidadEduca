using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UniversidadEduca_Tarea1.Models;

namespace AccesoDatos_UniversidadEduca {
    public class MapaDatos<T> {
        public string InformacionConexion { get; private set; }

        public MapaDatos() {
            InformacionConexion = ConfigurationManager.ConnectionStrings["conexionUniversidadEduca"].ConnectionString;
        }

        #region Crear

        public void CrearEstudiante(Estudiante estudiante) {

            string sentenciaSql = @"INSERT INTO Estudiante (IdEstudiante, IdSede, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero)
                                    VALUES (@IdEstudiante, @IdSede, @Nombre, @PrimerApellido, @SegundoApellido, @FechaNacimiento, @Genero)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

        public void CrearProfesor(Profesor profesor) {

            string sentenciaSql = @"INSERT INTO Profesor (IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna)
                                    VALUES (@IdProfesor, @IdSede, @Nombre, @PrimerApellido, @SegundoApellido, @Sueldo, @Usuario, @Contrasenna)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

            string sentenciaSql = @"INSERT INTO Curso (IdCurso, Nombre, Descripcion)
                                    VALUES (@IdCurso, @Nombre, @Descripcion)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

            string sentenciaSql = @"INSERT INTO Curso (IdSede, Descripcion)
                                    VALUES (@IdSede, @Descripcion)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

        public void MatricularEstudiante(int estudianteId, int cursoId) {

            string sentenciaSql = @"INSERT INTO CursoEstudiante (IdCurso, IdEstudiante)
                                    VALUES (@IdCurso, @IdEstudiante)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

        public void AsignarProfesorACurso(int profesorId, int cursoId) {

            string sentenciaSql = @"INSERT INTO CursoProfesor (IdCurso, IdProfesor)
                                    VALUES (@IdCurso, @IdProfesor)";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

        #endregion

        #region Obtener

        public List<Profesor> ObtenerProfesores() {

            List<Profesor> profesores = new List<Profesor>();
            SqlDataReader lector;
            

            string sentenciaSql = @"SELECT IdProfesor, IdSede, Nombre, PrimerApellido, SegundoApellido, Sueldo, Usuario, Contrasenna
                                    FROM Profesor";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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
                        var idSede = lector.GetString(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //El sueldo puede ser nulo según la BD, así que asignamos 0 en esos casos
                        var sueldo = lector.IsDBNull(5) ? 0 : lector.GetDecimal(5); 
                        var usuario = lector.GetString(6);
                        var contrasena = lector.GetString(7);
                        //Obtener objeto sede
                        Sede sede = ObtenerUnaSede(idSede);
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
            List<Curso> cursos = new List<Curso>();
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdCurso, NotaFinal
                                    FROM CursoEstudiante
                                    WHERE IdEstudiante = {profesorId}";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
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

        public List<Estudiante> ObtenerEstudiantes() {

            List<Estudiante> estudiantes = new List<Estudiante>();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdEstudiante, IdSede, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero
                                    FROM Estudiante";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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
                        var idSede = lector.GetString(1);
                        var nombre = lector.GetString(2);
                        var apellido = lector.GetString(3);
                        var segundoApellido = lector.GetString(4);
                        //La fecha de nacimiento puede ser nula según la BD, así que asignamos el valor defecto en esos casos
                        var fechaNacimiento = lector.IsDBNull(5) ? default : lector.GetDateTime(5); 
                        var genero = lector.GetChar(6);
                        //Obtener objeto sede
                        Sede sede = ObtenerUnaSede(idSede);
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
            List<Curso> cursos = new List<Curso>();
            SqlDataReader lector;


            string sentenciaSql = @$"SELECT IdCurso, NotaFinal
                                    FROM CursoEstudiante
                                    WHERE IdEstudiante = {estudianteId}";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
                    while (lector.Read()) {
                        var idCurso = lector.GetInt32(0);
                        var curso = ObtenerCurso(idCurso);
                        curso.Nota = lector.GetInt16(1);
                        cursos.Add(curso);
                    }
                }
            }

            return cursos;

        }

        public List<Curso> ObtenerCurriculo() {

            List<Curso> curriculo = new List<Curso>();
            SqlDataReader lector;


            string sentenciaSql = @"SELECT IdCurso, Nombre, Descripcion
                                    FROM Curso";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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
                                    WHERE IdCurso = {idCurso}";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

                SqlCommand comando = new SqlCommand {
                    CommandType = CommandType.Text,
                    CommandText = sentenciaSql,
                    Connection = conexion
                };

                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows) {
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


            string sentenciaSql = @"SELECT IdCurso, Nombre, Descripcion
                                    FROM Curso";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {

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

        private Sede ObtenerUnaSede(string idSede) {
            Sede sede = default;
            SqlDataReader lector;
            SqlCommand comando = new SqlCommand();

            string sentenciaSql = @$"SELECT IdSede, Descripcion
                                    FROM Sede
                                    WHERE IdSede = {idSede}";

            using (SqlConnection conexion = new SqlConnection(InformacionConexion)) {
                comando.CommandType = CommandType.Text;
                comando.CommandText = sentenciaSql;
                comando.Connection = conexion;

                lector = comando.ExecuteReader();

                if (lector.HasRows) {
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
