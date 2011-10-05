﻿namespace CloudFoundry.Net.Vmc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Types;

    public interface IVcapClient
    {
        string CurrentToken { get; }
        Uri CurrentTarget { get; }
        string CurrentUri { get; }

        VcapClientResult Info();

        VcapClientResult Target(string argUri);

        VcapClientResult Login();
        VcapClientResult Login(string email, string password);

        // TODO VcapClientResult ChangePassword(string username, string newpassword, string url, string accesstoken)

        VcapClientResult Push(string argName, string argDeployFQDN, DirectoryInfo argPath, uint memorysize);
        VcapClientResult Delete(string argName);
        VcapClientResult Bind(string argAppName, string argProvisionedServiceName);

        IEnumerable<SystemService> GetSystemServices();
        IEnumerable<ProvisionedService> GetProvisionedServices();

        void Stop(Application argApp);
        void Start(Application argApp);
        void Restart(Application argApp);

        Application GetApplication(string argName);

        string GetLogs(Application application, ushort instanceNumber);

        IEnumerable<StatInfo> GetStats(Application application);

        IEnumerable<ExternalInstance> GetInstances(Application application);

        IEnumerable<Crash> GetAppCrash(Application application);

        IEnumerable<Application> ListApps();

        VcapResponse UpdateApplication(Application application);
    }
}