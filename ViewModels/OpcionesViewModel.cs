using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using SieteYMedia.Models;

namespace SieteYMedia.ViewModels;

public class OpcionesViewModel : ViewModelBase
{
    ListaJugadores _jugadores = new();

    public ObservableCollection<Jugador> Lista => _jugadores.Lista;
    public string Titulo => "Estadísticas";
    public ReactiveCommand<Unit,ViewModelBase> AtrasCommand {get;}
    public OpcionesViewModel(MainWindowViewModel mainWindowViewModel)
    {
        AtrasCommand = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.MenuInstance);
    }
}