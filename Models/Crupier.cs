using System.Collections.Generic;
using System.Collections.ObjectModel;
using SieteYMedia.Models;

class Crupier
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
    #endregion

    #region Constructores
    public Crupier()
    {
        _mazo = new();
    }
    #endregion

    #region Metodos
    public void PedirCarta(INaipe naipe)
    {
        if(naipe != null)
            _mazo.Add((Naipe)naipe);
    }
    #endregion
}