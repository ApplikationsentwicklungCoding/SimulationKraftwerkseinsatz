using LibCx9020;

namespace Simulation.Model;
public class DatenRangieren(ModelSimulation modelSimulation, CxDaten cxDaten)
{
    public void Rangieren()
    {

        modelSimulation.StatusPotentiometer11 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 0);
        modelSimulation.StatusPotentiometer12 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 1);
        modelSimulation.StatusPotentiometer13 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 2);
        modelSimulation.StatusPotentiometer14 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 3);
        modelSimulation.StatusPotentiometer15 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 4);

        modelSimulation.StatusPotentiometer21 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 5);
        modelSimulation.StatusPotentiometer22 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 6);
        modelSimulation.StatusPotentiometer23 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 7);
        modelSimulation.StatusPotentiometer24 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 8);
        modelSimulation.StatusPotentiometer25 = cxDaten.GetByte(CxDaten.DatenBereich.Da, 9);


        modelSimulation.ValuePotentiometer11 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 0);
        modelSimulation.ValuePotentiometer12 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 2);
        modelSimulation.ValuePotentiometer13 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 4);
        modelSimulation.ValuePotentiometer14 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 6);
        modelSimulation.ValuePotentiometer15 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 8);

        modelSimulation.ValuePotentiometer21 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 10);
        modelSimulation.ValuePotentiometer22 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 12);
        modelSimulation.ValuePotentiometer23 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 14);
        modelSimulation.ValuePotentiometer24 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 16);
        modelSimulation.ValuePotentiometer25 = cxDaten.GetInt(CxDaten.DatenBereich.Aa, 18);
    }
}
