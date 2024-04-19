using System.Collections.Generic;

namespace Sandbox1
{
    public class SceneManager
    {
        private readonly Stack<IScene> sceneStack;

        public SceneManager()
        {
            sceneStack = new Stack<IScene>();
        }

        public void AddScene(IScene scene)
        {
            scene.Load();
            sceneStack.Push(scene);
        }

        public void RemoveScene()
        {
            sceneStack.Pop();
        }

        public IScene GetCurrentScene()
        {
            return sceneStack.Peek();
        }
    }
}