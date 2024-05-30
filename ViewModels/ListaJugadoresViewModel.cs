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
    private Jugador _jugadorSeleccionado = new();
    #endregion
    #region Propiedades
    public ObservableCollection<Jugador> Lista => _lista.Lista;
    public ReactiveCommand<Unit, ViewModelBase> Atrás { get; }
    public ReactiveCommand<Unit, ViewModelBase> IrCrearUsuario { get; }
    public ReactiveCommand<Unit, Unit> IniciarPartida { get; }
    public ReactiveCommand<Unit, Unit> EliminarJugador { get; }
    public Jugador JugadorSeleccionado
    {
        get => _jugadorSeleccionado;
        set => this.RaiseAndSetIfChanged(ref _jugadorSeleccionado, value);
    }
    #endregion

    #region Constructores
    public ListaJugadoresViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _lista.ObtenerJugadores();
        Atrás = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.MenuInstance);
        IrCrearUsuario = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadCreateUserInstance);
        var IsValidObservable = this.WhenAnyValue((x) => x.JugadorSeleccionado.Nombre, x => !string.IsNullOrEmpty(x));
        IniciarPartida = ReactiveCommand.Create(() =>
        {
            if (JugadorSeleccionado != null && !string.IsNullOrWhiteSpace(JugadorSeleccionado.Nombre))
            {
                mainWindowViewModel.LoadGameInstance.Jugador = _jugadorSeleccionado;
                mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadGameInstance;
            }
        }, IsValidObservable);
        EliminarJugador = ReactiveCommand.Create(() =>
        {
            if (JugadorSeleccionado != null && !string.IsNullOrWhiteSpace(JugadorSeleccionado.Nombre))
            {
                var jug = _lista.Lista.First((x) => x.ID == JugadorSeleccionado.ID && x.Nombre == JugadorSeleccionado.Nombre);
                _lista.Lista.Remove(jug);
                _lista.SerializarJSON();
                mainWindowViewModel.LoadOptionsInstance.Actualizar();
            }
        });
    }
    public void CrearUsuario(Jugador jug)
    {
        int id = 0;
        bool fin = false;
        while (!fin)
        {
            if (!Lista.Any((x) => x.ID == id))
            {
                jug.ID = id;
                Lista.Add(jug);
                SerializarJSON();
                fin = true;
            }
            else
                id++;
        }
    }
    public void SerializarJSON() => _lista.SerializarJSON();
    #endregion
}