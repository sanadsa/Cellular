﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Common.Interfaces
{
    public interface ICrmManager
    {
        // client
        Client AddClient(string name, string lastName, int idNumber, int clientTypeId,
                         string address, string contactNumber);
        Client UpdateClient(int clientId, string name, string lastName, int idNumber, int clientTypeId,
                         string address, string contactNumber, int callsToCenter);
        void DeleteClient(int clientId);
        // service agent
        ServiceAgent AddServiceAgent(string agentName, string password);
        ServiceAgent UpdateServiceAgent(int agentId, string name, string pass, int salesAmount);
        void LoginAgent(string name, string password);
        // line
        Line AddLine(int clientId, string number, eStatus status);
        Line UpdateLineStatus(int lineId, eStatus status);
        void DeleteLine(int lineId);
        // package
        Package AddPackage(string name, int lineId, double price, DateTime month, int maxMinute, int minutePrice,
            double discount, int favoritNumId, bool mostCalled, bool famDis);
        Package UpdatePackage(int packageId, string name, int lineId, double price, DateTime month, int maxMinute, int minutePrice,
            double discount, int favoritNumId, bool mostCalled, bool famDis);
    }
}
