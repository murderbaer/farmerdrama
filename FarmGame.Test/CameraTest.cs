using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FarmGame.Test
{
    [TestClass]
    public class CameraTest
    {
        private Camera camera = new ();


        [TestMethod]
        public void TestResize()
        {
            // Uncomment to run Window Resize Test in local machine. Server can not open Window

/*             var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
            var vec = new Vector2(1280, 720);
            camera.Resize(1280, 720);
        	Assert.AreEqual(camera.CameraWidth, 1280f);
            Assert.AreEqual(camera.CameraHeight, 720f);
            Assert.AreEqual(camera.CameraSize, vec);
            camera.Resize(1500, 720);
        	Assert.AreEqual(camera.CameraWidth, 1280f);
            Assert.AreEqual(camera.CameraHeight, 720f);
            Assert.AreEqual(camera.CameraSize, vec);
            camera.Resize(1280, 900);
        	Assert.AreEqual(camera.CameraWidth, 1280f);
            Assert.AreEqual(camera.CameraHeight, 720f);
            Assert.AreEqual(camera.CameraSize, vec);
            window.Close(); */
        }
    }
}
