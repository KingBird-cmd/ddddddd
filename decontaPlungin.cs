using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;

namespace DecontaPlugin
{
    public class DecontaPlugin : Plugin<Config>
    {
        public override string Name => "DecontaPlugin";
        public override string Author => "VotreNom";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
            base.OnEnabled();
            Exiled.Events.Handlers.Server.SendingConsoleCommand += OnSendingConsoleCommand;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.SendingConsoleCommand -= OnSendingConsoleCommand;
            base.OnDisabled();
        }

        private void OnSendingConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            if (ev.Name.ToLower() == "deconta" && ev.Player.Role == RoleType.NtfCaptain && ev.Player.CurrentItem.id == ItemType.Radio)
            {
                // Vérifie si le joueur a les permissions pour exécuter cette commande
                if (!ev.Player.CheckPermission("deconta.use"))
                {
                    ev.ReturnMessage = "Vous n'avez pas la permission d'utiliser cette commande.";
                    return;
                }

                Map.StartDecontamination();
                ev.ReturnMessage = "La décontamination de la LCZ a été lancée.";
            }
        }
    }

    public class Config : PluginConfig
    {
        public override bool IsEnabled { get; set; } = true;
    }
}
