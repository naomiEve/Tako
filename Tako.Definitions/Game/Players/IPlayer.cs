﻿using System.Numerics;
using Tako.Definitions.Network;
using Tako.Definitions.Network.Connections;

namespace Tako.Definitions.Game.Players;

/// <summary>
/// A player.
/// </summary>
public interface IPlayer
{
	/// <summary>
	/// The player id.
	/// </summary>
	sbyte PlayerId { get; }

	/// <summary>
	/// This player's name.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Is this player the op?
	/// </summary>
	bool Op { get; }

	/// <summary>
	/// The position of the player.
	/// </summary>
	Vector3 Position { get; }

	/// <summary>
	/// The connection this player has.
	/// </summary>
	IConnection? Connection { get; }

	/// <summary>
	/// The server this player is attached to.
	/// </summary>
	IServer Server { get; }

	/// <summary>
	/// Sets the op status.
	/// </summary>
	/// <param name="op">The op status.</param>
	void SetOpStatus(bool op);

	/// <summary>
	/// Sets the position of this player.
	/// </summary>
	/// <param name="position">The position.</param>
	void SetPosition(Vector3 position);

	/// <summary>
	/// Spawns the player at the given position.
	/// </summary>
	/// <param name="position">The position.</param>
	void Spawn(Vector3 position);
}
