﻿namespace Tako.NBT.Tags;

/// <summary>
/// A single signed byte
/// </summary>
public class ByteTag : Tag
{
    /// <summary>
    /// The value.
    /// </summary>
    public sbyte Value { get; set; }

    /// <summary>
    /// Creates a new byte tag.
    /// </summary>
    /// <param name="name">The name.</param>
    public ByteTag(string name) 
        : base(TagId.Byte, name)
    {
    }

    /// <inheritdoc/>
    public override object? GetValue()
    {
        return Value;
    }

    /// <inheritdoc/>
    internal override Tag Parse(BinaryReader reader)
    {
        Value = reader.ReadSByte();
        return this;
    }
}