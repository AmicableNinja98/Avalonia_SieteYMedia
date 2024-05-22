namespace SieteYMedia.Models;
public interface IBaraja{
    int NumNaipes{get;}
    void Barajar();
    INaipe? ExtraerNaipe();
    string ToString();
}