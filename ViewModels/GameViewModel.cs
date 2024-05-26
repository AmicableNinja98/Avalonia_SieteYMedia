using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Threading;
using ReactiveUI;
using SieteYMedia.Models;

namespace SieteYMedia.ViewModels;

public class GameViewModel : ViewModelBase
{
    #region Campos
    Jugador _jugador;
    Baraja _baraja;
    Crupier _crupier;
    float _puntosJug;
    float _puntosCrupier;
    DispatcherTimer _movCrupier;
    bool _pedirHabilitado;
    bool _plantarseHabilitado;
    string _mostrarGanador = string.Empty;
    ListaJugadores _listaJugadores = new();
    #endregion

    #region Propiedades
    public Jugador Jugador
    {
        get => _jugador;
        set => this.RaiseAndSetIfChanged(ref _jugador, value);
    }
    public float PuntosJug
    {
        get => _puntosJug;
        set => this.RaiseAndSetIfChanged(ref _puntosJug, value);
    }
    public float PuntosCrupier
    {
        get => _puntosCrupier;
        set => this.RaiseAndSetIfChanged(ref _puntosCrupier, value);
    }
    public bool PedirHabilitado
    {
        get => _pedirHabilitado;
        set => this.RaiseAndSetIfChanged(ref _pedirHabilitado, value);
    }
    public bool PlantarseHabilitado
    {
        get => _plantarseHabilitado;
        set => this.RaiseAndSetIfChanged(ref _plantarseHabilitado, value);
    }
    public string MostrarGanador
    {
        get => _mostrarGanador;
        set => this.RaiseAndSetIfChanged(ref _mostrarGanador, value);
    }
    public ObservableCollection<Naipe> MazoJugador => _jugador.Mazo;
    public ObservableCollection<Naipe> MazoCrupier => _crupier.Mazo;
    public ReactiveCommand<Unit, Unit> PedirCartaCommand { get; }
    public ReactiveCommand<Unit, Unit> PlantarseCommand { get; }
    public ReactiveCommand<Unit, Unit> GoMainMenuCommand { get; }
    #endregion

    #region Constructores
    public GameViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _baraja = new();
        _crupier = new();
        _jugador = new();
        _movCrupier = new();
        _movCrupier.Interval = TimeSpan.FromSeconds(1);
        _movCrupier.Tick += JugarCrupier;
        PedirHabilitado = true;
        PlantarseHabilitado = true;
        BarajarBaraja();
        PedirCartaCommand = ReactiveCommand.Create(() =>
        {
            var naipe = (Naipe)_baraja.ExtraerNaipe()!;
            if (naipe != null)
            {
                _jugador.PedirCarta(naipe);
                SumarPuntos(naipe);
                if (PuntosJug > 7.5)
                {
                    PedirHabilitado = false;
                    PlantarseHabilitado = false;
                    _movCrupier.Start();
                }
            }
        });
        PlantarseCommand = ReactiveCommand.Create(() =>
        {
            PedirHabilitado = false;
            PlantarseHabilitado = false;
            _movCrupier.Start();
        });
        GoMainMenuCommand = ReactiveCommand.Create(() =>
        {
            mainWindowViewModel.ContenidoViewModel = mainWindowViewModel.MenuInstance;
            _movCrupier.Stop();
            _listaJugadores.SerializarJSON();
            Reiniciar();
        });
    }
    #endregion

    #region Metodos
    private void BarajarBaraja() => _baraja.Barajar();
    private void Reiniciar()
    {
        PuntosJug = 0;
        PuntosCrupier = 0;
        PedirHabilitado = true;
        PlantarseHabilitado = true;
        MostrarGanador = string.Empty;
        _baraja.Reiniciar();
        _jugador.Mazo.Clear();
        _crupier.Mazo.Clear();
        BarajarBaraja();
    }
    private void SumarPuntos(INaipe naipe) => PuntosJug += (naipe.Peso <= Figura.Siete) ? (float)naipe.Peso : 0.5f;
    private void SumarPuntosCrupier(INaipe naipe) => PuntosCrupier += (naipe.Peso <= Figura.Siete) ? (float)naipe.Peso : 0.5f;
    private void Ganador()
    {
        _listaJugadores.ObtenerJugadores();
        var jug = _listaJugadores.Lista.First((x) => x.ID == _jugador.ID);
        if ((PuntosJug > PuntosCrupier && PuntosJug <= 7.5) || PuntosCrupier > 7.5)
        {
            MostrarGanador = $"Gana el jugador {_jugador.Nombre}";
            jug.PartidasGanadas++;
        }
        else
        {
            MostrarGanador = $"Gana el crupier";
            jug.PartidasPerdidas++;
        }
        jug.PartidasTotales++;
    }
    private void JugarCrupier(object? sender, EventArgs e)
    {
        if (PuntosJug > 7.5)
        {
            var naipe = _baraja.ExtraerNaipe();
            if (naipe != null)
            {
                _crupier.PedirCarta(naipe);
                SumarPuntosCrupier(naipe);
                Ganador();
                _movCrupier.Stop();
            }
        }
        else
        {
            if (PuntosCrupier < PuntosJug && PuntosCrupier < 7.5)
            {
                var naipe = _baraja.ExtraerNaipe();
                if (naipe != null)
                {
                    _crupier.PedirCarta(naipe);
                    SumarPuntosCrupier(naipe);
                }
            }
            else if (PuntosCrupier >= PuntosJug)
            {
                _movCrupier.Stop();
                Ganador();
            }
            else
            {
                if (PuntosCrupier > 7.5)
                {
                    _movCrupier.Stop();
                    Ganador();
                }
            }
        }
    }
    #endregion
}