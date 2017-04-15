
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using LJV_Dllconsolesample.Properties;
using System.Resources;
using LJV_Dllconsolesample.interfaces;
using LJV_Dllconsolesample.Datas;



namespace LJV_Dllconsolesample
{
    [Guid("7BAB832B-14D8-40FF-A135-E8FBCB23A8B6")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    [ProgId("LJV_Dllconsolesample.Server")]
    public partial class LJVWrapper
    {
        #region Enum
        /// <summary>
        /// Send command definition
        /// </summary>
        /// <remark>Defined for separate return code distinction</remark>
        public enum SendCommand
        {
            /// <summary>None</summary>
            None,
            /// <summary>Restart</summary>
            RebootController,
            /// <summary>Trigger</summary>
            Trigger,
            /// <summary>Start measurement</summary>
            StartMeasure,
            /// <summary>Stop measurement</summary>
            StopMeasure,
            /// <summary>Auto zero</summary>
            AutoZero,
            /// <summary>Timing</summary>
            Timing,
            /// <summary>Reset</summary>
            Reset,
            /// <summary>Program switch</summary>
            ChangeActiveProgram,
            /// <summary>Get measurement results</summary>
            GetMeasurementValue,

            /// <summary>Get profiles</summary>
            GetProfile,
            /// <summary>Get batch profiles (operation mode "high-speed (profile only)")</summary>
            GetBatchProfile,
            /// <summary>Get profiles (operation mode "advanced (with OUT measurement)")</summary>
            GetProfileAdvance,
            /// <summary>Get batch profiles (operation mode "advanced (with OUT measurement)").</summary>
            GetBatchProfileAdvance,

            /// <summary>Start storage</summary>
            StartStorage,
            /// <summary>Stop storage</summary>
            StopStorage,
            /// <summary>Get storage status</summary>
            GetStorageStatus,
            /// <summary>Manual storage request</summary>
            RequestStorage,
            /// <summary>Get storage data</summary>
            GetStorageData,
            /// <summary>Get profile storage data</summary>
            GetStorageProfile,
            /// <summary>Get batch profile storage data.</summary>
            GetStorageBatchProfile,

            /// <summary>Initialize USB high-speed data communication</summary>
            HighSpeedDataUsbCommunicationInitalize,
            /// <summary>Initialize Ethernet high-speed data communication</summary>
            HighSpeedDataEthernetCommunicationInitalize,
            /// <summary>Request preparation before starting high-speed data communication</summary>
            PreStartHighSpeedDataCommunication,
            /// <summary>Start high-speed data communication</summary>
            StartHighSpeedDataCommunication,
        }

        #endregion

        #region Field

        /// <summary>Ethernet settings structure </summary>
        private LJV7IF_ETHERNET_CONFIG _ethernetConfig;
        private int _currentDeviceId;
        /// <summary>Measurement data list</summary>
        private List<MeasureData> _measureDatas;
        private SendCommand _sendCommand;
        /// <summary>Array of controller information</summary>
        private LJV_Dllconsolesample.Datas.DeviceData[] _deviceData;
        string _error = string.Empty;
        #endregion

        #region Method(for single-function)
        /// <summary>
        /// Constructor
        /// </summary>
        public LJVWrapper()
        {
            _deviceData = new DeviceData[] { new DeviceData() };
            _measureDatas = new List<MeasureData>();
        }

        #endregion

        #region communicate
        /// <summary>
        /// "EthernetOpen" seitch case integer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public string EthernetOpen()    // amity: to establsihed the commenction between device and the dll.
        {

            OpenEthernetForm openEthernetForm = new OpenEthernetForm();
            LJV7IF_ETHERNET_CONFIG ethernetConfig = openEthernetForm.EthernetConfig;

            int rc = NativeMethods.LJV7IF_EthernetOpen(_currentDeviceId, ref ethernetConfig);
            AddLogResult(rc, Resources.SID_ETHERNET_OPEN);
            if (rc == (int)Rc.Ok)
            {
                _deviceData[_currentDeviceId].Status = DeviceStatus.Ethernet;
                _deviceData[_currentDeviceId].EthernetConfig = ethernetConfig;
            }
            else
            {
                _deviceData[_currentDeviceId].Status = DeviceStatus.NoConnection;
            }
            return rc.ToString();

        }
        /// <summary>
        /// "CommClose".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string CommClose_value()
        {
            int rc = NativeMethods.LJV7IF_CommClose(_currentDeviceId);
            AddLogResult(rc, Resources.SID_COMM_CLOSE);


            //_deviceData = new LJV_Dllconsolesample.Datas.DeviceData[NativeMethods.DeviceCount];
            //_deviceData[_currentDeviceId] = new Datas.DeviceData();
            //var status = _deviceData[_currentDeviceId].GetStatusString();

            _deviceData[_currentDeviceId].Status = LJV_Dllconsolesample.Datas.DeviceStatus.NoConnection;
            // _deviceStatusLabels[_currentDeviceId].Text = _deviceData[_currentDeviceId].GetStatusString();
            return rc.ToString();
        }
        #endregion

        #region for controlling the system
        /// <summary>
        /// "RebootController" clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string RebootController_value()
        {
            _sendCommand = SendCommand.RebootController;

            int rc = NativeMethods.LJV7IF_RebootController(_currentDeviceId);
            AddLogResult(rc, Resources.SID_REBOOT_CONTROLLER);
            return rc.ToString();
        }
        /// <summary>
        /// "RetrunToFactorySetting" clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string RetrunToFactorySetting_value()
        {
            int rc = NativeMethods.LJV7IF_RetrunToFactorySetting(_currentDeviceId);
            AddLogResult(rc, Resources.SID_RETRUN_TO_FACTORY_SETTING);
            return rc.ToString();
        }

        #endregion
        #region Measurementcontroll
        /// <summary>
        /// " amity:Trigger" on switch case integer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string Triggerconnect()  // amity: this is for the trigger to measurement.
        {
            _sendCommand = SendCommand.Trigger;

            int rc = NativeMethods.LJV7IF_Trigger(_currentDeviceId);
            AddLogResult(rc, Resources.SID_TRIGGER);
            return rc.ToString();
        }
        /// <summary>
        /// "amity:StartMeasure".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string StartMeasure_value()  // amity: To start measurement using device.
        {
            _sendCommand = SendCommand.StartMeasure;

            int rc = NativeMethods.LJV7IF_StartMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.SID_START_MEASURE);
            return rc.ToString();
        }
        /// <summary>
        /// " amity: StopMeasure".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string StopMeasure_value()   // amity: to stop measurement.
        {
            _sendCommand = SendCommand.StopMeasure;

            int rc = NativeMethods.LJV7IF_StopMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.SID_STOP_MEASURE);
            return rc.ToString();
        }
        #endregion

        #region for getting the measurement results
        /// <summary>
        /// "GetMeasurementValue".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string GetMeasurementValue_value()
        {
            _sendCommand = SendCommand.GetMeasurementValue;

            LJV7IF_MEASURE_DATA[] measureData = new LJV7IF_MEASURE_DATA[NativeMethods.MeasurementDataCount];
            int rc = NativeMethods.LJV7IF_GetMeasurementValue(_currentDeviceId, measureData);
            AddLogResult(rc, Resources.SID_GET_MEASUREMENT_VALUE);
            if (rc == (int)Rc.Ok)
            {
                _measureDatas.Clear();
                _measureDatas.Add(new MeasureData(0, measureData));

                for (int i = 0; i < NativeMethods.MeasurementDataCount; i++)
                {
                    AddLog(string.Format("  OUT{0:00}: {1}", (i + 1), Utility.ConvertToLogString(measureData[i]).ToString()));
                }
            }
            return rc.ToString();
        }
        #endregion

        /*Auther: Amity Timalsina
          Date  : 11/18/2015 
          Reson : Read and Get the data from the navigator controller to the file in specified location. */

        #region getstoragedata
       
        /// <summary>
        /// "Amity:GetStorageData" button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string GetStorageData_value()
        {
            _sendCommand = SendCommand.GetStorageData;
            GetStorageDataForm getStorageData = new GetStorageDataForm();
            _measureDatas.Clear();
            LJV7IF_GET_STORAGE_REQ req = getStorageData.Req;
            // @Point
            // † dwReadArea is the target surface to read.
            //    The target surface to read indicates where in the internal memory usage area to read.
            // † The method to use in specifying dwReadArea varies depending on how internal memory is allocated.
            //   * Double buffer
            //      0 indicates the active surface, 1 indicates surface A, and 2 indicates surface B.
            //   * Entire area (overwrite)
            //      Fixed to 1
            //   * Entire area (do not overwrite)
            //      After a setting modification, data is saved in surfaces 1, 2, 3, and so on in order, and 0 is set as the active surface.
            // † For details, see "9.2.9.2 Internal memory."

            LJV7IF_STORAGE_INFO storageInfo = new LJV7IF_STORAGE_INFO();
            LJV7IF_GET_STORAGE_RSP rsp = new LJV7IF_GET_STORAGE_RSP();
            uint oneDataSize = (uint)(Marshal.SizeOf(typeof(uint)) + (uint)Utility.GetByteSize(Utility.TypeOfStruct.MEASURE_DATA) * (uint)NativeMethods.MeasurementDataCount);
            uint allDataSize = Math.Min(LJV_Dllconsolesample.Define.READ_DATA_SIZE, oneDataSize * getStorageData.Req.dwDataCnt);
            byte[] receiveData = new byte[allDataSize];

            using (PinnedObject pin = new PinnedObject(receiveData))
            {
                int rc = NativeMethods.LJV7IF_GetStorageData(0, ref req, ref storageInfo, ref rsp, pin.Pointer, allDataSize);
                AddLogResult(rc, Resources.SID_GET_STORAGE_DATA);
                // @Point
                // † Terminology	
                //  * Base time … time expressed with 32 bits (<- the time when the setting was changed)
                //  * Accumulated date and time	 … counter value that indicates the elapsed time, in units of 10 ms, from the base time
                // † The accumulated date and time are stored in the accumulated data.
                // † The accumulated time of read data is calculated as shown below.
                //   Accumulated time = "base time (stBaseTime of LJV7IF_GET_STORAGE_RSP)" + "accumulated date and time × 10 ms"

                if (rc == (int)Rc.Ok)
                {
                    // Temporarily retain the get data.
                    int byteSize = MeasureData.GetByteSize();
                    for (int i = 0; i < (int)rsp.dwDataCnt; i++)
                    {
                        _measureDatas.Add(new MeasureData(receiveData, byteSize * i));
                    }
                    
                    //// Response data display
                    //AddLog(Utility.ConvertToLogString(storageInfo).ToString());
                    //AddLog(Utility.ConvertToLogString(rsp).ToString());
                }
                return rc.ToString();
            }


        }

        #endregion

        #region add log results
        /// <summary>
        /// Communication command result log output
        /// </summary>
        /// <param name="rc">Return code from the DLL</param>
        /// <param name="commandName">Command name to be output in the log</param>
        private void AddLogResult(int rc, string commandName)
        {
            if (rc == (int)Rc.Ok)
            {
                AddLog(string.Format(Resources.SID_LOG_FORMAT, commandName, Resources.SID_RESULT_OK, rc));
            }
            else
            {
                AddLog(string.Format(Resources.SID_LOG_FORMAT, commandName, Resources.SID_RESULT_NG, rc));
                AddErrorLog(rc);
            }
        }
        /// <summary>
        /// Log output
        /// </summary>
        /// <param name="strLog">Output log</param>
        private void AddLog(string strLog)
        {
            Console.WriteLine(strLog + Environment.NewLine);
            Console.ReadLine();
            _error = string.Concat(_error, strLog);
        }

        public string GetLastError()
        {
            return _error;
        }

        /// <summary>
        /// Error log output
        /// </summary>
        /// <param name="rc">Return code</param>
        private void AddErrorLog(int rc)
        {
            if (rc < 0x8000)
            {
                // Common return code
                CommonErrorLog(rc);
            }
            else
            {
                // Individual return code
                IndividualErrorLog(rc);
            }
        }

        /// <summary>
        /// Common return code log output
        /// </summary>
        /// <param name="rc">Return code</param>
        private void CommonErrorLog(int rc)
        {
            switch (rc)
            {
                case (int)Rc.Ok:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_OK));
                    break;
                case (int)Rc.ErrOpenDevice:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_OPEN_DEVICE));
                    break;
                case (int)Rc.ErrNoDevice:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_NO_DEVICE));
                    break;
                case (int)Rc.ErrSend:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_SEND));
                    break;
                case (int)Rc.ErrReceive:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_RECEIVE));
                    break;
                case (int)Rc.ErrTimeout:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_TIMEOUT));
                    break;
                case (int)Rc.ErrParameter:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_PARAMETER));
                    break;
                case (int)Rc.ErrNomemory:
                    AddLog(string.Format(Resources.SID_RC_FORMAT, Resources.SID_RC_ERR_NOMEMORY));
                    break;
                default:
                    AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                    break;
            }
        }


        #endregion

        #region individual error
        /// <summary>
        /// amity: Individual return code log error
        /// </summary>
        /// <param name="rc">Return code</param>
        public void IndividualErrorLog(int rc)
        {
            switch (_sendCommand)
            {
                case SendCommand.RebootController:
                    {
                        switch (rc)
                        {
                            case 0x80A0:
                                AddLog(string.Format(Resources.SID_RC_FORMAT, @"Accessing the save area"));
                                break;
                            default:
                                AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.Trigger:
                    {
                        switch (rc)
                        {
                            case 0x8080:
                                AddLog(string.Format(Resources.SID_RC_FORMAT, @"The trigger mode is not [external trigger]"));
                                break;
                            default:
                                AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.StartMeasure:
                case SendCommand.StopMeasure:
                    {
                        switch (rc)
                        {
                            case 0x8080:
                                AddLog(string.Format(Resources.SID_RC_FORMAT, @"Batch measurements are off"));
                                break;
                            case 0x80A0:
                                AddLog(string.Format(Resources.SID_RC_FORMAT, @"Batch measurement start processing could not be performed because the REMOTE terminal is off or the LASER_OFF terminal is on"));
                                break;
                            default:
                                AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.GetMeasurementValue:
                    {
                        switch (rc)
                        {
                            case 0x8080:
                                AddLog(string.Format(Resources.SID_RC_FORMAT, @"The operation mode is [high-speed (profile only)]"));
                                break;
                            default:
                                AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                default:
                    AddLog(string.Format(Resources.SID_NOT_DEFINE_FROMAT, rc));
                    break;
            }
        }
        public string errorfrom()
        {
            return _sendCommand.ToString();
        }
        #endregion
    }
}
