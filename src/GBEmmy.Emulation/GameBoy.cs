﻿// GBEmmy
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

using System.Threading.Tasks;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Processor;
using GBEmmy.Emulation.Processor.Registers;
using GBEmmy.Emulation.VideoProcessor;

namespace GBEmmy.Emulation
{
    public class GameBoy
    {
        private double _time;

        private readonly DIV _div;
        private readonly TIMA _tima;
        public GameBoy(Z80 processor, GPU videoProcessor)
        {
            Processor = processor;
            VideoProcessor = videoProcessor;

            _div =  Processor.Memory.Registers.Get<DIV>();
            _tima = Processor.Memory.Registers.Get<TIMA>();
        }

        public GameBoy(Cartridge cartridge) : this(new Z80(cartridge), null)
        {
            VideoProcessor = new GPU(Processor.Memory);
        }

        public Z80 Processor { get; private set; }
        public GPU VideoProcessor { get; private set; }

        public void Update()
        {
            _time += 0.01;

            //Timing:
            _div.Update(0.01);
            _tima.Update(0.01);

            while (_time > 0.0)
            {
                //double duration = VideoProcessor.Run(_time);

                Processor.Run(0.1);//(duration);
                //_time -= duration;
            }
        }

        public async void Run()
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