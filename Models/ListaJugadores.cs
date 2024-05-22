using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
namespace SieteYMedia.Models;
public class ListaJugadores
{
    #region Campos
    ObservableCollection<Jugador> _listaJugadores;
    private string _fichero = Path.Combine(Environment.CurrentDirectory,"Datos","jugadores.json");
    #endregion

    #region Propiedades
    public ObservableCollection<Jugador> Lista
    {
        get => _listaJugadores;
        set => _listaJugadores = value;
    }
    public int NumJugadores => _listaJugadores.Count;
    #endregion
    #region Constructores
    public ListaJugadores()
    {
        _listaJugadores = new();
    }
    #endregion

    #region Metodos
    public ObservableCollection<Jugador> ObtenerJugadores()
    {
        if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory,"Datos")))
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory,"Datos"));
        }
        if(File.Exists(_fichero))
        {
            using(StreamReader sr = File.OpenText(_fichero))
            {
                _listaJugadores = JsonConvert.DeserializeObject<ObservableCollection<Jugador>>(sr.ReadToEnd())!;
            }
        }
        else
            File.Create(_fichero);
        return _listaJugadores;
    }
    public void SerializarJSON()
    {
        var entrada = JsonConvert.SerializeObject(_listaJugadores,Formatting.Indented);
        File.WriteAllText(_fichero,entrada);
    }
    #endregion
}