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
    public ReactiveCommand<Unit,Unit> OkCommand { get;}
    public ReactiveCommand<Unit,ViewModelBase> CancelCommand { get;}
    #endregion

    #region Constructores
    public CrearUsuarioViewModel(MainWindowViewModel mainWindowViewModel)
    {
        var IsValidObservable = this.WhenAnyValue(x => x.Nombre,x => !string.IsNullOrWhiteSpace(x));
        OkCommand = ReactiveCommand.Create(() => 
        {
            var jug = new Jugador();
            jug.Nombre = this.Nombre;
            mainWindowViewModel.LoadUserInstance.CrearUsuario(jug);
            mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance;
        },IsValidObservable);
        CancelCommand = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance);
    }
    #endregion
}