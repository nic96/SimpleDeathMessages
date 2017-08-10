using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
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
                    {"gun_headshot","{0} [GUN - {3}] {2} {1}"},
                    {"gun","{0} [GUN - {2}] {1}"},
                    {"food","[FOOD] {0}"},
                    {"arena","[ARENA] {0}"},
                    {"shred","[SHRED] {0}"},
                    {"punch_headshot","{0} [PUNCH] {2} {1}"},
                    {"punch","{0} [PUNCH] {1}"},
                    {"bones","[BONES] {0}"},
                    {"melee_headshot","{0} [MELEE - {3}] {2} {1}"},
                    {"melee","{0} [MELEE- {2}] {1}"},
                    {"water","[WATER] {0}"},
                    {"breath","[BREATH] {0}"},
                    {"zombie","[ZOMBIE] {0}"},
                    {"animal","[ANIMAL] {0}"},
                    {"grenade","[GRENADE] {0}"},
                    {"vehicle","[VEHICLE] {0}"},
                    {"suicide","[SUICIDE] {0}"},
                    {"burning","[BURNING] {0}"},
                    {"headshot","+ [HEADSHOT]" },
                    {"landmine","[LANDMINE] {0}"},
                    {"roadkill","{0} [ROADKILL] {1}"},
                    {"bleeding","[BLEEDING] {0}"},
                    {"freezing","[FREEZING] {0}"},
                    {"sentry","[SENTRY] {0}"},
                    {"charge","[CHARGE] {0}"},
                    {"missile","[MISSILE] {0}"},
                    {"splash","[SPLASH] {0}"},
                    {"acid","[ACID] {0}"},
                    {"spark", "[SPARK] {0}"},
                    {"infection", "[INFECTION] {0}"},
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
                if (cause.ToString() == "SHRED" || cause.ToString() == "ZOMBIE" || cause.ToString() == "ANIMAL" || cause.ToString() == "SPARK" || cause.ToString() == "VEHICLE" || cause.ToString() == "FOOD" || cause.ToString() == "WATER" || cause.ToString() == "INFECTION" || cause.ToString() == "BLEEDING" || cause.ToString() == "LANDMINE" || cause.ToString() == "BREATH" || cause.ToString() == "KILL" || cause.ToString() == "FREEZING" || cause.ToString() == "SENTRY" || cause.ToString() == "CHARGE" || cause.ToString() == "MISSILE" || cause.ToString() == "BONES" || cause.ToString() == "SPLASH" || cause.ToString() == "ACID" || cause.ToString() == "SPIT" || cause.ToString() == "BURNING" || cause.ToString() == "BURNER" || cause.ToString() == "BOULDER" || cause.ToString() == "ARENA" || cause.ToString() == "GRENADE" || (Configuration.Instance.ShowSuicideMSG == true && cause.ToString() == "SUICIDE") || cause.ToString() == "ROADKILL" || cause.ToString() == "MELEE" || cause.ToString() == "GUN" || cause.ToString() == "PUNCH")
                {
                    if (cause.ToString() != "ROADKILL" && cause.ToString() != "MELEE" && cause.ToString() != "GUN" && cause.ToString() != "PUNCH")
                    {
                        UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    }
                    else if (cause.ToString() == "ROADKILL")
                    {
                        UnturnedChat.Say(Translate("roadkill", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    }
                    else if (cause.ToString() == "MELEE" || cause.ToString() == "GUN")
                    {
                        if (limb == ELimb.SKULL)
                            UnturnedChat.Say(Translate(cause.ToString().ToLower() + "_headshot", killer.DisplayName, player.DisplayName, headshot, Rocket.Unturned.Player.UnturnedPlayer.FromCSteamID(murderer).Player.equipment.asset.itemName.ToString()), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                        else
                            UnturnedChat.Say(Translate(cause.ToString().ToLower(), killer.DisplayName, player.DisplayName, Rocket.Unturned.Player.UnturnedPlayer.FromCSteamID(murderer).Player.equipment.asset.itemName.ToString()), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    }
                    else if (cause.ToString() == "PUNCH")
                    {
                        if (limb == ELimb.SKULL)
                            UnturnedChat.Say(Translate("punch_headshot", killer.DisplayName, player.DisplayName, headshot), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                        else
                            UnturnedChat.Say(Translate("punch", killer.DisplayName, player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                    }
                }
                else //No need to update the plugin later! (Just add the translation)
                {
                    if (Translate(cause.ToString().ToLower()) != null)
                    {
                        if (Translate(cause.ToString().ToLower()).Contains("{1}"))
                        {
                            UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName , killer.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                        }
                        else
                        {
                            UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName), UnturnedChat.GetColorFromName(Configuration.Instance.DeathMessagesColor, Color.green));
                        }
                    }
                    else
                    {
                        Rocket.Core.Logging.Logger.LogError("Please add translation for " + cause.ToString() + " | Parameters for custom translation: {0} = Player , {1} = Killer");
                    }
                }
            }
        }
    }
}
