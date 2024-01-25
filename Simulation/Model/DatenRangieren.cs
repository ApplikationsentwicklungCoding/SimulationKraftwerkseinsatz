using LibCx9020;

namespace Simulation.Model;
public class DatenRangieren(ModelSimulation modelSimulation, CxDaten cxDaten)
{
    public void Rangieren()
    {
        modelSimulation.StatusPotentiometer1 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 0);
        modelSimulation.StatusPotentiometer2 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 1);
        modelSimulation.StatusPotentiometer3 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 2);
        modelSimulation.StatusPotentiometer4 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 3);
        modelSimulation.StatusPotentiometer5 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 4);
        modelSimulation.StatusPotentiometer6 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 5);

        modelSimulation.ValuePotentiometer1 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 0);
        modelSimulation.ValuePotentiometer2 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 2);
        modelSimulation.ValuePotentiometer3 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 4);
        modelSimulation.ValuePotentiometer4 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 6);
        modelSimulation.ValuePotentiometer5 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 8);
        modelSimulation.ValuePotentiometer6 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 10);
    }
}
