using System;
using Types.ObservableValues;

namespace ColorMixer.Services.PlayerData
{
    [Serializable]
    public class Data
    {
        public ObservableSecureInt Level;

        public Data()
        {
            Level = new ObservableSecureInt(0);
        }
    }
}