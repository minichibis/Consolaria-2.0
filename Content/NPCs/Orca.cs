using Consolaria.Content.Items.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Consolaria.Content.NPCs
{
    public class Orca : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 4;

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Confused
                }
            };

            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults() {
            int width = 120; int height = 50;
            NPC.Size = new Vector2(width, height);

            NPC.damage = 50;
            NPC.lifeMax = 400;

            NPC.defense = 10;
            NPC.knockBackResist = 0.1f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.value = Item.buyPrice(silver: 12);
            NPC.noGravity = true;

            NPC.aiStyle = 16;
            AIType = NPCID.Shark;
            AnimationType = NPCID.Shark;


            NPC.buffImmune[BuffID.Confused] = true;
           // banner = NPC.type;
            //bannerItem = mod.ItemType("OrcaBanner");
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Once these ocean predators catch a whiff of blood, they become relentless and unstoppable in their ravenous pursuit.")
            });
        }

        public override void OnHitPlayer(Player target, int damage, bool crit) {
            if (Main.rand.Next(2) == 0)
                target.AddBuff(BuffID.Bleeding, 60 * 5);
        }

        public override void HitEffect(int hitDirection, double damage) {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
            if (NPC.life <= 0) {
                for (int k = 0; k < 20; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2.5f * hitDirection, -2.5f, 0, default(Color), 1f);
                
                Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Consolaria/Gore_490").Type, 1f);
                Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Consolaria/Gore_491").Type, 1f);
                Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Consolaria/Gore_492").Type, 1f);
                Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Consolaria/Gore_493").Type, 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            var sharksDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Shark, true);
            foreach (var sharkDropRule in sharksDropRules)
                npcLoot.Add(sharkDropRule);        
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoldenSeaweed>(), 15));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
            => SpawnCondition.OceanMonster.Chance * 0.33f;
    }
}