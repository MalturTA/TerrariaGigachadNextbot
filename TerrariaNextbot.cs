using System.ComponentModel;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using On.Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerrariaNextbot
{
	public class TerrariaNextbot : Mod
	{
		// public override void Load()
		// {
		// 	var ambianceLib = ModLoader.GetMod("TerrariaAmbienceAPI");
		// 	base.Load();
		// }
	}
	class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("Spawning")]
		[Label("Allow spawning naturally")]
		[BackgroundColor(128, 0, 32, 128)]
		[DefaultValue(true)]
		[Tooltip("Sometimes can be scary, and/or annoying\nDefault Value: On")]
		public bool SpawnNaturally;

		[Label("Chance of spawning naturally")]
		[BackgroundColor(128, 0, 32, 128)]
		[DefaultValue(0.1f)]
		[Tooltip("How frequent Nextbot will spawn naturally. Values above 0.5 are dangerous!\nDefault value: 0.1")]
		public float SpawnChance;
		
		[Label("Spawn with player")]
		[BackgroundColor(128, 0, 32, 128)]
		[DefaultValue(false)]
		[Tooltip("When player respawns, Nextbot will spawn immediately\nDefault Value: Off")]
		public bool SpawnWithPlayer;

		[Header("Audio")]
		[Label("Enable audio")]
		[BackgroundColor(128, 0, 32, 128)]
		[DefaultValue(true)]
		[Tooltip("Enables audio playback from Nextbot\nDefault Value: On")]
		public bool EnableAudio;
		
		[Label("Volume")]
		[BackgroundColor(128, 0, 32, 128)]
		[DefaultValue(0.9f)]
		[Tooltip("How loud music from Nextbot will be\nDefault Value: 0.9")]
		public float SoundVolume;

	}}