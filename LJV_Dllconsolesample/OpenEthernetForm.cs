//----------------------------------------------------------------------------- 
// <copyright file="OpenEthernetForm.cs" company="KEYENCE">
//	 Copyright (c) 2013 KEYENCE CORPORATION.  All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace LJV_Dllconsolesample
{
   
    public partial class OpenEthernetForm
    {
        //string _txtboxIpFirstSegment;
        //string _txtboxIpSecondSegment;
        //string _txtboxIpThirdSegment;
        //string _txtboxIpFourthSegment;
        //string _txtboxPort;



        #region Field
        /// <summary>
        /// Ethernet communication settings
        /// </summary>
        private LJV7IF_ETHERNET_CONFIG _ethernetConfig;
        #endregion

        #region Property
        /// <summary>
        /// Ethernet communication settings
        /// </summary>
        public LJV7IF_ETHERNET_CONFIG EthernetConfig
        {


            get { return _ethernetConfig; }
            set {
                _ethernetConfig = value;
                {
                    _ethernetConfig.abyIpAddress[0] = 192;
                    _ethernetConfig.abyIpAddress[1] = 168;
                    _ethernetConfig.abyIpAddress[2] = 196;
                    _ethernetConfig.abyIpAddress[3] = 18;

                        //_txtboxIpFirstSegment  = _ethernetConfig.abyIpAddress[0].ToString();
                        //_txtboxIpSecondSegment = _ethernetConfig.abyIpAddress[1].ToString();
                        //_txtboxIpThirdSegment  = _ethernetConfig.abyIpAddress[2].ToString();
                        //_txtboxIpFourthSegment = _ethernetConfig.abyIpAddress[3].ToString();
                    }
                     _ethernetConfig.wPortNo = 24691;
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// Close start event
        /// </summary>
        /// <param name="e"></param>
        public void OnClosing(CancelEventArgs e)
        {
            try
            {

                _ethernetConfig.abyIpAddress = new byte[]
					{
						Convert.ToByte(_ethernetConfig.abyIpAddress[0].ToString()),
						Convert.ToByte(_ethernetConfig.abyIpAddress[0].ToString()),
						Convert.ToByte(_ethernetConfig.abyIpAddress[2].ToString()),
						Convert.ToByte(_ethernetConfig.abyIpAddress[3].ToString())
					};
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                e.Cancel = true;
                return;
            }
           /* base.OnClosing(e);*/
        }       
      
        #endregion

        #region Method ethernet communication
        /// <summary>
        /// Constructor
        /// </summary>
     public OpenEthernetForm()
        {

            _ethernetConfig = new LJV7IF_ETHERNET_CONFIG();  
            _ethernetConfig.abyIpAddress = new byte[]
					{
						Convert.ToByte("192"),
						Convert.ToByte("168"),
						Convert.ToByte("196"),
						Convert.ToByte("18")
					};
					_ethernetConfig.wPortNo = Convert.ToUInt16("24691");
				}
        }
        #endregion       
    }
  
