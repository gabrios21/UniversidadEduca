using Gestores_UniversidadEduca;
using Modelos_UniversidadEduca.Excepciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UniversidadEduca_Tarea1.Exceptions;
using UniversidadEduca_Tarea1.Models;

namespace GUI_UniversidadEduca {
    public partial class FormularioPrincipal : Form {

        private List<Panel> Paneles;
        List<string> Generos => new() { "Masculino", "Femenino" };
        GestorSede gestorSede => new();
        GestorCurso gestorCurso => new();
        GestorProfesor gestorProfesor => new();
        GestorEstudiante gestorEstudiante => new();
        GestorNotas gestorNotas => new();
        public FormularioPrincipal() {

            InitializeComponent();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e) {
            Paneles = new List<Panel> {
                nuevaSedePanel,
                nuevoProfesorPanel,
                nuevoEstudiantePanel,
                nuevoCursoPanel,
                matricularPanel,
                ingresoPlataformaPanel,
                asignarProfesorPanel,
                registroNotasPanel,
                estudiantesPanel, 
                profesoresPanel
            };

            //Ocultar distintos paneles durante inicialización
            OcultarPaneles();

            //Cargar sedes en combobox
            var listaSedes = gestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                sedeProfesor.Items.Add(sede);
                sedeEstudiante.Items.Add(sede);
            }

            //Cargar generos en combobox
            foreach (string genero in Generos) {
                generoEstudiante.Items.Add(genero);
            }
        }

        private void registrarProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoProfesorPanel.Visible = true;
        }

        private void registrarEstudianteBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoEstudiantePanel.Visible = true;
        }

        private void registrarSedeBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevaSedePanel.Visible = true;
        }

        private void registrarCursoBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoCursoPanel.Visible = true;
        }

        private void registrarNotasBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            ingresoPlataformaPanel.Visible = true;
        }
        private void matricularEstudianteBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            matricularPanel.Visible = true;
        }

        private void asignarCursoProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            asignarProfesorPanel.Visible = true;
        }

        private void mostrarEstudiantesBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            estudiantesPanel.Visible = true;
        }

        private void mostrarProfesoresBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            profesoresPanel.Visible = true;
        }

        private void OcultarPaneles() {
            foreach (Panel panel in Paneles) {
                panel.Visible = false;
            }
        }

        private void AutenticacionPlataformaBtn_Click(object sender, EventArgs e) {
            try {
                string usuario = ObtenerTextoDeEntrada(nombreUsuario.Text, "usuario");
                string contrasenaAcceso = ObtenerTextoDeEntrada(contrasena.Text, "contraseña");

                bool estaAutorizado = gestorNotas.CredencialesSonCorrectas(usuario, contrasenaAcceso);
                if (estaAutorizado) {
                    ingresoPlataformaPanel.Visible = false;
                    CargarInformacionProfesor(usuario);
                    registroNotasPanel.Visible = true;
                } else {
                    throw new UnauthorizedAccessException("Usuario o contraseña incorrecto");
                }

            } catch(Exception ex){
                MostrarAlerta(ex.Message, "Error");
            } finally{
                var controles = new List<Control>() { contrasena, nombreUsuario };
                limpiarCamposTexto(controles);
            }
        }

        private void CargarInformacionProfesor(string usuario) {
            try {
                Profesor profesor = gestorProfesor.ObtenerProfesor(usuario);
                infoProfesor.Text = $"{profesor.Id} - {profesor.Nombre} {profesor.Apellido} {profesor.SegundoApellido}";
                infoSede.Text = profesor.Sede.ToString();
                foreach (Curso curso in profesor.Cursos) {
                    cursosProfesor.Items.Add(curso);
                }
            } catch (Exception ex) {
                MostrarAlerta($"Error al obtener la información del profesor: {ex.Message}", "Error");
            }
        }

        private void guardarSedeBtn_Click(object sender, EventArgs e) {
            try {
                int id = ObtenerNumeroDeEntrada(idSede.Text, "identificación");
                string descripcion = ObtenerTextoDeEntrada(descripcionSede.Text, "descripcion");

                Sede sede = new Sede(id, descripcion);
                gestorSede.AgregarSede(sede);

                var controles = new List<Control>() { idSede, descripcionSede };
                limpiarCamposTexto(controles);

                MostrarAlerta("Sede fue agregada correctamente", "Acción Completada");
            } catch (Exception ex) { 
                MostrarAlerta($"Error al agregar la sede: {ex.Message}", "Error");
            }
        }

        private void guardarCursoBtn_Click(object sender, EventArgs e) {
            try {
                int id = ObtenerNumeroDeEntrada(idCurso.Text, "identificación");
                string nombre = ObtenerTextoDeEntrada(nombreCurso.Text, "nombre");
                string descripcion = ObtenerTextoDeEntrada(descripcionCurso.Text, "descripción");

                Curso curso = new(id, nombre, descripcion);
                gestorCurso.AgregarCurso(curso);

                var controles = new List<Control>() { idCurso, nombreCurso, descripcionCurso };
                limpiarCamposTexto(controles);

                MostrarAlerta("Curso fue agregado correctamente", "Acción Completada");
            } catch (Exception ex) {
                MostrarAlerta($"Error al agregar el curso: {ex.Message}", "Error");
            }
        }

        private void guardarProfesorBtn_Click(object sender, EventArgs e) {
            try {
                int id = ObtenerNumeroDeEntrada(idProfesor.Text, "identificación");
                string nombre = ObtenerTextoDeEntrada(nombreProfesor.Text, "nombre");
                string apellido = ObtenerTextoDeEntrada(apellidoProfesor.Text, "apellido");
                string segundoApellido = ObtenerTextoDeEntrada(segundoApellidoProfesor.Text, "segundo apellido");
                decimal sueldo = ObtenerDecimalDeEntrada(sueldoProfesor.Text, "sueldo");
                string usuario = ObtenerTextoDeEntrada(usuarioProfesor.Text, "usuario");
                string contrasena = ObtenerTextoDeEntrada(contrasenaProfesor.Text, "contraseña");
                object seleccionSede = sedeProfesor.SelectedItem;
                ValidarSeleccionCombobox(seleccionSede, "sede");

                Sede sede = (Sede)seleccionSede;
                AccesoPlataforma plataforma = new(usuario, contrasena);
                Profesor profesor = new(id, nombre, apellido, segundoApellido, sueldo, sede, plataforma);
                gestorProfesor.AgregarProfesor(profesor);

                var controles = new List<Control>() { idProfesor, nombreProfesor, apellidoProfesor, 
                                                      segundoApellidoProfesor, sueldoProfesor, usuarioProfesor, 
                                                      contrasenaProfesor };
                limpiarCamposTexto(controles);
                sedeProfesor.SelectedIndex = -1; //Limpiar selección combobox
               
                MostrarAlerta("Profesor agregado correctamente", "Acción Completada");
            } catch (Exception ex) {
                MostrarAlerta($"Error al agregar el profesor: {ex.Message}", "Error");
            }
        }

        private void guardarEstudianteBtn_Click(object sender, EventArgs e) {
            try {
                int id = ObtenerNumeroDeEntrada(idEstudiante.Text, "identificación");
                string nombre = ObtenerTextoDeEntrada(nombreEstudiante.Text, "nombre");
                string apellido = ObtenerTextoDeEntrada(apellidoEstudiante.Text, "apellido");
                string segundoApellido = ObtenerTextoDeEntrada(segundoApellidoEstudiante.Text, "segundo apellido");
                DateTime fechaNacimiento = fechaNacimientoEstudiante.Value;
                object seleccionSede = sedeEstudiante.SelectedItem;
                object seleccionGenero = generoEstudiante.SelectedItem;

                ValidarFechaNacimiento(fechaNacimiento);
                ValidarSeleccionCombobox(seleccionSede, "sede");
                ValidarSeleccionCombobox(seleccionGenero, "género");

                Sede sede = (Sede)seleccionSede;
                char genero = seleccionGenero.ToString().Equals("Masculino") ? 'M' : 'F';
                Estudiante estudiante = new(id, nombre, apellido, segundoApellido, fechaNacimiento, genero, sede);

                gestorEstudiante.AgregarEstudiante(estudiante);
                

                var controles = new List<Control>() { idEstudiante, nombreEstudiante, apellidoEstudiante,
                                                      segundoApellidoEstudiante, fechaNacimientoEstudiante };
                limpiarCamposTexto(controles);
                sedeEstudiante.SelectedIndex = -1; //Limpiar selección combobox
                generoEstudiante.SelectedIndex = -1; 

                MostrarAlerta("Estudiante agregado correctamente", "Acción Completada");
            } catch (Exception ex) {
                MostrarAlerta($"Error al agregar el estudiante: {ex.Message}", "Error");
            }
        }

        private void limpiarCamposTexto(List<Control> controles) {
            foreach (Control control in controles) {
                control.Text = "";
            }
        }

        private void MostrarAlerta(string mensaje, string titulo) {
            MessageBox.Show(mensaje, titulo);
        }


        #region Validaciones
        /// En esta región se valida el número ingresado por el usuario. Si hay un error en el cast explícito, la excepción resultante es arrojada 
        /// para que sea manejada por el método que consume 

        private void ValidarSeleccionCombobox(object objSeleccionado, string nombreCampo) {
            if (objSeleccionado == null) {
                throw new ParametroVacíoException($"Por favor complete todos los campos. El campo {nombreCampo} es requerido");
            }
        }

        private int ObtenerNumeroDeEntrada(string entradaNumerica, string nombreCampo) {
            ObtenerTextoDeEntrada(entradaNumerica, nombreCampo); //En caso que el campo esté vacío
            if (!Int32.TryParse(entradaNumerica, out int numero)) {
                throw new NoEsNumeroException($"Por favor digite únicamente números en el campo de {nombreCampo}");
            }
            if (numero < 0) {
                throw new NumeroNegativoException($"Por favor digite únicamente números positivos en el campo de {nombreCampo}");
            }
            return numero;
        }

        private decimal ObtenerDecimalDeEntrada(string entradaDecimal, string nombreCampo) {
            ObtenerTextoDeEntrada(entradaDecimal, nombreCampo); //En caso que el campo esté vacío
            if (!Decimal.TryParse(entradaDecimal, out decimal number)) {
                throw new NoEsNumeroException($"Por favor digite únicamente números en el campo de {nombreCampo}");
            }
            return number;
        }

        private string ObtenerTextoDeEntrada(string texto, string nombreCampo) {
            return Vacio(texto) ? throw new ParametroVacíoException($"Por favor complete todos los campos. El campo {nombreCampo} es requerido") : texto;
        }

        private void ValidarFechaNacimiento(DateTime entradaFecha) {
            //Confirmar que la fecha no sea posterior a le fecha actual
            if (entradaFecha > DateTime.Now || entradaFecha == default) {
                throw new FechaIncorrectaException("Por favor seleccione una fecha válida");
            }
        }

        private char ObtenerCaracterDeEntrada(string entradaCaracter, char[] valoresPermitidos, string nombreCampo) {
            ObtenerTextoDeEntrada(entradaCaracter, nombreCampo); //En caso que el campo esté vacío
            entradaCaracter = entradaCaracter.ToUpper();
            if (!char.TryParse(entradaCaracter, out char character) || !Array.Exists(valoresPermitidos, c => c == character)) {
                throw new NoEsCaracterException($"Por favor digite un caracter dentro de las opciones mostradas en el campo de {nombreCampo}");
            }

            return character;
        }

        private bool Vacio(string inputText) {
            return "".Equals(inputText);
        }


        #endregion

        private void cursosProfesor_SelectedIndexChanged(object sender, EventArgs e) {
            MessageBox.Show("BOOMBOX");
        }
    }
}
