using System.Numerics;

namespace GeoStorm.Core
{
    struct Sprite
    {
        public Vector2[] verticies;
        public int[] lines;
        public Sprite(Vector2[] _verticies, int[] _lines)
        {
            verticies = new Vector2[_verticies.Length];
            verticies = _verticies;
            lines = new int[_lines.Length];
            lines = _lines;
        }
    }

    static class SpritesData
    {
        public static Sprite playerSprite = new Sprite(
            new Vector2[] {
                new Vector2(0.6f, 0.3f), // bg
                new Vector2(-0.2f, 0.55f), // biw
                new Vector2(-0.5f, 0.0f), // ic
                new Vector2(-0.2f, -0.55f), // tiw
                new Vector2(0.6f, -0.3f), // tg
                new Vector2(-0.4f, -0.8f), // tow
                new Vector2(-1.0f, 0.0f), // oc
                new Vector2(-0.4f, 0.8f) // bow
            },
            new int[] 
            { 
                0,1, 1,2, 2,3, 3,4, 4,5, 5,6, 6,7, 7,0
            }
        );


        public static Sprite gruntSprite = new Sprite(
            new Vector2[]
            {
                new Vector2(-1.0f, 0.0f),
                new Vector2(-0.0f, -1.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(-0.0f, 1.0f)
            },
            new int[]
            {
                0,1, 1,2, 2,3, 3,0
            }
        );

        public static Sprite bulletSprite = new Sprite(
            new Vector2[] {
                new Vector2(-0.3f, 0.0f),
                new Vector2(-0.1f, 0.2f),
                new Vector2(0.8f, 0.0f),
                new Vector2(-0.1f, -0.2f)
            },
            new int[]
            {
                0,1, 1,2, 2,3, 3,0
            }
        );

        public static Sprite weaponSprite = new Sprite(
            new Vector2[] {
                new Vector2(1,0),
                new Vector2(0,-1),
                new Vector2(0,1)
            },
            new int[]
            {
                0,1, 1,2, 2,0
            }
        );

        public static Sprite shurikenSprite = new Sprite(
            new Vector2[] {
                new Vector2(0,1),
                new Vector2(0.4f, 0.5f),
                new Vector2(-0.4f, -0.5f),
                new Vector2(0, -1),
                
                new Vector2(-1, 0),
                new Vector2(-0.5f, 0.4f),
                new Vector2(0.5f, -0.4f),
                new Vector2(1, 0),
                
            },
            new int[]
            {
                0,1, 1,2, 2,3, 3,0, 4,5, 5,6, 6,7, 7,4
            }
        );

        public static Sprite starSprite = new Sprite(
            new Vector2[] {
                new Vector2(1,0),

                new Vector2(0.3f,0.3f),
                new Vector2(0,1),

                new Vector2(-0.3f,0.3f),
                new Vector2(-1, 0),

                new Vector2(-0.3f,-0.3f),
                new Vector2(0, -1),

                new Vector2(0.3f,-0.3f)
            },
            new int[]
            {
                0,1, 1,2, 2,3, 3,4, 4,5, 5,6, 6,7, 7,0
            }
        );
        public static Sprite asteroidSprite = new Sprite(
                new Vector2[] {
                    new Vector2(1,0),

                    new Vector2(0.3f,0.3f),
                    new Vector2(0,1),

                    new Vector2(-0.3f,0.3f),
                    new Vector2(-1, 0),

                    new Vector2(-0.3f,-0.3f),
                    new Vector2(0, -1),

                    new Vector2(0.3f,-0.3f)
                },
                new int[]
                {
                    0,1, 1,2, 2,3, 3,4, 4,5, 5,6, 6,7, 7,0
                }
            );

        public static Sprite Heart = new Sprite(
                new Vector2[] {
                    new Vector2(0, 1),
                    new Vector2(-1, 0),
                    new Vector2(-1, -0.5f),
                    new Vector2(-0.5f, -1),
                    new Vector2(0, -0.5f),
                    new Vector2(0.5f,-1),
                    new Vector2(1, -0.5f),
                    new Vector2(1, 0)
                },
                new int[]
                {
                    0,1, 1,2, 2,3, 3,4, 4,5, 5,6, 6,7, 7,0
                }
            );
    }
}
