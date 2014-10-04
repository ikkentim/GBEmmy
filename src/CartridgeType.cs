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

namespace GBEmmy
{
    public enum CartridgeType : byte
    {
        ROM = 0,
        MBC1 = 1,
        MBC1_RAM = 2,
        MBC1_RAM_BATT = 3,
        MBC2 = 5,
        MBC2_BATT = 6,
        ROM_RAM = 8,
        ROM_RAM_BATT = 9,
        MMM01 = 0x0B,
        MMM01_SRAM = 0x0C,
        MMM01_SRAM_BATT = 0x0D,
        MBC3_TIMER_BATT = 0x0F,
        MBC3_TIMER_RAM_BATT = 0x10,
        MBC3 = 0x11,
        MBC3_RAM = 0x12,
        MBC3_RAM_BATT = 0x13,
        MBC5 = 0x19,
        MBC5_RAM = 0x1A,
        MBC5_RAM_BATT = 0x1B,
        MBC5_RUMBLE = 0x1C,
        MBC5_RUMBLE_SRAM = 0x1D,
        MBC5_RUMBLE_SRAM_BATT = 0x1E,
        PocketCamera = 0x1F,
        TAMA5 = 0xFD,
        HuC3 = 0xFE,
        HuC1 = 0xFF,
    }
}