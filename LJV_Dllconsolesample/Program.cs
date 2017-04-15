
using System.Runtime.InteropServices;



namespace LJV_Dllconsolesample.interfaces
{
    [Guid("B2BECDD6-A50C-4428-8141-C03C2429912F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]     
    public interface IProgram
    {
        [DispId(1)]
        string EthernetOpen();
        [DispId(2)]
        string Triggerconnect();
        [DispId(3)]
        string StartMeasure_value();
        [DispId(4)]
        string StopMeasure_value();
        [DispId(4)]
        string errorfrom();
        [DispId(5)]
        string RebootController_value();
        [DispId(6)]
        string CommClose_value();
        [DispId(7)]
        string GetMeasurementValue_value();
        [DispId(8)]
        string GetLastError();
        [DispId(9)]
        string RetrunToFactorySetting_value();

        
    }
}
