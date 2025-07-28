using Content.Shared.CartridgeLoader;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class ReporterLivestreamUIMessageEvent : CartridgeMessageEvent
{
    public readonly ReporterLivestreamUIAction Action;

    public ReporterLivestreamUIMessageEvent(ReporterLivestreamUIAction action)
    {
        Action = action;
    }

}

[Serializable, NetSerializable]
public enum ReporterLivestreamUIAction
{
    RefreshSubnet
}