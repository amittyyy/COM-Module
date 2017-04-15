using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LJV_Dllconsolesample
{
    public partial class GetStorageDataForm
    {
        #region Field
        /// <summary>
        /// Storage status request structure
        /// </summary>
        private LJV7IF_GET_STORAGE_REQ _req;
        #endregion

        #region Property
        /// <summary>
        /// Storage status request structure
        /// </summary>
        public LJV7IF_GET_STORAGE_REQ Req
        {
            get { return _req; }
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
                    _req.dwSurface = Convert.ToUInt32("0");
                    _req.dwStartNo = Convert.ToUInt32("0");
                    _req.dwDataCnt = Convert.ToUInt32("1000");
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

        #region Method
        /// <summary>
        /// Constructor
        /// </summary>
        public GetStorageDataForm()
        {
            _req = new LJV7IF_GET_STORAGE_REQ();
         //   InitializeComponent();
            _req.dwSurface = Convert.ToUInt32(0);
            _req.dwStartNo = Convert.ToUInt32(0);
            _req.dwDataCnt = Convert.ToUInt32(1000);
            // Field initialization
            
        }
        #endregion

    }
}
