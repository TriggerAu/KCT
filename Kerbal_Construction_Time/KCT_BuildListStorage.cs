﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Kerbal_Construction_Time
{
    public class KCT_BuildListStorage:ConfigNodeStorage
    {
        [Persistent]
        List<BuildListItem> VABBuildList = new List<BuildListItem>();
        [Persistent]
        List<BuildListItem> SPHBuildList = new List<BuildListItem>();
        [Persistent]
        List<BuildListItem> VABWarehouse = new List<BuildListItem>();
        [Persistent]
        List<BuildListItem> SPHWarehouse = new List<BuildListItem>();

        [Persistent]KCT_Reconditioning LPRecon;


        public override void OnDecodeFromConfigNode()
        {
            KCT_GameStates.VABList.Clear();
            KCT_GameStates.SPHList.Clear();
            KCT_GameStates.VABWarehouse.Clear();
            KCT_GameStates.SPHWarehouse.Clear();

            foreach (BuildListItem b in VABBuildList)
            {
                KCT_BuildListVessel blv = b.ToBuildListVessel();
                //if (ListContains(blv, KCT_GameStates.VABList) < 0)
                    KCT_GameStates.VABList.Add(blv);
            }
            foreach (BuildListItem b in SPHBuildList)
            {
                KCT_BuildListVessel blv = b.ToBuildListVessel();
                //if (ListContains(blv, KCT_GameStates.SPHList) < 0)
                    KCT_GameStates.SPHList.Add(blv);
            }
            foreach (BuildListItem b in VABWarehouse)
            {
                KCT_BuildListVessel blv = b.ToBuildListVessel();
               // if (ListContains(blv, KCT_GameStates.VABWarehouse) < 0)
                    KCT_GameStates.VABWarehouse.Add(blv);
            }
            foreach (BuildListItem b in SPHWarehouse)
            {
                KCT_BuildListVessel blv = b.ToBuildListVessel();
               // if (ListContains(blv, KCT_GameStates.SPHWarehouse) < 0)
                    KCT_GameStates.SPHWarehouse.Add(blv);
            }
            KCT_GameStates.LaunchPadReconditioning = LPRecon;
        }

        public override void OnEncodeToConfigNode()
        {
            VABBuildList.Clear();
            SPHBuildList.Clear();
            VABWarehouse.Clear();
            SPHWarehouse.Clear();
            foreach (KCT_BuildListVessel b in KCT_GameStates.VABList)
            {
                if (b.shipNode == null)
                {
                    Debug.LogError("[KCT] WARNING! DATA LOSS EVENT ON " + b.shipName + " IN VABList");
                    continue;
                }
                BuildListItem bls = new BuildListItem();
                bls.FromBuildListVessel(b);
                VABBuildList.Add(bls);
            }
            foreach (KCT_BuildListVessel b in KCT_GameStates.SPHList)
            {
                if (b.shipNode == null)
                {
                    Debug.LogError("[KCT] WARNING! DATA LOSS EVENT ON " + b.shipName + " IN SPHList");
                    continue;
                }
                BuildListItem bls = new BuildListItem();
                bls.FromBuildListVessel(b);
                SPHBuildList.Add(bls);
            }
            foreach (KCT_BuildListVessel b in KCT_GameStates.VABWarehouse)
            {
                if (b.shipNode == null)
                {
                    Debug.LogError("[KCT] WARNING! DATA LOSS EVENT ON " + b.shipName + " IN VABWarehouse");
                    continue;
                }
                BuildListItem bls = new BuildListItem();
                bls.FromBuildListVessel(b);
                VABWarehouse.Add(bls);
            }
            foreach (KCT_BuildListVessel b in KCT_GameStates.SPHWarehouse)
            {
                if (b.shipNode == null)
                {
                    Debug.LogError("[KCT] WARNING! DATA LOSS EVENT ON " + b.shipName + " IN SPHWarehouse");
                    continue;
                }
                BuildListItem bls = new BuildListItem();
                bls.FromBuildListVessel(b);
                SPHWarehouse.Add(bls);
            }
            LPRecon = KCT_GameStates.LaunchPadReconditioning;
        }

        public class BuildListItem
        {
            [Persistent]
            string shipName, shipID;
            [Persistent]
            double progress, buildTime;
            [Persistent]
            String launchSite, flag;
            [Persistent]
            List<string> InventoryParts;
            [Persistent]
            bool cannotEarnScience;
            [Persistent]
            float cost = 0;

            public KCT_BuildListVessel ToBuildListVessel()
            {
                KCT_BuildListVessel ret = new KCT_BuildListVessel(shipName, launchSite, buildTime, flag, cost);
                ret.progress = progress;
                if (InventoryParts != null)
                    ret.InventoryParts = InventoryParts;
                ret.id = new Guid(shipID);
                ret.cannotEarnScience = cannotEarnScience;
                return ret;
            }

            public BuildListItem FromBuildListVessel(KCT_BuildListVessel blv)
            {
                this.progress = blv.progress;
                this.buildTime = blv.buildPoints;
                this.launchSite = blv.launchSite;
                this.flag = blv.flag;
                //this.shipURL = blv.shipURL;
                this.shipName = blv.shipName;
                this.InventoryParts = blv.InventoryParts;
                this.shipID = blv.id.ToString();
                this.cannotEarnScience = blv.cannotEarnScience;
                this.cost = blv.cost;
                
                return this;

            }
        }
    }
}
/*
Copyright (C) 2014  Michael Marvin, Zachary Eck

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/