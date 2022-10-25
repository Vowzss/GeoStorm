using System.Collections.Generic;
using System.Numerics;
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static GeoStorm.MathExtend.MathE;
using GeoStorm.Core;
using GeoStorm.Interfaces;

namespace GeoStorm.Render
{
    static class Renderer
    {
        private static List<Vector2>[] Stars = new List<Vector2>[5];
        public static RenderTexture2D entitiesLayer, starsLayer, hudLayer, particlesLayer;

        public static void InitTexture()
        {
            hudLayer = LoadRenderTexture(GetMonitorWidth(0), GetMonitorHeight(0));
            starsLayer = LoadRenderTexture(GetMonitorWidth(0), GetMonitorHeight(0));
            entitiesLayer = LoadRenderTexture(GetMonitorWidth(0), GetMonitorHeight(0));
            particlesLayer = LoadRenderTexture(GetMonitorWidth(0), GetMonitorHeight(0));
        }

        public static void DrawBlackHole(Vector2 position)
        {
            BeginTextureMode(entitiesLayer);
            for (int i = 10; i > 0; i--)
                DrawPoly(position, 8, i * 4, GameInputs.time, VIOLET);
            EndTextureMode();
        }
        public static void Start()
        {
            for (int j = 0; j < Stars.Length; j++)
            {
                Stars[j] = new List<Vector2>();
                for(int i = 0; i < 250; i++)
                {
                    Vector2 starPos = RandomVector((int)GameInputs.screenSize.X, (int)GameInputs.screenSize.Y);
                    Stars[j].Add(starPos);
                }
            }
        }

        public static void DrawBackground()
        {
            BeginTextureMode(starsLayer);
                for (int i = 0; i < Stars.Length; i++)
                {
                    foreach(Vector2 star in Stars[i])
                    {
                        DrawPixelV(new Vector2(((star.X + GameInputs.time * (i + 3) * 20) % GameInputs.screenSize.X) * -1 + GameInputs.screenSize.X, star.Y), YELLOW);
                    }
                }
            EndTextureMode();
        }

        public static void DrawSprites(Vector2 position, Sprite spriteData, float scale, float rotation, Color color, string debugText = null)
        {
            BeginTextureMode(entitiesLayer);

                for(int i = 0; i < spriteData.lines.Length/2; i++)
                {
                    DrawLineEx(Rotate(spriteData.verticies[spriteData.lines[i * 2]], new Vector2(), rotation) * scale + position,
                    Rotate(spriteData.verticies[spriteData.lines[i * 2 + 1]], new Vector2(), rotation) * scale + position, 2, color);
                
                }
                if(DebugMode.debugmode)
                {
                    if(debugText != null)
                    {
                        DrawText(debugText, (int)position.X, (int)position.Y, 15, WHITE);
                    }
                    DrawCircleLines((int)position.X, (int)position.Y, scale, color);
                }

            EndTextureMode();
        }

        public static void DrawParticles(List<Particle> particles)
        {
            BeginTextureMode(particlesLayer);
                foreach (Particle part in particles)
                {
                    part.velocity *= (1 - part.friction * GameInputs.deltaTime);
                    part.position += part.velocity * GameInputs.deltaTime;
                    part.size -= GameInputs.deltaTime * 17;
                    switch (part.shape)
                    {
                        case ParticleShape.Circle:
                            Raylib.DrawCircleV(part.position, part.size, part.color);
                            break;

                        case ParticleShape.Line:
                            Raylib.DrawLineEx(part.position, (part.position - Vector2.Normalize(part.velocity) * part.size), 2, part.color);
                            break;

                        case ParticleShape.Square:
                            Raylib.DrawRectanglePro(new Rectangle(part.position.X - part.size / 2, part.position.Y - part.size / 2, part.size, part.size), new Vector2(), part.rotation * 180 / MathF.PI, part.color);
                            break;
                        default:
                            break;

                    }
                }
                foreach (Particle part in particles.ToArray())
                {
                    if (part.size <= 0)
                    {
                        particles.Remove(part);
                    }
                }
            EndTextureMode();
        }

        public static void DrawButton(Button button)
        {
            BeginTextureMode(hudLayer);
                DrawRectangleRoundedLines(new Rectangle((int)button.position.X, (int)button.position.Y, (int)button.size.X, (int)button.size.Y), 0.5f, 7, 2, button.colorOut);
                DrawRectangleRounded(new Rectangle((int)button.position.X, (int)button.position.Y, (int)button.size.X, (int)button.size.Y), 0.5f, 7, button.colorIn);
                DrawText(button.text, (int)button.position.X + (int)button.offset, (int)button.position.Y + 15, 20, WHITE);
            EndTextureMode();
        }
        public static void DrawBackgroundHUD(Vector2 pos, Vector2 size, int type)
        {
            BeginTextureMode(hudLayer);
            if (type == 1)
            {
                DrawRectangleRoundedLines(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, 2, WHITE);
                DrawRectangleRounded(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, GRAY);
            }
            if(type == 2)
            {
                DrawRectangleRoundedLines(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), 0.1f, 7, 2, WHITE);
                DrawRectangleRounded(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), 0.1f, 7, GRAY);
            }
            if(type == 3)
            {
                DrawRectangleRoundedLines(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, 2, WHITE);
                DrawRectangleRounded(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y + 120), 0.1f, 7, GRAY);
            }
            EndTextureMode();
        }

        public static void DrawTextHUD(int type)
        {
            BeginTextureMode(hudLayer);
            if(type == 0)
            {
                DrawText("GEOSTORM", GetMonitorWidth(0) / 2 - 425, GetMonitorHeight(0) / 2 - 450, 150, WHITE);
            }
            if (type == 1)
            {
                DrawText("Movements: Z,Q,S,D or W,A,S,D depending on your keyboard.", GetMonitorWidth(0) / 2 - 400, GetMonitorHeight(0) / 2 - 210, 25, BLACK);
                DrawText("How To Play: Shoot enemies on your screen to stay alive!", GetMonitorWidth(0) / 2 - 400, GetMonitorHeight(0) / 2 - 170, 25, BLACK);
                DrawText($"-> You have 10 lifes, when reaching 0 you will loose!", GetMonitorWidth(0) / 2 - 350, GetMonitorHeight(0) / 2 - 130, 25, BLACK);
                DrawText("By killing enemies your earn money.", GetMonitorWidth(0) / 2 - 400, GetMonitorHeight(0) / 2 - 90, 25, BLACK);
                DrawText("-> You can spend this money on upgrades for your player.", GetMonitorWidth(0) / 2 - 350, GetMonitorHeight(0) / 2 - 50, 25, BLACK);
                DrawText("-> Choose wisely to last long!", GetMonitorWidth(0) / 2 - 350, GetMonitorHeight(0) / 2 - 10, 25, BLACK);
            }
            if (type == 2)
            {
                DrawText("GAME IS OVER", GetMonitorWidth(0) / 2 - 260, GetMonitorHeight(0) / 2 - 200, 70, BLACK);
                DrawText("You Scored", GetMonitorWidth(0) / 2 - 210, GetMonitorHeight(0) / 2 - 120, 70, BLACK);
                DrawText($"{Player.score} Points", GetMonitorWidth(0) / 2 - 130, GetMonitorHeight(0) / 2 - 40, 70, BLACK);
            }
            if (type == 3)
                DrawText("GAME IS PAUSED", GetMonitorWidth(0) / 2 - 310, GetMonitorHeight(0) / 2 - 200, 70, BLACK);
            EndTextureMode();
        }

        public static void DrawLifeHUD(int life)
        {
            BeginTextureMode(hudLayer);
            for (int j = 0; j < life; j++)
            {
                for (int i = 0; i < SpritesData.Heart.lines.Length / 2; i++)
                {
                    DrawLineEx(Rotate(SpritesData.Heart.verticies[SpritesData.Heart.lines[i * 2]], new Vector2(), 0) * 16 + new Vector2(j * 45 + 45, 50),
                    Rotate(SpritesData.Heart.verticies[SpritesData.Heart.lines[i * 2 + 1]], new Vector2(), 0) * 16 + new Vector2(j * 45 + 45, 50), 2, RED);

                }
            }
            EndTextureMode();
        }

        public static void DrawLaser(Laser laser)
        {
            BeginTextureMode(entitiesLayer);
                DrawLineEx(laser.position, Rotate(new Vector2(10000, 0) + laser.position, laser.position, laser.rotation), 4, laser.color);
            EndTextureMode();
        }
        public static void ApplyShader(Shader shader)
        {

            ClearBackground(Color.BLACK);
                BeginShaderMode(shader);
                    DrawTextureRec(starsLayer.texture, new Rectangle(0, 0, (float)starsLayer.texture.width, (float)-starsLayer.texture.height), new Vector2(0, 0), WHITE);
                    DrawTextureRec(entitiesLayer.texture, new Rectangle(0, 0, (float)entitiesLayer.texture.width, (float)-entitiesLayer.texture.height), new Vector2(0, 0), WHITE);
                    DrawTextureRec(particlesLayer.texture, new Rectangle(0, 0, (float)particlesLayer.texture.width, (float)-particlesLayer.texture.height), new Vector2(0, 0), WHITE);
                EndShaderMode();
            DrawTextureRec(hudLayer.texture, new Rectangle(0, 0, (float)hudLayer.texture.width, (float)-hudLayer.texture.height), new Vector2(0, 0), WHITE);

            Color clearColor = new Color(0, 0, 50, 0);

            BeginTextureMode(hudLayer);
            ClearBackground(clearColor);
            EndTextureMode();

            BeginTextureMode(starsLayer);
            ClearBackground(clearColor);
            EndTextureMode();

            BeginTextureMode(entitiesLayer);
            ClearBackground(clearColor);
            EndTextureMode();

            BeginTextureMode(particlesLayer);
            ClearBackground(clearColor);
            EndTextureMode();
        }
    }
}