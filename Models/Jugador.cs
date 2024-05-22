using System.Collections.ObjectModel;
namespace SieteYMedia.Models;
public class Jugador
{
    #region Campos
    ObservableCollection<Naipe> _mazo;
    #endregion

    #region Propiedades
    public ObservableCollection<Naipe> Mazo 
    {
        get => _mazo;
        set => _mazo = value;
    }
    static float _puntos;
    public int ID {get; set;}
    public string Nombre{ get; set; } = string.Empty;
    public int PartidasTotales { get; set; }
    public int PartidasGanadas { get; set; }
    public int PartidasPerdidas { get; set; }
    public float Puntos => _puntos;
    #endregion
    public Jugador()
    {
        _mazo = new();
    }
    #region Constructores
    #endregion

    #region Metodos
    public void PedirCarta(INaipe naipe)
    {
        if(naipe != null)
            _mazo.Add((Naipe)naipe);
    }
    public void SumarPuntos(INaipe naipe)
    {
        if((int)naipe.Peso > 7) 
            _puntos += 0.5f;
        else
            _puntos += (int)naipe.Peso;
    }
    #endregion
}