using System;
using System.Collections.Generic;
using System.Text;

namespace ExamManagerMobile.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckInternetConnection();
    }
}
