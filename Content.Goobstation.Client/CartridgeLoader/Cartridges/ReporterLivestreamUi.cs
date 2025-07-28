using System.Linq;
using System.Reflection.PortableExecutable;
using Content.Client.UserInterface.Fragments;
using Content.Goobstation.Shared.CartridgeLoader;
using Content.Goobstation.Shared.CartridgeLoader.Cartridges;
using Content.Shared.Info;
using Content.Shared.SurveillanceCamera;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Goobstation.Client.CartridgeLoader.Cartridges;

/// <summary>
/// Class which displays the main UI for the reporter livestream app.
/// </summary>
public sealed partial class ReporterLivestreamUi : UIFragment
{
    private ReporterLivestreamUiFragment? _fragment;

    public override Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        _fragment = new ReporterLivestreamUiFragment();

        _fragment.SubnetRefresh += () => OnSubnetRefresh(userInterface);

    }

    public override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is SharedReporterLivestreamUiState cast)
            _fragment?.UpdateState(cast);
    }

    private void OnSubnetRefresh(BoundUserInterface userInterface)
    {
        userInterface.SendMessage(new SurveillanceCameraRefreshSubnetsMessage());
    }

}