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
    public OpcionesViewModel LoadOptionsInstance {get;}
    #endregion
    #region Constructores
    public MainWindowViewModel()
    {
        MenuInstance = new(this);
        LoadCreateUserInstance = new(this);
        LoadUserInstance = new(this);
        LoadGameInstance = new(this);
        LoadOptionsInstance = new(this);
        _contenidoViewModel = MenuInstance;
    }
    #endregion
}