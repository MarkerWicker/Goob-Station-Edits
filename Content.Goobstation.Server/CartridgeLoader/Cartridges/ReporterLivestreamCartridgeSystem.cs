using Content.Goobstation.Shared.CartridgeLoader;
using Content.Goobstation.Shared.CartridgeLoader.Cartridges;
using Content.Goobstation.Server.CartridgeLoader.Cartridges;
using Content.Server.Administration.Logs;
using Content.Server.CartridgeLoader;
using Content.Server.CartridgeLoader.Cartridges;
using Content.Shared.CartridgeLoader;
using Content.Shared.Kitchen.Components;
using Content.Shared.CartridgeLoader.Cartridges;
using Content.Server.SurveillanceCamera;
using System.Linq;
using Content.Shared.SurveillanceCamera;
using Content.Shared.PDA;

namespace Content.Goobstation.Server.CartridgeLoader.Cartridges;

public sealed class ReporterLivestreamCartridgeSystem : EntitySystem
{
    [Dependency] private readonly CartridgeLoaderSystem _cartridgeLoaderSystem = default!;
    [Dependency] private readonly SurveillanceCameraMonitorSystem _monitorSystem = default!;
    [Dependency] private readonly Robust.Server.GameObjects.UserInterfaceSystem _userInterface = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ReporterLivestreamCartridgeComponent, CartridgeUiReadyEvent>(OnUiReady);

        SubscribeLocalEvent<ReporterLivestreamCartridgeComponent, CartridgeAddedEvent>(OnCartridgeAdded);
        SubscribeLocalEvent<ReporterLivestreamCartridgeComponent, CartridgeRemovedEvent>(OnCartridgeRemoved);
    }

    private void OnUiReady(EntityUid uid, ReporterLivestreamCartridgeComponent component, CartridgeUiReadyEvent args)
    {
        UpdateUiState(uid, args.Loader, component);
    }

    private void UpdateUiState(EntityUid uid, EntityUid loaderUid, ReporterLivestreamCartridgeComponent? component)
    {
        if (!Resolve(uid, ref component))
            return;

        var state = new SharedReporterLivestreamUiState(Comp<SurveillanceCameraMonitorComponent>(uid).ActiveSubnet);
        _cartridgeLoaderSystem?.UpdateCartridgeUiState(loaderUid, state);
    }

    /// <summary>
    /// Adds TV related components to the user's PDA when this cartridge is inserted.
    /// </summary>
    private void OnCartridgeAdded(EntityUid uid, ReporterLivestreamCartridgeComponent component, ref CartridgeAddedEvent args)
    {
        EnsureComp<SurveillanceCameraSpeakerComponent>(args.Loader);
    }

    /// <summary>
    /// Removes TV related components from the user's PDA when the reporter livestream app is uninstalled.
    /// </summary>
    private void OnCartridgeRemoved(EntityUid uid, ReporterLivestreamCartridgeComponent component, ref CartridgeRemovedEvent args)
    {

        if (!_cartridgeLoaderSystem.HasProgram<ReporterLivestreamCartridgeComponent>(args.Loader))
        {
            RemComp<SurveillanceCameraSpeakerComponent>(args.Loader);
        }
    }

}
