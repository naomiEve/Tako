﻿using Tako.NBT.Serialization;

namespace Tako.NBT.Tags;

/// <summary>
/// A length-prefixed array of signed integers.
/// </summary>
public class IntArrayTag : Tag
{
    /// <summary>
    /// The values.
    /// </summary>
    public int[] Values { get; set; }

    /// <summary>
    /// Creates a new int array tag.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    public IntArrayTag(string name)
        : base(TagId.IntArray, name)
    {
        Values = Array.Empty<int>();
    }

    /// <inheritdoc/>
    public override object? GetValue()
    {
        return Values;
    }

    /// <inheritdoc/>
    internal override Tag Parse(NBTReader reader)
    {
        var length = reader.ReadInt32();
        Values = new int[length];

        for (var i = 0; i < length; i++)
            Values[i] = reader.ReadInt32();

        return this;
    }

    /// <inheritdoc/>
    internal override void Serialize(NBTWriter writer)
    {
        writer.WriteInt32(Values.Length);
        foreach (var val in Values)
            writer.WriteInt32(val);
    }
}
