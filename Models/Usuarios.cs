namespace Models;

public class Usuarios{

    public int IdUsuario{get; set;}
    public string  DNI {get; set;}
    public string Nombre {get; set;}
    public string Apellidos {get; set;}
    public string Email {get; set;}    
    public string Contrasenia {get; set;}
    public DateTime FechaRegistro {get; set;}

    public Usuarios(int idUsuario, string dni, string nombre, string apellidos, string email, string contrasenia, DateTime fechaRegistro){

        IdUsuario = idUsuario;
        DNI = dni;
        Nombre = nombre;
        Apellidos = apellidos;
        Email = email;
        Contrasenia = contrasenia;
        FechaRegistro = fechaRegistro;

    }

    public Usuarios(){}

}
