﻿using Tako.Common.Logging;
using Tako.Common.Numerics;
using Tako.Definitions.Game.World;
using Tako.Definitions.Network.Connections;
using Tako.Server.Logging;
using Tako.Server.Network.Packets.Server;

namespace Tako.Server.Game.World;

/// <summary>
/// A base for any world.
/// </summary>
public class BaseWorld : IWorld
{
	/// <summary>
	/// The world data.
	/// </summary>
	private byte[] _worldData;

	/// <summary>
	/// The dimensions of the world.
	/// </summary>
	private Vector3Int _dimensions;

	/// <summary>
	/// The logger.
	/// </summary>
	private ILogger<BaseWorld> _logger = LoggerFactory<BaseWorld>.Get();

	/// <summary>
	/// The base world data.
	/// </summary>
	/// <param name="dimensions">The dimensions of the world.</param>
	public BaseWorld(Vector3Int dimensions)
	{
		_worldData = new byte[dimensions.X * dimensions.Y * dimensions.Z];
		_dimensions = dimensions;
	}

	/// <inheritdoc/>
	public byte GetBlock(Vector3Int pos)
	{
		var i = pos.X + pos.Z * _dimensions.X + pos.Y * _dimensions.X * _dimensions.Z;
		return _worldData[i];
	}

	/// <inheritdoc/>
	public void SetBlock(Vector3Int pos, byte block)
	{
		var i = pos.X + pos.Z * _dimensions.X + pos.Y * _dimensions.X * _dimensions.Z;
		_worldData[i] = block;
	}

	/// <inheritdoc/>
	public void Simulate()
	{

	}

	/// <inheritdoc/>
	public void StreamTo(IConnection conn)
	{
		new WorldStreamer()
			.ToConnection(conn)
			.WithWorldDimensions(_dimensions)
			.WithBlockArray(_worldData)
			.Stream()
			.Finish();
	}
}
