﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Consolaria.Content.NPCs.Turkor
{
	public class TurkorNeck : ModNPC
	{
		private int neck = 0;
		private bool spawn = false;

		public override void SetStaticDefaults() => DisplayName.SetDefault("");

		public override void SetDefaults()
		{
			NPC.lifeMax = 1;
			NPC.knockBackResist = 0.5f;
			NPC.width = 18;
			NPC.height = 18;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.dontTakeDamage = true;
			NPC.netAlways = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.lavaImmune = true;
			NPC.alpha = 255;
			NPC.damage = 0;
		}

		public static Vector2 CenterPoint(Vector2 A, Vector2 B)
			=> new Vector2((A.X + B.X) / 2f, (A.Y + B.Y) / 2f);
		
		public static Vector2 CenterPoint1(Vector2 A, Vector2 B)
			=> new Vector2((A.X + B.X - 50) / 2f, (A.Y + B.Y + 20) / 2f);

        public override void HitEffect(int hitDirection, double damage) {
			if (NPC.life <= 0 || !NPC.active) {
				for (int k = 0; k < 5; k++) {
					int dust_ = Dust.NewDust(NPC.position, NPC.width, NPC.height, 26, 3f * hitDirection, -3f, 0, default(Color), NPC.scale);
					Main.dust[dust_].velocity *= 0.2f;
				}
			}
		}

		public override void AI() {
			NPC.TargetClosest(true);
			if (!spawn && NPC.localAI[0] > 0) {
				spawn = true;
				NPC.realLife = NPC.whoAmI;
				neck = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y + 20, ModContent.NPCType<TurkorNeck>(), NPC.whoAmI, 0, NPC.whoAmI);
				Main.npc[neck].localAI[0] = NPC.localAI[0] - 1;
				Main.npc[neck].ai[0] = NPC.whoAmI;
				Main.npc[neck].ai[1] = Main.npc[(int)NPC.ai[1]].whoAmI;
				Main.npc[neck].realLife = NPC.whoAmI;
			}
			NPC.alpha = Main.npc[(int)NPC.ai[0]].alpha;
			if (!Main.npc[(int)NPC.ai[0]].active) {
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
			}

			if (spawn && NPC.localAI[0] > 0)
				NPC.Center = CenterPoint(Main.npc[neck].Center, Main.npc[(int)NPC.ai[0]].Center);

			else if (NPC.localAI[0] <= 0) {
				for (int k = 0; k < Main.maxNPCs; k++) {
					if (Main.npc[k].type == ModContent.NPCType<TurkortheUngrateful>() && Main.npc[k].active)
						NPC.Center = CenterPoint1(Main.npc[k].Center, Main.npc[(int)NPC.ai[0]].Center);		
				}
			}
			NPC.direction = Main.player[NPC.target].Center.X < NPC.Center.X ? -1 : 1;
			NPC.rotation = (float)Math.Atan2(Main.npc[(int)NPC.ai[0]].Center.Y - NPC.Center.Y, Main.npc[(int)NPC.ai[0]].Center.X - NPC.Center.X) + 1.57f;
		}
	}
}