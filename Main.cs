using System;
using System.Linq;
using System.Reflection;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
using Rocket.Core;
using System.Collections.Generic;
using Steamworks;
using Rocket.Unturned.Events;

namespace coolpuppy24.simpledeathmessages
{
    public class Main : RocketPlugin<Configuration>
    {
        public static Main Instance = null;

        protected override void Load()
        {
            Instance = this;
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            Rocket.Core.Logging.Logger.LogWarning("[SimpleDeathMessages] Made by: Coolpuppy24");
            Rocket.Core.Logging.Logger.LogWarning("------------------------------------------------------");
            Rocket.Core.Logging.Logger.LogWarning("[SimpleDeathMessages] Death Message Color: " + Configuration.Instance.DeathMessagesColor);
            Rocket.Core.Logging.Logger.LogWarning("[SimpleDeathMessages] Show Suicide Messages: " + Configuration.Instance.ShowSuicideMSG);
            Rocket.Core.Logging.Logger.Log("Successfully Loaded!");
        }


        protected override void Unload()
        {
            Instance = null;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            Rocket.Core.Logging.Logger.Log("Unload");
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"gun_headshot","{0} [GUN] {2} {1}"},
                    {"gun","{0} [GUN] {1}"},
                    {"food","[FOOD] {0}"},
                    {"arena","[ARENA] {0}"},
                    {"shred","[SHRED] {0}"},
                    {"punch_headshot","{0} [PUNCH] {2} {1}"},
                    {"punch","{0} [PUNCH] {1}"},
                    {"bones","[BONES] {0}"},
                    {"melee_headshot","{0} [MELEE] {2} {1}"},
                    {"melee","{0} [MELEE] {1}"},
                    {"water","[WATER] {0}"},
                    {"breath","[BREATH] {0}"},
                    {"zombie","[ZOMBIE] {0}"},
                    {"animal","[ANIMAL] {0}"},
                    {"grenade","[GRENADE] {0}"},
                    {"vehicle","[VEHICLE] {0}"},
                    {"suicide","[SUICIDE] {0}"},
                    {"burning","[BURNING] {0}"},
                    {"headshot","[HEADSHOT]" },
                    {"landmine","[LANDMINE] {0}"},
                    {"roadkill","{0} [ROADKILL] {1}"},
                    {"bleeding","[BLEEDING] {0}"},
                    {"freezing","[FREEZING] {0}"},
                    {"sentry","[SENTRY] {0}"},
                    {"charge","[CHARGE] {0}"},
                    {"missile","[MISSILE] {0}"},
                    {"splash","[SPLASH] {0}"},
                    {"acid","[ACID] {0}"},
                    {"spit","[SPIT] {0}"},
                    {"kill","[ADMIN KILL] {0}"},
                    {"boulder","[BOULDER] {0}"},
                };
            }
        }


        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, Steamworks.CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);

            string headshot = Translate("headshot");
            {
                if (cause.ToString() == "GUN")
                {
                    if (limb == ELimb.SKULL)
                        UnturnedChat.Say(Translate("gun_headshot", killer.DisplayName, player.DisplayName, headshot), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    else
                        UnturnedChat.Say(Translate("gun", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "MELEE")
                {
                    if (limb == ELimb.SKULL)
                        UnturnedChat.Say(Translate("melee_headshot", killer.DisplayName, player.DisplayName, headshot), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    else
                        UnturnedChat.Say(Translate("melee", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "PUNCH")
                {
                    if (limb == ELimb.SKULL)
                        UnturnedChat.Say(Translate("punch_headshot", killer.DisplayName, player.DisplayName, headshot), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    else
                        UnturnedChat.Say(Translate("punch", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SHRED")
                {
                    UnturnedChat.Say(Translate("shred", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "ZOMBIE")
                {
                    UnturnedChat.Say(Translate("zombie", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "ANIMAL")
                {
                    UnturnedChat.Say(Translate("animal", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "ROADKILL")
                {
                    UnturnedChat.Say(Translate("roadkill", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SPARK")
                {
                    UnturnedChat.Say(Translate("spark", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "VEHICLE")
                {
                    UnturnedChat.Say(Translate("vehicle", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "FOOD")
                {
                    UnturnedChat.Say(Translate("food", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "WATER")
                {
                    UnturnedChat.Say(Translate("water", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "INFECTION")
                {
                    UnturnedChat.Say(Translate("infection", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BLEEDING")
                {
                    UnturnedChat.Say(Translate("bleeding", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "LANDMINE")
                {
                    UnturnedChat.Say(Translate("landmine", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BREATH")
                {
                    UnturnedChat.Say(Translate("breath", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "KILL")
                {
                    UnturnedChat.Say(Translate("kill", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "FREEZING")
                {
                    UnturnedChat.Say(Translate("freezing", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SENTRY")
                {
                    UnturnedChat.Say(Translate("sentry", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "CHARGE")
                {
                    UnturnedChat.Say(Translate("charge", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "MISSILE")
                {
                    UnturnedChat.Say(Translate("missile", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BONES")
                {
                    UnturnedChat.Say(Translate("bones", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SPLASH")
                {
                    UnturnedChat.Say(Translate("splash", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "ACID")
                {
                    UnturnedChat.Say(Translate("acid", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SPIT")
                {
                    UnturnedChat.Say(Translate("spit", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BURNING")
                {
                    UnturnedChat.Say(Translate("burning", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BURNER")
                {
                    UnturnedChat.Say(Translate("burner", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "BOULDER")
                {
                    UnturnedChat.Say(Translate("boulder", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "ARENA")
                {
                    UnturnedChat.Say(Translate("arena", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "SUICIDE" && Configuration.Instance.ShowSuicideMSG)
                {
                    UnturnedChat.Say(Translate("suicide", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
                else if (cause.ToString() == "GRENADE")
                {
                    UnturnedChat.Say(Translate("grenade", player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                }
            }
        }
    }
}
