using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LJV_Dllconsolesample
{
    public sealed class PinnedObject : IDisposable
    {
        //private byte[] receiveData;

        //public PinnedObject(byte[] receiveData)
        //{
        //    // TODO: Complete member initialization
        //    this.receiveData = receiveData;
        //}
        #region Field

        private GCHandle _Handle;	   // Garbage collector handle

        #endregion

        #region Property

        /// <summary>
        /// Get the address.
        /// </summary>
        public IntPtr Pointer
        {
            // Get the leading address of the current object that is pinned.
            get { return _Handle.AddrOfPinnedObject(); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target">Target to protect from the garbage collector</param>
        public PinnedObject(object target)
        {
            // Pin the target to protect it from the garbage collector.
            _Handle = GCHandle.Alloc(target, GCHandleType.Pinned);
        }

        #endregion

        #region Interface
        /// <summary>
        /// Interface
        /// </summary>
        public void Dispose()
        {
            _Handle.Free();
            _Handle = new GCHandle();
        }

        #endregion
    }
}
