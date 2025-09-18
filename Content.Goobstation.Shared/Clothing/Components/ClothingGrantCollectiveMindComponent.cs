using Robust.Shared.Prototypes;
using Content.Shared._Starlight.CollectiveMind;

namespace Content.Goobstation.Shared.Clothing.Components
{
    [RegisterComponent]
    public sealed partial class ClothingGrantCollectiveMindComponent : Component
    {
        [DataField("minds", required: true)]
        public List<ProtoId<CollectiveMindPrototype>> Minds = new();
        [DataField]
        public ProtoId<CollectiveMindPrototype>? defaultChannel = null;
    }
}
