// GBEmmy
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GBEmmy
{
    public class GameBoyDisplay : GLControl
    {
        private float _angle;
        private GPU _gpu;
        private bool _loaded;

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Firebrick);

            Application.Idle += Application_Idle;

            _loaded = true;
        }

        #endregion

        #region OnClosing

        protected override void Dispose(bool disposing)
        {
            Application.Idle -= Application_Idle;

            base.Dispose(disposing);
        }

        #endregion

        #region Application_Idle event

        private void Application_Idle(object sender, EventArgs e)
        {
            while (IsIdle)
            {
                Render();
            }
        }

        #endregion

        public void SetGPU(GPU gpu)
        {
            _gpu = gpu;
        }

        protected override void OnResize(EventArgs e)
        {
            if (!_loaded) return;

            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Render();
        }

        #region private void Render()

        private static Color ToColor(uint color)
        {
            var b = (int) (color & 0x000000FF);
            var g = (int) ((color & 0x0000FF00) >> 8);
            var r = (int) ((color & 0x00FF0000) >> 16);
            var a = (int) ((color & 0xFF000000) >> 24);

            return Color.FromArgb(a, r, g, b);
        }

        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            double pxw = 1.0/(GPU.Width/2);
            double pxh = 1.0/(GPU.Height/2);

            for (int y = 0; y < GPU.Height; y++)
                for (int x = 0; x < GPU.Width; x++)
                {
                    GL.Begin(PrimitiveType.Quads);
                    GL.Color3(ToColor(_gpu.ScreenBuffer[y*GPU.Width + x]));

                    double xx = (x - GPU.Width/2)*pxw;
                    double yy = (y - GPU.Height/2)*pxh;

                    GL.Vertex2(new Vector2d(xx, yy));
                    GL.Vertex2(new Vector2d(xx + pxw, yy));
                    GL.Vertex2(new Vector2d(xx + pxw, yy + pxh));
                    GL.Vertex2(new Vector2d(xx, yy + pxh));

                    GL.End();
                }

            SwapBuffers();
        }

        #endregion
    }
}