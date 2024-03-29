﻿using System.Reflection;
using iTechArt.iTechQuiz.Foundation.Interfaces;

namespace iTechArt.iTechQuiz.Foundation.Services
{
    public class AppVersionService : IAppVersionService
    {
        public string Version => Assembly.GetEntryAssembly()?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;
    }
}
