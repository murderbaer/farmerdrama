#if Windows
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using System.Linq;

using FarmGame.Core;
using FarmGame.Model;
using FarmGame.Services;

using OpenTK.Graphics.OpenGL;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class SceneTest
    {
        Scene scene;

        public SceneTest()
        {
            GL.LoadBindings(new WglBindingsContext());
            scene = Game.LoadScene();
        }

        [TestMethod]
        public void TestLoadScene()
        {
            scene = Game.LoadScene();
            Assert.IsTrue(scene.GetGameObjects("Camera") != null);
        }

        [TestMethod]
        public void TestUpdate()
        {
            scene.Update(0.2f);
            GameTime t = scene.GetGameObjects("GameOver").First().GetComponent<GameTime>();
            Assert.AreEqual(t.ElapsedTime, 0.0f);
            UpdateService updateService = scene.GetService<UpdateService>();
            updateService.IsIntro = false;
            scene.Update(0.2f);
            Assert.AreEqual(t.ElapsedTime, 0.2f);
        }
    }
}
#endif