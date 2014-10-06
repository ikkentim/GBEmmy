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

using System.Diagnostics;
using System.Threading.Tasks;
using GBEmmy.Processor;

namespace GBEmmy
{
    internal class GameBoy
    {
        private double _time;

        public GameBoy(Z80 processor, GPU videoProcessor)
        {
            Processor = processor;
            VideoProcessor = videoProcessor;
        }

        public Z80 Processor { get; private set; }
        public GPU VideoProcessor { get; private set; }

        public void Update()
        {
            //TODO: Process intervals

            _time += 0.1;
            while (_time > 0.0)
            {
                double duration = VideoProcessor.Run(_time);

                Debug.WriteLine("Processor.Run %0", duration);
                Processor.Run(duration);
                _time -= duration;
            }
        }

        public void Run()
        {
            Loop();
        }

        public async void Loop()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Update();
                }
            });
        }
    }
}