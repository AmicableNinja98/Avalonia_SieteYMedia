using System.Reactive;
using ReactiveUI;
using SieteYMedia.Models;

namespace SieteYMedia.ViewModels;
public class CrearUsuarioViewModel : ViewModelBase
{
    #region Campos
    string _nombre = string.Empty;
    #endregion

    #region Propiedades
    public string Nombre
    {
        get => _nombre;
        set => this.RaiseAndSetIfChanged(ref _nombre,value);
    }
    public ReactiveCommand<Unit,Jugador> OkCommand { get;}
    public ReactiveCommand<Unit,ViewModelBase> CancelCommand { get;}
    #endregion

    #region Constructores
    public CrearUsuarioViewModel(MainWindowViewModel mainWindowViewModel)
    {
        var IsValidObservable = this.WhenAnyValue(x => x.Nombre,x => !string.IsNullOrWhiteSpace(x));
        OkCommand = ReactiveCommand.Create(() => new Jugador{Nombre = Nombre},IsValidObservable);
        mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance;
        CancelCommand = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance);
    }
    #endregion
}