using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_DynamicConfiguration
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }
}
