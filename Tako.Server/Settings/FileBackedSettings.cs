﻿using Tako.Common.Logging;
using Tako.Definitions.Settings;
using Tako.Server.Logging;

namespace Tako.Server.Settings;

/// <summary>
/// The server settings (usually stored in server.properties).
/// </summary>
public class FileBackedSettings : ISettings
{
	/// <summary>
	/// The comment character
	/// </summary>
	private const char COMMENT = '#';

	/// <summary>
	/// The separator equals.
	/// </summary>
	private const char EQUALS = '=';

	/// <summary>
	/// The store for all the settings.
	/// </summary>
	private readonly Dictionary<string, string> _store;

	/// <summary>
	/// The path.
	/// </summary>
	private readonly string _path;

	/// <summary>
	/// The logger.
	/// </summary>
	private readonly ILogger<FileBackedSettings> _logger = LoggerFactory<FileBackedSettings>.Get();

	/// <summary>
	/// Creates a new settings instance with the path to the settings file.
	/// </summary>
	/// <param name="path">The path to the file.</param>
	/// <param name="defaultsCreator">The function that defines the defaults for this settings instance.</param>
	public FileBackedSettings(
		string path, 
		Action<ISettings> defaultsCreator)
	{
		_path = path;
		_store = new Dictionary<string, string>();

		if (!File.Exists(path))
			LoadDefaults(defaultsCreator);
		else
			LoadFromFile();

		_logger.Info($"Loaded configuration file '{path}'.");
	}

	/// <inheritdoc/>
	public string? Get(string key)
	{
		if (_store.TryGetValue(key, out var value))
			return value;

		return null;
	}

	/// <inheritdoc/>
	public void Set(string key, string value)
	{
		_store[key] = value;
	}

	/// <inheritdoc/>
	public void Save()
	{
		using var fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		using var sw = new StreamWriter(fs);

		sw.WriteLine($"{COMMENT} Generated by Tako (https://github.com/naomiEve/Tako)");
		sw.WriteLine($"{COMMENT} For more information, visit the GitHub page.");
		sw.WriteLine();

		foreach (var pair in _store)
			sw.WriteLine($"{pair.Key}{EQUALS}{pair.Value}");
	}

	/// <summary>
	/// Loads the default values.
	/// </summary>
	/// <param name="defaultsCreator">The function responsible for setting all the defaults.</param>
	private void LoadDefaults(Action<ISettings> defaultsCreator)
	{
		defaultsCreator(this);

		Save();
	}

	/// <summary>
	/// Loads the settings from file.
	/// </summary>
	private void LoadFromFile()
	{
		// We only need to split into two substrings, the key and the value.
		const int maxSubstrings = 2;

		using var fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
		using var sr = new StreamReader(fs);

		while (!sr.EndOfStream)
		{
			var line = sr.ReadLine();
			if (line is null)
				continue;

			if (line.StartsWith(COMMENT))
				continue;

			var split = line.Split(EQUALS, maxSubstrings);
			if (split.Length < 2)
				continue;

			Set(split[0], split[1]);
		}
	}
}
