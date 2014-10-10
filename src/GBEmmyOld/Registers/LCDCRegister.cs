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

namespace GBEmmy.Registers
{
    /// <summary>
    ///     LCD Control (R/W).
    /// </summary>
    public class LCDCRegister : Register
    {
        public LCDCRegister()
        {
            Value = 0x91;
        }

        public bool BackgroundEnabled
        {
            get { return ((Value >> 0) & 0x01) != 0; }
        }

        public bool SpritesEnabled
        {
            get { return ((Value >> 1) & 0x01) != 0; }
        }

        public byte SpriteSize
        {
            get { return (byte) ((Value >> 2) & 0x01); }
        }

        public byte BackgroundTileMap
        {
            get { return (byte) ((Value >> 3) & 0x01); }
        }

        public byte TileData
        {
            get { return (byte) ((Value >> 4) & 0x01); }
        }

        public TileDataTable TileDataTable
        {
            get { return TileData == 0 ? new TileDataTable(0x8000, false) : new TileDataTable(0x8800, true); }
        }

        public bool WindowEnabled
        {
            get { return ((Value >> 5) & 0x01) != 0; }
        }

        public byte WindowTileMap
        {
            get { return (byte) ((Value >> 6) & 0x01); }
        }

        public bool DisplayEnabled
        {
            get { return ((Value >> 7) & 0x01) != 0; }
        }
    }
}