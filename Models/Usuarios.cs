namespace Models;

public class Usuarios{

    public int IdUsuario{get; set;}
    public string Nombre {get; set;}
    public string Email {get; set;}    
    public string Password {get; set;}
    public DateTime FechaRegistro {get; set;} //¿Tiene que ser DateTime? o solo era porque era en C# y al tener BBDD hay que cambiarlo

    public Usuarios(int idUsuario, string nombre, string email, string password, DateTime fechaRegistro){

        IdUsuario = idUsuario;
        Nombre = nombre;
        Email = email;
        Password = password;
        FechaRegistro = fechaRegistro;

    }

    public Usuarios(){}

}
