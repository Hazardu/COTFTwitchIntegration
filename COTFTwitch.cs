using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModAPI.Attributes;
using ModAPI;
using ChampionsOfForest;
using ChampionsOfForest.Player;
using ModAPITwitchIntegration;
using TheForest.Utils;
using ChampionsOfForest.Enemies.EnemyAbilities;
using ChampionsOfForest.Effects;

namespace COTFTwitchIntegration
{
    public class COTFTwitch
    {

        static int[] positiveBuffs = new int[] { 4, 5, 9, 15, 14 };
        static float[] positiveBuffs_amounts = new float[] { 1, 1.35f, 1.35f, 1000, 1.35f };

        static int[] negativeBuffs = new int[] { 1,2,3,10,18,21 };
        static float[] negativeBuffs_amounts = new float[] { 0.6f, 0.6f, 25,0.5f , 100,2000};

        [ExecuteOnGameStart]
        public static void Init()
        {
            TwitchMod.Register("randomitem", s => ChampionsOfForest.Network.NetworkManager.SendItemDrop(ItemDataBase.GetRandomItem(Random.value * 2000), LocalPlayer.Transform.position + Vector3.up * 3f));
            TwitchMod.Register("buff", s =>
            {
                int r = Random.Range(0, positiveBuffs.Length);
                BuffDB.AddBuff(positiveBuffs[r], 90000, positiveBuffs_amounts[r], 10);
            });
            TwitchMod.Register("debuff", s =>
            {
                int r = Random.Range(0, negativeBuffs.Length);
                BuffDB.AddBuff(negativeBuffs[r], 90001, negativeBuffs_amounts[r], 10);
            });
            TwitchMod.Register("meteors", s => Meteor.CreateEnemy(LocalPlayer.Transform.position,Random.Range(0,1000000)));
            TwitchMod.Register("laser", s => EnemyLaser.CreateLaser(LocalPlayer.Transform.position-LocalPlayer.Transform.forward * 10,LocalPlayer.Transform.forward));
            TwitchMod.Register("cataclysm", s => Cataclysm.Create(LocalPlayer.Transform.position,7,100,5, Cataclysm.TornadoType.Fire,true));

        }
    }
}
