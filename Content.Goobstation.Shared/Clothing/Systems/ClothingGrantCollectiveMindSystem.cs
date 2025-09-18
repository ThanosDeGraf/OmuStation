using Content.Goobstation.Shared.Clothing.Components;
using Content.Shared.Clothing.Components;
using Content.Shared.Inventory.Events;
using Robust.Shared.Serialization.Manager;
using Content.Shared._Starlight.CollectiveMind;

namespace Content.Goobstation.Shared.Clothing.Systems;

/// <summary>
/// Basically the same as <see cref="ClothingGrantingSystem"/>, but specifically to handle items granting access to hivemind channels like binary.
/// </summary>
public sealed class ClothingGrantCollectiveMindSystem : EntitySystem
{
    [Dependency] private readonly IComponentFactory _componentFactory = default!;
    [Dependency] private readonly ISerializationManager _serializationManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ClothingGrantCollectiveMindComponent, GotEquippedEvent>(OnCompEquip);
        SubscribeLocalEvent<ClothingGrantCollectiveMindComponent, GotUnequippedEvent>(OnCompUnequip);
    }

    private void OnCompEquip(EntityUid uid, ClothingGrantCollectiveMindComponent comp, GotEquippedEvent args)
    {
        if (!TryComp<ClothingComponent>(uid, out var clothing))
            return;

        if (!clothing.Slots.HasFlag(args.SlotFlags))
            return;

        var mind = EnsureComp<CollectiveMindComponent>(args.Equipee);
        foreach (var channel in comp.Minds)
        {
            mind.Channels.Add(channel);
        }

        if (comp.defaultChannel is null)
        {
            mind.DefaultChannel = comp.defaultChannel;
        }
    }


    private void OnCompUnequip(EntityUid uid, ClothingGrantCollectiveMindComponent component, GotUnequippedEvent args)
    {
        var mind = EnsureComp<CollectiveMindComponent>(args.Equipee);
        foreach (var channel in component.Minds)
        {
            mind.Channels.Remove(channel);
        }
    }
}
