using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversidadEduca_Tarea1.Exceptions;
using UniversidadEduca_Tarea1.Models;

namespace UniversidadEduca_Tarea1
{
    class ConsoleUI {

        private string[] MenuOptions;
        private bool Exit;
        private UniversityManager Manager;

        public ConsoleUI() {
            SetMenuOptions();
            Exit = false;
            Manager = new UniversityManager();
        }

        public void Start() {
            Console.WriteLine("Universidad Educa - Sistema de Administración");

            while (!Exit ) {
                try {
                    ShowMainMenu();
                    int selectedOption = GetSelectedOption(Console.ReadLine(), MenuOptions.Length);
                    ProcessActionSelected(selectedOption);


                } catch (System.ArgumentNullException) {
                    ShowErrorMessage("Por favor seleccione una opción del menú. Ninguna opción fue seleccionada");
                } catch (InvalidSelectionException ex) {
                    ShowErrorMessage(ex.Message);
                } catch (DuplicateObjectException ex) {
                    ShowErrorMessage(ex.Message);
                } catch (NotANumberException ex) {
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        private void ShowMainMenu() {
            Console.WriteLine("\nSeleccione la acción que desea realizar");
            for (int i = 0; i < MenuOptions.Length; i++) {
                Console.WriteLine($"{i+1}. {MenuOptions[i]}");
            }
        }

        private int GetSelectedOption(string userSelectedOption, int arrayLength) {
            if (IsEmpty(userSelectedOption)) {
                throw new ArgumentNullException(userSelectedOption);
            }

            int selectedOption = GetNumberFromInput(userSelectedOption);

            if (selectedOption <= 0 || selectedOption > arrayLength) {
                throw new InvalidSelectionException("El número digitado no pertenece a las opciones disponibles");
            }

            return selectedOption;
        }

        private void ShowErrorMessage(string message) {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private void SetMenuOptions() {
            MenuOptions = new string[9]{"Registrar nueva sede",
                                        "Registrar profesor",
                                        "Registrar estudiante",
                                        "Registrar curso",
                                        "Matricular estudiante en curso",
                                        "Asignar curso a profesor",
                                        "Mostrar lista de estudiantes",
                                        "Mostrar lista de profesores",
                                        "Salir" };
        }

        private void ProcessActionSelected(int selectedOption) {
            switch (selectedOption) {
                case 1:
                    try {
                        Console.WriteLine("\n----- Agregar Nueva Sede -----");
                        Console.WriteLine("\nDigite el número de identificación de la nueva sede");
                        int campusId = GetNumberFromInput(Console.ReadLine());

                        Console.WriteLine("Digite la descripción de la nueva sede");
                        string description = GetTextFromInput(Console.ReadLine());

                        Manager.AddCampus(campusId, description);
                        Console.WriteLine("\nLa sede fue agregada correctamente");
                        Console.WriteLine("Digite 1 para agregar otra o cualquier otra tecla para retornar al menú principal");
                        if (int.TryParse(Console.ReadLine(), out int option)) {
                            if (option == 1) {
                                ProcessActionSelected(1);
                            }
                        }

                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(1); //Llamada recursiva en caso de error
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(1); //Llamada recursiva en caso de error
                    } catch (System.ArgumentNullException) {
                        ShowErrorMessage("Debe digitar los datos solicitados");
                        ProcessActionSelected(1); 
                    } catch (System.IndexOutOfRangeException) {
                        ShowErrorMessage("Se ha alcanzado el número máximo de sedes");
                        //Acá no hacemos la llamada recursiva, ya que crearíamos un soft lock
                    }
                    break;
                case 2:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Profesor -----");
                        if (AreThereCampuses()) {
                            ShowCampuses();
                            int professorCampusId = GetNumberFromInput(Console.ReadLine());
                            if (!Manager.CampusExists(professorCampusId, out Sede campus)) {
                                throw new InvalidSelectionException("La sede digitada no existe");
                            }

                            Console.WriteLine("\n----- Información personal -----");
                            Console.WriteLine("Digite el número de identificación");
                            int professorId = GetNumberFromInput(Console.ReadLine());
                            Console.WriteLine("Digite el nombre");
                            string professorName = GetTextFromInput(Console.ReadLine());
                            if (professorName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el apellido");
                            string professorLastName = GetTextFromInput(Console.ReadLine());
                            if (professorLastName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el segundo apellido");
                            string professorSecLastName = GetTextFromInput(Console.ReadLine());
                            if (professorSecLastName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el sueldo");
                            decimal salary = GetDecimalFromInput(Console.ReadLine());

                            Console.WriteLine("\n----- Información de ingreso a plataforma virtual -----");
                            Console.WriteLine("Digite el nombre de usuario");
                            string userName = GetTextFromInput(Console.ReadLine());
                            Console.WriteLine("Digite la contraseña");
                            string password = GetTextFromInput(Console.ReadLine());

                            Manager.AddProfessor(professorId, professorName, professorLastName, professorSecLastName, salary, userName, password, campus);
                            Console.WriteLine("\nEl profesor fue agregado correctamente");
                            Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                            if (int.TryParse(Console.ReadLine(), out int option)) {
                                if (option == 1) {
                                    ProcessActionSelected(2);
                                }
                            }
                        } else {
                            ShowErrorMessage("Debe agregar al menos una sede primero");
                        }

                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(2);
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(2); //Llamada recursiva en caso de error
                    } catch (NotACharException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(2);
                    } catch (System.ArgumentNullException) {
                        ShowErrorMessage("Debe digitar los datos solicitados");
                        ProcessActionSelected(2);
                    } catch (System.IndexOutOfRangeException) {
                        ShowErrorMessage("Se ha alcanzado el número máximo de profesores");
                    }
                    break;
                case 3:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Estudiante -----");
                        if (AreThereCampuses()) {
                            ShowCampuses();
                            int studentCampusId = GetNumberFromInput(Console.ReadLine());
                            if (!Manager.CampusExists(studentCampusId, out Sede campus)) {
                                throw new InvalidSelectionException("La sede digitada no existe");
                            }

                            Console.WriteLine("\n----- Información personal -----");
                            Console.WriteLine("Digite el número de identificación");
                            int studentId = GetNumberFromInput(Console.ReadLine());
                            Console.WriteLine("Digite el nombre");
                            string studentName = GetTextFromInput(Console.ReadLine());
                            if (studentName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el apellido");
                            string studentLastName = GetTextFromInput(Console.ReadLine());
                            if (studentLastName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite el segundo apellido");
                            string studentSecLastName = GetTextFromInput(Console.ReadLine());
                            if (studentSecLastName.Any(char.IsDigit)) {
                                throw new NotACharException("Por favor digite únicamente letras");
                            }

                            Console.WriteLine("Digite la fecha de nacimiento (DD/MM/YYYY)");
                            DateTime dob = GetDateFromInput(Console.ReadLine());
                            ShowGenders();
                            char selectedGender = GetCharFromInput(Console.ReadLine(), new char[] { 'M', 'F' });

                            Manager.AddStudent(studentId, studentName, studentLastName, studentSecLastName, dob, selectedGender, campus);
                            
                            Console.WriteLine("\nEl estudiante fue agregado correctamente");
                            Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                            if (int.TryParse(Console.ReadLine(), out int option)) {
                                if (option == 1) {
                                    ProcessActionSelected(3);
                                }
                            }

                        } else {
                            ShowErrorMessage("Debe agregar al menos una sede primero");
                        }

                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(3);
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(3); //Llamada recursiva en caso de error
                    } catch (NotADateException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(3);
                    } catch (NotACharException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(3);
                    } catch (System.ArgumentNullException) {
                        ShowErrorMessage("Debe digitar los datos solicitados");
                        ProcessActionSelected(3);
                    } catch (System.IndexOutOfRangeException) {
                        ShowErrorMessage("Se ha alcanzado el número máximo de estudiantes");
                    }
                    break;
                case 4:
                    try {
                        Console.WriteLine("\n----- Agregar Nuevo Curso -----");
                        Console.WriteLine("\nDigite el código numérico del nuevo curso");
                        int courseId = GetNumberFromInput(Console.ReadLine());

                        Console.WriteLine("Digite el nombre del nuevo curso");
                        string courseName = GetTextFromInput(Console.ReadLine());

                        Console.WriteLine("Digite la descripción del nuevo curso");
                        string courseDescription = GetTextFromInput(Console.ReadLine());

                        Manager.AddCourse(courseId, courseName, courseDescription);
                        Console.WriteLine("\nEl curso fue agregado correctamente");
                        Console.WriteLine("Digite 1 para agregar otro o cualquier otra tecla para retornar al menú principal");
                        if (int.TryParse(Console.ReadLine(), out int option)) {
                            if (option == 1) {
                                ProcessActionSelected(4);
                            }
                        }

                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(4); //Llamada recursiva en caso de error
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(4); //Llamada recursiva en caso de error
                    } catch (System.ArgumentNullException) {
                        ShowErrorMessage("Debe digitar los datos solicitados");
                        ProcessActionSelected(4);
                    } catch (System.IndexOutOfRangeException) {
                        ShowErrorMessage("Se ha alcanzado el número máximo de cursos");
                    }
                    break;
                case 5:
                    try {
                        Console.WriteLine("\n----- Matricular Estudiante en Curso -----");
                        if (AreThereCampuses()) {
                            if (AreThereCourses()) {
                                ShowCampuses();
                                int campusId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.CampusExists(campusId, out _)) {
                                    throw new InvalidSelectionException("La sede digitada no existe");
                                }

                                ShowStudentsByCampus(campusId);
                                int studentId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.StudentExists(studentId, out Estudiante student)) {
                                    throw new InvalidSelectionException("El estudiante digitado no existe");
                                }

                                ShowCourses();
                                int courseId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.CourseExists(courseId, out Curso course)) {
                                    throw new InvalidSelectionException("El curso digitado no existe");
                                }

                                Manager.Enroll(student, course);
                                Console.WriteLine("\nEl estudiante fue matriculado correctamente");
                                Console.WriteLine("Digite 1 para matricular otro curso o cualquier otra tecla para retornar al menú principal");
                                if (int.TryParse(Console.ReadLine(), out int option)) {
                                    if (option == 1) {
                                        ProcessActionSelected(5);
                                    }
                                }
                            } else {
                                ShowErrorMessage("Debe agregar al menos un curso primero");
                            }
                        } else {
                            ShowErrorMessage("Debe agregar al menos una sede primero");
                        }
                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(5); //Llamada recursiva en caso de error
                    } 
                    break;
                case 6:
                    try {
                        Console.WriteLine("\n----- Agregar Profesor a Curso -----");
                        if (AreThereCampuses()) {
                            if (AreThereCourses()) {
                                ShowCampuses();
                                int campusId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.CampusExists(campusId, out _)) {
                                    throw new InvalidSelectionException("La sede digitada no existe");
                                }

                                ShowProfessorsByCampus(campusId);
                                int professorId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.ProfessorExists(professorId, out Profesor professor)) {
                                    throw new InvalidSelectionException("El profesor digitado no existe");
                                }

                                ShowCourses();
                                int courseId = GetNumberFromInput(Console.ReadLine());
                                if (!Manager.CourseExists(courseId, out Curso course)) {
                                    throw new InvalidSelectionException("El curso digitado no existe");
                                }

                                Manager.Enroll(professor, course);
                                Console.WriteLine("\nEl profesor fue agregado al curso correctamente");
                                Console.WriteLine("Digite 1 para agregar otro curso o cualquier otra tecla para retornar al menú principal");
                                if (int.TryParse(Console.ReadLine(), out int option)) {
                                    if (option == 1) {
                                        ProcessActionSelected(6);
                                    }
                                }
                            } else {
                                ShowErrorMessage("Debe agregar al menos un curso primero");
                            }
                        } else {
                            ShowErrorMessage("Debe agregar al menos una sede primero");
                        }
                    } catch (NotANumberException ex) {
                        ShowErrorMessage(ex.Message);
                    } catch (NegativeNumberException ex) {
                        ShowErrorMessage(ex.Message);
                        ProcessActionSelected(6); //Llamada recursiva en caso de error
                    }
                    break;
                case 7:
                    ShowStudents();
                    break;
                case 8:
                    ShowProfessors();
                    break;
                case 9:
                    Console.WriteLine("Está seguro que desea salir? Y/N");
                    string isExit = Console.ReadLine();
                    if (isExit.ToUpper() == "Y") {
                        Console.WriteLine("\nSesión finalizada exitosamente");
                        Exit = true;
                    }
                    break;
            }
        }


        #region Validaciones
        /// En esta región se valida el número ingresado por el usuario. Si hay un error en el cast explícito, la excepción resultante es arrojada 
        /// para que sea manejada por el método que consume 

        private int GetNumberFromInput(string inputNumber) {
            if (!Int32.TryParse(inputNumber, out int number)) {
                throw new NotANumberException("Por favor digite únicamente números");
            }
            if (number < 0) {
                throw new NegativeNumberException("Por favor digite únicamente números positivos");
            }
            return number;
        }

        private decimal GetDecimalFromInput(string inputDecimal) {
            if (!Decimal.TryParse(inputDecimal, out decimal number)) {
                throw new NotANumberException("Por favor digite únicamente números");
            }
            return number;
        }

        private string GetTextFromInput(string text) {
            return IsEmpty(text) ? throw new ArgumentNullException(text) : text;
        }

        private DateTime GetDateFromInput(string inputDate) {
            //Debemos seleccionar el culture correcto para poder transformar adecuadamente la fecha ingresada
            if (!DateTime.TryParse(inputDate, System.Globalization.CultureInfo.GetCultureInfo("es-CR"), System.Globalization.DateTimeStyles.None, out DateTime date)) {
                throw new NotADateException("Formato de fecha incorrecto");
            }
            return date;
        }

        private char GetCharFromInput(string inputChar, char[] permittedValues) {
            inputChar = inputChar.ToUpper();
            if (!char.TryParse(inputChar, out char character) || !Array.Exists(permittedValues, c => c == character)) {
                throw new NotACharException("Por favor digite un caracter dentro de las opciones mostradas");
            }

            return character;
        }

        private bool IsEmpty(string inputText) {
            return "".Equals(inputText);
        }

        #endregion

        #region mostrar Opciones de Seleccion
        private void ShowCampuses() {
            Console.WriteLine("Seleccione la sede");
            for (int i = 0; i < Manager.CampusCounter; i++) {
                Console.WriteLine($"{Manager.Campuses[i].Id} - {Manager.Campuses[i].Description}");
            }
        }

        private void ShowStudentsByCampus(int campusId) {
            Console.WriteLine("\nSeleccione el estudiante que desea matricular");
            foreach(Estudiante student in Manager.Students) {
                if (student != null && student.Campus.Id == campusId) {
                    Console.WriteLine($"\nId: {student.Id}. Nombre: {student.Name} {student.LastName} {student.SecondLastName}");
                    Console.WriteLine($"Fecha Nacimiento: {student.DateOfBirth}, Genero: {student.Gender}\n");
                }
            }
        }

        private void ShowProfessorsByCampus(int campusId) {
            Console.WriteLine("Seleccione el profesor que desea agregar al curso");
            foreach (Profesor professor in Manager.Faculty) {
                if (professor != null && professor.Campus.Id == campusId) {
                    Console.WriteLine($"\nId: {professor.Id}. Nombre: {professor.Name} {professor.LastName} {professor.SecondLastName}");
                }
            }
        }

        private void ShowStudents() {
            Console.WriteLine("\n----- Lista de Estudiantes -----");
            foreach (Estudiante student in Manager.Students) {
                if (student != null) {
                    Console.WriteLine($"\nId: {student.Id}. Nombre: {student.Name} {student.LastName} {student.SecondLastName}");
                    Console.WriteLine($"Fecha Nacimiento: {student.DateOfBirth}, Genero: {student.Gender}");
                    Console.WriteLine($"Sede a la que pertenece : {student.Campus.Id} - {student.Campus.Description}");

                    if (student.CourseCounter > 0) {
                        Console.WriteLine("Cursos Matriculados:");
                        foreach (Curso course in student.Courses) {
                            if (course != null) {
                                Console.WriteLine($"{course.Id} - {course.Name} - {course.Description}");
                            }
                        }
                    }
                    
                }
            }
            Console.WriteLine("\nDigite cualquier tecla para regresar al menú principal");
            Console.ReadLine();
        }

        private void ShowProfessors() {
            Console.WriteLine("\n----- Lista de Profesores -----");
            foreach (Profesor professor in Manager.Faculty) {
                if (professor != null) {
                    Console.WriteLine($"\nId: {professor.Id}. Nombre: {professor.Name} {professor.LastName} {professor.SecondLastName}");
                    Console.WriteLine($"Sueldo: {professor.Salary}, Nombre de Usuario: {professor.LoginInfo.User}, Contraseña: {professor.LoginInfo.Password}");
                    Console.WriteLine($"Sede a la que pertenece: {professor.Campus.Id} - {professor.Campus.Description}");

                    if (professor.CourseCounter > 0) {
                        Console.WriteLine("Cursos Matriculados:");
                        foreach (Curso course in professor.Courses) {
                            if (course != null) {
                                Console.WriteLine($"{course.Id} - {course.Name} - {course.Description}");
                            }
                        }
                    }

                }
            }
            Console.WriteLine("\nDigite cualquier tecla para regresar al menú principal");
            Console.ReadLine();
        }

        private void ShowCourses() {
            Console.WriteLine("\nSeleccione el código del curso que desea matricular");
            foreach(Curso course in Manager.Curriculum) {
                if (course != null) {
                    Console.WriteLine($"Código: {course.Id}. Nombre: {course.Name}. Descripción: {course.Description}\n");
                }
            }
        }

        private void ShowGenders() {
            Console.WriteLine("Seleccione el género");
            Console.WriteLine("M. Masculino");
            Console.WriteLine("F. Femenino");
        }

        #endregion

        private bool AreThereCampuses() {
            return Manager.CampusCounter > 0;
        }

        private bool AreThereCourses() {
            return Manager.CoursesCounter > 0;
        }

    }
}
