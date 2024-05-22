using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace SieteYMedia.Models;
public class Baraja : IBaraja
{
    ObservableCollection<Naipe> _mazo;
    public ObservableCollection<Naipe> Mazo 
    {
        get => _mazo;
        set => _mazo = value;
    }
    public int NumNaipes
    {
        get => _mazo.Count;
    }
    public Baraja()
    {
        int figura;
        int numPalos = Enum.GetValues(typeof(PaloSP)).Length; //4
        int nFiguras = Enum.GetValues(typeof(Figura)).Length; //12
        _mazo = new();

        for (int p = 0; p < numPalos; p++)
        {
            for (int f = 1; f <= nFiguras; f++)
            {
                figura = (f > 7) ? f + 2 : f;
                _mazo.Add(new Naipe((PaloSP)p, (Figura)figura));
            }

        }
    }
    public void Barajar()
    {
        int pos;
        var rnd = new Random();
        List<Naipe> tmp = new(_mazo);
        _mazo.Clear(); //vaciamos y rellenamos aleatoriamente

        while (tmp.Count != 0)
        {
            pos = rnd.Next(tmp.Count);
            _mazo.Add(tmp[pos]);
            tmp.RemoveAt(pos);
        }
    }
    public void Reiniciar()
    {
        int figura;
        int numPalos = Enum.GetValues(typeof(PaloSP)).Length; //4
        int nFiguras = Enum.GetValues(typeof(Figura)).Length; //12

        for (int p = 0; p < numPalos; p++)
        {
            for (int f = 1; f <= nFiguras; f++)
            {
                figura = (f > 7) ? f + 2 : f;
                _mazo.Add(new Naipe((PaloSP)p, (Figura)figura));
            }

        }
    }
    public INaipe? ExtraerNaipe()
    {
        Naipe naipe;
        naipe = _mazo[0];
        _mazo.RemoveAt(0);
        return naipe;
    }
    public override string ToString()
    {
        string cadena = "Baraja: ";
        foreach (Naipe naipe in _mazo)
            cadena += string.Format($"{naipe} ");
        return cadena;
    }
    public bool Contiene(Naipe naipe)
    {
        return _mazo.Contains(naipe);
    }
}