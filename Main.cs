using System.Collections.Generic;
using System.Drawing;
using Rocket.API.DependencyInjection;
using Rocket.API.Eventing;
using Rocket.API.User;
using Rocket.Core.I18N;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player.Events;

namespace coolpuppy24.simpledeathmessages
{
    public class Main : Plugin<SimpleDeathMessagesConfiguration>, IEventListener<UnturnedPlayerDeathEvent>
    {
        private readonly IUserManager _userManager;

        protected override void OnLoad(bool isFromReload)
        {
            Logger.LogInformation("Successfully Loaded!");
        }

        protected override void OnUnload()
        {
            Logger.LogInformation("Unloaded!");
        }

        public override Dictionary<string, string> DefaultTranslations => new Dictionary<string, string>
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

        public Main(IDependencyContainer container, IEventManager eventManager, IUserManager userManager) : base("SimpleDeathMessages", container)
        {
            _userManager = userManager;
            eventManager.AddEventListener(this, this);
        }

        public void HandleEvent(IEventEmitter emitter, UnturnedPlayerDeathEvent @event)
        {
            var player = (UnturnedPlayer)@event.Player;

            UnturnedPlayer killer = ((UnturnedPlayerEntity)@event.Killer).UnturnedPlayer;
            var cause = @event.DeathCause;
            var limb = @event.Limb;

            var deathmessageColor = Color.Green; //ConfigurationInstance.DeathMessagesColor;


            string headshot = Translations.Get("headshot");
            if (cause.ToString() == "SHRED" || cause.ToString() == "ZOMBIE" || cause.ToString() == "ANIMAL" ||
                cause.ToString() == "SPARK" || cause.ToString() == "VEHICLE" || cause.ToString() == "FOOD" ||
                cause.ToString() == "WATER" || cause.ToString() == "INFECTION" || cause.ToString() == "BLEEDING" ||
                cause.ToString() == "LANDMINE" || cause.ToString() == "BREATH" || cause.ToString() == "KILL" ||
                cause.ToString() == "FREEZING" || cause.ToString() == "SENTRY" || cause.ToString() == "CHARGE" ||
                cause.ToString() == "MISSILE" || cause.ToString() == "BONES" || cause.ToString() == "SPLASH" ||
                cause.ToString() == "ACID" || cause.ToString() == "SPIT" || cause.ToString() == "BURNING" ||
                cause.ToString() == "BURNER" || cause.ToString() == "BOULDER" || cause.ToString() == "ARENA" ||
                cause.ToString() == "GRENADE" ||
                (ConfigurationInstance.ShowSuicideMSG && cause.ToString() == "SUICIDE") ||
                cause.ToString() == "ROADKILL" || cause.ToString() == "MELEE" || cause.ToString() == "GUN" ||
                cause.ToString() == "PUNCH")
            {
                if (cause.ToString() != "ROADKILL" && cause.ToString() != "MELEE" && cause.ToString() != "GUN" &&
                    cause.ToString() != "PUNCH")
                {
                    _userManager.BroadcastLocalized(Translations, cause.ToString().ToLower(), deathmessageColor, player.DisplayName);
                }
                else if (cause.ToString() == "ROADKILL")
                {
                    _userManager.BroadcastLocalized(Translations, "roadkill", deathmessageColor, player.DisplayName, killer.DisplayName);
                }
                else if (cause.ToString() == "MELEE" || cause.ToString() == "GUN")
                {
                    if (limb == ELimb.SKULL)
                        _userManager.BroadcastLocalized(Translations, cause.ToString().ToLower() + "_headshot", deathmessageColor, player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset.itemName);
                    else
                        _userManager.BroadcastLocalized(Translations, cause.ToString().ToLower(), deathmessageColor, player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset.itemName);
                }
                else if (cause.ToString() == "PUNCH")
                {
                    _userManager.BroadcastLocalized(Translations,
                        limb == ELimb.SKULL ? "punch_headshot" : "punch", deathmessageColor, player.DisplayName, killer.DisplayName, headshot);
                }
            }
            else //No need to update the plugin later! (Just add the translation)
            {
                if (Translations.Get(cause.ToString().ToLower()) != null)
                {
                    if (Translations.Get(cause.ToString().ToLower()).Contains("{1}"))
                    {
                        _userManager.BroadcastLocalized(Translations, cause.ToString().ToLower(), deathmessageColor, player.DisplayName, killer.DisplayName, headshot);
                    }
                    else
                    {
                        _userManager.BroadcastLocalized(Translations, cause.ToString().ToLower(), deathmessageColor, player.DisplayName, headshot);
                    }
                }
                else
                {
                    Logger.LogError("Please add translation for " + cause +
                                    " | Parameters for custom translation: {0} = Player , {1} = Killer");
                }
            }
        }
    }
}
