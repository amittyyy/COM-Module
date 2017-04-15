using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJV_Dllconsolesample.Properties;



namespace LJV_Dllconsolesample
{
    class Program
    {
        #region takeinput
        static void Main(string[] args)
        {

            int param;
            var wrapper = new LJVWrapper();
            wrapper.EthernetOpen();
            Console.WriteLine("please input any number");
            param = Convert.ToInt32(Console.ReadLine());
            /* var parameter = string.Join(string.Empty, args);*/

            switch (param)
            {
                case 1:
                    wrapper.RebootController_value();
                    break;
                case 2:
                    wrapper.CommClose_value();
                    break;
                case 3:
                    wrapper.Triggerconnect();
                    break;
                case 4:
                    wrapper.StartMeasure_value();
                    break;
                case 5:
                    wrapper.StopMeasure_value();
                    break;
                case 6:
                    wrapper.GetMeasurementValue_value();
                    break;               
                case 8:
                    wrapper.GetStorageData_value();
                    break;
            }

            //  Console.WriteLine(parameter);
            //  Console.ReadLine();
        }
        #endregion

    }
}
