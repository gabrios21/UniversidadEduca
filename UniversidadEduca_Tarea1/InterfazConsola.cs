using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Exceptions;
using UniversidadEduca_Tarea1.Models;

namespace UniversidadEduca_Tarea1
{
    class InterfazConsola {

        private string[] OpcionesMenu;
        private bool Salir;
        private GestorUniversidad Gestor;

        public InterfazConsola() {
            DefinirOpcionesMenu();
            Salir = false;
            Gestor = new GestorUniversidad();
        }

        public void Start() {
            Console.WriteLine("Universidad Educa - Sistema de Administración");

            while (!Salir ) {
                try {
                    MostrarMenuPrincipal();
                    int opcionSeleccionada = ObtenerOpcionSeleccionada(Console.ReadLine(), OpcionesMenu.Length);
                    ProcesarAccion(opcionSeleccionada);


                } catch (System.ArgumentNullException) {
                    MostrarError("Por favor seleccione una opción del menú. Ninguna opción fue seleccionada");
                } catch (SeleccionInvalidaException ex) {
                    MostrarError(ex.Message);
                } catch (ObjectoDuplicadoException ex) {
                    MostrarError(ex.Message);
                } catch (NoEsNumeroException ex) {
                    MostrarError(ex.Message);
                }
            }
        }

        private void MostrarMenuPrincipal() {
            Console.WriteLine("\nSeleccione la acción que desea realizar");
            for (int i = 0; i < OpcionesMenu.Length; i++) {
                Console.WriteLine($"{i+1}. {OpcionesMenu[i]}");
            }
        }

        private int ObtenerOpcionSeleccionada(string userSelectedOption, int arrayLength) {
            if (EstaVacio(userSelectedOption)) {
                throw new ArgumentNullException(userSelectedOption);
            }

            int opcionSeleccionada = ObtenerNumeroEntrada(userSelectedOption);

            if (opcionSeleccionada <= 0 || opcionSeleccionada > arrayLength) {
                throw new SeleccionInvalidaException("El número digitado no pertenece a las opciones disponibles");
            }

            return opcionSeleccionada;
        }

        private void MostrarError(string message) {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private void DefinirOpcionesMenu() {
            OpcionesMenu = new string[9]{"Registrar nueva sede",
                                        "Registrar profesor",
                                        "Registrar estudiante",
                                        "Registrar curso",
                                        "Matricular estudiante en curso",
                                        "Asignar curso a profesor",
                                        "Mostrar lista de estudiantes",
                                        "Mostrar lista de profesores",
                                        "Salir" };
        }

        private void ProcesarAccion(int opcionSeleccionada) {
            switch (opcionSeleccionada) {
                case 1:
                    try {
                        Console.WriteLine("\n----- Agregar Nueva Sede -----");
                        Console.WriteLine("\nDigite el número de identificación de la nueva sede");
                        int idSede = ObtenerNumeroEntrada(Console.ReadLine());

                        Console.WriteLine("Digite la descripción de la nueva sede");
                        string descripcion = ObtenerTextoEntrada(Console.ReadLine());

                        Gestor.AgregarCampus(idSede, descripcion);
                        Console.WriteLine("\nLa sede fue agregada correctamente");
                        Console.WriteLine("Digite 1 para agregar otra o cualquier otra tecla para retornar al menú principal");
                        if (int.TryParse(Console.ReadLine(), out int opcion)) {
                            if (opcion == 1) {
                                ProcesarAccion(1);
                            }
                        }

                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(1); //Llamada recursiva en caso de error
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(1); //Llamada recursiva en caso de error
                    } catch (System.ArgumentNullException) {
                        MostrarError("Debe digitar los datos solicitados");
                        ProcesarAccion(1); 
                    } catch (System.IndexOutOfRangeException) {
                        MostrarError("Se ha alcanzado el número máximo de sedes");
                        //Acá no hacemos la llamada recursiva, ya que crearíamos un soft lock
                    }
                    break;
                case 2:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Profesor -----");
                        if (ExistenSedes()) {
                            MostrarSedes();
                            int sedeProfesor = ObtenerNumeroEntrada(Console.ReadLine());
                            if (!Gestor.SedeExiste(sedeProfesor, out Sede campus)) {
                                throw new SeleccionInvalidaException("La sede digitada no existe");
                            }

                            Console.WriteLine("\n----- Información personal -----");
                            Console.WriteLine("Digite el número de identificación");
                            int idProfesor = ObtenerNumeroEntrada(Console.ReadLine());
                            Console.WriteLine("Digite el nombre");
                            string nombreProfesor = ObtenerTextoEntrada(Console.ReadLine());
                            if (nombreProfesor.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el apellido");
                            string aoellidoProfesor = ObtenerTextoEntrada(Console.ReadLine());
                            if (aoellidoProfesor.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el segundo apellido");
                            string segApellidoProfesor = ObtenerTextoEntrada(Console.ReadLine());
                            if (segApellidoProfesor.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el sueldo");
                            decimal sueldo = ObtenerDecimalEntrada(Console.ReadLine());

                            Console.WriteLine("\n----- Información de ingreso a plataforma virtual -----");
                            Console.WriteLine("Digite el nombre de usuario");
                            string nombreUsuario = ObtenerTextoEntrada(Console.ReadLine());
                            Console.WriteLine("Digite la contraseña");
                            string contrasena = ObtenerTextoEntrada(Console.ReadLine());

                            Gestor.AgregarProfesor(idProfesor, nombreProfesor, aoellidoProfesor, segApellidoProfesor, sueldo, nombreUsuario, contrasena, campus);
                            Console.WriteLine("\nEl profesor fue agregado correctamente");
                            Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                            if (int.TryParse(Console.ReadLine(), out int opcion)) {
                                if (opcion == 1) {
                                    ProcesarAccion(2);
                                }
                            }
                        } else {
                            MostrarError("Debe agregar al menos una sede primero");
                        }

                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(2);
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(2); //Llamada recursiva en caso de error
                    } catch (NoEsCaracterException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(2);
                    } catch (System.ArgumentNullException) {
                        MostrarError("Debe digitar los datos solicitados");
                        ProcesarAccion(2);
                    } catch (System.IndexOutOfRangeException) {
                        MostrarError("Se ha alcanzado el número máximo de profesores");
                    }
                    break;
                case 3:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Estudiante -----");
                        if (ExistenSedes()) {
                            MostrarSedes();
                            int sedeEstudiante = ObtenerNumeroEntrada(Console.ReadLine());
                            if (!Gestor.SedeExiste(sedeEstudiante, out Sede sede)) {
                                throw new SeleccionInvalidaException("La sede digitada no existe");
                            }

                            Console.WriteLine("\n----- Información personal -----");
                            Console.WriteLine("Digite el número de identificación");
                            int idEstudiante = ObtenerNumeroEntrada(Console.ReadLine());
                            Console.WriteLine("Digite el nombre");
                            string nombreEstudiante = ObtenerTextoEntrada(Console.ReadLine());
                            if (nombreEstudiante.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el apellido");
                            string apellidoEstudiante = ObtenerTextoEntrada(Console.ReadLine());
                            if (apellidoEstudiante.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el segundo apellido");
                            string segApellidoEstudiante = ObtenerTextoEntrada(Console.ReadLine());
                            if (segApellidoEstudiante.Any(char.IsDigit)) {
                                throw new NoEsCaracterException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite la fecha de nacimiento (DD/MM/YYYY)");
                            DateTime fechaNacimiento = ObtenerFechaEntrada(Console.ReadLine());
                            MostrarGeneros();
                            char generoSeleccionado = ObtenerCaracterEntrada(Console.ReadLine(), new char[] { 'M', 'F' });

                            Gestor.AgregarEstudiante(idEstudiante, nombreEstudiante, apellidoEstudiante, segApellidoEstudiante, fechaNacimiento, generoSeleccionado, sede);
                            
                            Console.WriteLine("\nEl estudiante fue agregado correctamente");
                            Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                            if (int.TryParse(Console.ReadLine(), out int opcion)) {
                                if (opcion == 1) {
                                    ProcesarAccion(3);
                                }
                            }

                        } else {
                            MostrarError("Debe agregar al menos una sede primero");
                        }

                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(3);
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(3); //Llamada recursiva en caso de error
                    } catch (NoEsFechaException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(3);
                    } catch (NoEsCaracterException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(3);
                    } catch (System.ArgumentNullException) {
                        MostrarError("Debe digitar los datos solicitados");
                        ProcesarAccion(3);
                    } catch (System.IndexOutOfRangeException) {
                        MostrarError("Se ha alcanzado el número máximo de estudiantes");
                    }
                    break;
                case 4:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Curso -----");
                        Console.WriteLine("\nDigite el código numérico del nuevo curso");
                        int idCurso = ObtenerNumeroEntrada(Console.ReadLine());

                        Console.WriteLine("Digite el nombre del nuevo curso");
                        string nombreCurso = ObtenerTextoEntrada(Console.ReadLine());

                        Console.WriteLine("Digite la descripción del nuevo curso");
                        string descripcionCurso = ObtenerTextoEntrada(Console.ReadLine());

                        Gestor.AgregarCurso(idCurso, nombreCurso, descripcionCurso);
                        Console.WriteLine("\nEl curso fue agregado correctamente");
                        Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                        if (int.TryParse(Console.ReadLine(), out int opcion)) {
                            if (opcion == 1) {
                                ProcesarAccion(4);
                            }
                        }

                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(4); //Llamada recursiva en caso de error
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(4); //Llamada recursiva en caso de error
                    } catch (System.ArgumentNullException) {
                        MostrarError("Debe digitar los datos solicitados");
                        ProcesarAccion(4);
                    } catch (System.IndexOutOfRangeException) {
                        MostrarError("Se ha alcanzado el número máximo de cursos");
                    }
                    break;
                case 5:
                    try {
                        Console.WriteLine("\n----- Matricular Estudiante en Curso -----");
                        if (ExistenSedes()) {
                            if (ExistenCursos()) {
                                MostrarSedes();
                                int idSede = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.SedeExiste(idSede, out _)) {
                                    throw new SeleccionInvalidaException("La sede digitada no existe");
                                }

                                MostrarEstudiantesPorSede(idSede);
                                int idEstudiante = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.EstudianteExiste(idEstudiante, out Estudiante estudiante)) {
                                    throw new SeleccionInvalidaException("El estudiante digitado no existe");
                                }

                                MostrarCursos();
                                int idCurso = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.CursoExiste(idCurso, out Curso curso)) {
                                    throw new SeleccionInvalidaException("El curso digitado no existe");
                                }

                                Gestor.Matricular(estudiante, curso);
                                Console.WriteLine("\nEl estudiante fue matriculado correctamente");
                                Console.WriteLine("Digite 1 para matricular otro curso o cualquier otra tecla para retornar al menú principal");
                                if (int.TryParse(Console.ReadLine(), out int option)) {
                                    if (option == 1) {
                                        ProcesarAccion(5);
                                    }
                                }
                            } else {
                                MostrarError("Debe agregar al menos un curso primero");
                            }
                        } else {
                            MostrarError("Debe agregar al menos una sede primero");
                        }
                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(5); //Llamada recursiva en caso de error
                    } 
                    break;
                case 6:
                    try {
                        Console.WriteLine("\n----- Agregar Profesor a Curso -----");
                        if (ExistenSedes()) {
                            if (ExistenCursos()) {
                                MostrarSedes();
                                int idSede = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.SedeExiste(idSede, out _)) {
                                    throw new SeleccionInvalidaException("La sede digitada no existe");
                                }

                                MostrarProfesoresPorSede(idSede);
                                int professorId = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.ProfesorExiste(professorId, out Profesor profesor)) {
                                    throw new SeleccionInvalidaException("El profesor digitado no existe");
                                }

                                MostrarCursos();
                                int courseId = ObtenerNumeroEntrada(Console.ReadLine());
                                if (!Gestor.CursoExiste(courseId, out Curso curso)) {
                                    throw new SeleccionInvalidaException("El curso digitado no existe");
                                }

                                Gestor.Matricular(profesor, curso);
                                Console.WriteLine("\nEl profesor fue agregado al curso correctamente");
                                Console.WriteLine("Digite 1 para agregar otro curso o cualquier otra tecla para retornar al menú principal");
                                if (int.TryParse(Console.ReadLine(), out int opcion)) {
                                    if (opcion == 1) {
                                        ProcesarAccion(6);
                                    }
                                }
                            } else {
                                MostrarError("Debe agregar al menos un curso primero");
                            }
                        } else {
                            MostrarError("Debe agregar al menos una sede primero");
                        }
                    } catch (NoEsNumeroException ex) {
                        MostrarError(ex.Message);
                    } catch (NumeroNegativoException ex) {
                        MostrarError(ex.Message);
                        ProcesarAccion(6); //Llamada recursiva en caso de error
                    }
                    break;
                case 7:
                    MostrarEstudiantes();
                    break;
                case 8:
                    MostrarProfesores();
                    break;
                case 9:
                    Console.WriteLine("Está seguro que desea salir? Y/N");
                    string deseaSalir = Console.ReadLine();
                    if (deseaSalir.ToUpper() == "Y") {
                        Console.WriteLine("\nSesión finalizada exitosamente");
                        Salir = true;
                    }
                    break;
            }
        }


        #region Validaciones
        /// En esta región se valida el número ingresado por el usuario. Si hay un error en el cast explícito, la excepción resultante es arrojada 
        /// para que sea manejada por el método que consume 

        private int ObtenerNumeroEntrada(string entradaNumerica) {
            if (!Int32.TryParse(entradaNumerica, out int number)) {
                throw new NoEsNumeroException("Por favor digite únicamente números");
            }
            if (number < 0) {
                throw new NumeroNegativoException("Por favor digite únicamente números positivos");
            }
            return number;
        }

        private decimal ObtenerDecimalEntrada(string entradaDecimal) {
            if (!Decimal.TryParse(entradaDecimal, out decimal number)) {
                throw new NoEsNumeroException("Por favor digite únicamente números");
            }
            return number;
        }

        private string ObtenerTextoEntrada(string entradaTextual) {
            return EstaVacio(entradaTextual) ? throw new ArgumentNullException(entradaTextual) : entradaTextual;
        }

        private DateTime ObtenerFechaEntrada(string entradaFecha) {
            //Debemos seleccionar el culture correcto para poder transformar adecuadamente la fecha ingresada
            if (!DateTime.TryParse(entradaFecha, System.Globalization.CultureInfo.GetCultureInfo("es-CR"), System.Globalization.DateTimeStyles.None, out DateTime date)) {
                throw new NoEsFechaException("Formato de fecha incorrecto");
            }
            return date;
        }

        private char ObtenerCaracterEntrada(string entradaCaracter, char[] valoresPermitidos) {
            entradaCaracter = entradaCaracter.ToUpper();
            if (!char.TryParse(entradaCaracter, out char character) || !Array.Exists(valoresPermitidos, c => c == character)) {
                throw new NoEsCaracterException("Por favor digite un caracter dentro de las opciones mostradas");
            }

            return character;
        }

        private bool EstaVacio(string inputText) {
            return "".Equals(inputText);
        }

        #endregion

        #region mostrar Opciones de Seleccion
        private void MostrarSedes() {
            Console.WriteLine("Seleccione la sede");
            for (int i = 0; i < Gestor.ContadorSedes; i++) {
                Console.WriteLine($"{Gestor.Campuses[i].Id} - {Gestor.Campuses[i].Descripcion}");
            }
        }

        private void MostrarEstudiantesPorSede(int idSede) {
            Console.WriteLine("\nSeleccione el estudiante que desea matricular");
            foreach(Estudiante estudiante in Gestor.Estudiante) {
                if (estudiante != null && estudiante.Sede.Id == idSede) {
                    Console.WriteLine($"\nId: {estudiante.Id}. Nombre: {estudiante.Nombre} {estudiante.Apellido} {estudiante.SegundoApellido}");
                    Console.WriteLine($"Fecha Nacimiento: {estudiante.FechaNacimiento}, Genero: {estudiante.Genero}\n");
                }
            }
        }

        private void MostrarProfesoresPorSede(int idSede) {
            Console.WriteLine("Seleccione el profesor que desea agregar al curso");
            foreach (Profesor profesor in Gestor.Profesorado) {
                if (profesor != null && profesor.Sede.Id == idSede) {
                    Console.WriteLine($"\nId: {profesor.Id}. Nombre: {profesor.Nombre} {profesor.Apellido} {profesor.SegundoApellido}");
                }
            }
        }

        private void MostrarEstudiantes() {
            Console.WriteLine("\n----- Lista de Estudiantes -----");
            foreach (Estudiante estudiante in Gestor.Estudiante) {
                if (estudiante != null) {
                    Console.WriteLine($"\nId: {estudiante.Id}. Nombre: {estudiante.Nombre} {estudiante.Apellido} {estudiante.SegundoApellido}");
                    Console.WriteLine($"Fecha Nacimiento: {estudiante.FechaNacimiento}, Genero: {estudiante.Genero}");
                    Console.WriteLine($"Sede a la que pertenece : {estudiante.Sede.Id} - {estudiante.Sede.Descripcion}");

                    if (estudiante.ContadorCursosMatriculados > 0) {
                        Console.WriteLine("Cursos Matriculados:");
                        foreach (Curso curso in estudiante.Cursos) {
                            if (curso != null) {
                                Console.WriteLine($"{curso.Id} - {curso.Nombre} - {curso.Descripcion}");
                            }
                        }
                    }
                    
                }
            }
            Console.WriteLine("\nDigite cualquier tecla para regresar al menú principal");
            Console.ReadLine();
        }

        private void MostrarProfesores() {
            Console.WriteLine("\n----- Lista de Profesores -----");
            foreach (Profesor profesor in Gestor.Profesorado) {
                if (profesor != null) {
                    Console.WriteLine($"\nId: {profesor.Id}. Nombre: {profesor.Nombre} {profesor.Apellido} {profesor.SegundoApellido}");
                    Console.WriteLine($"Sueldo: {profesor.Sueldo}, Nombre de Usuario: {profesor.Plataforma.Usuario}, Contraseña: {profesor.Plataforma.Contrasena}");
                    Console.WriteLine($"Sede a la que pertenece: {profesor.Sede.Id} - {profesor.Sede.Descripcion}");

                    if (profesor.ContadorCursosMatriculados > 0) {
                        Console.WriteLine("Cursos Matriculados:");
                        foreach (Curso curso in profesor.Cursos) {
                            if (curso != null) {
                                Console.WriteLine($"{curso.Id} - {curso.Nombre} - {curso.Descripcion}");
                            }
                        }
                    }

                }
            }
            Console.WriteLine("\nDigite cualquier tecla para regresar al menú principal");
            Console.ReadLine();
        }

        private void MostrarCursos() {
            Console.WriteLine("\nSeleccione el código del curso que desea matricular");
            foreach(Curso curso in Gestor.Curriculo) {
                if (curso != null) {
                    Console.WriteLine($"Código: {curso.Id}. Nombre: {curso.Nombre}. Descripción: {curso.Descripcion}\n");
                }
            }
        }

        private void MostrarGeneros() {
            Console.WriteLine("Seleccione el género");
            Console.WriteLine("M. Masculino");
            Console.WriteLine("F. Femenino");
        }

        #endregion

        private bool ExistenSedes() {
            return Gestor.ContadorSedes > 0;
        }

        private bool ExistenCursos() {
            return Gestor.ContadorCursos > 0;
        }

    }
}
