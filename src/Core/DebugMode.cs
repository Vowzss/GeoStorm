using System;
using System.Collections.Generic;
using System.Linq;
using static Raylib_cs.Raylib;
using ImGuiNET;

namespace GeoStorm.Core
{
    class DebugMode
    {
        static public bool debugmode = false;
        private WeaponType weaponType;

        public void Update(GameInputs inputs, EntitiesManager gameData, List<Event> events)
        {
            foreach (Entity entity in gameData.entities)
            {
                entity.Update(inputs, gameData, events);
                if (debugmode)
                    DrawCircleLines((int)entity.position.X, (int)entity.position.Y, entity.collisionRadius, entity.color);
            }

            if (IsKeyPressed(Raylib_cs.KeyboardKey.KEY_DELETE))
            {
                debugmode = !debugmode;
            }

            if (debugmode)
            {
                DrawFPS(10, 10);
                //ShowCursor();

                if (ImGui.Begin("Debug Menu"))
                {
                    if (ImGui.CollapsingHeader("GameInputs", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.Text($"Mouse Position : {inputs.shootTarget}");
                        ImGui.Text($"Last Key Pressed : {inputs.keyPressed}");
                        ImGui.Text($"Move Axis : {inputs.moveAxis}");
                    }

                    if (ImGui.CollapsingHeader("Entities", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.Text($"Total Entities = {gameData.entities.Count()}");
                        ImGui.Text($"Total Enemies = {gameData.enemies.Count()}");
                        ImGui.Text($"Total Bullets = {gameData.bullets.Count()}");
                        ImGui.Text($"Total BlackHoles = {gameData.blackHoles.Count()}");
                    }

                    if (ImGui.CollapsingHeader("Player", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.Checkbox("Invincible", ref gameData.player.isInvicible);
                        ImGui.Text($"Position : {gameData.player.position}");
                        ImGui.Text($"Velocity : {gameData.player.velocity}");
                        ImGui.Text($"Rotation : {gameData.player.rotation * 180f / MathF.PI}");
                    }

                    if (ImGui.CollapsingHeader("Player Stats", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.Text($"Player Score : {Player.score}");
                        ImGui.Text($"Player Max Life : {gameData.player.maxLife}");
                        ImGui.Text($"Buy Count : {ShoppingSystem.buyCount}");
                        ImGui.Text($"Enemy Killed : {gameData.player.enemyKilled}");
                        ImGui.SliderInt("Player Life", ref gameData.player.currentLife, 1, 20);
                        ImGui.SliderInt("Money", ref gameData.player.money, 1, 1000000);
                    }

                    if (ImGui.CollapsingHeader("Weapon", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        int weaponTmp = (int)weaponType;
                        if (ImGui.Combo("Weapon Type", ref weaponTmp, Enum.GetNames(typeof(WeaponType)), Enum.GetValues(typeof(WeaponType)).Length))
                        {
                            weaponType = (WeaponType)weaponTmp;
                            switch (weaponType)
                            {
                                case WeaponType.PISTOL:
                                    gameData.player.weapon = new Weapon(EntitiesStats.pistolWeaponStat);
                                    gameData.player.weapon.player = gameData.player;
                                    break;
                                case WeaponType.SHOTGUN:
                                    gameData.player.weapon = new Weapon(EntitiesStats.shotgunWeaponStat);
                                    gameData.player.weapon.player = gameData.player;
                                    break;
                                case WeaponType.CATAPULTE:
                                    gameData.player.weapon = new Weapon(EntitiesStats.catapulteWeaponStat);
                                    gameData.player.weapon.player = gameData.player;
                                    break;
                                case WeaponType.LASER:
                                    gameData.player.weapon = new Weapon(EntitiesStats.laserWeaponStat);
                                    gameData.player.weapon.player = gameData.player;
                                    break;
                                case WeaponType.LAUNCHER:
                                    gameData.player.weapon = new Weapon(EntitiesStats.launcherWeaponStat);
                                    gameData.player.weapon.player = gameData.player;
                                    break;
                                default:
                                    break;
                            }
                        }
                        ImGui.SliderInt("BulletPerShoot", ref gameData.player.weapon.bulletperShoot, 1, 50);
                        ImGui.SliderFloat($"Bullet Speed", ref gameData.player.weapon.speed, 1, 5);
                    }

                    if (ImGui.CollapsingHeader("Other", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        ImGui.Checkbox("CanShoot", ref Weapon.canShoot);
                        ImGui.Text($"Time Until Next Wave {EnemySpawnSystem.timeUntilNextWave}");
                        ImGui.Text($"Wave Count {EnemySpawnSystem.waveCount}");
                        ImGui.SliderFloat("ew", ref EnemySpawnSystem.timeUntilNextWave, 0, 10);
                    }
                }
                ImGui.End();
            }
        }
    }
}
