using ArmorMaster.Buisiness.DTO.ModelsDTO;
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
        private readonly double weaponPotentialMultipier = 1.5;
        private readonly double specialEquipmentPotentialMultiplier = 1.25;
        private readonly List<string> availiableItemStatTypes = new List<string>()
        {
            "Critical Chance",
            "Health",
            "Constitution",
            "Strength",
            "Agility",
            "Armor Pierce Chance"
        };
        private readonly List<int> availiableItemLevels = new List<int>()
           {
               1,
               20,
               40,
               60,
               80,
               100
           };

        private readonly List<ItemStatCost> availiableItemStatCosts = new List<ItemStatCost>()
            {
                new ItemStatCost() {StatType ="Critical Chance" , StatAmount =0.02 , StatCost = 10},
                new ItemStatCost() {StatType ="Health", StatAmount = 20 , StatCost = 10},
                new ItemStatCost() {StatType ="Constitution", StatAmount = 1 , StatCost = 10},
                new ItemStatCost() {StatType ="Strength", StatAmount = 1 , StatCost = 10},
                new ItemStatCost() {StatType ="Agility", StatAmount = 1, StatCost = 10},
                new ItemStatCost() {StatType ="Armor Pierce Chance", StatAmount =0.02 , StatCost = 10}
            };

        

        private readonly List<string> availiableItemTypes = new List<string>()
            {
                "Helmet",
                "Arms",
                "Legs",
                "Chest",
                "Ring",
                "Talisman",
                "Weapon",
                "Belt",
                "Cloak"
            };

        private readonly List<string> specialEquipmentList = new List<string>()
        {
            "Ring",
            "Talisman"
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
            return availiableItemLevels;
        }

        public IEnumerable<ItemStatCost> GetAvailiableItemStatCosts()
        {
            return availiableItemStatCosts;
        }

        public IEnumerable<string> GetAvailiableItemTypes()
        {
            return availiableItemTypes;
        }

        public IEnumerable<string> GetAvailiableItemStatTypes()
        {
            return availiableItemStatTypes;
        }

        public int GetPotentialByItemLvlAndItemType(int itemLvl, string itemType)
        {
            int currentItemsLvl = availiableItemLevels.Where(x => x.Equals(itemLvl)).FirstOrDefault();
            if (String.IsNullOrEmpty(currentItemsLvl.ToString()))
            {
                throw new InvalidItemLevelException();
            }
            string currentItemType = availiableItemTypes.Where(x => x.Equals(itemType)).FirstOrDefault();
            if (currentItemType == null)
            {
                throw new InvalidItemTypeException();
            }
            int itemsBasePotential = ItemsPotentialsList.Where(x => x.Level.Equals(currentItemsLvl)).Select(x => x.Potential).FirstOrDefault();

            return CalculatePotential(itemsBasePotential, currentItemType);
            
        }

        public bool ItemLevelIsValid(int lvl)
        {
            return availiableItemLevels.Contains(lvl);
        }

        public bool ItemTypeExists(string type)
        {
            return availiableItemTypes.Contains(type);
        }

        private int CalculatePotential(int itemsBasePotential , string currentItemType)
        {
            if (currentItemType == "Weapon")
            {
                return Convert.ToInt32(itemsBasePotential * weaponPotentialMultipier);
            }
            if (specialEquipmentList.Contains(currentItemType))
            {
                return Convert.ToInt32(itemsBasePotential * specialEquipmentPotentialMultiplier);
            }
            return itemsBasePotential;
        }
    }
}
