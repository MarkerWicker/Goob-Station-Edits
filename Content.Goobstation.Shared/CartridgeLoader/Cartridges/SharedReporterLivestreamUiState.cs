using Robust.Shared.Serialization;
using Content.Shared.SurveillanceCamera;

namespace Content.Goobstation.Shared.CartridgeLoader.Cartridges;

/// <summary>
/// Currently unused in favour of the existing SurveillanceCameraMonitorUiState
/// </summary>
[Serializable, NetSerializable]
public sealed class SharedReporterLivestreamUiState : BoundUserInterfaceState
{
    public readonly string ActiveSubnet;

    public SharedReporterLivestreamUiState(string activeSubnet)
    {
        ActiveSubnet = activeSubnet;
    }

}