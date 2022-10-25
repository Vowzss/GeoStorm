namespace GeoStorm.Core
{
    class SceneManager
    {
        public static SceneManager currentScene;
        public static bool sceneActivated = false;

        public virtual void Start() { }
        
        public virtual void Update(GameInputs inputs) { }
        
        public static void LoadScene(SceneManager scene)
        {
            sceneActivated = true;
            currentScene = scene;
        }
    }
}
