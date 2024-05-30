using System.Reactive;
using ReactiveUI;

namespace SieteYMedia.ViewModels;
public class MenuViewModel : ViewModelBase
{
    #region Propiedades
    public string Titulo => "SIETE Y MEDIA";
    public string Subtitulo => "Creado por AmicableNinja98";
    public string Inicio => "Iniciar Partida";
    public string Opciones => "Opciones";
    public ReactiveCommand<Unit,ViewModelBase> IrOpciones {get;}
    public ReactiveCommand<Unit,ViewModelBase> IrCargarUsuarios {get;}
    #endregion

    #region Constructores
    public MenuViewModel(MainWindowViewModel mainWindowViewModel)
    {
        IrCargarUsuarios = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadUserInstance);
        IrOpciones = ReactiveCommand.Create(() => mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.LoadOptionsInstance);
    }
    #endregion
}