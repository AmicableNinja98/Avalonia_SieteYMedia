using System.Reactive;
using ReactiveUI;

namespace SieteYMedia.ViewModels;
public class MenuViewModel : ViewModelBase
{
    #region Propiedades
    public string Titulo => "SIETE Y MEDIA";
    public string Subtitulo => "Creado por AmicableNinja98";
    public string Inicio => "Iniciar Partida";
    public string Estadísticas => "Estadísticas";
    public ReactiveCommand<Unit,ViewModelBase> IrEstadísticas {get;}
    public ReactiveCommand<Unit,ViewModelBase> IrCargarUsuarios {get;}
    #endregion

    #region Constructores
    public MenuViewModel(MainWindowViewModel mainWindowViewModel)
    {
        IrCargarUsuarios = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance);
        IrEstadísticas = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadOptionsInstance);
    }
    #endregion
}