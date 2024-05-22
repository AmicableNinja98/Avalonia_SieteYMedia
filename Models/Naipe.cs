namespace SieteYMedia.Models;
using System;
public class Naipe:INaipe,IEquatable<Naipe> 
{
    public PaloSP Palo {get;} //Autopropiedad
    public Figura Peso{get;}
    public string RutaImagen {get;set;}

    public Naipe(PaloSP elPalo,Figura elPeso)
    {
        Palo = elPalo;
        Peso = elPeso;
        RutaImagen = $"avares://SieteYMedia/Assets/BarajaEspañola/{(int)Peso}{Palo}.png";
    }
    public Naipe()
    {
        var alea = new Random();
        Palo = (PaloSP)alea.Next(Enum.GetValues(typeof(PaloSP)).Length);
        Peso = (Figura)alea.Next(Enum.GetValues(typeof(Figura)).Length);
        RutaImagen = $"avares://SieteYMedia/Assets/BarajaEspañola{(int)Peso}{Palo}.png";
    }
    public override string ToString()
    {
        return string.Format($"[{Peso} de {Palo}]");
    }

    public bool Equals(Naipe? other)
    {
        if (other != null)
            return Palo == other.Palo && Peso == other.Peso;
        else
            return false;
    }
}