using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaNextbot.NPCs
{
	public class Gigachad : ModNPC
	{
		Mod ambianceLib = ModLoader.GetMod("TerrariaAmbienceAPI");
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gigachad");

			Main.npcFrameCount[Type] = 1; 

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
			
			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData {
				SpecificallyImmuneTo = new int[] {
					BuffID.Poisoned,

					BuffID.Confused // Most NPCs have this
				}
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
		}
		
		public override void SetDefaults() {
			NPC.width = 100;
			NPC.height = 100;
			NPC.damage = 240;
			NPC.defense = 60;
			NPC.lifeMax = 10000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 60f;
			NPC.knockBackResist = 0.0f;
			NPC.aiStyle = -1; //5;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.ModNPC.Music = 5; //This is correct, but legacy way to set music. NOT WORKING.
			
			// AIType = NPCID.MeteorHead; 
			AIType = -1;
			// AnimationType = NPCID.MeteorHead; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
			// Banner = Item.NPCtoBanner(NPCID.Zombie); // Makes this NPC get affected by the normal zombie banner.
			// BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
			// SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false; //base.DrawHealthBar(hbPosition, ref scale, ref position);
		}

		// private SlotId soundId;
		public override void OnSpawn(IEntitySource source)
		{
			// Mod ambianceLib = ModLoader.GetMod("TerrariaAmbienceAPI");
			if (ambianceLib != null && false) // Adding false, until i figure out how to make it work.
			{ // Try using AmbienceAPI
				// I don't know how to do it properly, i'm too new to dependency management in tModLoader.
				
				// There must be boolean "playWhen" function, to make it work.
				// Passing "true" does not work. Easy way failed miserably!
				ambianceLib.Call($"{nameof(TerrariaNextbot)}/SFX/Gigachad",0.9f,0.1f,true);
			}
			else
			{
				var sound = new SoundStyle($"{nameof(TerrariaNextbot)}/SFX/Gigachad") with
				{
					Volume = 0.9f,
					MaxInstances = 1
				};
				SoundEngine.PlaySound(sound);
			}
			base.OnSpawn(source);
		}

		public override void OnKill()
		{
			// var snd = SoundEngine.TryGetActiveSound(default, out result);
			SoundEngine.SoundPlayer.StopAll();
		
			base.OnKill();
		}

		private int time = 0;
		public override void AI()
		{
			time++;
			Player target = Main.player[NPC.target];
			Vector2 toPlayer = NPC.DirectionTo(target.Center) * 5;//Get the direction to the player.
			
			if (target.dead && time % 180 == 0)
			{
				// Stop music every 3 seconds, if player died.
				SoundEngine.SoundPlayer.StopAll();
			}
			if (!target.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity = toPlayer;//set our velocity to direction to the player, so we move towards it.
			}
			else NPC.TargetClosest(false);

			/*
			* Here must be another check
			* If no custom sound plays, start playing
			* But i don't know how to check reliably for that
			* So, any help is appreciated.
			*/

			// ambianceLib.Call($"{nameof(TerrariaNextbot)}/SFX/Gigachad",0.9f,0.1f,true);
			
			if (time >= 13680) // Play sound, after finishing last one
			{
				time = 0; // Reset time upon reaching end of the sound.
				var sound = new SoundStyle($"{nameof(TerrariaNextbot)}/SFX/Gigachad") with
				{
					Volume = 0.9f,
					MaxInstances = 1
				};
				SoundEngine.PlaySound(sound);
			}
			base.AI();
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			// return SpawnCondition.OverworldNightMonster.Chance * 0.2f; // Spawn with 1/5th the chance of a regular zombie.
			return 0.1f;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				// BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("This is what peak-performance male body looks like.")
			});
		}

		// public override void HitEffect(int hitDirection, double damage) {
		// 	// Spawn confetti when this zombie is hit.
		//
		// 	for (int i = 0; i < 10; i++) {
		// 		int dustType = Main.rand.Next(139, 143);
		// 		var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);
		//
		// 		dust.velocity.X += Main.rand.NextFloat(-0.05f, 0.05f);
		// 		dust.velocity.Y += Main.rand.NextFloat(-0.05f, 0.05f);
		//
		// 		dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
		// 	}
		// }

		// public override void OnHitPlayer(Player target, int damage, bool crit) {
		// 	// Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
		// 	// Common use is applying buffs/debuffs:
		//
		// 	int buffType = ModContent.BuffType<AnimatedBuff>();
		// 	// Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
		//
		// 	int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
		// 	target.AddBuff(buffType, timeToAdd);
		// }
	}
}
