﻿using ArmorMaster.Buisiness.DTO.ModelsDTO;
using ArmorMaster.Buisiness.DTO.ModelsDTO.ConstantsModels;
using ArmorMaster.Buisiness.Exceptions;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class ConstantsService : IConstantsService
    {
        
        
        

        private readonly List<ItemStatCost> availiableItemStatCosts = new List<ItemStatCost>()
            {
                new ItemStatCost() {StatType ="Critical Chance" , StatAmount =0.02 , StatCost = 10},
                new ItemStatCost() {StatType ="Health", StatAmount = 20 , StatCost = 10},
                new ItemStatCost() {StatType ="Constitution", StatAmount = 1 , StatCost = 10},
                new ItemStatCost() {StatType ="Strength", StatAmount = 1 , StatCost = 10},
                new ItemStatCost() {StatType ="Agility", StatAmount = 1, StatCost = 10},
            };


        private readonly List<ItemType> availibleItemTypes = new List<ItemType>()
        {
            new ItemType() {Type = "Helmet" , BaseStatType = "Defence", BaseStatInitialValue = 25},
            new ItemType() {Type = "Arms" , BaseStatType = "Defence", BaseStatInitialValue = 25},
            new ItemType() {Type = "Legs" , BaseStatType = "Defence", BaseStatInitialValue = 25},
            new ItemType() {Type = "Chest" , BaseStatType = "Defence", BaseStatInitialValue = 25},
            new ItemType() {Type = "Weapon" , BaseStatType = "Damage", BaseStatInitialValue = 60},
            new ItemType() {Type = "Ring" , BaseStatType = "Constitution", BaseStatInitialValue = 20},
            new ItemType() {Type = "Talisman" , BaseStatType = "Damage", BaseStatInitialValue = 40},
            new ItemType() {Type = "Cloak" , BaseStatType = "Impact Damage Percent", BaseStatInitialValue = 5},
            new ItemType() {Type = "Belt" , BaseStatType = "Defence", BaseStatInitialValue = 25}

        };
        

        

        private readonly List<ItemPotentialModel> ItemsPotentialsList = new List<ItemPotentialModel>()
        {
            new ItemPotentialModel(){Level = 1 , Potential = 200 },
            new ItemPotentialModel(){Level = 20 , Potential = 500 },
            new ItemPotentialModel(){Level = 40 , Potential = 1200 },
            new ItemPotentialModel(){Level = 60 , Potential = 2000 },
            new ItemPotentialModel(){Level = 80 , Potential = 4000 },
            new ItemPotentialModel(){Level = 100 , Potential = 10000 }
        };
        public IEnumerable<int> GetAvailiableItemLevels()
        {
            return ItemsPotentialsList.Select( x => x.Level);
        }

        public IEnumerable<ItemStatCost> GetAvailiableItemStatCosts()
        {
            return availiableItemStatCosts;
        }

        public IEnumerable<ItemType> GetAvailiableItemTypes()
        {
            return availibleItemTypes;
        }

        public IEnumerable<string> GetAvailiableItemStatTypes()
        {
            return availiableItemStatCosts.Select(x => x.StatType);
        }

        public int GetPotentialByItemLvlAndItemType(int itemLvl, string itemType)
        {
            int currentItemsLvl = GetAvailiableItemLevels().Where(x => x.Equals(itemLvl)).FirstOrDefault();
            if (String.IsNullOrEmpty(currentItemsLvl.ToString()))
            {
                throw new InvalidItemLevelException();
            }
            string currentItemType = GetAvailiableItemTypes().Select(x => x.Type).Where(x => x.Equals(itemType)).FirstOrDefault();
            if (currentItemType == null)
            {
                throw new InvalidItemTypeException();
            }
            int itemsBasePotential = ItemsPotentialsList.Where(x => x.Level.Equals(currentItemsLvl)).Select(x => x.Potential).FirstOrDefault();

            return CalculatePotential(itemsBasePotential, currentItemType);
            
        }

        public bool ItemLevelIsValid(int lvl)
        {
            return GetAvailiableItemLevels().Contains(lvl);
        }

        public bool ItemTypeExists(string type)
        {
            return GetAvailiableItemTypes().Select(x => x.Type).Contains(type);
        }

        private int CalculatePotential(int itemsBasePotential , string currentItemType)
        {
            
            return itemsBasePotential;
        }
    }
}
