using System.Reactive.Linq;
using ReactiveUI;
using SieteYMedia.Models;
using System;
using System.Linq;
namespace SieteYMedia.ViewModels;
public class MainWindowViewModel : ViewModelBase
{
    #region Campos
    ViewModelBase _contenidoViewModel;
    ListaJugadores _jugadores = new();
    #endregion

    #region Propiedades
    public ViewModelBase ContenidoViewModel
    {
        get => _contenidoViewModel;
        set => this.RaiseAndSetIfChanged(ref _contenidoViewModel,value);
    }
    public MenuViewModel MenuInstance {get;}
    public ListaJugadoresViewModel LoadUserInstance {get;}
    public CrearUsuarioViewModel LoadCreateUserInstance {get;}
    public GameViewModel LoadGameInstance {get;}
    #endregion
    #region Constructores
    public MainWindowViewModel()
    {
        MenuInstance = new(this);
        LoadCreateUserInstance = new(this);
        LoadUserInstance = new(this);
        LoadGameInstance = new(this);
        _jugadores.ObtenerJugadores();
        _contenidoViewModel = MenuInstance;
    }
    #endregion

    #region Metodos
    /*public void AñadirUsuario()
    {
        Observable.Merge(
            LoadCreateUserInstance.OkCommand,LoadCreateUserInstance.CancelCommand.Select(_ => (Jugador?)null)).Take(1).Subscribe(nuevoUser =>
            {
                if(nuevoUser != null)
                {
                    nuevoUser.ID = _jugadores.NumJugadores;
                    _jugadores.Lista.Add(nuevoUser);
                    _jugadores.SerializarJSON();
                }
                ContenidoViewModel = LoadUserInstance;
            });
        ContenidoViewModel = LoadCreateUserInstance;
    }*/
    public void SeleccionarJugador()
    {
        if(LoadUserInstance.JugadorSeleccionado != null && !string.IsNullOrWhiteSpace(LoadUserInstance.JugadorSeleccionado.Nombre))
        {
            _jugadores.ObtenerJugadores();
            var jug = _jugadores.Lista.First((x) => x.ID == LoadUserInstance.JugadorSeleccionado.ID && x.Nombre == LoadUserInstance.JugadorSeleccionado.Nombre);
            LoadGameInstance.Jugador = jug;
            ContenidoViewModel = LoadGameInstance;
        }
    }
    #endregion
}