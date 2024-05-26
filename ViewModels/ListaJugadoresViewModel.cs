using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using SieteYMedia.Models;

namespace SieteYMedia.ViewModels;

public class ListaJugadoresViewModel : ViewModelBase
{
    #region Campos
    private ListaJugadores _lista = new();
    private Jugador? _jugadorSeleccionado;
    #endregion
    #region Propiedades
    public ObservableCollection<Jugador> Lista => _lista.Lista;
    public ReactiveCommand<Unit, ViewModelBase> Atrás { get; }
    public ReactiveCommand<Unit,ViewModelBase> IrCrearUsuario{get;}
    public Jugador? JugadorSeleccionado
    {
        get => _jugadorSeleccionado;
        set => this.RaiseAndSetIfChanged(ref _jugadorSeleccionado,value);
    }
    #endregion

    #region Constructores
    public ListaJugadoresViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _lista.ObtenerJugadores();
        Atrás = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.MenuInstance);
        IrCrearUsuario = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadCreateUserInstance);
        JugadorSeleccionado = new();
    }
    public void EliminarJugador()
    {
        if(JugadorSeleccionado != null && !string.IsNullOrWhiteSpace(JugadorSeleccionado.Nombre))
        {
            var jug = _lista.Lista.First((x) => x.ID == JugadorSeleccionado.ID && x.Nombre == JugadorSeleccionado.Nombre);
            _lista.Lista.Remove(jug);
            _lista.SerializarJSON();
        }
    }
    public void CrearUsuario(Jugador jug)
    {
        jug.ID = _lista.NumJugadores;
        _lista.Lista.Add(jug);
        _lista.SerializarJSON();
    }
    public void SerializarJSON() => _lista.SerializarJSON();
    #endregion
}