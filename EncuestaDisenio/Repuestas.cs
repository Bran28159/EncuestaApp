using EncuestaDisenio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("respuestas")]
public class Respuesta
{
    [Key]
    [Column("numero")]
    public int Numero { get; set; }

    [Column("I")]
    public string Nombre { get; set; }

    [Column("II")]
    public string Apellido { get; set; }

    [Column("III")]
    public int SexoClave { get; set; }
    public iii_sexo? Sexo { get; set; }

    [Column("IV")]
    public string Identidad { get; set; }

    [Column("V")]
    public int DepartamentoClave { get; set; }
    public v_departamento? Departamento { get; set; }

    [Column("VI")]
    public int CiudadClave { get; set; }
    public vi_ciudad? Ciudad { get; set; }

    [Column("VII")]
    public int FacultadClave { get; set; }
    public vii_facultad? Facultad { get; set; }

    [Column("VIII")]
    public int CarreraClave { get; set; }
    public viii_carrera? Carrera { get; set; }

    [Column("IX")]
    public int PreguntaIX { get; set; }

    [Column("X")]
    public int MatriculaClave { get; set; }
    public x_matricula? Matricula { get; set; }

    [Column("XI")]
    public int BecadoClave { get; set; }
    public xi_becado? Becado { get; set; }

    [Column("XII")]
    public int XiiClave { get; set; }
    public xii? Xii { get; set; }

    [Column("XIII")]
    public int XiiiClave { get; set; }
    public xiii? Xiii { get; set; }

    [Column("XIV")]
    public int XivClave { get; set; }
    public xiv? Xiv { get; set; }

    [Column("XV")]
    public int XvClave { get; set; }
    public xv? Xv { get; set; }

    [Column("XVI")]
    public int XviClave { get; set; }
    public xvi? Xvi { get; set; }

    [Column("XVII")]
    public int XviiClave { get; set; }
    public xvii? Xvii { get; set; }
}
