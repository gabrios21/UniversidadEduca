using Gestores_UniversidadEduca;
using Modelos_UniversidadEduca.Excepciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UniversidadEduca_Tarea1.Models;

namespace GUI_UniversidadEduca {
    public partial class FormularioPrincipal : Form {

        private List<Panel> Paneles;
        private List<string> Generos => new() { "Masculino", "Femenino" };
        private GestorSede GestorSede => new();
        private GestorCurso GestorCurso => new();
        private GestorProfesor GestorProfesor => new();
        private GestorEstudiante GestorEstudiante => new();
        private GestorNotas GestorNotas => new();

        private (List<Estudiante>,int) EstudiantesEnCurso { get; set; }
        public FormularioPrincipal() {
            InitializeComponent();
            EstudiantesEnCurso = (new List<Estudiante>(), 0);
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

            //Cargar generos en combobox
            foreach (string genero in Generos) {
                generoEstudiante.Items.Add(genero);
            }
        }

        private void registrarProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoProfesorPanel.Visible = true;
            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                if (!sedeProfesor.Items.Contains(sede)) {
                    sedeProfesor.Items.Add(sede);
                }
            }
        }

        private void registrarEstudianteBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            nuevoEstudiantePanel.Visible = true;
            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                if (!sedeEstudiante.Items.Contains(sede)) {
                    sedeEstudiante.Items.Add(sede);
                }
            }
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

            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes)
            {
                if (!sedeMatricula.Items.Contains(sede))
                {
                    sedeMatricula.Items.Add(sede);
                }
            }
        }

        private void asignarCursoProfesorBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            asignarProfesorPanel.Visible = true;

            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                if (!asignacionSedeProfesor.Items.Contains(sede)) {
                    asignacionSedeProfesor.Items.Add(sede);
                }
            }
        }

        private void mostrarEstudiantesBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            estudiantesPanel.Visible = true;

            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                if (!seleccionSedeEstudiante.Items.Contains(sede)) {
                    seleccionSedeEstudiante.Items.Add(sede);
                }
            }

        }

        private void mostrarProfesoresBtn_Click(object sender, EventArgs e) {
            OcultarPaneles();
            profesoresPanel.Visible = true;

            //Cargar sedes en combobox
            var listaSedes = GestorSede.ObtenerListaSedes();
            foreach (Sede sede in listaSedes) {
                if (!seleccionSedeProf.Items.Contains(sede)) {
                    seleccionSedeProf.Items.Add(sede);
                }
            }
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

                bool estaAutorizado = GestorNotas.CredencialesSonCorrectas(usuario, contrasenaAcceso);
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
                Profesor profesor = GestorProfesor.ObtenerProfesor(usuario);
                infoProfesor.Text = $"{profesor.Id} - {profesor.Nombre} {profesor.Apellido} {profesor.SegundoApellido}";
                infoSede.Text = profesor.Sede.ToString();
                foreach (Curso curso in profesor.Cursos) {
                    if (!cursosProfesor.Items.Contains(curso)) { //Solo se agrega si es un curso nuevo
                        cursosProfesor.Items.Add(curso);
                    }
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
                GestorSede.AgregarSede(sede);

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
                GestorCurso.AgregarCurso(curso);

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
                object seleccionSede = this.sedeProfesor.SelectedItem;
                ValidarSeleccionCombobox(seleccionSede, "sede");

                Sede sedeSeleccionadaProfesor = (Sede)seleccionSede;
                AccesoPlataforma plataforma = new(usuario, contrasena);
                Profesor profesor = new(id, nombre, apellido, segundoApellido, sueldo, sedeSeleccionadaProfesor, plataforma);
                GestorProfesor.AgregarProfesor(profesor);

                var controles = new List<Control>() { idProfesor, nombreProfesor, apellidoProfesor, 
                                                      segundoApellidoProfesor, sueldoProfesor, usuarioProfesor, 
                                                      contrasenaProfesor };
                limpiarCamposTexto(controles);
                this.sedeProfesor.SelectedIndex = -1; //Limpiar selección combobox
               
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

                Sede sedeSeleccionada = (Sede)seleccionSede;
                char genero = seleccionGenero.ToString().Equals("Masculino") ? 'M' : 'F';
                Estudiante estudiante = new(id, nombre, apellido, segundoApellido, fechaNacimiento, genero, sedeSeleccionada);

                GestorEstudiante.AgregarEstudiante(estudiante);
               
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
        
        private void cursosProfesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Crear fuente de datos
            var origenDatos = new BindingSource();
            //Obtener datos
            var cursoSeleccionado = (Curso)cursosProfesor.SelectedItem;
            EstudiantesEnCurso = (GestorEstudiante.ObtenerEstudiantesEnCurso(cursoSeleccionado.Id), cursoSeleccionado.Id);
            //Cargar datos en Tabla(DataGridView)
            origenDatos.DataSource = EstudiantesEnCurso.Item1;
            notasCursos.DataSource = origenDatos;
            AjustarOrdenColumnas();
            ConfigurarEdicionColumnas();
        }

        private void asignacionSedeProfesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            asignacionProfesor.SelectedIndex = -1;
            asignacionProfesor.Items.Clear();

            Sede sedeSeleccionada = (Sede)asignacionSedeProfesor.SelectedItem;

            if (sedeSeleccionada != null) {
                //Cargar profesores en comboBox
                var listaProfesores = GestorProfesor.ObtenerProfesoresPorSede(sedeSeleccionada.Id);
                foreach (Profesor profesor in listaProfesores)
                {
                    if (!asignacionProfesor.Items.Contains(profesor))
                    {
                        asignacionProfesor.Items.Add(profesor);
                    }
                }
            }
        }

        private void asignacionProfesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cargar cursos en comboBox
            var listaCursos = GestorCurso.ObtenerCursos();
            foreach (Curso curso in listaCursos) {
                if (!asignacionProfesorCurso.Items.Contains(curso))
                {
                    asignacionProfesorCurso.Items.Add(curso);
                }
            }
        }

        private void asignarBtn_Click(object sender, EventArgs e) {
            try
            {
                object seleccionSede = asignacionSedeProfesor.SelectedItem;
                object seleccionProfesor = asignacionProfesor.SelectedItem;
                object seleccionCurso = asignacionProfesorCurso.SelectedItem;

                ValidarSeleccionCombobox(seleccionSede, "sede");
                ValidarSeleccionCombobox(seleccionProfesor, "profesor");
                ValidarSeleccionCombobox(seleccionCurso, "curso");

                Profesor profesorSeleccionado = (Profesor)seleccionProfesor;
                Curso cursoSeleccionado = (Curso)seleccionCurso;

                GestorProfesor.AsignarCurso(cursoSeleccionado, profesorSeleccionado);
                MostrarAlerta($"El curso fue asignado exitosamente", "Acción Completada");

                asignacionSedeProfesor.SelectedIndex = -1;
                asignacionProfesor.SelectedIndex = -1;
                asignacionProfesorCurso.SelectedIndex = -1;
            }
            catch (Exception ex) {
                MostrarAlerta($"Error al asignar curso a profesor: {ex.Message}", "Error");
            }
        }

        private void sedeMatricula_SelectedIndexChanged(object sender, EventArgs e) {
            estudianteMatricula.SelectedIndex = -1;
            estudianteMatricula.Items.Clear();

            Sede sedeSeleccionada = (Sede)sedeMatricula.SelectedItem;

            if (sedeSeleccionada != null)
            {
                //Cargar estudiantes en comboBox
                var listaEstudiantes = GestorEstudiante.ObtenerEstudiantesPorSede(sedeSeleccionada.Id);
                foreach (Estudiante estudiante in listaEstudiantes)
                {
                    if (!estudianteMatricula.Items.Contains(estudiante)) {
                        estudianteMatricula.Items.Add(estudiante);
                    }
                }
            }
        }

        private void estudianteMatricula_SelectedIndexChanged(object sender, EventArgs e) {
            //Cargar cursos en comboBox
            var listaCursos = GestorCurso.ObtenerCursos();
            foreach (Curso curso in listaCursos)
            {
                if (!cursoMatricula.Items.Contains(curso))
                {
                    cursoMatricula.Items.Add(curso);
                }
            }
        }

        private void matricularBtn_Click(object sender, EventArgs e) {
            try {
                object seleccionSede = sedeMatricula.SelectedItem;
                object seleccionEstudiante = estudianteMatricula.SelectedItem;
                object seleccionCurso = cursoMatricula.SelectedItem;

                ValidarSeleccionCombobox(seleccionSede, "sede");
                ValidarSeleccionCombobox(seleccionEstudiante, "estudiante");
                ValidarSeleccionCombobox(seleccionCurso, "curso");

                Estudiante estudianteSeleccionado = (Estudiante)seleccionEstudiante;
                Curso cursoSeleccionado = (Curso)seleccionCurso;

                GestorEstudiante.MatricularCurso(cursoSeleccionado, estudianteSeleccionado);
                MostrarAlerta($"El estudiante fue matriculado exitosamente", "Acción Completada");

                sedeMatricula.SelectedIndex = -1;
                estudianteMatricula.SelectedIndex = -1;
                cursoMatricula.SelectedIndex = -1;
            }
            catch (Exception ex) {
                MostrarAlerta($"Error al matricular estudiante: {ex.Message}", "Error");
            }
        }

        private void seleccionSedeEstudiante_SelectedIndexChanged(object sender, EventArgs e) {
            seleccionEstudiante.SelectedIndex = -1;
            seleccionEstudiante.Items.Clear();

            var controles = new List<Control>() { idEstudianteInfo, nombreEstudianteInfo, apellidoEstudianteInfo,
                                                      segApellidoEstudianteInfo, fechaNacimientoEstudianteInfo,  generoEstudianteInfo};
            limpiarCamposTexto(controles);
            infoCursos.Rows.Clear();

            Sede sedeSeleccionada = (Sede)seleccionSedeEstudiante.SelectedItem;

            if (sedeSeleccionada != null) {
                //Cargar estudiantes en comboBox
                var listaEstudiantes = GestorEstudiante.ObtenerEstudiantesPorSede(sedeSeleccionada.Id);
                foreach (Estudiante estudiante in listaEstudiantes) {
                    if (!seleccionEstudiante.Items.Contains(estudiante)) {
                        seleccionEstudiante.Items.Add(estudiante);
                    }
                }
            }
        }

        private void seleccionEstudiante_SelectedIndexChanged(object sender, EventArgs e) {
            object estudianteSeleccionado = seleccionEstudiante.SelectedItem;
            Estudiante estudiante = (Estudiante)estudianteSeleccionado;

            if (estudiante != null) {
                //Cargar información estudiante
                idEstudianteInfo.Text = estudiante.Id.ToString();
                nombreEstudianteInfo.Text = estudiante.Nombre;
                apellidoEstudianteInfo.Text = estudiante.Apellido;
                segApellidoEstudianteInfo.Text = estudiante.SegundoApellido;
                fechaNacimientoEstudianteInfo.Value = estudiante.FechaNacimiento;
                generoEstudianteInfo.Text = estudiante.Genero.Equals('M') ? Generos[0] : Generos[1];

                //Cargar cursos

                //Crear fuente de datos
                var origenDatos = new BindingSource();

                //Cargar datos en Tabla(DataGridView)
                origenDatos.DataSource = estudiante.Cursos;
                infoCursos.DataSource = origenDatos;
            }
        }

        private void guardarNotasBtn_Click(object sender, EventArgs e) {
            try
            {
                ValidarNotasEditadas();
                GestorNotas.ActualizarNotas(EstudiantesEnCurso);
                MostrarAlerta("Las notas fueron actualizadas correctamente", "Acción Completada");
            }
            catch (InvalidCastException)
            {
                MostrarAlerta($"Error al actualizar las notas: Por favor digite únicamente números en el campo de notas", "Error");
            }
            catch (Exception ex)
            {
                MostrarAlerta($"Error al actualizar las notas: {ex.Message}", "Error");
            }
        }

        private void seleccionSedeProf_SelectedIndexChanged(object sender, EventArgs e) {
            profesorInfo.SelectedIndex = -1;
            profesorInfo.Items.Clear();
            var controles = new List<Control>() { idProfesorInfo, nombreProfesorInfo, apellidoProfesorInfo,
                                                      segApellidoProfesorInfo, sueldoProfesorInfo,  usuarioProfesorInfo, contrasenaProfesorInfo};
            limpiarCamposTexto(controles);
            cursosProfesorInfo.Rows.Clear();

            Sede sedeSeleccionada = (Sede)seleccionSedeProf.SelectedItem;

            if (sedeSeleccionada != null) {
                //Cargar profesores en comboBox
                var listaProfesores = GestorProfesor.ObtenerProfesoresPorSede(sedeSeleccionada.Id);
                foreach (Profesor profesor in listaProfesores)
                {
                    if (!profesorInfo.Items.Contains(profesor)) {
                        profesorInfo.Items.Add(profesor);
                    }
                }
            }
        }

        private void profesorInfo_SelectedIndexChanged(object sender, EventArgs e) {
            object profesorSeleccionado = profesorInfo.SelectedItem;
            Profesor profesor = (Profesor)profesorSeleccionado;

            if (profesor != null) {
                //Cargar información estudiante
                idProfesorInfo.Text = profesor.Id.ToString();
                nombreProfesorInfo.Text = profesor.Nombre;
                apellidoProfesorInfo.Text = profesor.Apellido;
                segApellidoProfesorInfo.Text = profesor.SegundoApellido;
                sueldoProfesorInfo.Text = profesor.Sueldo.ToString();
                usuarioProfesorInfo.Text = profesor.Plataforma.Usuario;
                contrasenaProfesorInfo.Text = profesor.Plataforma.Contrasena;

                //Cargar cursos

                //Crear fuente de datos
                var origenDatos = new BindingSource();

                //Cargar datos en Tabla(DataGridView)
                origenDatos.DataSource = profesor.Cursos;
                cursosProfesorInfo.DataSource = origenDatos;
                cursosProfesorInfo.Columns["Nota"].Visible = false;
            }
        }

        private void ConfigurarEdicionColumnas()
        {
            notasCursos.Columns["Id"].ReadOnly = true;
            notasCursos.Columns["Nombre"].ReadOnly = true;
            notasCursos.Columns["Apellido"].ReadOnly = true;
            notasCursos.Columns["SegundoApellido"].ReadOnly = true;
            notasCursos.Columns["Nota"].ReadOnly = false;
            notasCursos.Columns["FechaNacimiento"].ReadOnly = true;
            notasCursos.Columns["Genero"].ReadOnly = true;
            notasCursos.Columns["Sede"].ReadOnly = true;
        }

        private void AjustarOrdenColumnas()
        {
            notasCursos.Columns["Id"].DisplayIndex = 0;
            notasCursos.Columns["Nombre"].DisplayIndex = 1;
            notasCursos.Columns["Apellido"].DisplayIndex = 2;
            notasCursos.Columns["SegundoApellido"].DisplayIndex = 3;
            notasCursos.Columns["Nota"].DisplayIndex = 4;
            notasCursos.Columns["FechaNacimiento"].DisplayIndex = 5;
            notasCursos.Columns["Genero"].DisplayIndex = 6;
            notasCursos.Columns["Sede"].DisplayIndex = 7;
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

        private void ValidarNotasEditadas()
        {
            var indiceColumnaNotas = 3;
            foreach (DataGridViewRow fila in notasCursos.Rows) {
                var valorEnCelda = fila.Cells[indiceColumnaNotas].Value;
                var nuevaNota = (int)valorEnCelda;
                if (nuevaNota < 0)
                {
                    throw new NumeroNegativoException($"Por favor digite únicamente números positivos en el campo de notas");
                }
            }
        }

        #endregion

        private void notasCursos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MostrarAlerta($"Error al actualizar las notas. Por favor digite únicamente números en el campo de notas", "Error");
        }
    }
}
