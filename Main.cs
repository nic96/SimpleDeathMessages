using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using Rocket.Unturned.Events;
using Rocket.Unturned.Chat;
using Rocket.Core.Logging;
using Color = UnityEngine.Color;
using Steamworks;

namespace coolpuppy24.simpledeathmessages
{
    public class Main : RocketPlugin<SimpleDeathMessagesConfiguration>
    {
        protected override void Load()
        {
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
           Logger.Log("Successfully Loaded!");
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            Logger.Log("Unloaded!");
        }

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                {"gun","{0} was shot in the {1} by {2} using a {3}"},
                {"food","{0} starved to death!"},
                {"arena","{0} was eliminated by the arena!"},
                {"shred","{0} was shreaded to bits!"},
                {"punch","{0} was punched in the {1} by {2}"},
                {"bones","{0} shattered his bones and died!"},
                {"melee","{0} was chopped to death in the {1} by {2} using a {3}"},
                {"water","{0} dehydrated to death!"},
                {"breath","{0} suffocated to death!"},
                {"zombie","{0} was mauled by a zombie!"},
                {"animal","{0} was mauled by an animal!"},
                {"grenade","{0} was blown up by {1} with a grenade!"},
                {"vehicle","{0} was blown up by a vehicle!"},
                {"suicide","{0} died at their own hand. Everyone is disappointed."},
                {"burning","{0} burned to death!"},
                {"landmine","{0} was blown up by a landmine!"},
                {"roadkill","{0} was roadkilled by {1}"},
                {"bleeding","{0} bled to death!"},
                {"freezing","{0} froze to death!"},
                {"sentry","{0} was shot by a sentry gun!"},
                {"charge","{0} was obliterated by {1} with a remote detonator!"},
                {"missile","{0} was annihilated by {1} with a missile!"},
                {"splash","{0} was blown up by {1} with an explosive bullet!"},
                {"acid","{0} was blown up by a zombie!"},
                {"spark", "{0} was electrocuted by a zombie!"},
                {"infection", "{0} died of infection"},
                {"spit","{0} was dissolved by a zombie!"},
                {"kill","{0} was killed by an admin!"},
                {"boulder","{0} was crushed by a zombie using a big boulder!"},
                {"left_foot", "left foot"},
                {"left_leg", "left leg"},
                {"right_foot", "right foot"},
                {"right_leg", "right leg"},
                {"left_hand", "left hand"},
                {"left_arm", "left arm"},
                {"right_hand", "right hand"},
                {"right_arm", "right arm"},
                {"left_back", "back"},
                {"right_back", "back"},
                {"left_front", "torso"},
                {"right_front", "torso"},
                {"spine", "spine"},
                {"skull", "head"},
            };

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            var deathMessageColor = Color.red;

            var killer = UnturnedPlayer.FromCSteamID(murderer);

            if (cause.ToString() == "SHRED" || cause.ToString() == "ZOMBIE" || cause.ToString() == "ANIMAL" ||
                cause.ToString() == "SPARK" || cause.ToString() == "VEHICLE" || cause.ToString() == "FOOD" ||
                cause.ToString() == "WATER" || cause.ToString() == "INFECTION" || cause.ToString() == "BLEEDING" ||
                cause.ToString() == "LANDMINE" || cause.ToString() == "BREATH" || cause.ToString() == "KILL" ||
                cause.ToString() == "FREEZING" || cause.ToString() == "SENTRY" || cause.ToString() == "CHARGE" ||
                cause.ToString() == "MISSILE" || cause.ToString() == "BONES" || cause.ToString() == "SPLASH" ||
                cause.ToString() == "ACID" || cause.ToString() == "SPIT" || cause.ToString() == "BURNING" ||
                cause.ToString() == "BURNER" || cause.ToString() == "BOULDER" || cause.ToString() == "ARENA" ||
                cause.ToString() == "GRENADE" ||
                (Configuration.Instance.ShowSuicideMSG && cause.ToString() == "SUICIDE") ||
                cause.ToString() == "ROADKILL" || cause.ToString() == "MELEE" || cause.ToString() == "GUN" ||
                cause.ToString() == "PUNCH")
            {
                if (cause.ToString() != "ROADKILL" && cause.ToString() != "MELEE" && cause.ToString() != "GUN" &&
                    cause.ToString() != "PUNCH" && cause.ToString() != "GRENADE" && cause.ToString() != "MISSILE" &&
                    cause.ToString() != "CHARGE" && cause.ToString() != "SPLASH")
                {
                    UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName), deathMessageColor);
                }
                else switch (cause.ToString())
                {
                    case "ROADKILL":
                    case "MISSILE":
                    case "CHARGE":
                    case "SPLASH":
                        UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName, killer.DisplayName), deathMessageColor);
                        break;
                    case "PUNCH":
                        UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName, Translate(limb.ToString().ToLower()), killer.DisplayName), deathMessageColor);
                        break;
                    case "MELEE":
                    case "GUN":
                        var limbString = Translate(limb.ToString().ToLower());
                        UnturnedChat.Say(Translate(cause.ToString().ToLower(), player.DisplayName, limbString, killer.DisplayName, killer.Player.equipment.asset.itemName), deathMessageColor);
                        break;
                }

                return;
            }

            if (Translate(cause.ToString().ToLower()) != null)
            {
                UnturnedChat.Say(
                    Translate(cause.ToString().ToLower()).Contains("{1}")
                        ? Translate(cause.ToString().ToLower(), player.DisplayName, killer.DisplayName)
                        : Translate(cause.ToString().ToLower(), player.DisplayName), deathMessageColor);

                return;
            }

            Logger.LogError("Please add translation for " + cause +
                            " | Parameters for custom translation: {0} = Player , {1} = Killer");
        }
    }
}
