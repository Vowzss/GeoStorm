using GeoStorm.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace GeoStorm.Core
{
    class ShoppingSystem : ISystem
    {
        public bool isInShop = false;

        public static int buyCount;
        public static int actualPrice;

        Button shopButton;

        Button buyMoreBulletsButton;
        Button buyMoreLifeButton;
        Button buyLifeRefillButton;

        Button buyWeaponPistolButton;
        Button buyWeaponCatapulteButton;
        Button buyWeaponShotgunButton;
        Button buyWeaponLaserButton;
        Button buyWeaponLauncherButton;

        Button buyMoreDamage;

        private int setprice(int price)
        {
            return price + (int)MathF.Floor(MathF.Pow(buyCount, 1.3f)*buyCount);
        }

        public ShoppingSystem(EntitiesManager data)
        {
            isInShop = false;

            shopButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 920),
                new Vector2(350, 50),
                25,
                "SHOPING",
                WHITE,
                GRAY,
                () => isInShop = !isInShop
            );

            buyMoreBulletsButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 840),
                new Vector2(350, 50),
                25,
                "+ 1 Bullet",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(250);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon.bulletperShoot++;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyMoreLifeButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 780),
                new Vector2(350, 50),
                25,
                "+ 1 MAX HP",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(500);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.maxLife++;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyLifeRefillButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 720),
                new Vector2(350, 50),
                25,
                "+ 1 HP",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(500);
                    if (data.player.money >= actualPrice)
                    {
                        if (data.player.maxLife <= data.player.currentLife)
                            return;
                        else
                        {
                            data.player.currentLife++;
                            data.player.money -= actualPrice;
                        }
                    }
                }
            );
            buyMoreDamage = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 660),
                new Vector2(350, 50),
                25,
                "+ 1 Damage",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(1000);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon.damage++;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyWeaponPistolButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 580),
                new Vector2(350, 50),
                25,
                "Pistol Weapon",
                WHITE,
                GRAY,
                () => 
                {
                    actualPrice = setprice(2500);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon = new Weapon(EntitiesStats.pistolWeaponStat);
                        data.player.weapon.player = data.player;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyWeaponCatapulteButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 520),
                new Vector2(350, 50),
                25,
                "Catapulte Weapon",
                WHITE,
                GRAY,
                () => 
                {
                    actualPrice = setprice(4000);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon = new Weapon(EntitiesStats.catapulteWeaponStat);
                        data.player.weapon.player = data.player;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyWeaponShotgunButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 460),
                new Vector2(350, 50),
                25,
                "Shotgun Weapon",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(5000);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon = new Weapon(EntitiesStats.shotgunWeaponStat);
                        data.player.weapon.player = data.player;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyWeaponLaserButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 400),
                new Vector2(350, 50),
                25,
                "Laser Weapon",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(6000);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon = new Weapon(EntitiesStats.laserWeaponStat);
                        data.player.weapon.player = data.player;
                        data.player.money -= actualPrice;
                    }
                }
            );
            buyWeaponLauncherButton = new Button(
                new Vector2(GetScreenWidth() - 370, GetScreenHeight() - 340),
                new Vector2(350, 50),
                25,
                "Rocket Launcher",
                WHITE,
                GRAY,
                () =>
                {
                    actualPrice = setprice(8000);
                    if (data.player.money >= actualPrice)
                    {
                        buyCount++;
                        data.player.weapon = new Weapon(EntitiesStats.launcherWeaponStat);
                        data.player.weapon.player = data.player;
                        data.player.money -= actualPrice;
                    }
                }
            );
        }

        public void Update(GameInputs inputs, EntitiesManager data, List<Event> events)
        {
            shopButton.Update(inputs);
            ShopMenu(inputs);
        }

        private void ShopMenu(GameInputs inputs)
        {
            if (isInShop)
            {
                buyMoreBulletsButton.Update(inputs);
                buyMoreLifeButton.Update(inputs);
                buyMoreDamage.Update(inputs);
                buyLifeRefillButton.Update(inputs);

                buyWeaponPistolButton.Update(inputs);
                buyWeaponCatapulteButton.Update(inputs);
                buyWeaponShotgunButton.Update(inputs);
                buyWeaponLaserButton.Update(inputs);
                buyWeaponLauncherButton.Update(inputs);


                /*Vector2 pos = new Vector2(GetMonitorWidth(0) / 2 + 450, GetMonitorHeight(0) / 2 - 225), size = new Vector2(800, 200);
                DrawRectangleRoundedLines(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, 2, WHITE);
                DrawRectangleRounded(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, GRAY);*/

                DrawText("SHOPPING CENTER", GetMonitorWidth(0) / 2 - 310, GetMonitorHeight(0) / 2 - 200, 70, WHITE);
            }
        }
    }
}
