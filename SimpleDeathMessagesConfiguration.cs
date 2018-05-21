using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coolpuppy24.simpledeathmessages
{
    public class SimpleDeathMessagesConfiguration : IRocketPluginConfiguration
    {
        public string DeathMessagesColor;
        public bool ShowSuicideMSG;

        public void LoadDefaults()
        {
            DeathMessagesColor = "Red";
            ShowSuicideMSG = true;
        }
    }
}

