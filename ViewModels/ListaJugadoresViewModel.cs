using System.Collections.ObjectModel;
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
        JugadorSeleccionado = new();
    }
    public void SerializarJSON() => _lista.SerializarJSON();
    #endregion
}